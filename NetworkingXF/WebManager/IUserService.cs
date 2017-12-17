using System;
using System.Threading.Tasks;

namespace NetworkingXF.WebManager
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> GetUserProfileAsync();
        Task<bool> ChangePassword(string oldPassword, string newPassword);
        Task LogoutAsync();
        Task GetPostsAsync();
    }
}
