using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

// სესხის სერვისი - მოთხოვნა, დამტკიცება, უარყოფა
public class LoanService
{
    private readonly FileRepository _repo;

    public LoanService(FileRepository repo) => _repo = repo;

    // სესხის მოთხოვნის გაგზავნა - ერთი მომლოდინე მოთხოვნა დაშვებულია
    public bool RequestLoan(string username, decimal amount)
    {
        var loans = _repo.LoadLoans();
        if (loans.Any(l => l.Username == username && l.Status == "Pending")) return false;
        loans.Add(new LoanRequest { Username = username, Amount = amount });
        _repo.SaveLoans(loans);
        return true;
    }

    // ყველა მომლოდინე სესხის მოთხოვნა
    public LoanRequest[] GetPending() =>
        [.. _repo.LoadLoans().Where(l => l.Status == "Pending")];

    // კონკრეტული მომხმარებლის სესხის მოთხოვნები
    public LoanRequest[] GetUserLoans(string username) =>
        [.. _repo.LoadLoans().Where(l => l.Username == username)];

    // სესხის დამტკიცება - თანხის ჩარიცხვა მომხმარებლის ანგარიშზე
    public bool Approve(string username, decimal amount, BankingService bank, User adminUser)
    {
        var loans = _repo.LoadLoans();
        var loan = loans.FirstOrDefault(l => l.Username == username && l.Amount == amount && l.Status == "Pending");
        if (loan == null) return false;

        var users = _repo.LoadUsers();
        var target = users.FirstOrDefault(u => u.Username == username);
        if (target == null) return false;

        // თანხის დამატება და ლოგირება
        target.Balance += amount;
        _repo.SaveUsers(users);
        _repo.LogTransaction(new Transaction { Username = username, Type = "Loan", Amount = amount });

        loan.Status = "Approved";
        _repo.SaveLoans(loans);
        return true;
    }

    // სესხის უარყოფა
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