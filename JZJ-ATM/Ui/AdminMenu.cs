using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

/// <summary>
/// Displays the admin menu and handles user registration approvals and loan management.
/// </summary>
public class AdminMenu(User admin, AdminService adminSvc, LoanService loanSvc, BankingService bank)
{
    /// <summary>
    /// Starts the admin menu loop until the admin logs out.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            Display.Header($"{(Lang.Current == "Georgian" ? "ადმინი" : "Admin")}: {admin.Username}");
            Console.WriteLine($"  [1] {Lang.Get("PendingReg")}");
            Console.WriteLine($"  [2] {Lang.Get("PendingLoans")}");
            Console.WriteLine($"  [0] {Lang.Get("Logout")}");
            Display.Line();

            switch (Display.Prompt(Lang.Get("Choice")))
            {
                case "1": ManagePending(); break;
                case "2": ManageLoans(); break;
                case "0": return;
                default: Display.Error(Lang.Get("InvalidChoice")); Display.Pause(); break;
            }
        }
    }

    /// <summary>
    /// Lists all pending user registrations and allows the admin to approve, reject, or skip each one.
    /// </summary>
    private void ManagePending()
    {
        var pending = adminSvc.GetPending();
        Display.Header(Lang.Get("PendingReg"));
        if (pending.Length == 0) { Display.Info(Lang.Get("NoPending")); Display.Pause(); return; }

        foreach (var u in pending)
        {
            Display.Line();
            Console.WriteLine($"  {Lang.Get("User")}: {u.Username}");
            switch (Display.Prompt(Lang.Get("Approve")).ToUpper())
            {
                case "A":
                    if (adminSvc.Approve(u.Username))
                        Display.Success($"{u.Username} approved.");
                    break;
                case "R":
                    if (adminSvc.Reject(u.Username))
                        Display.Success($"{u.Username} rejected.");
                    break;
                default:
                    Display.Info(Lang.Get("Skipped"));
                    break;
            }
        }
        Display.Pause();
    }

    /// <summary>
    /// Lists all pending loan requests and allows the admin to approve, reject, or skip each one.
    /// </summary>
    private void ManageLoans()
    {
        var pending = loanSvc.GetPending();
        Display.Header(Lang.Get("PendingLoans"));
        if (pending.Length == 0) { Display.Info(Lang.Get("NoPendingLoans")); Display.Pause(); return; }

        foreach (var loan in pending)
        {
            Display.Line();
            Console.WriteLine($"  {Lang.Get("User")}: {loan.Username}   {Lang.Get("Amount")}: {loan.Amount:F2} GEL");
            switch (Display.Prompt(Lang.Get("Approve")).ToUpper())
            {
                case "A":
                    if (loanSvc.Approve(loan.Username, loan.Amount, bank, admin))
                        Display.Success($"Loan of {loan.Amount:F2} GEL approved for {loan.Username}.");
                    break;
                case "R":
                    if (loanSvc.Reject(loan.Username, loan.Amount))
                        Display.Success($"Loan rejected for {loan.Username}.");
                    break;
                default:
                    Display.Info(Lang.Get("Skipped"));
                    break;
            }
        }
        Display.Pause();
    }
}