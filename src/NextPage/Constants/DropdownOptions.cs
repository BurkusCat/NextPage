using NextPage.Data;
using NextPage.Models;
using NextPage.Properties;

namespace NextPage.Constants;

public static class DropdownOptions
{ 
    
    public static List<DropdownOption<GenreEnum>> Genres = new List<DropdownOption<GenreEnum>>()
    {
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreAcademic,
            Value = GenreEnum.Academic,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreAction,
            Value = GenreEnum.Action,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreAdventure,
            Value = GenreEnum.Adventure,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreBiography,
            Value = GenreEnum.Biography,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreComedy,
            Value = GenreEnum.Comedy,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreCooking,
            Value = GenreEnum.Cooking,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreCrimeAndMystery,
            Value = GenreEnum.CrimeAndMystery,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreFantasy,
            Value = GenreEnum.Fantasy,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreHorror,
            Value = GenreEnum.Horror,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreRomance,
            Value = GenreEnum.Romance,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreSciFi,
            Value = GenreEnum.SciFi,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreSelfHelp,
            Value = GenreEnum.SelfHelp,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreTravel,
            Value = GenreEnum.Travel,
        },
        new DropdownOption<GenreEnum>
        {
            Description = Resources.GenreTrueCrime,
            Value = GenreEnum.TrueCrime,
        },
    };
}
