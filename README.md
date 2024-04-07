# Next Page
Next Page is a Personal Book Library application developed with .NET MAUI and C#. This application was written by Ronan Burke.

## Prerequisites
- Visual Studio 2022 17.9.4
  - .NET MAUI workload
  - .NET SDK 8.0.203 https://dotnet.microsoft.com/en-us/download/dotnet/8.0
  - Android SDKs and emulator
  - XAMLStyler extension https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler2022
  
## Getting started
1. Open the `src` folder
2. Open the `NextPage.sln` solution file in Visual Studio.
3. If you need the MAUI workload: Inside a terminal, run `dotnet workload install maui`
4. From the debug menu, select Android emulators and choose your preferred emulator
5. Press Debug to run the app

For convience, I have provided the release version of the application produced from the build pipeline: `nextpage.apk` which you can drag into an emulator to install. This should not be done in typical, production repositories.

## Overview
The project is built using the MVVM pattern. I have used a library I developed, [Burkus.Mvvm.Maui](https://github.com/BurkusCat/Burkus.Mvvm.Maui), in combination with [MVVM Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/) to reduce boilerplate, repeated code, and to make app easily unit testable.

The business logic for the pages is written within `ViewModels`. Properties are used to expose data to the UI layer so data can be viewed and modified by the user.

The pages use dependency injection to bring in `Services` for navigation, saving/loading data, and showing alert messages.

The page UI is built using XAML. Pages use [compiled bindings](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/compiled-bindings?view=net-maui-8.0) to improve performance and to give compile-time resolving of bindings. Most UI text is stored within `Resources.resx` to allow for string reuse, separation from the code, and to make it easier to localize the application in the future. The app has been tested with the Android TalkBack screen reader and at higher display scale/font sizes to ensure it is accessible.

To improve the user experience, this project includes: validation messages, swipe to delete + delete on the book page itself, as-you-type searching, and sorting.

The unit test project uses strict mocks to ensure that only expected dependencies are ran with the correct parameters. This makes sure that no unexpected code/services are invoked.

The database used is Entity Framework Core + SQLite. SQLite is a popular choice for mobile databases and EFCore is a powerful ORM layer that has great features like data migrations.

## EFCore Migrations

### Setup

1. Open the "Package Manager Console" in Visual Studio
2. Run `Install-Package Microsoft.EntityFrameworkCore.Tools` (use `Update-Package Microsoft.EntityFrameworkCore.Tools` to update instead)

### Creating a migration

1. Set `NextPage.Data` as the startup project in Visual Studio
2. Open the "Package Manager Console" in Visual Studio
3. Set the default project to `NextPage.Data` in the Package manager console
4. Run `Add-Migration DescriptiveMigrationName`

## Pipelines
In the GitHub repository, ["Workflow permissions"](https://docs.github.com/actions/reference/authentication-in-a-workflow#modifying-the-permissions-for-the-github_token) needs set to "Read and write permissions" to allow releases to be tagged correctly. Set "
Allow GitHub Actions to create and approve pull requests" to `true`.

The project contains two GitHub Actions pipelines, one for running the unit tests and one for building a release version of the app. The following secret variables must be setup:

- RELEASE_KEYSTORE
- RELEASE_KEYSTORE_ALIAS
- RELEASE_KEYSTORE_PASSWORD