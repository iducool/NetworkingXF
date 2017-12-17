using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using ModernHttpClient;
using System.Text;
using System.Net.Http;
using NetworkingXF.WebManager;
using Prism.Services;

namespace NetworkingXF.WebManager
{
    public class WebClient : IWebClient
    {
        const string localServer = "https://jsonplaceholder.typicode.com/";

        const string BaseWebServiceURL = localServer;
        readonly RestClient _restClient;
        readonly IPageDialogService _dialogService;

        public WebClient(IPageDialogService dialogService)
        {
            _restClient = new RestClient(BaseWebServiceURL);
            _restClient.HttpClientFactory = new ModernHttpClientFactory();
            _restClient.IgnoreResponseStatusCode = true;
            _dialogService = dialogService;
        }

        public async Task<T> ExecuteGet<T>(BaseRestRequest request) where T : new()
        {
            //PrintRequest(request);
            /*
            try
            {
                var response = await _restClient.Execute(request);
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (HttpRequestException ex)
            {
                if (request.WantCommonHanldingOfException)
                {
                    Debug.WriteLine(ex.HResult);
                    Debug.WriteLine(ex.Message);
                }
                throw ex;
            }
            catch (WebException ex)
            {
                if (request.WantCommonHanldingOfException)
                {
                    Debug.WriteLine(ex.Message);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (request.WantCommonHanldingOfException)
                {
                    Debug.WriteLine(ex.Message);
                }
                throw ex;
            }
            */

            request.AddAuthenticationParameter();
            var response = await _restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Debug.WriteLine("Status code:{0} and Description:{1}", response.StatusCode, response.StatusDescription);
                if (request.WantCommonHanldingOfException)
                {
                    if (await TryToRefreshTokenIfPossible())
                    {
                        //await ExecuteGet<T>(request);
                    }
                }
                else
                {
                    throw new ApiErrorException(response.StatusCode, response.StatusDescription);
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound ||
                     response.StatusCode == HttpStatusCode.BadRequest)
            {
                Debug.WriteLine("Status code:{0} and Description:{1}", (int)response.StatusCode, response.StatusDescription);
                if (request.WantCommonHanldingOfException)
                {
                    //TODO: display alert if required.
                    await _dialogService.DisplayAlertAsync("Error", response.StatusDescription, "Okay");
                }
                else
                {
                    throw new ApiErrorException(response.StatusCode, response.StatusDescription);
                }
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError ||
                    response.StatusCode == HttpStatusCode.ServiceUnavailable ||
                     response.StatusCode == HttpStatusCode.BadGateway)
            {
                Debug.WriteLine("Status code:{0} and Description:{1}", response.StatusCode, response.StatusDescription);
                if (request.WantCommonHanldingOfException)
                {
                    //TODO: display alert if required.
                    Debug.WriteLine("Status code:{0} and Description:{1}", response.StatusCode, response.StatusDescription);
                    await _dialogService.DisplayAlertAsync("Error", response.StatusDescription, "Okay");
                }
                else
                {
                    throw new ApiErrorException(response.StatusCode, response.StatusDescription);
                }
            }

            return default(T);
        }

        public async Task<T> ExecutePost<T>(BaseRestRequest request, bool isRequestRepeated = false) where T : class, new()
        {
            request.Method = Method.POST;

            try
            {
                //App.IsConnectedToInternet(true);
                IRestResponse response = await _restClient.Execute(request);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                Debug.WriteLine("Service Ended:{0}", request.Resource);
            }

            return null;
        }

        void PrintRequest(RestRequest request)
        {
            var sb = new StringBuilder();
            foreach (var param in request.Parameters)
            {
                sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
            }
            Debug.WriteLine("request: " + sb);
        }

        async Task<bool> TryToRefreshTokenIfPossible()
        {
            try
            {
                /*
                var userDetail = RSCommonUtility.DeserializedModelFromDisk<User>(Constants.CachedLoginViewModelPath());
                string UserName = CrossSecureStorage.Current.GetValue(Constants.UserNameKey);
                string Password = CrossSecureStorage.Current.GetValue(Constants.PasswordKey);
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    var user = new User();
                    user.Email = UserName;
                    user.Password = Password;
                    var request = new RestRequest("login");
                    var client = new BaseClient();
                    if (!String.IsNullOrEmpty(userDetail.FacebookUId))
                    {
                        request = new RestRequest("loginbysocialmedia");
                        request.AddParameter("facebook_uid", userDetail.FacebookUId);
                    }
                    else
                    {
                        request.AddParameter("password", Password);
                    }
                    request.AddParameter("email", UserName);
                    RootUserObject baseObj = await client.ExecutePost<RootUserObject>(request, false);
                    if (baseObj != null && baseObj.user != null)
                    {
                        App.User = baseObj.user;
                        CrossSecureStorage.Current.SetValue(Constants.UserNameKey, baseObj.user.Email);
                        if (!String.IsNullOrEmpty(Password))
                        {
                            CrossSecureStorage.Current.SetValue(Constants.PasswordKey, Password);
                        }
                        CrossSecureStorage.Current.SetValue(Constants.Token, baseObj.user.Token);
                        RSCommonUtility.SerializeModelOnDisk(baseObj.user, Constants.CachedLoginViewModelPath());
                        return true;
                    }
                }
                */
            }
            catch (Exception ex)
            {

            }
            return false;
        }


    }
}

