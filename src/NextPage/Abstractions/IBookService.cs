using NextPage.ViewModels;

namespace NextPage.Abstractions;

public interface IBookService
{
    /// <summary>
    /// Returns a list of all stored books.
    /// </summary>
    /// <returns>A list of books</returns>
    IEnumerable<BookViewModel> GetAllBooks();

    /// <summary>
    /// Add/updates a book and stores it.
    /// </summary>
    /// <param name="bookViewModel">The book to add/update</param>
    void AddOrUpdateBook(BookViewModel bookViewModel);

    /// <summary>
    /// Delete a book from the database.
    /// </summary>
    /// <param name="bookViewModel">The book to delete</param>
    void DeleteBook(BookViewModel bookViewModel);
}
