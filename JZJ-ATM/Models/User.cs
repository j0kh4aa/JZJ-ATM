namespace JZJ_ATM.Models;

// მომხმარებლის მოდელი
public class User
{
    // მომხმარებლის მონაცემები
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public decimal Balance { get; set; } = 0;
    public Role Role { get; set; } = Role.Client;
    public bool Approved { get; set; } = false;

    // მომხმარებლის მონაცემების ფაილში ჩაწერა
    public string Serialize() =>
        $"{Username}|{Password}|{Balance}|{Role}|{Approved}";

    // ფაილიდან წაკითხული ხაზის მომხმარებლად გარდაქმნა
    public static User Deserialize(string line)
    {
        var p = line.Split('|');
        return new User
        {
            Username = p[0],
            Password = p[1],
            Balance = decimal.Parse(p[2]),
            Role = Enum.Parse<Role>(p[3]),
            Approved = bool.Parse(p[4])
        };
    }
}