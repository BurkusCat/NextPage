using NextPage.Data;
using NextPage.Mappers;
using NextPage.ViewModels;

namespace NextPage.UnitTests.ViewModels;

public class BookMapperTests
{
    [Fact]
    public void MapBookViewModelToBookModel_WhenViewModelProvided_ReturnsMappedModel()
    {
        // arrange
        var viewModel = new BookViewModel
        {
            Title = "Science Book",
            Author = "Claire Lee",
            Description = "A book description",
            Year = 1990,
            Genre = GenreEnum.Academic,
        };

        // act
        var model = BookMapper.MapBookViewModelToBookModel(viewModel);

        // assert
        Assert.Equal("Science Book", model.Title);
        Assert.Equal("Claire Lee", model.Author);
        Assert.Equal("A book description", model.Description);
        Assert.Equal(1990, model.Year);
        Assert.Equal(GenreEnum.Academic, model.Genre);
    }

    [Fact]
    public void MapBookModelToBookViewModel_WhenModelProvided_ReturnsMappedViewModel()
    {
        // arrange
        var model = new BookModel
        {
            Title = "Science Book",
            Author = "Claire Lee",
            Description = "A book description",
            Year = 1990,
            Genre = GenreEnum.Academic,
        };

        // act
        var viewModel = BookMapper.MapBookModelToBookViewModel(model);

        // assert
        Assert.Equal("Science Book", viewModel.Title);
        Assert.Equal("Claire Lee", viewModel.Author);
        Assert.Equal("A book description", viewModel.Description);
        Assert.Equal(1990, viewModel.Year);
        Assert.Equal(GenreEnum.Academic, viewModel.Genre);
    }
}