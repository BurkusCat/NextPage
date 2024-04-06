using Moq;
using NextPage.Abstractions;
using NextPage.Constants;
using NextPage.Data;
using NextPage.ViewModels;

namespace NextPage.UnitTests.ViewModels;

public class BookPageViewModelTests
{
    private readonly Mock<IBookService> mockBookService = new Mock<IBookService>(MockBehavior.Strict);
    private readonly Mock<IDialogService> mockDialogService = new Mock<IDialogService>(MockBehavior.Strict);
    private readonly Mock<INavigationService> mockNavigationService = new Mock<INavigationService>(MockBehavior.Strict);

    public BookPageViewModel ViewModel
    {
        get => new BookPageViewModel(
            mockBookService.Object,
            mockDialogService.Object,
            mockNavigationService.Object);
    }

    [Fact]
    public async Task OnNavigatedTo_WhenAddingNewBook_ShouldSetupPageInAddMode()
    {
        // arrange
        var viewModel = ViewModel;
        var parameters = new NavigationParameters
        {
            { NavigationParameterKeys.AddBook, true },
        };

        // act
        await viewModel.OnNavigatedTo(parameters);

        // assert
        Assert.Equal("Add a new book", viewModel.Title);
        Assert.Null(viewModel.Book.Title);
        Assert.Null(viewModel.Book.Author);
        Assert.Null(viewModel.Book.Description);
        Assert.Null(viewModel.Book.Genre);
        Assert.Equal(DateTime.Now.Year, viewModel.Book.Year);
        Assert.True(viewModel.IsEditing);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenViewingExistingBook_ShouldSetupPageInViewExistingBookMode()
    {
        // arrange
        var viewModel = ViewModel;
        var book = new BookViewModel
        {
            Title = "Llama",
            Author = "Ronan Burke",
            Description = "A book about llamas",
            Year = 2012,
            Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Comedy),
        };

        var parameters = new NavigationParameters
        {
            { NavigationParameterKeys.Book, book },
        };

        // act
        await viewModel.OnNavigatedTo(parameters);

        // assert
        Assert.Equal("Book: Llama", viewModel.Title);
        Assert.Equal("Llama", viewModel.Book.Title);
        Assert.Equal("Ronan Burke", viewModel.Book.Author);
        Assert.Equal("A book about llamas", viewModel.Book.Description);
        Assert.Equal(DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Comedy), viewModel.Book.Genre);
        Assert.Equal(2012, viewModel.Book.Year);
        Assert.False(viewModel.IsEditing);

        VerifyAll();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void EditCommand_WhenPressed_SwitchesPageToEditMode(
        bool isEditing)
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.IsEditing = isEditing;

        // act
        viewModel.EditCommand.Execute(null);

        // assert
        Assert.True(viewModel.IsEditing);

