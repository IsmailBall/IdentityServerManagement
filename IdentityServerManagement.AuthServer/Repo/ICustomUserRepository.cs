using IdentityServerManagement.AuthServer.Models;

namespace IdentityServerManagement.AuthServer.Repo
{
    public interface ICustomUserRepository
    {
        Task<bool> Validate(string email, string password);

        Task<CustomUser> FindById(int id);

        Task<CustomUser> FindByEmail(string email);
    }
}
