using NextPage.Data;
using NextPage.ViewModels;

namespace NextPage.Mappers;

public static class BookMapper
{
    public static BookModel MapBookViewModelToBookModel(BookViewModel bookViewModel)
    {
        return new BookModel
        {
            Id = bookViewModel.Id,
            Title = bookViewModel.Title,
            Author = bookViewModel.Author,
            Description = bookViewModel.Description,
            Genre = (GenreEnum)bookViewModel.Genre,
            Year = bookViewModel.Year,
        };
    }

    public static BookViewModel MapBookModelToBookViewModel(BookModel bookModel)
    {
        return new BookViewModel
        {
            Id = bookModel.Id,
            Title = bookModel.Title,
            Author = bookModel.Author,
            Description = bookModel.Description,
            Genre = bookModel.Genre,
            Year = bookModel.Year,
        };
    }
}
