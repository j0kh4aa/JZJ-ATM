using JZJ_ATM.Data;
using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

// კლიენტის მენიუ - საბანკო ოპერაციები
public class ClientMenu(User user, BankingService bank, FileRepository repo, LoanService loans)
{
    public void Run()
    {
        while (true)
        {
            Display.Header($"Client: {user.Username}");
            Console.WriteLine("  [1] Check Balance");
            Console.WriteLine("  [2] Deposit");
            Console.WriteLine("  [3] Withdraw");
            Console.WriteLine("  [4] Transaction History");
            Console.WriteLine("  [5] Request a Loan");
            Console.WriteLine("  [6] My Loan Requests");
            Console.WriteLine("  [0] Logout");
            Display.Line();

            switch (Display.Prompt("Choice"))
            {
                case "1": ShowBalance(); break;
                case "2": DoDeposit(); break;
                case "3": DoWithdraw(); break;
                case "4": ShowHistory(); break;
                case "5": RequestLoan(); break;
                case "6": ShowLoans(); break;
                case "0": return;
                default: Display.Error("Invalid choice."); Display.Pause(); break;
            }
        }
    }

    // ბალანსის ნახვა
    private void ShowBalance()
    {
        Display.Header("Balance");
        Display.Info($"Balance: {bank.GetBalance(user.Username):F2} GEL");
        Display.Pause();
    }

    // თანხის შეტანა ანგარიშზე
    private void DoDeposit()
    {
        Display.Header("Deposit");
        var input = Display.Prompt("Amount (GEL)");
        if (!Validator.TryParseAmount(input, out decimal amount)) { Display.Error("Invalid amount."); Display.Pause(); return; }
        bank.Deposit(user, amount);
        Display.Success($"Deposited {amount:F2} GEL. Balance: {user.Balance:F2} GEL");
        Display.Pause();
    }

    // თანხის გატანა ანგარიშიდან
    private void DoWithdraw()
    {
        Display.Header("Withdraw");
        var input = Display.Prompt("Amount (GEL)");
        if (!Validator.TryParseAmount(input, out decimal amount)) { Display.Error("Invalid amount."); Display.Pause(); return; }
        if (bank.Withdraw(user, amount))
            Display.Success($"Withdrawn {amount:F2} GEL. Balance: {user.Balance:F2} GEL");
        else
            Display.Error("Insufficient funds.");
        Display.Pause();
    }

    // ტრანზაქციების ისტორიის ნახვა
    private void ShowHistory()
    {
        Display.Header("History");
        var lines = repo.GetUserTransactions(user.Username);
        if (lines.Length == 0) Display.Info("No transactions yet.");
        else foreach (var l in lines) Console.WriteLine($"  {l}");
        Display.Pause();
    }

    // სესხის მოთხოვნის გაგზავნა
    private void RequestLoan()
    {
        Display.Header("Request a Loan");
        var input = Display.Prompt("Loan amount (GEL)");
        if (!Validator.TryParseAmount(input, out decimal amount)) { Display.Error("Invalid amount."); Display.Pause(); return; }
        if (loans.RequestLoan(user.Username, amount))
            Display.Success("Loan request submitted! Wait for admin approval.");
        else
            Display.Error("You already have a pending loan request.");
        Display.Pause();
    }

    // კლიენტის სესხის მოთხოვნების სტატუსის ნახვა
    private void ShowLoans()
    {
        Display.Header("My Loan Requests");
        var myLoans = loans.GetUserLoans(user.Username);
        if (myLoans.Length == 0) { Display.Info("No loan requests yet."); Display.Pause(); return; }

        foreach (var l in myLoans)
        {
            // სტატუსის მიხედვით ფერის შეცვლა
            Console.ForegroundColor = l.Status switch
            {
                "Approved" => ConsoleColor.Green,
                "Rejected" => ConsoleColor.Red,
                _ => ConsoleColor.Yellow
            };
            Console.WriteLine($"  {l.Amount,10:F2} GEL  ->  {l.Status}");
            Console.ResetColor();
        }
        Display.Pause();
    }
}
