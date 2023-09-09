using LoginComponent.DataBase;
using LoginComponent.Models;
using LoginComponent.Models.Request;
using Microsoft.EntityFrameworkCore;
using LoginComponent.Interface.IRepositories.Auth;

namespace LoginComponent.Repositories.Auth
{
    public class AuthRepositories : IAuthRepositories
    {
        private readonly AplicationContext _loginContext;
        public AuthRepositories(AplicationContext loginContext)
        {
            _loginContext = loginContext;
        }

        public async Task<User> GetDataAsync(Guid id)
        {
            var user = await _loginContext.Users.FindAsync(id);

            return user;
        }

        public async Task<User> GetDataAsync(string email)
        {
            var user = await _loginContext.Users.SingleOrDefaultAsync(
                user => user.Email == email);

            return user;
        }

        public async Task<int> SaveDataAsync(User user)
        {
            await _loginContext.Users.AddAsync(user);

            return await _loginContext.SaveChangesAsync();
        }
    }

}
