using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using NextPage.Abstractions;

namespace NextPage.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    #region Fields

    private IBookService bookService;

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private ObservableRangeCollection<BookViewModel> books = new ObservableRangeCollection<BookViewModel>();

    #endregion Properties

    #region Constructors

    public HomePageViewModel(
        IBookService bookService,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.bookService = bookService;
    }

    #endregion Constructors

    #region Lifecycle Events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        IsLoading = true;

        var books = bookService.GetAllBooks();
        Books.ReplaceRange(books);

        IsLoading = false;
    }

    #endregion Lifecycle Events

    #region Commands

    [RelayCommand]
    private async Task AddBook()
    {
    }

    #endregion Commands
}
