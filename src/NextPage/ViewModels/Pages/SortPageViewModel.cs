using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NextPage.Constants;
using NextPage.Models;
using NextPage.Models.Enums;

namespace NextPage.ViewModels;

public partial class SortPageViewModel : ViewModelBase
{
    #region Properties

    [ObservableProperty]
    private SortOrderEnum? sortOrder;

    [ObservableProperty]
    private BookSortTypeEnum? sortType;

    public List<DropdownOption<BookSortTypeEnum>> SortOptions { get; } = DropdownOptions.SortTypeOptions;

    #endregion Properties

    #region Constructors

    public SortPageViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Lifecycle Events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        SortOrder = parameters.GetValue<SortOrderEnum?>(NavigationParameterKeys.SortOrder);
        SortType = parameters.GetValue<BookSortTypeEnum?>(NavigationParameterKeys.SortType);
    }

    public override async Task OnNavigatedFrom(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        // add the select sort options as navigation parameters when leaving
        parameters.Add(NavigationParameterKeys.SortOrder, SortOrder);
        parameters.Add(NavigationParameterKeys.SortType, SortType);
    }

    #endregion Lifecycle Events

    #region Commands

    [RelayCommand]
    private void SelectSortOption(DropdownOption<BookSortTypeEnum> sortOption)
    {
        if (EqualityComparer<BookSortTypeEnum?>.Default.Equals(SortType, sortOption.Value))
        {
            // cycle through the sort order options
            switch (SortOrder)
            {
                case SortOrderEnum.Ascending:
                    SortType = sortOption.Value;
                    SortOrder = SortOrderEnum.Descending;
                    break;

                case SortOrderEnum.Descending:
                    SortType = default;
                    SortOrder = null;
                    break;

                default:
                    SortType = sortOption.Value;
                    SortOrder = SortOrderEnum.Ascending;
                    break;
            }
        } else
        {
            // this sort type is being selected for the first time
            SortOrder = SortOrderEnum.Ascending;
            SortType = sortOption.Value;
        }
    }

    [RelayCommand]
    private async Task Close()
    {
        await navigationService.Pop();
    }

    #endregion Commands
}
