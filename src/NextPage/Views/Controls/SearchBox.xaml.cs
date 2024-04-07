using System.Windows.Input;

namespace NextPage.Views;

public partial class SearchBox : ContentView
{
    /// <summary>
    /// Bindable property for the text of the search box control.
    /// </summary>
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            returnType: typeof(string),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(SearchBox),
            defaultValue: string.Empty);

    /// <summary>
    /// Gets or sets the text of the search box control.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    /// <summary>
    /// Bindable property for the text of the search box control.
    /// </summary>
    public static readonly BindableProperty SearchCommandProperty =
        BindableProperty.Create(
            nameof(SearchCommand),
            returnType: typeof(ICommand),
            defaultBindingMode: BindingMode.OneTime,
            declaringType: typeof(SearchBox));

    /// <summary>
    /// Gets or sets the text of the search box control.
    /// </summary>
    public ICommand SearchCommand
    {
        get => (ICommand)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }

    public SearchBox()
    {
        InitializeComponent();
    }
}
