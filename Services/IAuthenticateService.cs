using RetoBackendApp.Models;

namespace RetoBackendApp.Services
{
    public interface IAuthenticationService
    {
        User Authenticate(string userName, string password);
    }
}
