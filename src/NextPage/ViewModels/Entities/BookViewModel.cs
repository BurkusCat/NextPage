
using CommunityToolkit.Mvvm.ComponentModel;
using NextPage.Data;
using NextPage.Models;

namespace NextPage.ViewModels;

public partial class BookViewModel : ObservableObject
{
    [ObservableProperty]
    private Guid id;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string author;

    [ObservableProperty]
    private DropdownOption<GenreEnum>? genre;

    /// <summary>
    /// To make conversions from the UI entry easier, store the year
    /// as a string when editing. The <see cref="Year"/> property can be
    /// used to store the final year or null if no year was entered.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Year))]
    private string yearAsString;

    public int? Year
    {
        get
        {
            if (string.IsNullOrWhiteSpace(YearAsString))
            {
                return null;
            }

            int.TryParse(yearAsString, out var year);
            return year;
        }
    }

    [ObservableProperty]
    private string description;
}