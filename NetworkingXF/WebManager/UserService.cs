using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkingXF.Models;
using NetworkingXF.WebManager;
using Prism.Services;
using RestSharp.Portable;

namespace NetworkingXF.WebManager
{
    public class UserService : BaseService, IUserService
    {
        readonly IWebClient _webClient;
        readonly IPageDialogService _dialogService;

        public UserService(IWebClient webClient, IPageDialogService dialogService)
        {
            _webClient = webClient;
            _dialogService = dialogService;
        }

        public Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetUserProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            var request = new BaseRestRequest("?action=tba_login_api_v1");
            request.AuthenticationType = RequestAuthenticationType.None;
            request.AddJsonBody(new { email = username, password = password });

            //var response = _webClient.ExecutePost<LoginResponse>(request);

            return null;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetPostsAsync()
        {
            try
            {
                var request = new BaseRestRequest("posts1");
                request.AuthenticationType = RequestAuthenticationType.None;
                request.WantCommonHanldingOfException = false;
                var posts = await _webClient.ExecuteGet<List<RootObject>>(request);
            }
            catch (ApiErrorException ex)
            {
                await _dialogService.DisplayAlertAsync("Error", ex.ReasonPhrase, "Okay");
            }
            catch (Exception ex)
            {

            }

        }
    }
}
