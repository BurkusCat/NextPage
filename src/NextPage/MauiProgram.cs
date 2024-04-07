using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NextPage.Abstractions;
using NextPage.Data;
using NextPage.Services;
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
            .RegisterPersistanceRepositories()
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
        mauiAppBuilder.Services.AddTransient<BookPageViewModel>();
        mauiAppBuilder.Services.AddTransient<HomePageViewModel>();
        mauiAppBuilder.Services.AddTransient<SortPageViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<BookPage>();
        mauiAppBuilder.Services.AddTransient<HomePage>();
        mauiAppBuilder.Services.AddTransient<SortPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IBookService, BookService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPersistanceRepositories(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IBookRepository, BookRepository>();

        mauiAppBuilder.Services.AddDbContext<NextPageDbContext>();

        return mauiAppBuilder;
    }
}
