using JZJ_ATM.Data;
using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;
using JZJ_ATM.UI;

// UTF-8 - ქართული სიმბოლოებისთვის
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// სერვისების ინიციალიზაცია
var repo = new FileRepository();
var auth = new AuthService(repo);
var bank = new BankingService(repo);
var adminSvc = new AdminService(repo);
var loanSvc = new LoanService(repo);

// პირველი გაშვებისას ადმინის შექმნა
var users = repo.LoadUsers();
if (users.Count == 0)
{
    auth.Register("admin", "admin123");
    users = repo.LoadUsers();
    users[0].Role = Role.Admin;
    users[0].Approved = true;
    repo.SaveUsers(users);
}

// მთავარი ციკლი
while (true)
{
    var user = new MainMenu(auth).Run();
    if (user == null) break;

    if (user.Role == Role.Admin) new AdminMenu(user, adminSvc, loanSvc, bank).Run();
    else new ClientMenu(user, bank, repo, loanSvc).Run();
}

Console.Clear();
Console.WriteLine($"\n  {Lang.Get("Goodbye")}\n");