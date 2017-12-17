using System;
using System.Net.Http;
using ModernHttpClient;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient.Impl;

namespace NetworkingXF.WebManager
{
    public class ModernHttpClientFactory : DefaultHttpClientFactory
    {
        protected override HttpMessageHandler CreateMessageHandler(IRestClient client)
        {
            return new NativeMessageHandler();
        }
    }
}
