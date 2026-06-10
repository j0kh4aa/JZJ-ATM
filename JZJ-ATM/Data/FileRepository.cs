using JZJ_ATM.Models;

namespace JZJ_ATM.Data;

/// <summary>
/// Handles all file read and write operations for users, transactions, and loans.
/// </summary>
public class FileRepository
{
    /// <summary>
    /// Ensures the Data folder and all required files exist on startup.
    /// </summary>
    public FileRepository()
    {
        Directory.CreateDirectory("Data");
        if (!File.Exists(FilePaths.Users)) File.Create(FilePaths.Users).Close();
        if (!File.Exists(FilePaths.Transactions)) File.Create(FilePaths.Transactions).Close();
        if (!File.Exists(FilePaths.Loans)) File.Create(FilePaths.Loans).Close();
    }

    /// <summary>
    /// Loads and returns all users from the users file.
    /// </summary>
    public List<User> LoadUsers() =>
        File.ReadAllLines(FilePaths.Users)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(User.Deserialize)
            .ToList();

    /// <summary>
    /// Saves the given list of users to the users file.
    /// </summary>
    public void SaveUsers(List<User> users) =>
        File.WriteAllLines(FilePaths.Users, users.Select(u => u.Serialize()));

    /// <summary>
    /// Appends a single transaction entry to the transactions log file.
    /// </summary>
    public void LogTransaction(Transaction tx) =>
        File.AppendAllText(FilePaths.Transactions, tx.Serialize() + Environment.NewLine);

    /// <summary>
    /// Returns all transaction lines that belong to the specified user.
    /// </summary>
    public string[] GetUserTransactions(string username) =>
        File.ReadAllLines(FilePaths.Transactions)
            .Where(l => l.Contains($"| {username,-15} |"))
            .ToArray();

    /// <summary>
    /// Loads and returns all loan requests from the loans file.
    /// </summary>
    public List<LoanRequest> LoadLoans() =>
        File.ReadAllLines(FilePaths.Loans)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(LoanRequest.Deserialize)
            .ToList();

    /// <summary>
    /// Saves the given list of loan requests to the loans file.
    /// </summary>
    public void SaveLoans(List<LoanRequest> loans) =>
        File.WriteAllLines(FilePaths.Loans, loans.Select(l => l.Serialize()));
}