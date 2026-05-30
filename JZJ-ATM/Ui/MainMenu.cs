using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

// მთავარი მენიუ - ენის არჩევა, შესვლა, რეგისტრაცია
public class MainMenu(AuthService auth)
{
    public User? Run()
    {
        // პირველი გაშვებისას ენის არჩევა
        SelectLanguage();

        while (true)
        {
            Display.Header(Lang.Get("Welcome"));
            Console.WriteLine($"  [1] {Lang.Get("Login")}");
            Console.WriteLine($"  [2] {Lang.Get("Register")}");
            Console.WriteLine($"  [L] Language / ენა");
            Console.WriteLine($"  [0] {Lang.Get("Exit")}");
            Display.Line();

            switch (Display.Prompt(Lang.Get("Choice")).ToUpper())
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
                case "L":
                    SelectLanguage();
                    break;
                case "0":
                    return null;
                default:
                    Display.Error(Lang.Get("InvalidChoice"));
                    Display.Pause();
                    break;
            }
        }
    }

    // ენის არჩევის ეკრანი
    private static void SelectLanguage()
    {
        Display.Header("Language / ენა");
        Console.WriteLine("  [1] English");
        Console.WriteLine("  [2] ქართული");
        Display.Line();
        var choice = Display.Prompt("Choice / არჩევანი");
        Lang.Set(choice == "2" ? "Georgian" : "English");
    }

    // შესვლის პროცესი
    private User? DoLogin()
    {
        Display.Header(Lang.Get("Login"));
        var username = Display.Prompt(Lang.Get("Username"));
        Console.Write($"  {Lang.Get("Password")}: ");
        var password = Display.ReadPassword();
        var user = auth.Login(username, password);
        if (user == null)
        {
            Display.Error(Lang.Get("WrongCredentials"));
            Display.Pause();
        }
        return user;
    }

    // რეგისტრაციის პროცესი
    private void DoRegister()
    {
        Display.Header(Lang.Get("Register"));
        var username = Display.Prompt(Lang.Get("Username"));
        Console.Write($"  {Lang.Get("Password")}: ");
        var password = Display.ReadPassword();
        if (auth.Register(username, password))
            Display.Success(Lang.Get("Registered"));
        else
            Display.Error(Lang.Get("UsernameTaken"));
        Display.Pause();
    }
}
