namespace JZJ_ATM.Models;

/// <summary>
/// Represents a single financial transaction (Deposit, Withdraw, or Loan).
/// </summary>
public class Transaction
{
    /// <summary>Gets or sets the username associated with this transaction.</summary>
    public string Username { get; set; } = "";

    /// <summary>Gets or sets the transaction type: Deposit, Withdraw, or Loan.</summary>
    public string Type { get; set; } = "";

    /// <summary>Gets or sets the transaction amount in GEL.</summary>
    public decimal Amount { get; set; }

    /// <summary>Gets or sets the date and time the transaction occurred.</summary>
    public DateTime Date { get; set; } = DateTime.Now;

    /// <summary>
    /// Formats the transaction as a human-readable log entry for file storage.
    /// </summary>
    public string Serialize() =>
        $"{Date:yyyy-MM-dd HH:mm:ss} | {Username,-15} | {Type,-10} | {Amount,10:F2} GEL";
}