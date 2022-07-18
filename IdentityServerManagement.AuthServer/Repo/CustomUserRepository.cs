using IdentityServerManagement.AuthServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerManagement.AuthServer.Repo
{
    public class CustomUserRepository : ICustomUserRepository
    {
        private readonly CustomerDbContext _customerDbContext;

        public CustomUserRepository(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<CustomUser?> FindByEmail(string email)
        {
            return await _customerDbContext.CustomUsers.FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<CustomUser?> FindById(int id)
        {
            return await _customerDbContext.CustomUsers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Validate(string email, string password)
        {
            return await _customerDbContext.CustomUsers.AnyAsync(c => c.Email.Equals(email) && c.Password.Equals(password));
        }
    }
}
