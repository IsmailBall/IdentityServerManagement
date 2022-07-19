using IdentityModel;
using IdentityServer4.Validation;
using IdentityServerManagement.AuthServer.Repo;

namespace IdentityServerManagement.AuthServer.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private readonly ICustomUserRepository _customUserRepository;

        public ResourceOwnerPasswordValidator(ICustomUserRepository customUserRepository)
        {
            _customUserRepository = customUserRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var isExist = await _customUserRepository.Validate(context.UserName, context.Password);

            if (isExist)
            {
                var user = await _customUserRepository.FindByEmail(context.UserName);

                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
