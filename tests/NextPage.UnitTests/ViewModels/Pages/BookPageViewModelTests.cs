using Moq;
using NextPage.Abstractions;
using NextPage.Constants;
using NextPage.Models.Enums;
using NextPage.ViewModels;
using NextPage.Views;

namespace NextPage.UnitTests.ViewModels;

public class BookPageViewModelTests
{
    private readonly Mock<IBookService> mockBookService = new Mock<IBookService>(MockBehavior.Strict);
    private readonly Mock<INavigationService> mockNavigationService = new Mock<INavigationService>(MockBehavior.Strict);

    public BookPageViewModel ViewModel
    {
        get => new BookPageViewModel(
            mockBookService.Object,
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
            Genre = GenreEnum.Comedy,
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
        Assert.Equal(GenreEnum.Comedy, viewModel.Book.Genre);
        Assert.Equal(2012, viewModel.Book.Year);
        Assert.False(viewModel.IsEditing);

        VerifyAll();
    }

    private void VerifyAll()
    {
        mockBookService.VerifyAll();
        mockNavigationService.VerifyAll();
    }
}