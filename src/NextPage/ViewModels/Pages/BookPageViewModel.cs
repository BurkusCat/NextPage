using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NextPage.Abstractions;
using NextPage.Constants;
using NextPage.Data;
using NextPage.Models;
using NextPage.Properties;

namespace NextPage.ViewModels;

public partial class BookPageViewModel : ViewModelBase
{
    #region Fields

    private readonly IBookService bookService;
    private readonly IDialogService dialogService;

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

    public IList<DropdownOption<GenreEnum>> Genres { get; } = DropdownOptions.Genres;

    #endregion Properties

    #region Constructors

    public BookPageViewModel(
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

    [RelayCommand]
    private async Task Save()
    {
        var errorMessage = ValidateBook();

        if (string.IsNullOrEmpty(errorMessage))
        {
            IsLoading = true;

            // book is valid and can be saved
            bookService.AddOrUpdateBook(Book);

            // update the title
            Title = string.Format(
                Resources.EditBookPageTitle,
                Book.Title);

            await FinishBookSaving();
        }
        else
        {
            var bookErrorMessage = string.Format(
                Resources.BookErrorMessageFormat,
                errorMessage);

            await dialogService.DisplayAlert(
                Resources.Error,
                bookErrorMessage,
                Resources.ButtonOK);
        }
    }

    [RelayCommand]
    private void Edit()
    {
        IsEditing = true;
    }

    [RelayCommand]
    private async Task Cancel()
    {
        var confirmed = await dialogService.DisplayAlert(
            Resources.ConfirmDiscard,
            Resources.AreYouSureDiscard,
            Resources.ButtonDiscard,
            Resources.ButtonCancel);

        if (!confirmed)
        {
            // user did not want to cancel edits
            return;
        }

        // discard your changes
        await navigationService.Pop();
    }

    [RelayCommand]
    private async Task Delete()
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

        IsLoading = true;

        bookService.DeleteBook(Book);
        await navigationService.Pop();
    }

    #endregion Commands

    #region Private methods

    /// <summary>
    /// Validates the book on this page and returns an error message.
    /// </summary>
    /// <returns>An error message if anything is wrong with the book</returns>
    private string ValidateBook()
    {
        string errorString = string.Empty;

        if (string.IsNullOrEmpty(Book.Title))
        {
            errorString += Resources.EmptyTitleValidationMessage;
        }

        if (string.IsNullOrEmpty(Book.Author))
        {
            errorString += Resources.EmptyAuthorValidationMessage;
        }

        if (Book.Genre == null)
        {
            errorString += Resources.EmptyGenreValidationMessage;
        }

        if (Book.Year < 1 || Book.Year > 9999)
        {
            errorString += Resources.InvalidYearValidationMessage;
        }

        return errorString;
    }

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

    private async Task FinishBookSaving()
    {
        if (Book.Id == Guid.Empty)
        {
            // we were adding a book so leave the page
            await navigationService.Pop();
        }
        else
        {
            // exit edit mode for existing books
            IsEditing = false;
            IsLoading = false;
        }
    }

    #endregion Private methods
}
