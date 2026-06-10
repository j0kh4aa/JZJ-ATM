namespace JZJ_ATM.Helpers;

/// <summary>
/// Manages the current display language and provides localized text lookups.
/// Supports English and Georgian.
/// </summary>
public static class Lang
{
    /// <summary>Gets the currently active language. Default is English.</summary>
    public static string Current { get; private set; } = "English";

    /// <summary>
    /// Sets the active language for all text lookups.
    /// </summary>
    public static void Set(string lang) => Current = lang;

    /// <summary>
    /// Dictionary of all UI text keys mapped to their English and Georgian translations.
    /// </summary>
    private static readonly Dictionary<string, Dictionary<string, string>> Texts = new()
    {
        ["Welcome"] = new() { ["English"] = "Welcome", ["Georgian"] = "მოგესალმებით" },
        ["Login"] = new() { ["English"] = "Login", ["Georgian"] = "შესვლა" },
        ["Register"] = new() { ["English"] = "Register", ["Georgian"] = "რეგისტრაცია" },
        ["Exit"] = new() { ["English"] = "Exit", ["Georgian"] = "გასვლა" },
        ["Choice"] = new() { ["English"] = "Choice", ["Georgian"] = "არჩევანი" },
        ["Username"] = new() { ["English"] = "Username", ["Georgian"] = "მომხმარებლის სახელი" },
        ["Password"] = new() { ["English"] = "Password", ["Georgian"] = "პაროლი" },
        ["Logout"] = new() { ["English"] = "Logout", ["Georgian"] = "გამოსვლა" },
        ["Balance"] = new() { ["English"] = "Check Balance", ["Georgian"] = "ბალანსის შემოწმება" },
        ["Deposit"] = new() { ["English"] = "Deposit", ["Georgian"] = "შეტანა" },
        ["Withdraw"] = new() { ["English"] = "Withdraw", ["Georgian"] = "გატანა" },
        ["History"] = new() { ["English"] = "Transaction History", ["Georgian"] = "ტრანზაქციების ისტორია" },
        ["RequestLoan"] = new() { ["English"] = "Request a Loan", ["Georgian"] = "სესხის მოთხოვნა" },
        ["MyLoans"] = new() { ["English"] = "My Loan Requests", ["Georgian"] = "ჩემი სესხები" },
        ["PendingReg"] = new() { ["English"] = "Pending Registrations", ["Georgian"] = "მომლოდინე რეგისტრაციები" },
        ["PendingLoans"] = new() { ["English"] = "Pending Loan Requests", ["Georgian"] = "მომლოდინე სესხები" },
        ["Amount"] = new() { ["English"] = "Amount (GEL)", ["Georgian"] = "თანხა (GEL)" },
        ["LoanAmount"] = new() { ["English"] = "Loan amount (GEL)", ["Georgian"] = "სესხის თანხა (GEL)" },
        ["Approve"] = new() { ["English"] = "[A]pprove / [R]eject / [S]kip", ["Georgian"] = "[A] დამტკიცება / [R] უარყოფა / [S] გამოტოვება" },
        ["NoTransactions"] = new() { ["English"] = "No transactions yet.", ["Georgian"] = "ტრანზაქცია არ არის." },
        ["NoLoans"] = new() { ["English"] = "No loan requests yet.", ["Georgian"] = "სესხის მოთხოვნა არ არის." },
        ["NoPending"] = new() { ["English"] = "No pending registrations.", ["Georgian"] = "მომლოდინე რეგისტრაცია არ არის." },
        ["NoPendingLoans"] = new() { ["English"] = "No pending loan requests.", ["Georgian"] = "მომლოდინე სესხი არ არის." },
        ["InvalidChoice"] = new() { ["English"] = "Invalid choice.", ["Georgian"] = "არასწორი არჩევანი." },
        ["InvalidAmount"] = new() { ["English"] = "Invalid amount.", ["Georgian"] = "არასწორი თანხა." },
        ["InsufficientFunds"] = new() { ["English"] = "Insufficient funds.", ["Georgian"] = "არასაკმარისი თანხა." },
        ["AlreadyPending"] = new() { ["English"] = "You already have a pending loan request.", ["Georgian"] = "უკვე გაქვთ მომლოდინე სესხის მოთხოვნა." },
        ["LoanSubmitted"] = new() { ["English"] = "Loan request submitted! Wait for admin approval.", ["Georgian"] = "სესხის მოთხოვნა გაიგზავნა! დაელოდეთ ადმინის დამტკიცებას." },
        ["Registered"] = new() { ["English"] = "Registered! Wait for admin approval.", ["Georgian"] = "რეგისტრაცია წარმატებულია! დაელოდეთ ადმინის დამტკიცებას." },
        ["UsernameTaken"] = new() { ["English"] = "Username already taken.", ["Georgian"] = "ასეთი მომხმარებელი უკვე არსებობს." },
        ["WrongCredentials"] = new() { ["English"] = "Wrong credentials or account not approved.", ["Georgian"] = "არასწორი მონაცემები ან ანგარიში არ არის დამტკიცებული." },
        ["PressAnyKey"] = new() { ["English"] = "Press any key to continue...", ["Georgian"] = "დააჭირეთ ნებისმიერ ღილაკს..." },
        ["Goodbye"] = new() { ["English"] = "Thank you for using JZJ ATM. Goodbye!", ["Georgian"] = "გმადლობთ JZJ ATM-ით სარგებლობისთვის. ნახვამდის!" },
        ["Skipped"] = new() { ["English"] = "Skipped.", ["Georgian"] = "გამოტოვებულია." },
        ["SelectLanguage"] = new() { ["English"] = "Select Language", ["Georgian"] = "აირჩიეთ ენა" },
        ["User"] = new() { ["English"] = "User", ["Georgian"] = "მომხმარებელი" },
    };

    /// <summary>
    /// Returns the localized text for the given key in the current language.
    /// Falls back to the key itself if no match is found.
    /// </summary>
    public static string Get(string key) =>
        Texts.TryGetValue(key, out var t) && t.TryGetValue(Current, out var v) ? v : key;
}
