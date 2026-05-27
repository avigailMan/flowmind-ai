using FlowMind.Core.Models;

namespace FlowMind.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}