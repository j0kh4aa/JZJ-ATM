namespace JZJ_ATM.Models;

/// <summary>
/// Represents a loan request submitted by a user.
/// </summary>
public class LoanRequest
{
    /// <summary>Gets or sets the username of the loan applicant.</summary>
    public string Username { get; set; } = "";

    /// <summary>Gets or sets the requested loan amount in GEL.</summary>
    public decimal Amount { get; set; }

    /// <summary>Gets or sets the current status: Pending, Approved, or Rejected.</summary>
    public string Status { get; set; } = "Pending";

    /// <summary>
    /// Serializes the loan request into a pipe-delimited string for file storage.
    /// </summary>
    public string Serialize() => $"{Username}|{Amount}|{Status}";

    /// <summary>
    /// Deserializes a pipe-delimited string from file into a LoanRequest object.
    /// </summary>
    public static LoanRequest Deserialize(string line)
    {
        var p = line.Split('|');
        return new LoanRequest
        {
            Username = p[0],
            Amount = decimal.Parse(p[1]),
            Status = p[2]
        };
    }
}