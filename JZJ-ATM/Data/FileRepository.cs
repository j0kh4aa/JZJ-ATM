using JZJ_ATM.Models;

namespace JZJ_ATM.Data;

// ფაილებთან მუშაობის კლასი - კითხვა და ჩაწერა
public class FileRepository
{
    // საჭირო ფაილების და საქაღალდის შექმნა თუ არ არსებობს
    public FileRepository()
    {
        Directory.CreateDirectory("Data");
        if (!File.Exists(FilePaths.Users)) File.Create(FilePaths.Users).Close();
        if (!File.Exists(FilePaths.Transactions)) File.Create(FilePaths.Transactions).Close();
        if (!File.Exists(FilePaths.Loans)) File.Create(FilePaths.Loans).Close();
    }

    // მომხმარებლების ჩატვირთვა ფაილიდან
    public List<User> LoadUsers() =>
        File.ReadAllLines(FilePaths.Users)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(User.Deserialize)
            .ToList();

    // მომხმარებლების შენახვა ფაილში
    public void SaveUsers(List<User> users) =>
        File.WriteAllLines(FilePaths.Users, users.Select(u => u.Serialize()));

    // ტრანზაქციის ლოგში დამატება
    public void LogTransaction(Transaction tx) =>
        File.AppendAllText(FilePaths.Transactions, tx.Serialize() + Environment.NewLine);

    // კონკრეტული მომხმარებლის ტრანზაქციების წაკითხვა
    public string[] GetUserTransactions(string username) =>
        File.ReadAllLines(FilePaths.Transactions)
            .Where(l => l.Contains($"| {username,-15} |"))
            .ToArray();

    // სესხების ჩატვირთვა ფაილიდან
    public List<LoanRequest> LoadLoans() =>
        File.ReadAllLines(FilePaths.Loans)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(LoanRequest.Deserialize)
            .ToList();

    // სესხების შენახვა ფაილში
    public void SaveLoans(List<LoanRequest> loans) =>
        File.WriteAllLines(FilePaths.Loans, loans.Select(l => l.Serialize()));
}