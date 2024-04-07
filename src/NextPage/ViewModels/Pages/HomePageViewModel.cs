using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using NextPage.Abstractions;
using NextPage.Constants;
using NextPage.Models.Enums;
using NextPage.Properties;
using NextPage.Utilities;
using NextPage.Views;

namespace NextPage.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    #region Fields

    private readonly IBookService bookService;
    private readonly IDialogService dialogService;

    private List<BookViewModel> originalBooks = new List<BookViewModel>();

    private readonly BookViewModelComparer bookViewModelComparer = new BookViewModelComparer();

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilteringOrSorting))]
    private string searchQuery;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilteringOrSorting))]
    private SortOrderEnum? sortOrder;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilteringOrSorting))]
    private BookSortTypeEnum? sortType;

    [ObservableProperty]
    private ObservableRangeCollection<BookViewModel> filteredBooks = new ObservableRangeCollection<BookViewModel>();

    public bool IsFilteringOrSorting
    {
        get => SortOrder != null
            || SortType != null
            || !string.IsNullOrWhiteSpace(SearchQuery);
    }

    #endregion Properties

    #region Constructors

    public HomePageViewModel(
        IBookService bookService,
        IDialogService dialogService,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.bookService = bookService;
        this.dialogService = dialogService;
    }

    #endregion Constructors

    #region Lifecycle Events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        IsLoading = true;

        // if these parameters are passed, update the selected sort options
        if (parameters.ContainsKey(NavigationParameterKeys.SortOrder)
            && parameters.ContainsKey(NavigationParameterKeys.SortType))
        {
            SortOrder = parameters.GetValue<SortOrderEnum?>(NavigationParameterKeys.SortOrder);
            SortType = parameters.GetValue<BookSortTypeEnum?>(NavigationParameterKeys.SortType);
        }

        originalBooks = bookService.GetAllBooks()
            .ToList();
        SortAndSearchBooks();

        IsLoading = false;
    }

    #endregion Lifecycle Events

    #region Commands

    [RelayCommand]
    private async Task AddBook()
    {
        var navigationParameters = new NavigationParameters
        {
            { NavigationParameterKeys.AddBook, true },
        };

        await navigationService.Push<BookPage>(navigationParameters);
    }

    [RelayCommand]
    private async Task ViewExistingBook(BookViewModel book)
    {
        var navigationParameters = new NavigationParameters
        {
            { NavigationParameterKeys.Book, book },
        };

        await navigationService.Push<BookPage>(navigationParameters);
    }

    [RelayCommand]
    private async Task Delete(BookViewModel book)
    {
        var confirmed = await dialogService.DisplayAlert(
            Resources.ConfirmDelete,
            Resources.AreYouSureDelete,
            Resources.ButtonDelete,
            Resources.ButtonCancel);

        if (!confirmed)
        {
            // user did not want to delete book
            return;
        }

        bookService.DeleteBook(book);
        originalBooks.Remove(book);
        FilteredBooks.Remove(book);
    }

    [RelayCommand]
    private void Search()
    {
        SortAndSearchBooks();
    }

    [RelayCommand]
    private async Task Sort()
    {
        // pass the currently selected sort options
        var navigationParameters = new NavigationParameters
        {
            { NavigationParameterKeys.SortOrder, SortOrder },
            { NavigationParameterKeys.SortType, SortType },
        };

        await navigationService.Push<SortPage>(navigationParameters);
    }

    [RelayCommand]
    private void ClearFiltersAndSort()
    {
        SearchQuery = string.Empty;
        SortType = null;
        SortOrder = null;

        SortAndSearchBooks();
    }

    #endregion Commands

    #region Private methods

    private void SortAndSearchBooks()
    {
        IEnumerable<BookViewModel> filteredBooks = originalBooks;

        // 1. Search
        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            // filter results with a case insensitive search
            filteredBooks = filteredBooks
                .Where(book =>
                    book.Title.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    book.Author.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    book.Description.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        // 2. Sort
        var sortedBooks = filteredBooks.ToList();

        if (SortOrder != null && SortType != null)
        {
            // sort the list if the user has requested it
            bookViewModelComparer.SortType = SortType.Value;
            bookViewModelComparer.SortOrder = SortOrder.Value;

            sortedBooks.Sort(bookViewModelComparer);
        }

        FilteredBooks.ReplaceRange(sortedBooks);
    }


    #endregion Private methods
}
