using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

/// <summary>
/// Handles admin operations: viewing, approving, and rejecting pending user registrations.
/// </summary>
public class AdminService : AccountService
{
    /// <summary>
    /// Initializes the AdminService with the given file repository.
    /// </summary>
    public AdminService(FileRepository repo) : base(repo) { }

    /// <summary>
    /// Returns all client accounts that are awaiting admin approval.
    /// </summary>
    public User[] GetPending()
    {
        Users = Repo.LoadUsers();
        return [.. Users.Where(u => u.Role == Role.Client && !u.Approved)];
    }

    /// <summary>
    /// Approves the specified user account. Returns false if not found or already approved.
    /// </summary>
    public bool Approve(string username)
    {
        Users = Repo.LoadUsers();
        var user = Find(username);
        if (user == null || user.Approved) return false;
        user.Approved = true;
        Save();
        return true;
    }

    /// <summary>
    /// Rejects and removes the specified user account. Returns false if not found or already approved.
    /// </summary>
    public bool Reject(string username)
    {
        Users = Repo.LoadUsers();
        var user = Find(username);
        if (user == null || user.Approved) return false;
        Users.Remove(user);
        Save();
        return true;
    }
}