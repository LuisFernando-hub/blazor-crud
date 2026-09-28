using System.ComponentModel;

namespace BlazorCrud.Enums;

public enum Gender
{
    [Description("male")]
    Male,
    [Description("female")]
    Female,
    [Description("other")]
    Other
}