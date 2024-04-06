using Moq;
using NextPage.Abstractions;
using NextPage.ViewModels;

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

    private void VerifyAll()
    {
        mockBookService.VerifyAll();
        mockNavigationService.VerifyAll();
    }
}