using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

// მთავარი მენიუ - შესვლა და რეგისტრაცია
public class MainMenu(AuthService auth)
{
    public User? Run()
    {
        while (true)
        {
            Display.Header("Welcome");
            Console.WriteLine("  [1] Login");
            Console.WriteLine("  [2] Register");
            Console.WriteLine("  [0] Exit");
            Display.Line();

            switch (Display.Prompt("Choice"))
            {
                case "1":
                    {
                        // შესვლის მცდელობა
                        var u = DoLogin();
                        if (u != null) return u;
                        break;
                    }
                case "2":
                    DoRegister();
                    break;
                case "0":
                    return null;
                default:
                    Display.Error("Invalid choice.");
                    Display.Pause();
                    break;
            }
        }
    }

    // შესვლის პროცესი - სახელი და პაროლი
    private User? DoLogin()
    {
        Display.Header("Login");
        var username = Display.Prompt("Username");
        Console.Write("  Password: ");
        var password = Display.ReadPassword();
        var user = auth.Login(username, password);
        if (user == null)
        {
            Display.Error("Wrong credentials or account not approved.");
            Display.Pause();
        }
        return user;
    }

    // რეგისტრაციის პროცესი - ადმინის დამტკიცება საჭიროა
    private void DoRegister()
    {
        Display.Header("Register");
        var username = Display.Prompt("Username");
        Console.Write("  Password: ");
        var password = Display.ReadPassword();
        if (auth.Register(username, password))
            Display.Success("Registered! Wait for admin approval.");
        else
            Display.Error("Username already taken.");
        Display.Pause();
    }
}
