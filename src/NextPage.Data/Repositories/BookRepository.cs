﻿namespace NextPage.Data;

public class BookRepository : BaseRepository, IBookRepository
{
    /// <summary>
    /// A repository for managing Books.
    /// </summary>
    /// <param name="dbContext"></param>
    public BookRepository(
        NextPageDbContext dbContext)
        : base(dbContext)
    {
    }

    public IEnumerable<BookModel> GetAllBooks()
    {
        return dbContext.Books;
    }

    public void AddOrUpdateBook(BookModel book)
    {
        AddOrUpdate(book);
    }

    public void DeleteBook(Guid id)
    {
        var bookToDelete = dbContext.Books.FirstOrDefault(x => x.Id == id);

        if (bookToDelete != null)
        {
            dbContext.Books.Remove(bookToDelete);
            dbContext.SaveChanges();
        }
    }
}
