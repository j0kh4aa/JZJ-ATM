using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

// საბანკო ოპერაციების სერვისი
public class BankingService : AccountService
{
    public BankingService(FileRepository repo) : base(repo) { }

    // თანხის შეტანა ანგარიშზე
    public void Deposit(User user, decimal amount)
    {
        Users = Repo.LoadUsers();
        var stored = Find(user.Username)!;
        stored.Balance += amount;
        user.Balance = stored.Balance;
        Save();
        Repo.LogTransaction(new Transaction { Username = user.Username, Type = "Deposit", Amount = amount });
    }

    // თანხის გატანა ანგარიშიდან - false თუ არ არის საკმარისი თანხა
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

    // მიმდინარე ბალანსის წაკითხვა ფაილიდან
    public decimal GetBalance(string username)
    {
        Users = Repo.LoadUsers();
        return Find(username)?.Balance ?? 0;
    }
}
