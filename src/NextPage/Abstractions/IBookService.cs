using NextPage.ViewModels;

namespace NextPage.Abstractions;

public interface IBookService
{
    IEnumerable<BookViewModel> GetAllBooks();
}
