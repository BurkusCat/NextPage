using NextPage.Constants;
using NextPage.Converters;
using NextPage.Data;
using NextPage.Mappers;
using NextPage.Models.Enums;
using NextPage.ViewModels;
using System.Globalization;

namespace NextPage.UnitTests.ViewModels;

public class SortOrderToSortImageConverterTests
{
    [Fact]
    public void Convert_NullSortOrder_ToSortIconImageSource()
    {
        // arrange
        var converter = new SortOrderToSortImageConverter();

        // act
        var imageSource = converter.Convert(null, null, null, CultureInfo.InvariantCulture);

        // assert
        Assert.Equal("sort.png", imageSource);
    }

    [Fact]
    public void Convert_AscendingSortOrder_ToAscendingIconImageSource()
    {
        // arrange
        var converter = new SortOrderToSortImageConverter();

        // act
        var imageSource = converter.Convert(SortOrderEnum.Ascending, null, null, CultureInfo.InvariantCulture);

        // assert
        Assert.Equal("ascending.png", imageSource);
    }

    [Fact]
    public void Convert_DescendingSortOrder_ToDescendinIconImageSource()
    {
        // arrange
        var converter = new SortOrderToSortImageConverter();

        // act
        var imageSource = converter.Convert(SortOrderEnum.Descending, null, null, CultureInfo.InvariantCulture);

        // assert
        Assert.Equal("descending.png", imageSource);
    }
}