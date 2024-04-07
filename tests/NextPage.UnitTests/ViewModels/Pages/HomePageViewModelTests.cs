using Moq;
using NextPage.Abstractions;
using NextPage.Constants;
using NextPage.Data;
using NextPage.Models.Enums;
using NextPage.ViewModels;
using NextPage.Views;

namespace NextPage.UnitTests.ViewModels;

public class HomePageViewModelTests
{
    private readonly Mock<IBookService> mockBookService = new Mock<IBookService>(MockBehavior.Strict);
    private readonly Mock<IDialogService> mockDialogService = new Mock<IDialogService>(MockBehavior.Strict);
    private readonly Mock<INavigationService> mockNavigationService = new Mock<INavigationService>(MockBehavior.Strict);

    public HomePageViewModel ViewModel
    {
        get => new HomePageViewModel(
            mockBookService.Object,
            mockDialogService.Object,
            mockNavigationService.Object);
    }

    [Fact]
    public void Constructor_WhenInitialized_ShouldHaveEmptyCollection()
    {
        // arrange

        // act
        var viewModel = ViewModel;

        // assert
        Assert.NotNull(viewModel.FilteredBooks);
        Assert.Empty(viewModel.FilteredBooks);

        Assert.Equal(string.Empty, viewModel.SearchQuery);
        Assert.Null(viewModel.SortOrder);
        Assert.Null(viewModel.SortType);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenPageLoading_ShouldLoadAllBooks()
    {
        // arrange
        var viewModel = ViewModel;
        SetupMockBooks();

        // act
        await viewModel.OnNavigatedTo(new NavigationParameters());

        // assert
        Assert.Equal(4, viewModel.FilteredBooks.Count);

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
    [Fact]
    public void DeleteCommand_WhenUserDoesNotConfirm_DoesNothing()
    {
        // arrange
        var viewModel = ViewModel;
        var book = new BookViewModel();
        viewModel.FilteredBooks.Add(book);

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm delete",
                "Are you sure want to delete this book? This cannot be undone.",
                "Delete",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(false);

        // act
        viewModel.DeleteCommand.Execute(book);

        // assert
        Assert.Single(viewModel.FilteredBooks);

        VerifyAll();
    }

    [Fact]
    public void DeleteCommand_WhenUserConfirms_DeletesBook()
    {
        // arrange
        var viewModel = ViewModel;
        var book = new BookViewModel();
        viewModel.FilteredBooks.Add(book);

        mockDialogService
            .SetupSequence(x => x.DisplayAlert(
                "Confirm delete",
                "Are you sure want to delete this book? This cannot be undone.",
                "Delete",
                "Cancel",
                It.IsAny<FlowDirection>()))
            .ReturnsAsync(true);

        mockBookService.SetupSequence(x => x.DeleteBook(book));

        // act
        viewModel.DeleteCommand.Execute(book);

        // assert
        Assert.Empty(viewModel.FilteredBooks);

        VerifyAll();
    }

    [Theory]
    [InlineData("2000", 1)]
    [InlineData("cOoKi", 1)]
    [InlineData("hungry", 1)]
    [InlineData("sPy", 1)]
    [InlineData("i", 4)]
    [InlineData("Ca", 2)]
    [InlineData("e ", 2)]
    public async Task SearchCommand_WhenQueryTextProvided_ShouldShowResultsMatchingText(
        string searchQuery,
        int expectedResultsCount)
    {
        // Arrange
        var viewModel = ViewModel;
        SetupMockBooks();
        await viewModel.OnNavigatedTo(new NavigationParameters());

        viewModel.SearchQuery = searchQuery;

        // Act
        viewModel.SearchCommand.Execute(null);

        // Assert
        Assert.Equal(expectedResultsCount, viewModel.FilteredBooks.Count);

        VerifyAll();
    }

    [Fact]
    public async Task ClearFiltersAndSort_WhenPressed_ShouldResetAnySearchTermOrSort()
    {
        // arrange
        var viewModel = ViewModel;
        SetupMockBooks();
        viewModel.SearchQuery = "Doesn't exist";
        viewModel.SortOrder = SortOrderEnum.Ascending;
        viewModel.SortType = BookSortTypeEnum.Title;
        await viewModel.OnNavigatedTo(new NavigationParameters());

        // act
        viewModel.ClearFiltersAndSortCommand.Execute(null);

        // assert
        Assert.Equal(4, viewModel.FilteredBooks.Count);
        Assert.Equal(string.Empty, viewModel.SearchQuery);
        Assert.Null(viewModel.SortOrder);
        Assert.Null(viewModel.SortType);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenReturningFromSortPage_ShouldSetAscendingSortSelection()
    {
        // Arrange
        var viewModel = ViewModel;
        SetupMockBooks();

        var parameters = new NavigationParameters
        {
            { "SortType", BookSortTypeEnum.Title },
            { "SortOrder", SortOrderEnum.Ascending },
        };

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Equal(BookSortTypeEnum.Title, viewModel.SortType);
        Assert.Equal(SortOrderEnum.Ascending, viewModel.SortOrder);
        Assert.Equal("Captain Underpants", viewModel.FilteredBooks[0].Title);
        Assert.Equal("I Spy", viewModel.FilteredBooks[1].Title);
        Assert.Equal("If You Give A Mouse A Cookie", viewModel.FilteredBooks[2].Title);
        Assert.Equal("The Very Hungry Caterpillar", viewModel.FilteredBooks[3].Title);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenReturningFromSortPage_ShouldSetDescendingSortSelection()
    {
        // Arrange
        var viewModel = ViewModel;
        SetupMockBooks();

        var parameters = new NavigationParameters
        {
            { "SortType", BookSortTypeEnum.Title },
            { "SortOrder", SortOrderEnum.Descending },
        };

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Equal(BookSortTypeEnum.Title, viewModel.SortType);
        Assert.Equal(SortOrderEnum.Descending, viewModel.SortOrder);
        Assert.Equal("The Very Hungry Caterpillar", viewModel.FilteredBooks[0].Title);
        Assert.Equal("If You Give A Mouse A Cookie", viewModel.FilteredBooks[1].Title);
        Assert.Equal("I Spy", viewModel.FilteredBooks[2].Title);
        Assert.Equal("Captain Underpants", viewModel.FilteredBooks[3].Title);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenReturningFromSortPage_ShouldClearSortSelection()
    {
        // Arrange
        var viewModel = ViewModel;
        SetupMockBooks();

        viewModel.SortType = BookSortTypeEnum.Title;
        viewModel.SortOrder = SortOrderEnum.Descending;

        var parameters = new NavigationParameters
        {
            { "SortType", null },
            { "SortOrder", null },
        };

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Null(viewModel.SortType);
        Assert.Null(viewModel.SortOrder);

        VerifyAll();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenReturningFromOtherPages_ShouldNotOverwriteSortSelection()
    {
        // Arrange
        var viewModel = ViewModel;
        SetupMockBooks();

        viewModel.SortType = BookSortTypeEnum.Title;
        viewModel.SortOrder = SortOrderEnum.Descending;

        var parameters = new NavigationParameters();

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Equal(BookSortTypeEnum.Title, viewModel.SortType);
        Assert.Equal(SortOrderEnum.Descending, viewModel.SortOrder);

        VerifyAll();
    }

    [Theory]
    [InlineData("", BookSortTypeEnum.Title, SortOrderEnum.Ascending, true)]
    [InlineData("   ", null, null, false)]
    [InlineData("", BookSortTypeEnum.Title, null, true)]
    [InlineData(null, null, SortOrderEnum.Ascending, true)]
    [InlineData("Mountain", null, null, true)]
    public void IsFilteringOrSorting_WhenSearchingOrSorting_ReturnsTrue(
        string searchQuery,
        BookSortTypeEnum? sortType,
        SortOrderEnum? sortOrder,
        bool expectedIsFilteringOrSorting)
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.SearchQuery = searchQuery;
        viewModel.SortType = sortType;
        viewModel.SortOrder = sortOrder;

        // Act
        var isFilteringOrSorting = viewModel.IsFilteringOrSorting;

        // Assert
        Assert.Equal(expectedIsFilteringOrSorting, isFilteringOrSorting);

        VerifyAll();
    }

    private void SetupMockBooks()
    {
        mockBookService.SetupSequence(x => x.GetAllBooks())
            .Returns(new List<BookViewModel>
            {
                new BookViewModel
                {
                    Title = "The Very Hungry Caterpillar",
                    Author = "Eric Carle",
                    YearAsString = "2000",
                    Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.SelfHelp),
                },
                new BookViewModel
                {
                    Title = "If You Give A Mouse A Cookie",
                    Author = "Laura Joffe Numeroff",
                    YearAsString = "1992",
                    Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Cooking),
                },
                new BookViewModel
                {
                    Title = "I Spy",
                    Author = "Walter Wick and Jean Marzollo",
                    YearAsString = "1990",
                    Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Adventure),
                },
                new BookViewModel
                {
                    Title = "Captain Underpants",
                    Author = "Dav Pilkey",
                    YearAsString = "1997",
                    Genre = DropdownOptions.Genres.Find(x => x.Value == GenreEnum.Adventure),
                },
            });
    }

    private void VerifyAll()
    {
        mockBookService.VerifyAll();
        mockDialogService.VerifyAll();
        mockNavigationService.VerifyAll();
    }
}