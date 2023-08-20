using LoginRegister3DLayer.Core.Interface;
using LoginRegister3DLayer.Core.ViewModels;
using LoginRegister3DLayer.Database.Classes;
using LoginRegister3DLayer.Database.Context;
using LoginRegister3DLayer.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister3DLayer.Core.Services;

public class AccountService : IAccount
{
    private readonly DatabaseContext _context;
    public AccountService(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    public async Task<bool> AddUser(RegisterViewModel register)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Mobile == register.Mobile);
            if (user != null)
            {
                return false;
            }
            if (register.Password!=register.RePassword)
            {
                return false;
            }

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                RoleId = _context.Roles.SingleOrDefault(n => n.RoleName == "user").Id,
                Mobile = register.Mobile,
                Password = await new Security().HashPassword(register.Password),
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Dispose()
    {
        if (_context != null) 
            _context.Dispose();
    }

    public async Task<User> LoginUser(LoginViewModel login)
    {
        try
        {
            var hashPassword = await new Security().HashPassword(login.Password);
            var user = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Mobile == login.Mobile
                                                                                    && u.Password == hashPassword);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

}
