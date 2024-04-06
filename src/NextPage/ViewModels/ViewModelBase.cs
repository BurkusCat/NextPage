using CommunityToolkit.Mvvm.ComponentModel;

namespace NextPage.ViewModels;

public abstract class ViewModelBase : ObservableObject, INavigatedEvents, INavigatingEvents
{
    #region Fields

    protected INavigationService navigationService { get; private set; }

    #endregion Fields

    #region Constructors

    public ViewModelBase(
        INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    #endregion Constructors

    #region Lifecycle Methods

    public virtual Task OnNavigatedTo(NavigationParameters parameters)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatedFrom(NavigationParameters parameters)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatingFrom(NavigationParameters parameters)
    {
        return Task.CompletedTask;
    }

    #endregion Lifecycle Methods
}
