namespace NextPage.Data;

public interface IBookRepository
{
    /// <summary>
    /// Get a list of all saved books.
    /// </summary>
    /// <returns>List of books.</returns>
    IEnumerable<BookModel> GetAllBooks();

    /// <summary>
    /// Adds or updates a book.
    /// </summary>
    /// <param name="seed">The book to save</param>
    void AddOrUpdateBook(BookModel book);

    /// <summary>
    /// Delete a saved book.
    /// </summary>
    /// <param name="book">The ID of the book to delete</param>
    void DeleteBook(Guid id);
}
