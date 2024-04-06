
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

    [ObservableProperty]
    private int year;

    [ObservableProperty]
    private string description;
}