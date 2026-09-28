using System.ComponentModel;

namespace BlazorCrud.Enums;

public enum ContactTypes
{
    [Description("phone")]
    Phone,
    [Description("email")]
    Email,
    [Description("telegram")]
    Telegram,
    [Description("instagram")]
    Instagram,
    [Description("linkedin")]
    LinkedIn
}