using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

// ავტენტიფიკაციის სერვისი - რეგისტრაცია და შესვლა
public class AuthService : AccountService
{
    public AuthService(FileRepository repo) : base(repo) { }

    // ახალი მომხმარებლის რეგისტრაცია
    public bool Register(string username, string password)
    {
        Users = Repo.LoadUsers();
        if (Find(username) != null) return false; // მომხმარებელი უკვე არსებობს
        Users.Add(new User { Username = username, Password = password, Role = Role.Client, Approved = false });
        Save();
        return true;
    }

    // მომხმარებლის შესვლა - შემოწმება და დაბრუნება
    public User? Login(string username, string password)
    {
        Users = Repo.LoadUsers();
        var user = Find(username);
        if (user == null || user.Password != password || !user.Approved) return null;
        return user;
    }
}
