using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

/// <summary>
/// Manages loan requests: submission, approval, and rejection.
/// </summary>
public class LoanService
{
    private readonly FileRepository _repo;

    /// <summary>
    /// Initializes the LoanService with the given file repository.
    /// </summary>
    public LoanService(FileRepository repo) => _repo = repo;

    /// <summary>
    /// Submits a new loan request for the user.
    /// Returns false if the user already has a pending request.
    /// </summary>
    public bool RequestLoan(string username, decimal amount)
    {
        var loans = _repo.LoadLoans();
        if (loans.Any(l => l.Username == username && l.Status == "Pending")) return false;
        loans.Add(new LoanRequest { Username = username, Amount = amount });
        _repo.SaveLoans(loans);
        return true;
    }

    /// <summary>
    /// Returns all loan requests currently in Pending status.
    /// </summary>
    public LoanRequest[] GetPending() =>
        [.. _repo.LoadLoans().Where(l => l.Status == "Pending")];

    /// <summary>
    /// Returns all loan requests belonging to the specified user.
    /// </summary>
    public LoanRequest[] GetUserLoans(string username) =>
        [.. _repo.LoadLoans().Where(l => l.Username == username)];

    /// <summary>
    /// Approves a pending loan, credits the amount to the user's balance, and logs the transaction.
    /// Returns false if the loan or user is not found.
    /// </summary>
    public bool Approve(string username, decimal amount, BankingService bank, User adminUser)
    {
        var loans = _repo.LoadLoans();
        var loan = loans.FirstOrDefault(l => l.Username == username && l.Amount == amount && l.Status == "Pending");
        if (loan == null) return false;

        var users = _repo.LoadUsers();
        var target = users.FirstOrDefault(u => u.Username == username);
        if (target == null) return false;

        target.Balance += amount;
        _repo.SaveUsers(users);
        _repo.LogTransaction(new Transaction { Username = username, Type = "Loan", Amount = amount });

        loan.Status = "Approved";
        _repo.SaveLoans(loans);
        return true;
    }

    /// <summary>
    /// Rejects a pending loan request by updating its status to Rejected.
    /// Returns false if the loan is not found.
    /// </summary>
    public bool Reject(string username, decimal amount)
    {
        var loans = _repo.LoadLoans();
        var loan = loans.FirstOrDefault(l => l.Username == username && l.Amount == amount && l.Status == "Pending");
        if (loan == null) return false;
        loan.Status = "Rejected";
        _repo.SaveLoans(loans);
        return true;
    }
}