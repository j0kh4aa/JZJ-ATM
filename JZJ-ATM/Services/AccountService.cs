using JZJ_ATM.Data;
using JZJ_ATM.Models;

namespace JZJ_ATM.Services;

// საბაზო სერვისი - სხვა სერვისები მემკვიდრეობით იღებენ
public abstract class AccountService
{
    protected readonly FileRepository Repo;
    protected List<User> Users;

    protected AccountService(FileRepository repo)
    {
        Repo = repo;
        Users = repo.LoadUsers();
    }

    // მომხმარებლების ფაილში შენახვა
    protected void Save() => Repo.SaveUsers(Users);

    // მომხმარებლის პოვნა სახელით
    protected User? Find(string username) =>
        Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}