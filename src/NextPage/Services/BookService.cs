using NextPage.Abstractions;
using NextPage.Data;
using NextPage.ViewModels;

namespace NextPage.Services;

public class BookService : IBookService
{
    public IEnumerable<BookViewModel> GetAllBooks()
    {
        return
        [
            new BookViewModel
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description = "Harry Potter has no idea how famous he is. That's because he's being raised by his miserable aunt and uncle who are terrified Harry will learn that he's really a wizard, just as his parents were. But everything changes when Harry is summoned to attend an infamous school for wizards, and he begins to discover some clues about his illustrious birthright. From the surprising way he is greeted by a lovable giant, to the unique curriculum and colorful faculty at his unusual school, Harry finds himself drawn deep inside a mystical world he never knew existed and closer to his own noble destiny.",
                Year = 1997,
                Genre = GenreEnum.Fantasy,
            },
            new BookViewModel
            {
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Description = "The Great Gatsby, F. Scott Fitzgerald's third book, stands as the supreme achievement of his career. This exemplary novel of the Jazz Age has been acclaimed by generations of readers. The story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan, of lavish parties on Long Island at a time when The New York Times noted \"gin was the national drink and sex the national obsession,\" it is an exquisitely crafted tale of America in the 1920s.",
                Year = 1925,
                Genre = GenreEnum.Romance,
            },
            new BookViewModel
            {
                Title = "Brave New World",
                Author = "Aldous Huxley",
                Description = "Aldous Huxley's profoundly important classic of world literature, Brave New World is a searching vision of an unequal, technologically-advanced future where humans are genetically bred, socially indoctrinated, and pharmaceutically anesthetized to passively uphold an authoritarian ruling order–all at the cost of our freedom, full humanity, and perhaps also our souls. “A genius [who] who spent his life decrying the onward march of the Machine” (The New Yorker), Huxley was a man of incomparable talents: equally an artist, a spiritual seeker, and one of history’s keenest observers of human nature and civilization.",
                Year = 1932,
                Genre = GenreEnum.SciFi,
            },
            new BookViewModel
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description = "Harry Potter has no idea how famous he is. That's because he's being raised by his miserable aunt and uncle who are terrified Harry will learn that he's really a wizard, just as his parents were. But everything changes when Harry is summoned to attend an infamous school for wizards, and he begins to discover some clues about his illustrious birthright. From the surprising way he is greeted by a lovable giant, to the unique curriculum and colorful faculty at his unusual school, Harry finds himself drawn deep inside a mystical world he never knew existed and closer to his own noble destiny.",
                Year = 1997,
                Genre = GenreEnum.Fantasy,
            },
            new BookViewModel
            {
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Description = "The Great Gatsby, F. Scott Fitzgerald's third book, stands as the supreme achievement of his career. This exemplary novel of the Jazz Age has been acclaimed by generations of readers. The story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan, of lavish parties on Long Island at a time when The New York Times noted \"gin was the national drink and sex the national obsession,\" it is an exquisitely crafted tale of America in the 1920s.",
                Year = 1925,
                Genre = GenreEnum.Romance,
            },
            new BookViewModel
            {
                Title = "Brave New World",
                Author = "Aldous Huxley",
                Description = "Aldous Huxley's profoundly important classic of world literature, Brave New World is a searching vision of an unequal, technologically-advanced future where humans are genetically bred, socially indoctrinated, and pharmaceutically anesthetized to passively uphold an authoritarian ruling order–all at the cost of our freedom, full humanity, and perhaps also our souls. “A genius [who] who spent his life decrying the onward march of the Machine” (The New Yorker), Huxley was a man of incomparable talents: equally an artist, a spiritual seeker, and one of history’s keenest observers of human nature and civilization.",
                Year = 1932,
                Genre = GenreEnum.SciFi,
            },
            new BookViewModel
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description = "Harry Potter has no idea how famous he is. That's because he's being raised by his miserable aunt and uncle who are terrified Harry will learn that he's really a wizard, just as his parents were. But everything changes when Harry is summoned to attend an infamous school for wizards, and he begins to discover some clues about his illustrious birthright. From the surprising way he is greeted by a lovable giant, to the unique curriculum and colorful faculty at his unusual school, Harry finds himself drawn deep inside a mystical world he never knew existed and closer to his own noble destiny.",
                Year = 1997,
                Genre = GenreEnum.Fantasy,
            },
        ];
    }
}
