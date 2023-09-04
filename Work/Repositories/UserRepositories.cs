using LoginComponent.Interface.IRepositories;
using LoginComponent.DataBase;
using LoginComponent.Models;
using LoginComponent.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.Repositories
{
    public class UserRepositories : IUserRepositories 
    {
        private readonly AplicationContext _loginContext;
        public UserRepositories(AplicationContext loginContext)
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
