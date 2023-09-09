using LoginComponent.Models;

namespace LoginComponent.Interface.IRepositories.Auth
{
    public interface IAuthRepositories
    {
        Task<User> GetDataAsync(Guid id);
        Task<User> GetDataAsync(string email);
        Task<int> SaveDataAsync(User user);
    }
}
