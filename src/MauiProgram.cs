using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NextPage.ViewModels;
using NextPage.Views;

namespace NextPage;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBurkusMvvm(burkusMvvm =>
            {
                burkusMvvm.OnStart(async (INavigationService navigationService) =>
                {
                    await navigationService.Push<HomePage>();
                });
            })
            .UseMauiCommunityToolkit()
            .RegisterViewModels()
            .RegisterViews()
            .RegisterAppServices()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<HomeViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<HomePage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        return mauiAppBuilder;
    }
}
