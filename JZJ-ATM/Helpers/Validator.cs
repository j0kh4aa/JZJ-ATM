namespace JZJ_ATM.Helpers;

// შეყვანილი მონაცემების შემოწმების კლასი
public static class Validator
{
    // თანხის ვალიდაცია - უნდა იყოს დადებითი რიცხვი
    public static bool TryParseAmount(string input, out decimal amount) =>
        decimal.TryParse(input, out amount) && amount > 0;
}
