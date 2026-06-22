using System.ComponentModel;

namespace Articles.Abstractions.Enums;

public enum UserRoleType
{
    //Cross-domain:1-9
    [Description("Editorial Office")] Eof = 1,

    //Sub-domain:11-19
    [Description("Author")] Aut = 11,

    [Description("Corresponding Author")] CorAut = 12,
}

public static class Role
{
    public const string Eof = nameof(UserRoleType.Eof);
    public const string Aut = nameof(UserRoleType.Aut);
    public const string CorAut = nameof(UserRoleType.CorAut);
}