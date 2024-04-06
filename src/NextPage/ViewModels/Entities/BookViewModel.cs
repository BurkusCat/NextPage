
using CommunityToolkit.Mvvm.ComponentModel;
using NextPage.Data;

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
    private GenreEnum? genre;

    [ObservableProperty]
    private int year;

    [ObservableProperty]
    private string description;
}