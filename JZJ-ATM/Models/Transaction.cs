namespace JZJ_ATM.Models;

// ტრანზაქციის მოდელი
public class Transaction
{
    // ტრანზაქციის მონაცემები
    public string Username { get; set; } = "";
    public string Type { get; set; } = ""; // Deposit / Withdraw / Loan
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    // ტრანზაქციის ლოგ ფაილში ჩაწერის ფორმატი
    public string Serialize() =>
        $"{Date:yyyy-MM-dd HH:mm:ss} | {Username,-15} | {Type,-10} | {Amount,10:F2} GEL";
}
