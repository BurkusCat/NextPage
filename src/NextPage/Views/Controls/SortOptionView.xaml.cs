using NextPage.Models.Enums;

namespace NextPage.Views;

public partial class SortOptionView : ContentView
{
    /// <summary>
    /// Bindable property for the text of the sort option
    /// </summary>
    public static readonly BindableProperty SortOptionNameProperty =
        BindableProperty.Create(
            nameof(SortOptionName),
            returnType: typeof(string),
            declaringType: typeof(SortOptionView),
            defaultValue: string.Empty);

    /// <summary>
    /// Gets or sets the text of the sort option
    /// </summary>
    public string SortOptionName
    {
        get => (string)GetValue(SortOptionNameProperty);
        set => SetValue(SortOptionNameProperty, value);
    }

    /// <summary>
    /// Bindable property for the sort type of the option
    /// </summary>
    public static readonly BindableProperty SortTypeProperty =
        BindableProperty.Create(
            nameof(SortType),
            returnType: typeof(Enum),
            declaringType: typeof(SortOptionView),
            defaultValue: null);

    /// <summary>
    /// Gets or sets the sort type of the option
    /// </summary>
    public Enum SortType
    {
        get => (Enum)GetValue(SortTypeProperty);
        set => SetValue(SortTypeProperty, value);
    }

    /// <summary>
    /// Bindable property for the page's currently selected sort type
    /// </summary>
    public static readonly BindableProperty CurrentSortTypeProperty =
        BindableProperty.Create(
            nameof(CurrentSortType),
            returnType: typeof(Enum),
            declaringType: typeof(SortOptionView),
            defaultValue: null,
            propertyChanged: OnCurrentSortTypeChanged);

    private static void OnCurrentSortTypeChanged(BindableObject bindable, object oldvalue, object newValue)
        => ((SortOptionView)bindable).CurrentSortTypeChanged(newValue);

    /// <summary>
    /// Gets or sets the page's currently selected sort type
    /// </summary>
    public Enum CurrentSortType
    {
        get => (Enum)GetValue(CurrentSortTypeProperty);
        set => SetValue(CurrentSortTypeProperty, value);
    }

    /// <summary>
    /// Bindable property for the page's currently selected sort order
    /// </summary>
    public static readonly BindableProperty CurrentSortOrderProperty =
        BindableProperty.Create(
            nameof(CurrentSortOrder),
            returnType: typeof(Enum),
            declaringType: typeof(SortOptionView),
            defaultValue: null,
            propertyChanged: OnCurrentSortOrderChanged);

    private static void OnCurrentSortOrderChanged(BindableObject bindable, object oldvalue, object newValue)
        => ((SortOptionView)bindable).CurrentSortOrderChanged(newValue);

    /// <summary>
    /// Gets or sets the page's currently selected sort order
    /// </summary>
    public Enum CurrentSortOrder
    {
        get => (Enum)GetValue(CurrentSortOrderProperty);
        set => SetValue(CurrentSortOrderProperty, value);
    }

    public SortOptionView()
    {
        InitializeComponent();
    }

    private void CurrentSortTypeChanged(object newValue)
    {
        var sortType = (Enum)newValue;

        // if the page's sort type is equal to this sort option, that means this
        // option is currently selected
        if (sortType != null && Equals(sortType, SortType))
        {
            view.SetDynamicResource(BackgroundColorProperty, "SortSelectedColor");
            borderView.IsVisible = true;
        }
        else
        {
            view.SetDynamicResource(BackgroundColorProperty, "Transparent");
            borderView.IsVisible = false;
        }
    }

    private void CurrentSortOrderChanged(object newValue)
    {
        var sortOrder = (SortOrderEnum?)newValue;

        if (sortOrder == SortOrderEnum.Ascending)
        {
            sortOrderImage.Source = ImageSource.FromFile("ascending.png");
        }
        else if (sortOrder == SortOrderEnum.Descending)
        {
            sortOrderImage.Source = ImageSource.FromFile("descending.png");
        }
    }
}
