using FlowMind.Core.Models;

namespace FlowMind.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> Register(ApplicationUser user, string password);

        Task<ApplicationUser?> Login(string email, string password);

        Task<bool> UserExists(string email);
    }
}