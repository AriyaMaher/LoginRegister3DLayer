using LoginRegister3DLayer.Core.ViewModels;
using LoginRegister3DLayer.Database.Models;

namespace LoginRegister3DLayer.Core.Interface;

public interface IAccount:IDisposable
{
    Task<bool> AddUser(RegisterViewModel register);
    Task<User> LoginUser(LoginViewModel login);
}
