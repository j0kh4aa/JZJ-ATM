namespace JZJ_ATM.Models;

/// <summary>
/// Represents a registered ATM user with credentials, balance, role, and approval status.
/// </summary>
public class User
{
    /// <summary>Gets or sets the unique username.</summary>
    public string Username { get; set; } = "";

    /// <summary>Gets or sets the user's password.</summary>
    public string Password { get; set; } = "";

    /// <summary>Gets or sets the account balance in GEL.</summary>
    public decimal Balance { get; set; } = 0;

    /// <summary>Gets or sets the user's role: Client or Admin.</summary>
    public Role Role { get; set; } = Role.Client;

    /// <summary>Gets or sets whether the account has been approved by an admin.</summary>
    public bool Approved { get; set; } = false;

    /// <summary>
    /// Serializes the user into a pipe-delimited string for file storage.
    /// </summary>
    public string Serialize() =>
        $"{Username}|{Password}|{Balance}|{Role}|{Approved}";

    /// <summary>
    /// Deserializes a pipe-delimited string from file into a User object.
    /// </summary>
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