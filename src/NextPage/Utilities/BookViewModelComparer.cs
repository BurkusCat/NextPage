using NextPage.Models.Enums;
using NextPage.ViewModels;

namespace NextPage.Utilities;

public class BookViewModelComparer : IComparer<BookViewModel>
{
    public SortOrderEnum SortOrder { get; set; }

    public BookSortTypeEnum SortType { get; set; }

    public int Compare(BookViewModel x, BookViewModel y)
    {
        int comparisonResult = 0;

        if (SortType == BookSortTypeEnum.Title)
        {
            comparisonResult = Comparer<string>.Default.Compare(x.Title, y.Title);
        }
        else if (SortType == BookSortTypeEnum.Author)
        {
            comparisonResult = Comparer<string>.Default.Compare(x.Author, y.Author);
        }
        else if (SortType == BookSortTypeEnum.Year)
        {
            comparisonResult = Comparer<int>.Default.Compare(x.Year, y.Year);
        }

        // the sort order may be used to flip the comparison result
        var sortOrderMultiplier = SortOrder == SortOrderEnum.Ascending ? 1 : -1;
        return comparisonResult * sortOrderMultiplier;
    }
}
