using Moq;
using NextPage.Models.Enums;
using NextPage.ViewModels;

namespace NextPage.UnitTests.ViewModels;

public class SortPageViewModelTests
{
    private readonly Mock<INavigationService> mockNavigationService = new Mock<INavigationService>(MockBehavior.Strict);

    public SortPageViewModel ViewModel
    {
        get => new SortPageViewModel(
            mockNavigationService.Object);
    }

    [Fact]
    public void Constructor_ShouldSet_SortOptions()
    {
        // Arrange

        // Act
        var viewModel = ViewModel;

        // Assert
        Assert.Equal(3, viewModel.SortOptions.Count);
        Assert.Null(viewModel.SortOrder);
        Assert.Null(viewModel.SortType);
    }

    [Theory]
    [InlineData(BookSortTypeEnum.Title, SortOrderEnum.Ascending)]
    [InlineData(BookSortTypeEnum.Author, SortOrderEnum.Descending)]
    [InlineData(null, null)]
    public async Task OnNavigatedTo_WhenExistingSortSelected_SetsInitialSort(
        BookSortTypeEnum? sortType,
        SortOrderEnum? sortOrder)
    {
        // Arrange
        var viewModel = ViewModel;

        var parameters = new NavigationParameters
        {
            { "SortType", sortType },
            { "SortOrder", sortOrder },
        };

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Equal(sortType, viewModel.SortType);
        Assert.Equal(sortOrder, viewModel.SortOrder);

        VerifyAll();
    }

    [Theory]
    [InlineData(BookSortTypeEnum.Title, SortOrderEnum.Ascending)]
    [InlineData(BookSortTypeEnum.Author, SortOrderEnum.Descending)]
    [InlineData(null, null)]
    public async Task OnNavigatedFrom_WhenSortOptionSelected_ShouldPassSelectedSortOption(
        BookSortTypeEnum? sortType,
        SortOrderEnum? sortOrder)
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.SortType = sortType;
        viewModel.SortOrder = sortOrder;

        var parameters = new NavigationParameters();

        // Act
        await viewModel.OnNavigatedFrom(parameters);

        // Assert
        Assert.Equal(sortType, parameters.GetValue<BookSortTypeEnum?>("SortType"));
        Assert.Equal(sortOrder, parameters.GetValue<SortOrderEnum?>("SortOrder"));

        VerifyAll();
    }

    [Fact]
    public void SelectSortOptionCommand_WhenNoPreviousOptionSelected_ShouldUpdateSortChoice()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.SelectSortOptionCommand.Execute(viewModel.SortOptions
            .Find(option => option.Value == BookSortTypeEnum.Title));

        // Assert
        Assert.Equal(BookSortTypeEnum.Title, viewModel.SortType);
        Assert.Equal(SortOrderEnum.Ascending, viewModel.SortOrder);

        VerifyAll();
    }

    [Fact]
    public void SelectSortOptionCommand_WhenSameAscendingOptionSelected_ShouldUpdateSortChoiceToDescending()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.SortType = BookSortTypeEnum.Title;
        viewModel.SortOrder = SortOrderEnum.Ascending;

        // Act
        viewModel.SelectSortOptionCommand.Execute(viewModel.SortOptions
            .Find(option => option.Value == BookSortTypeEnum.Title));

        // Assert
        Assert.Equal(BookSortTypeEnum.Title, viewModel.SortType);
        Assert.Equal(SortOrderEnum.Descending, viewModel.SortOrder);

        VerifyAll();
    }

    [Fact]
    public void SelectSortOptionCommand_WhenSameDescendingOptionSelected_ShouldUpdateSortChoiceToUnselected()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.SortType = BookSortTypeEnum.Title;
        viewModel.SortOrder = SortOrderEnum.Descending;

        // Act
        viewModel.SelectSortOptionCommand.Execute(viewModel.SortOptions
            .Find(option => option.Value == BookSortTypeEnum.Title));

        // Assert
        Assert.Null(viewModel.SortType);
        Assert.Null(viewModel.SortOrder);

        VerifyAll();
    }

    [Fact]
    public void SelectSortOptionCommand_WhenDifferentOptionSelected_ShouldUpdateSortChoice()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.SortType = BookSortTypeEnum.Title;
        viewModel.SortOrder = SortOrderEnum.Descending;

        // Act
        viewModel.SelectSortOptionCommand.Execute(viewModel.SortOptions
            .Find(option => option.Value == BookSortTypeEnum.Author));

        // Assert
        Assert.Equal(BookSortTypeEnum.Author, viewModel.SortType);
        Assert.Equal(SortOrderEnum.Ascending, viewModel.SortOrder);

        VerifyAll();
    }

    [Fact]
    public async Task CloseCommand_WhenPressed_ShouldPopTheSortPage()
    {
        // Arrange
        var viewModel = ViewModel;

        mockNavigationService.SetupSequence(x => x.Pop())
            .Returns(Task.CompletedTask);

        // Act
        await viewModel.CloseCommand.ExecuteAsync(null);

        // Assert
        VerifyAll();
    }

    private void VerifyAll()
    {
        mockNavigationService.VerifyAll();
    }
}