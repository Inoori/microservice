using System.Text.RegularExpressions;

namespace Submission.Domain.ValueObjects;

public partial class EmailAddress
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex EmailRegex();

    public string Value { get; private set; }

    private EmailAddress(string value) => Value = value;


    public static EmailAddress Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return !IsValid(value)
            ? throw new ArgumentException("Invalid email address", nameof(value))
            : new EmailAddress(value);
    }

    private static bool IsValid(string email)
    {
        try
        {
            return EmailRegex().IsMatch(email);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}