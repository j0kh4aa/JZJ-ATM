using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

/// <summary>
/// Handles user authentication: registration and login.
/// </summary>
public class AuthService : AccountService
{
    /// <summary>
    /// Initializes the AuthService with the given file repository.
    /// </summary>
    public AuthService(FileRepository repo) : base(repo) { }

    /// <summary>
    /// Registers a new user with the given credentials.
    /// Returns false if the username is already taken.
    /// </summary>
    public bool Register(string username, string password)
    {
        Users = Repo.LoadUsers();
        if (Find(username) != null) return false;
        Users.Add(new User { Username = username, Password = password, Role = Role.Client, Approved = false });
        Save();
        return true;
    }

    /// <summary>
    /// Validates credentials and returns the matching approved user.
    /// Returns null if credentials are wrong or the account is not yet approved.
    /// </summary>
    public User? Login(string username, string password)
    {
        Users = Repo.LoadUsers();
        var user = Find(username);
        if (user == null || user.Password != password || !user.Approved) return null;
        return user;
    }
}