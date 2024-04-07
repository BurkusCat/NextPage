using System.Windows.Input;

namespace NextPage.Views;

public partial class IconButton : ContentView
{
    /// <summary>
    /// Bindable property for the icon image source of the icon button control.
    /// </summary>
    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(
            nameof(IconSource),
            returnType: typeof(ImageSource),
            defaultBindingMode: BindingMode.TwoWay,
            declaringType: typeof(IconButton));

    /// <summary>
    /// Gets or sets the icon image source of the icon button control.
    /// </summary>
    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }


    /// <summary>
    /// Bindable property for the command of the icon button control.
    /// </summary>
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            returnType: typeof(ICommand),
            defaultBindingMode: BindingMode.OneTime,
            declaringType: typeof(IconButton));

    /// <summary>
    /// Gets or sets the command for the icon button control control.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public IconButton()
    {
        InitializeComponent();
    }
}
