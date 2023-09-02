using LoginComponent.Models;

namespace LoginComponent.Interface.IRepositories
{
    public interface IUserRepositories
    {
        Task<User> GetDataAsync(Guid id);
        Task<User> GetDataAsync(string email);
        Task<int> SaveDataAsync(User user);
    }
}
