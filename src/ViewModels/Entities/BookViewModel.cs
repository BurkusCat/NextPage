
using CommunityToolkit.Mvvm.ComponentModel;
using NextPage.Models.Enums;

namespace NextPage.ViewModels;

public partial class BookViewModel : ObservableObject
{
    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string author;

    [ObservableProperty]
    private GenreEnum genre;

    [ObservableProperty]
    private int year;

    [ObservableProperty]
    private string description;
}