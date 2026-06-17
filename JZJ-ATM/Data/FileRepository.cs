using System.Text.Json;
using JZJ_ATM.Models;

namespace JZJ_ATM.Data;

/// <summary>
/// Handles all file read and write operations for users, transactions, and loans.
/// </summary>
public class FileRepository
{
    private static readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

    /// <summary>
    /// Ensures the Data folder and all required files exist on startup.
    /// </summary>
    public FileRepository()
    {
        Directory.CreateDirectory("Data");
        if (!File.Exists(FilePaths.Users)) File.WriteAllText(FilePaths.Users, "[]");
        if (!File.Exists(FilePaths.Transactions)) File.WriteAllText(FilePaths.Transactions, "[]");
        if (!File.Exists(FilePaths.Loans)) File.WriteAllText(FilePaths.Loans, "[]");
    }

    /// <summary>
    /// Loads and returns all users from the users file.
    /// </summary>
    public List<User> LoadUsers() =>
        JsonSerializer.Deserialize<List<User>>(File.ReadAllText(FilePaths.Users), _opts) ?? [];

    /// <summary>
    /// Saves the given list of users to the users file.
    /// </summary>
    public void SaveUsers(List<User> users) =>
        File.WriteAllText(FilePaths.Users, JsonSerializer.Serialize(users, _opts));

    /// <summary>
    /// Appends a single transaction entry to the transactions log file.
    /// </summary>
    public void LogTransaction(Transaction tx)
    {
        var list = JsonSerializer.Deserialize<List<Transaction>>(File.ReadAllText(FilePaths.Transactions), _opts) ?? [];
        list.Add(tx);
        File.WriteAllText(FilePaths.Transactions, JsonSerializer.Serialize(list, _opts));
    }

    /// <summary>
    /// Returns all transaction lines that belong to the specified user.
    /// </summary>
    public string[] GetUserTransactions(string username)
    {
        var list = JsonSerializer.Deserialize<List<Transaction>>(File.ReadAllText(FilePaths.Transactions), _opts) ?? [];
        return list
            .Where(t => t.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            .Select(t => $"{t.Date:yyyy-MM-dd HH:mm:ss} | {t.Username,-15} | {t.Type,-10} | {t.Amount,10:F2} GEL")
            .ToArray();
    }

    /// <summary>
    /// Loads and returns all loan requests from the loans file.
    /// </summary>
    public List<LoanRequest> LoadLoans() =>
        JsonSerializer.Deserialize<List<LoanRequest>>(File.ReadAllText(FilePaths.Loans), _opts) ?? [];

    /// <summary>
    /// Saves the given list of loan requests to the loans file.
    /// </summary>
    public void SaveLoans(List<LoanRequest> loans) =>
        File.WriteAllText(FilePaths.Loans, JsonSerializer.Serialize(loans, _opts));
}