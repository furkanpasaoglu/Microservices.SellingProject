using IdentityServer.Application.Models;
using System.Threading.Tasks;

namespace IdentityServer.Application.Services
{
    public interface IIdentityService
    {
        Task<LoginResponseModel> Login(LoginRequestModel requestModel);
    }
}