        VerifyAll();
    }

    [Fact]
    public void CancelCommand_WhenUserDoesNotConfirm_DoesNothing()
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.IsEditing = true;

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm discard",
                "Are you sure want to discard your changes to this book?",
                "Discard",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(false);

        // act
        viewModel.CancelCommand.Execute(null);

        // assert
        Assert.True(viewModel.IsEditing);

        VerifyAll();
    }

    [Fact]
    public void CancelCommand_WhenUserConfirms_CancelsAddingNewBook()
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.IsEditing = true;
        viewModel.Book = new BookViewModel();

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm discard",
                "Are you sure want to discard your changes to this book?",
                "Discard",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(true);

        mockNavigationService.SetupSequence(x => x.Pop())
            .Returns(Task.CompletedTask);

        // act
        viewModel.CancelCommand.Execute(null);

        // assert
        VerifyAll();
    }

    [Fact]
    public void CancelCommand_WhenUserConfirms_CancelsAddingExistingBook()
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.IsEditing = true;
        viewModel.Book = new BookViewModel
        {
            Id = new Guid("a0e9cc5e-87f3-4ecb-a1d0-0caa9cdedbc7"),
        };

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm discard",
                "Are you sure want to discard your changes to this book?",
                "Discard",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(true);

        // act
        viewModel.CancelCommand.Execute(null);

        // assert
        Assert.False(viewModel.IsEditing);

        VerifyAll();
    }

    [Fact]
    public void DeleteCommand_WhenUserDoesNotConfirm_DoesNothing()
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.IsEditing = true;

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm delete",
                "Are you sure want to delete this book? This cannot be undone.",
                "Delete",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(false);

        // act
        viewModel.DeleteCommand.Execute(null);

        // assert
        VerifyAll();
    }

    [Fact]
    public void DeleteCommand_WhenUserConfirms_DeletesBook()
    {
        // arrange
        var viewModel = ViewModel;
        var book = new BookViewModel();
        viewModel.Book = book;

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm delete",
                "Are you sure want to delete this book? This cannot be undone.",
                "Delete",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(true);

        mockBookService.SetupSequence(x => x.DeleteBook(book));

        mockNavigationService.SetupSequence(x => x.Pop())
            .Returns(Task.CompletedTask);

        // act
        viewModel.DeleteCommand.Execute(null);

        // assert
        VerifyAll();
    }

    [Fact]
    public void SaveCommand_WhenBookHasManyThingsWrong_ShowsLongErrorMessage()
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.Book = new BookViewModel();

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Error",
                "There is a problem with the book you tried to save. Please correct the following error(s):\r\n\r\n- The book must have a Title\r\n- The book must have an Author\r\n- The book must have a Genre\r\n- The book must have a valid year e.g. 1999\r\n",
                "OK",
                It.IsAny<FlowDirection>()))
            .Returns(Task.CompletedTask);

        // act
        viewModel.SaveCommand.Execute(null);

        // assert
        VerifyAll();
    }

    [Fact]
    public void SaveCommand_WhenBookHasOneThingWrong_ShowsShortErrorMessage()
    {
        // arrange
        var viewModel = ViewModel;
        viewModel.Book = new BookViewModel
        {
            Author = "Ronan Burke",
            Year = 1853,
            Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Romance),
        };

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Error",
                "There is a problem with the book you tried to save. Please correct the following error(s):\r\n\r\n- The book must have a Title\r\n",
                "OK",
                It.IsAny<FlowDirection>()))
            .Returns(Task.CompletedTask);

        // act
        viewModel.SaveCommand.Execute(null);

        // assert
        VerifyAll();
    }

    [Fact]
    public void SaveCommand_WhenAddingValidBook_SavesAndLeavesPage()
    {
        // arrange
        var viewModel = ViewModel;
        var book = new BookViewModel
        {
            Title = "Trains",
            Author = "Ronan Burke",
            Year = 1853,
            Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Romance),
        };
        viewModel.Book = book;

        mockBookService.SetupSequence(x => x.AddOrUpdateBook(book));

        mockNavigationService.SetupSequence(x => x.Pop())
            .Returns(Task.CompletedTask);

        // act
        viewModel.SaveCommand.Execute(null);

        // assert
        Assert.Equal("Book: Trains", viewModel.Title);

        VerifyAll();
    }

    [Fact]
    public void SaveCommand_WhenEditingValidBook_SavesAndExitsEditMode()
    {
        // arrange
        var viewModel = ViewModel;
        var book = new BookViewModel
        {
            Id = new Guid("5e5669af-8c8b-4154-a2d6-122cd44d546e"),
            Title = "Trains",
            Author = "Ronan Burke",
            Year = 1853,
            Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Romance),
        };
        viewModel.Book = book;
        viewModel.IsEditing = true;

        mockBookService.SetupSequence(x => x.AddOrUpdateBook(book));

        // act
        viewModel.SaveCommand.Execute(null);

        // assert
        Assert.Equal("Book: Trains", viewModel.Title);
        Assert.False(viewModel.IsEditing);

        VerifyAll();
    }

    private void VerifyAll()
    {
        mockBookService.VerifyAll();
        mockDialogService.VerifyAll();
        mockNavigationService.VerifyAll();
    }
}