using CommunityToolkit.Mvvm.ComponentModel;
using NextPage.Abstractions;
using NextPage.Constants;
using NextPage.Properties;

namespace NextPage.ViewModels;

public partial class BookPageViewModel : ViewModelBase
{
    #region Fields

    private IBookService bookService;

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isEditing;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private BookViewModel book;

    #endregion Properties

    #region Constructors

    public BookPageViewModel(
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

        if (parameters.GetValue<bool>(NavigationParameterKeys.AddBook))
        {
            SetupAddMode();
        }
        else
        {
            SetupViewExistingBookMode(parameters);
        }

        IsLoading = false;
    }

    #endregion Lifecycle Events

    #region Commands

    #endregion Commands

    #region Private methods

    private void SetupAddMode()
    {
        Title = Resources.AddBookPageTitle;
        Book = new BookViewModel
        {
            // default year to now
            Year = DateTime.Now.Year,
        };

        // allow user to immediately start entering data
        IsEditing = true;
    }

    private void SetupViewExistingBookMode(NavigationParameters parameters)
    {
        Book = parameters.GetValue<BookViewModel>(NavigationParameterKeys.Book);
        Title = string.Format(
            Resources.EditBookPageTitle,
            Book.Title);
    }

    #endregion Private methods
}
