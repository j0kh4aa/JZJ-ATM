using System.Text;

namespace JZJ_ATM.Helpers;

/// <summary>
/// Helper class for all console output — headers, messages, prompts, and password input.
/// </summary>
public static class Display
{
    private const string Border = "═══════════════════════════════════════";

    /// <summary>
    /// Clears the screen and prints a formatted ATM header with the given title.
    /// </summary>
    public static void Header(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n  ╔{Border}╗");
        Console.WriteLine($"  ║        JZJ ATM  -  {title,-19}║");
        Console.WriteLine($"  ╚{Border}╝");
        Console.ResetColor();
    }

    /// <summary>Prints a green success message prefixed with OK.</summary>
    public static void Success(string msg) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"  OK  {msg}"); Console.ResetColor(); }

    /// <summary>Prints a red error message prefixed with ERR.</summary>
    public static void Error(string msg) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"  ERR {msg}"); Console.ResetColor(); }

    /// <summary>Prints a yellow info message prefixed with >>.</summary>
    public static void Info(string msg) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"  >>  {msg}"); Console.ResetColor(); }

    /// <summary>Prints a horizontal divider line.</summary>
    public static void Line() => Console.WriteLine($"  {new string('-', 37)}");

    /// <summary>
    /// Reads a password from the console, masking each character with an asterisk.
    /// Supports backspace for correction.
    /// </summary>
    public static string ReadPassword()
    {
        var pwd = new StringBuilder();
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Backspace && pwd.Length > 0)
            {
                pwd.Remove(pwd.Length - 1, 1);
                Console.Write("\b \b");
            }
            else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                pwd.Append(key.KeyChar);
                Console.Write('*');
            }
        }
        while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return pwd.ToString();
    }

    /// <summary>
    /// Displays a labeled prompt and returns the trimmed user input.
    /// </summary>
    public static string Prompt(string label)
    {
        Console.Write($"  {label}: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    /// <summary>
    /// Pauses execution and waits for any key press. Uses the current language for the message.
    /// </summary>
    public static void Pause() { Console.Write($"\n  {Lang.Get("PressAnyKey")}"); Console.ReadKey(true); }
}