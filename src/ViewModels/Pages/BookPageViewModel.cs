using CommunityToolkit.Mvvm.ComponentModel;
using NextPage.Abstractions;

namespace NextPage.ViewModels;

public partial class BookPageViewModel : ViewModelBase
{
    #region Fields

    private IBookService bookService;

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private bool isLoading;

    #endregion Properties

    #region Constructors

    public BookPageViewModel(
        IBookService bookService,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.bookService = bookService;
    }

    #endregion Constructors
}
