using JZJ_ATM.Helpers;
using JZJ_ATM.Models;
using JZJ_ATM.Services;

namespace JZJ_ATM.UI;

/// <summary>
/// Displays the main entry menu: language selection, login, and registration.
/// </summary>
public class MainMenu(AuthService auth)
{
    /// <summary>
    /// Runs the main menu loop. Returns the logged-in User or null if the user exits.
    /// </summary>
    public User? Run()
    {
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
                    var u = DoLogin();
                    if (u != null) return u;
                    break;
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

    /// <summary>
    /// Displays a language selection screen and sets the active language.
    /// </summary>
    private static void SelectLanguage()
    {
        Display.Header("Language / ენა");
        Console.WriteLine("  [1] English");
        Console.WriteLine("  [2] ქართული");
        Display.Line();
        var choice = Display.Prompt("Choice / არჩევანი");
        Lang.Set(choice == "2" ? "Georgian" : "English");
    }

    /// <summary>
    /// Prompts for credentials and attempts to log the user in.
    /// Returns the User object on success, or null on failure.
    /// </summary>
    private User? DoLogin()
    {
        Display.Header(Lang.Get("Login"));
        var username = Display.Prompt(Lang.Get("Username"));
        Console.Write($"  {Lang.Get("Password")}: ");
        var password = Display.ReadPassword();
        var user = auth.Login(username, password);
        if (user == null) { Display.Error(Lang.Get("WrongCredentials")); Display.Pause(); }
        return user;
    }

    /// <summary>
    /// Prompts for credentials and registers a new user account pending admin approval.
    /// </summary>
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
