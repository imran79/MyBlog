using System.Threading.Tasks;
using Common.Models;
namespace ApplicationShell.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<System.Net.Http.HttpResponseMessage> Register(RegisterModel registerModel);
    }
}