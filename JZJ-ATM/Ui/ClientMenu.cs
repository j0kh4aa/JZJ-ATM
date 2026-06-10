using JZJ_ATM.Data;
using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

/// <summary>
/// Displays the client menu and handles all banking operations for a logged-in user.
/// </summary>
public class ClientMenu(User user, BankingService bank, FileRepository repo, LoanService loans)
{
    /// <summary>
    /// Starts the client menu loop until the user logs out.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            Display.Header($"{(Lang.Current == "Georgian" ? "კლიენტი" : "Client")}: {user.Username}");
            Console.WriteLine($"  [1] {Lang.Get("Balance")}");
            Console.WriteLine($"  [2] {Lang.Get("Deposit")}");
            Console.WriteLine($"  [3] {Lang.Get("Withdraw")}");
            Console.WriteLine($"  [4] {Lang.Get("History")}");
            Console.WriteLine($"  [5] {Lang.Get("RequestLoan")}");
            Console.WriteLine($"  [6] {Lang.Get("MyLoans")}");
            Console.WriteLine($"  [0] {Lang.Get("Logout")}");
            Display.Line();

            switch (Display.Prompt(Lang.Get("Choice")))
            {
                case "1": ShowBalance(); break;
                case "2": DoDeposit(); break;
                case "3": DoWithdraw(); break;
                case "4": ShowHistory(); break;
                case "5": RequestLoan(); break;
                case "6": ShowLoans(); break;
                case "0": return;
                default: Display.Error(Lang.Get("InvalidChoice")); Display.Pause(); break;
            }
        }
    }

    /// <summary>
    /// Displays the current account balance.
    /// </summary>
    private void ShowBalance()
    {
        Display.Header(Lang.Get("Balance"));
        Display.Info($"Balance: {bank.GetBalance(user.Username):F2} GEL");
        Display.Pause();
    }

    /// <summary>
    /// Prompts the user for an amount and performs a deposit.
    /// </summary>
    private void DoDeposit()
    {
        Display.Header(Lang.Get("Deposit"));
        var input = Display.Prompt(Lang.Get("Amount"));
        if (!Validator.TryParseAmount(input, out decimal amount)) { Display.Error(Lang.Get("InvalidAmount")); Display.Pause(); return; }
        bank.Deposit(user, amount);
        Display.Success($"{Lang.Get("Deposit")}: {amount:F2} GEL | Balance: {user.Balance:F2} GEL");
        Display.Pause();
    }

    /// <summary>
    /// Prompts the user for an amount and performs a withdrawal if funds are sufficient.
    /// </summary>
    private void DoWithdraw()
    {
        Display.Header(Lang.Get("Withdraw"));
        var input = Display.Prompt(Lang.Get("Amount"));
        if (!Validator.TryParseAmount(input, out decimal amount)) { Display.Error(Lang.Get("InvalidAmount")); Display.Pause(); return; }
        if (bank.Withdraw(user, amount))
            Display.Success($"{Lang.Get("Withdraw")}: {amount:F2} GEL | Balance: {user.Balance:F2} GEL");
        else
            Display.Error(Lang.Get("InsufficientFunds"));
        Display.Pause();
    }

    /// <summary>
    /// Displays the full transaction history for the current user.
    /// </summary>
    private void ShowHistory()
    {
        Display.Header(Lang.Get("History"));
        var lines = repo.GetUserTransactions(user.Username);
        if (lines.Length == 0) Display.Info(Lang.Get("NoTransactions"));
        else foreach (var l in lines) Console.WriteLine($"  {l}");
        Display.Pause();
    }

    /// <summary>
    /// Prompts the user for a loan amount and submits a loan request.
    /// </summary>
    private void RequestLoan()
    {
        Display.Header(Lang.Get("RequestLoan"));
        var input = Display.Prompt(Lang.Get("LoanAmount"));
        if (!Validator.TryParseAmount(input, out decimal amount)) { Display.Error(Lang.Get("InvalidAmount")); Display.Pause(); return; }
        if (loans.RequestLoan(user.Username, amount))
            Display.Success(Lang.Get("LoanSubmitted"));
        else
            Display.Error(Lang.Get("AlreadyPending"));
        Display.Pause();
    }

    /// <summary>
    /// Displays all loan requests for the current user with color-coded statuses.
    /// </summary>
    private void ShowLoans()
    {
        Display.Header(Lang.Get("MyLoans"));
        var myLoans = loans.GetUserLoans(user.Username);
        if (myLoans.Length == 0) { Display.Info(Lang.Get("NoLoans")); Display.Pause(); return; }

        foreach (var l in myLoans)
        {
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