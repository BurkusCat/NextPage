using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NextPage.Constants;
using NextPage.Models;
using NextPage.Models.Enums;
using NextPage.Properties;

namespace NextPage.ViewModels;

public partial class SortPageViewModel : ViewModelBase
{
    #region Fields

    private readonly ISemanticScreenReader semanticScreenReader;

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private SortOrderEnum? sortOrder;

    [ObservableProperty]
    private BookSortTypeEnum? sortType;

    public List<DropdownOption<BookSortTypeEnum>> SortOptions { get; } = DropdownOptions.SortTypeOptions;

    #endregion Properties

    #region Constructors

    public SortPageViewModel(
        INavigationService navigationService,
        ISemanticScreenReader semanticScreenReader)
        : base(navigationService)
    {
        this.semanticScreenReader = semanticScreenReader;
    }

    #endregion Constructors

    #region Lifecycle Events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        SortOrder = parameters.GetValue<SortOrderEnum?>(NavigationParameterKeys.SortOrder);
        SortType = parameters.GetValue<BookSortTypeEnum?>(NavigationParameterKeys.SortType);

        AnnounceSortStateForScreenReaders();
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
        }
        else
        {
            // this sort type is being selected for the first time
            SortOrder = SortOrderEnum.Ascending;
            SortType = sortOption.Value;
        }

        AnnounceSortStateForScreenReaders();
    }

    #endregion Commands

    #region Private methods

    private void AnnounceSortStateForScreenReaders()
    {
        // announce the current state of the sort to screen readers
        if (SortOrder == null || SortType == null)
        {
            semanticScreenReader.Announce(Resources.SemanticDescriptionNoSortSelected);
        }
        else
        {
            var sortTypeMessage = string.Format(
                Resources.SemanticDescriptionCurrentSortDescription,
                SortType?.ToString(),
                SortOrder?.ToString());

            semanticScreenReader.Announce(sortTypeMessage);
        }
    }

    #endregion Private methods
}
