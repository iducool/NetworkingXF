using System;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace NetworkingXF.WebManager
{
    public interface IWebClient
    {
        Task<T> ExecuteGet<T>(BaseRestRequest request) where T : new();
        Task<T> ExecutePost<T>(BaseRestRequest request, bool isRequestRepeated = false) where T : class, new();
    }
}
