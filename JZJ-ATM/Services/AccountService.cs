using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

/// <summary>
/// Abstract base service providing shared repository access and common user operations.
/// All other services inherit from this class.
/// </summary>
public abstract class AccountService
{
    /// <summary>The file repository used for reading and writing data.</summary>
    protected readonly FileRepository Repo;

    /// <summary>The in-memory list of users loaded from file.</summary>
    protected List<User> Users;

    /// <summary>
    /// Initializes the service and loads users from the repository.
    /// </summary>
    protected AccountService(FileRepository repo)
    {
        Repo = repo;
        Users = repo.LoadUsers();
    }

    /// <summary>
    /// Persists the current user list to the file repository.
    /// </summary>
    protected void Save() => Repo.SaveUsers(Users);

    /// <summary>
    /// Finds and returns a user by username (case-insensitive).
    /// Returns null if not found.
    /// </summary>
    protected User? Find(string username) =>
        Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}