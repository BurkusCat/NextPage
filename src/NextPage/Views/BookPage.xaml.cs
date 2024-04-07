using NextPage.ViewModels;

namespace NextPage.Views;

[DisableBackButtonNavigator]
public partial class BookPage : ContentPage
{
    private BookPageViewModel viewModel;

    public BookPage()
    {
        viewModel = BindingContext as BookPageViewModel;

        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (viewModel == null)
        {
            viewModel = (BookPageViewModel)BindingContext;
        }
    }

    protected override bool OnBackButtonPressed()
    {
        if (!viewModel.IsEditing)
        {
            // allow users to freely leave when not editing
            return BackButtonNavigator.HandleBackButtonPressed();
        }

        _ = viewModel.CancelCommand.ExecuteAsync(null);

        // cancel the back button command
        return true;
    }
}
