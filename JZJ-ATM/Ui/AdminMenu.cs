using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

// ადმინის მენიუ - მომხმარებლებისა და სესხების მართვა
public class AdminMenu(User admin, AdminService adminSvc, LoanService loanSvc, BankingService bank)
{
    public void Run()
    {
        while (true)
        {
            Display.Header($"Admin: {admin.Username}");
            Console.WriteLine("  [1] Pending Registrations");
            Console.WriteLine("  [2] Pending Loan Requests");
            Console.WriteLine("  [0] Logout");
            Display.Line();

            switch (Display.Prompt("Choice"))
            {
                case "1": ManagePending(); break;
                case "2": ManageLoans(); break;
                case "0": return;
                default: Display.Error("Invalid choice."); Display.Pause(); break;
            }
        }
    }

    // მომლოდინე რეგისტრაციების დამტკიცება ან უარყოფა
    private void ManagePending()
    {
        var pending = adminSvc.GetPending();
        Display.Header("Pending Registrations");
        if (pending.Length == 0) { Display.Info("No pending registrations."); Display.Pause(); return; }

        foreach (var u in pending)
        {
            Display.Line();
            Console.WriteLine($"  User: {u.Username}");
            switch (Display.Prompt("  [A]pprove / [R]eject / [S]kip").ToUpper())
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
                    Display.Info("Skipped.");
                    break;
            }
        }
        Display.Pause();
    }

    // სესხის მოთხოვნების დამტკიცება ან უარყოფა
    private void ManageLoans()
    {
        var pending = loanSvc.GetPending();
        Display.Header("Pending Loans");
        if (pending.Length == 0) { Display.Info("No pending loan requests."); Display.Pause(); return; }

        foreach (var loan in pending)
        {
            Display.Line();
            Console.WriteLine($"  User: {loan.Username}   Amount: {loan.Amount:F2} GEL");
            switch (Display.Prompt("  [A]pprove / [R]eject / [S]kip").ToUpper())
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
                    Display.Info("Skipped.");
                    break;
            }
        }
        Display.Pause();
    }
}
