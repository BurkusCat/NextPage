using Moq;
using NextPage.Abstractions;
using NextPage.ViewModels;
using NextPage.Views;

namespace NextPage.UnitTests.ViewModels;

public class HomePageViewModelTests
{
    private readonly Mock<IBookService> mockBookService = new Mock<IBookService>(MockBehavior.Strict);
    private readonly Mock<INavigationService> mockNavigationService = new Mock<INavigationService>(MockBehavior.Strict);

    public HomePageViewModel ViewModel
    {
        get => new HomePageViewModel(
            mockBookService.Object,
            mockNavigationService.Object);
    }

    [Fact]
    public void Constructor_WhenInitialized_ShouldHaveEmptyCollection()
    {
        // arrange

        // act
        var viewModel = ViewModel;

        // assert
        Assert.NotNull(viewModel.Books);
        Assert.Empty(viewModel.Books);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenPageLoading_ShouldLoadAllBooks()
    {
        // arrange
        var viewModel = ViewModel;
        mockBookService.SetupSequence(x => x.GetAllBooks())
            .Returns(new List<BookViewModel>
            {
                new BookViewModel { Title = "Test" },
            });

        // act
        await viewModel.OnNavigatedTo(new NavigationParameters());

        // assert
        Assert.Single(viewModel.Books);

        VerifyAll();
    }

    [Fact]
    public async Task AddBookCommand_WhenPressed_ShouldNavigateToAddBookPage()
    {
        // arrange
        var viewModel = ViewModel;
        mockNavigationService.SetupSequence(x => x.Push<BookPage>(
            It.Is<NavigationParameters>(parameters => parameters.GetValue<bool>("AddBook"))))
            .Returns(Task.CompletedTask);

        // act
        await viewModel.AddBookCommand.ExecuteAsync(null);

        // assert
        VerifyAll();
    }

    [Fact]
    public async Task ViewExistingBookCommand_WhenPressed_ShouldNavigateToViewBookPage()
    {
        // arrange
        var viewModel = ViewModel;

        var book = new BookViewModel { Title = "ABC" };

        mockNavigationService.SetupSequence(x => x.Push<BookPage>(
            It.Is<NavigationParameters>(parameters => parameters.GetValue<BookViewModel>("Book") == book)))
            .Returns(Task.CompletedTask);

        // act
        await viewModel.ViewExistingBookCommand.ExecuteAsync(book);

        // assert
        VerifyAll();
    }

    private void VerifyAll()
    {
        mockBookService.VerifyAll();
        mockNavigationService.VerifyAll();
    }
}