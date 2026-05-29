namespace JZJ_ATM.Models;

// სესხის მოთხოვნის მოდელი
public class LoanRequest
{
    // სესხის მონაცემები
    public string Username { get; set; } = "";
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending / Approved / Rejected

    // სესხის ფაილში ჩაწერა
    public string Serialize() => $"{Username}|{Amount}|{Status}";

    // ფაილიდან სესხის წაკითხვა
    public static LoanRequest Deserialize(string line)
    {
        var p = line.Split('|');
        return new LoanRequest
        {
            Username = p[0],
            Amount = decimal.Parse(p[1]),
            Status = p[2]
        };
    }
}