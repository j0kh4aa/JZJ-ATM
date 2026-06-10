using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

/// <summary>
/// Handles core banking operations: deposit, withdrawal, and balance inquiry.
/// </summary>
public class BankingService : AccountService
{
    /// <summary>
    /// Initializes the BankingService with the given file repository.
    /// </summary>
    public BankingService(FileRepository repo) : base(repo) { }

    /// <summary>
    /// Deposits the specified amount into the user's account and logs the transaction.
    /// </summary>
    public void Deposit(User user, decimal amount)
    {
        Users = Repo.LoadUsers();
        var stored = Find(user.Username)!;
        stored.Balance += amount;
        user.Balance = stored.Balance;
        Save();
        Repo.LogTransaction(new Transaction { Username = user.Username, Type = "Deposit", Amount = amount });
    }

    /// <summary>
    /// Withdraws the specified amount from the user's account.
    /// Returns false if the balance is insufficient.
    /// </summary>
    public bool Withdraw(User user, decimal amount)
    {
        Users = Repo.LoadUsers();
        var stored = Find(user.Username)!;
        if (stored.Balance < amount) return false;
        stored.Balance -= amount;
        user.Balance = stored.Balance;
        Save();
        Repo.LogTransaction(new Transaction { Username = user.Username, Type = "Withdraw", Amount = amount });
        return true;
    }

    /// <summary>
    /// Reads and returns the current balance for the specified user from file.
    /// </summary>
    public decimal GetBalance(string username)
    {
        Users = Repo.LoadUsers();
        return Find(username)?.Balance ?? 0;
    }
}