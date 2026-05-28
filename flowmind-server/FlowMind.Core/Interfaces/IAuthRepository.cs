using FlowMind.Core.Models;

namespace FlowMind.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);

        Task<User?> Login(string email, string password);

        Task<bool> UserExists(string email);

        Task<User?> GetUserByEmail(string email);

        Task<User> RegisterGoogleUser(User user);
    }
}