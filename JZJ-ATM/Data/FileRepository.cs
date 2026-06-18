using System.Text.Json;
using JZJ_ATM.Models;

namespace JZJ_ATM.Data;

/// <summary>
/// Handles all file read and write operations for users, transactions, and loans.
/// </summary>
public class FileRepository
{
    private static readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

    private readonly string _usersPath;
    private readonly string _transactionsPath;
    private readonly string _loansPath;

    public FileRepository()
    {
        _usersPath = GenerateFilePath(FilePaths.Users);
        _transactionsPath = GenerateFilePath(FilePaths.Transactions);
        _loansPath = GenerateFilePath(FilePaths.Loans);
    }

    private string GenerateFilePath(string fileName)
    {
        string projectPath = Directory.GetParent(Environment.CurrentDirectory)?
            .Parent?.Parent?.FullName ?? string.Empty;
        string folderPath = Path.Combine(projectPath, "Data");
        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "[]");

        return filePath;
    }

    /// <summary>Loads and returns all users from the users file.</summary>
    public List<User> LoadUsers() =>
        JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_usersPath), _opts) ?? [];

    /// <summary>Saves the given list of users to the users file.</summary>
    public void SaveUsers(List<User> users) =>
        File.WriteAllText(_usersPath, JsonSerializer.Serialize(users, _opts));

    /// <summary>Appends a single transaction entry to the transactions log file.</summary>
    public void LogTransaction(Transaction tx)
    {
        var list = JsonSerializer.Deserialize<List<Transaction>>(File.ReadAllText(_transactionsPath), _opts) ?? [];
        list.Add(tx);
        File.WriteAllText(_transactionsPath, JsonSerializer.Serialize(list, _opts));
    }

    /// <summary>Returns all transactions that belong to the specified user.</summary>
    public string[] GetUserTransactions(string username)
    {
        var list = JsonSerializer.Deserialize<List<Transaction>>(File.ReadAllText(_transactionsPath), _opts) ?? [];
        return list
            .Where(t => t.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            .Select(t => $"{t.Date:yyyy-MM-dd HH:mm:ss} | {t.Username,-15} | {t.Type,-10} | {t.Amount,10:F2} GEL")
            .ToArray();
    }

    /// <summary>Loads and returns all loan requests from the loans file.</summary>
    public List<LoanRequest> LoadLoans() =>
        JsonSerializer.Deserialize<List<LoanRequest>>(File.ReadAllText(_loansPath), _opts) ?? [];

    /// <summary>Saves the given list of loan requests to the loans file.</summary>
    public void SaveLoans(List<LoanRequest> loans) =>
        File.WriteAllText(_loansPath, JsonSerializer.Serialize(loans, _opts));
}