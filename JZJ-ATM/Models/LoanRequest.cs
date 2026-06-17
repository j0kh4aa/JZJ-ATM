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
}