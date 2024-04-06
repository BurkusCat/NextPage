using NextPage.Abstractions;
using NextPage.Data;
using NextPage.Mappers;
using NextPage.ViewModels;

namespace NextPage.Services;

public class BookService : IBookService
{
    #region Fields

    private readonly IBookRepository bookRepository;

    #endregion Fields

    #region Constructor

    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    #endregion Constructor

    public IEnumerable<BookViewModel> GetAllBooks()
    {
        return bookRepository.GetAllBooks()
            .Select(BookMapper.MapBookModelToBookViewModel);
    }

    public void AddOrUpdateBook(BookViewModel bookViewModel)
    {
        var bookModel = BookMapper.MapBookViewModelToBookModel(bookViewModel);

        if (bookModel.Id == Guid.Empty)
        {
            // new books need a generated ID
            bookModel.Id = Guid.NewGuid();
        }

        bookRepository.AddOrUpdateBook(bookModel);
    }

    public void DeleteBook(BookViewModel bookViewModel)
    {
        bookRepository.DeleteBook(bookViewModel.Id);
    }
}
