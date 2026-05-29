using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

// ადმინის სერვისი - მომხმარებლების დამტკიცება/უარყოფა
public class AdminService : AccountService
{
    public AdminService(FileRepository repo) : base(repo) { }

    // მომლოდინე მომხმარებლების სია
    public User[] GetPending()
    {
        Users = Repo.LoadUsers();
        return [.. Users.Where(u => u.Role == Role.Client && !u.Approved)];
    }

    // მომხმარებლის დამტკიცება
    public bool Approve(string username)
    {
        Users = Repo.LoadUsers();
        var user = Find(username);
        if (user == null || user.Approved) return false;
        user.Approved = true;
        Save();
        return true;
    }

    // მომხმარებლის უარყოფა და წაშლა
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
