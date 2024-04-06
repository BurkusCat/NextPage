using System.ComponentModel;

namespace NextPage.Data;

public enum GenreEnum
{
    [Description("Academic")]
    Academic,

    [Description("Action")]
    Action,

    [Description("Adventure")]
    Adventure,

    [Description("Biography")]
    Biography,

    [Description("Comedy")]
    Comedy,

    [Description("Cooking")]
    Cooking,

    [Description("Crime and mystery")]
    CrimeAndMystery,

    [Description("Fantasy")]
    Fantasy,

    [Description("Horror")]
    Horror,

    [Description("Romance")]
    Romance,

    [Description("SciFi")]
    SciFi,

    [Description("Self-help")]
    SelfHelp,

    [Description("Travel")]
    Travel,

    [Description("True crime")]
    TrueCrime
}