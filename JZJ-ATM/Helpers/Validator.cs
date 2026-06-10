namespace JZJ_ATM.Helpers;

/// <summary>
/// Provides input validation utilities for user-entered data.
/// </summary>
public static class Validator
{
    /// <summary>
    /// Tries to parse the input as a positive decimal amount.
    /// Returns true if the value is valid and greater than zero.
    /// </summary>
    public static bool TryParseAmount(string input, out decimal amount) =>
        decimal.TryParse(input, out amount) && amount > 0;
}
