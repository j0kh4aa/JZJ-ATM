using System.Text;

namespace JZJ_ATM.Helpers;

// კონსოლის გამოტანის დამხმარე კლასი
public static class Display
{
    private const string Border = "═══════════════════════════════════════";

    // სათაურის გამოტანა კონსოლში
    public static void Header(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n  ╔{Border}╗");
        Console.WriteLine($"  ║        JZJ ATM  -  {title,-19}║");
        Console.WriteLine($"  ╚{Border}╝");
        Console.ResetColor();
    }

    // შეტყობინებების გამოტანა ფერებით
    public static void Success(string msg) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"  OK  {msg}"); Console.ResetColor(); }
    public static void Error(string msg) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"  ERR {msg}"); Console.ResetColor(); }
    public static void Info(string msg) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"  >>  {msg}"); Console.ResetColor(); }
    public static void Line() => Console.WriteLine($"  {new string('-', 37)}");

    // პაროლის წაკითხვა - სიმბოლოები იმალება ვარსკვლავებით
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

    // მომხმარებლისგან ტექსტის წაკითხვა
    public static string Prompt(string label)
    {
        Console.Write($"  {label}: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    // პაუზა - ენის მიხედვით
    public static void Pause() { Console.Write($"\n  {Lang.Get("PressAnyKey")}"); Console.ReadKey(true); }
}
