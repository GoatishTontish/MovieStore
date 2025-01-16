using MovieStore.Models.DTO;

namespace MovieStore.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegisterationModel model);
    }
}
