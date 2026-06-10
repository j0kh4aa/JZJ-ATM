using JZJ_ATM.Data;
using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;
using JZJ_ATM.UI;

// Enable UTF-8 encoding for Georgian character support
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// Initialize all services
var repo = new FileRepository();
var auth = new AuthService(repo);
var bank = new BankingService(repo);
var adminSvc = new AdminService(repo);
var loanSvc = new LoanService(repo);

// Create a default admin account on first run if no users exist
var users = repo.LoadUsers();
if (users.Count == 0)
{
    auth.Register("admin", "admin123");
    users = repo.LoadUsers();
    users[0].Role = Role.Admin;
    users[0].Approved = true;
    repo.SaveUsers(users);
}

// Main application loop — route to Admin or Client menu based on role
while (true)
{
    var user = new MainMenu(auth).Run();
    if (user == null) break;

    if (user.Role == Role.Admin)
        new AdminMenu(user, adminSvc, loanSvc, bank).Run();
    else
        new ClientMenu(user, bank, repo, loanSvc).Run();
}

Console.Clear();
Console.WriteLine($"\n  {Lang.Get("Goodbye")}\n");