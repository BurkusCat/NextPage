using NextPage.Models.Enums;
using NextPage.Utilities;
using NextPage.ViewModels;

namespace NextPage.UnitTests.ViewModels;

public class BookViewModelComparerTests
{
    private static BookViewModel titleBAuthorCYearA = new BookViewModel
    {
        Title = "Metallic",
        Author = "Zed",
        Year = 1907,
    };

    private static BookViewModel titleAAuthorBYearC = new BookViewModel
    {
        Title = "Apples",
        Author = "Michael",
        Year = 2024,
    };

    private static BookViewModel titleCAuthorAYearB = new BookViewModel
    {
        Title = "X-ray",
        Author = "Andrew",
        Year = 1990,
    };

    private List<BookViewModel> testViewModels = new List<BookViewModel>
    {
        titleBAuthorCYearA,
        titleAAuthorBYearC,
        titleCAuthorAYearB,
    };

    public BookViewModelComparer Comparer
    {
        get => new BookViewModelComparer();
    }

    [Fact]
    public void Compare_WhenSortingTitleByAscending_ShouldReturnCorrectOrder()
    {
        // Arrange
        var comparer = Comparer;
        comparer.SortType = BookSortTypeEnum.Title;
        comparer.SortOrder = SortOrderEnum.Ascending;

        // Act
        testViewModels.Sort(comparer);

        // Assert
        Assert.Equal(titleAAuthorBYearC, testViewModels[0]);
        Assert.Equal(titleBAuthorCYearA, testViewModels[1]);
        Assert.Equal(titleCAuthorAYearB, testViewModels[2]);
    }

    [Fact]
    public void Compare_WhenSortingTitleByDescending_ShouldReturnCorrectOrder()
    {
        // Arrange
        var comparer = Comparer;
        comparer.SortType = BookSortTypeEnum.Title;
        comparer.SortOrder = SortOrderEnum.Descending;

        // Act
        testViewModels.Sort(comparer);

        // Assert
        Assert.Equal(titleCAuthorAYearB, testViewModels[0]);
        Assert.Equal(titleBAuthorCYearA, testViewModels[1]);
        Assert.Equal(titleAAuthorBYearC, testViewModels[2]);
    }

    [Fact]
    public void Compare_WhenSortingAuthorByAscending_ShouldReturnCorrectOrder()
    {
        // Arrange
        var comparer = Comparer;
        comparer.SortType = BookSortTypeEnum.Author;
        comparer.SortOrder = SortOrderEnum.Ascending;

        // Act
        testViewModels.Sort(comparer);

        // Assert
        Assert.Equal(titleCAuthorAYearB, testViewModels[0]);
        Assert.Equal(titleAAuthorBYearC, testViewModels[1]);
        Assert.Equal(titleBAuthorCYearA, testViewModels[2]);
    }

    [Fact]
    public void Compare_WhenSortingAuthorByDescending_ShouldReturnCorrectOrder()
    {
        // Arrange
        var comparer = Comparer;
        comparer.SortType = BookSortTypeEnum.Author;
        comparer.SortOrder = SortOrderEnum.Descending;

        // Act
        testViewModels.Sort(comparer);

        // Assert
        Assert.Equal(titleBAuthorCYearA, testViewModels[0]);
        Assert.Equal(titleAAuthorBYearC, testViewModels[1]);
        Assert.Equal(titleCAuthorAYearB, testViewModels[2]);
    }

    [Fact]
    public void Compare_WhenSortingYearByAscending_ShouldReturnCorrectOrder()
    {
        // Arrange
        var comparer = Comparer;
        comparer.SortType = BookSortTypeEnum.Year;
        comparer.SortOrder = SortOrderEnum.Ascending;

        // Act
        testViewModels.Sort(comparer);

        // Assert
        Assert.Equal(titleBAuthorCYearA, testViewModels[0]);
        Assert.Equal(titleCAuthorAYearB, testViewModels[1]);
        Assert.Equal(titleAAuthorBYearC, testViewModels[2]);
    }

    [Fact]
    public void Compare_WhenSortingYearByDescending_ShouldReturnCorrectOrder()
    {
        // Arrange
        var comparer = Comparer;
        comparer.SortType = BookSortTypeEnum.Year;
        comparer.SortOrder = SortOrderEnum.Descending;

        // Act
        testViewModels.Sort(comparer);

        // Assert
        Assert.Equal(titleAAuthorBYearC, testViewModels[0]);
        Assert.Equal(titleCAuthorAYearB, testViewModels[1]);
        Assert.Equal(titleBAuthorCYearA, testViewModels[2]);
    }
}