using System;
using RestSharp.Portable;
using Xamarin.Forms;

namespace NetworkingXF.WebManager
{
    public enum RequestAuthenticationType
    {
        Mandatory,
        NonMandatory,
        None
    }

    public class BaseRestRequest : RestRequest
    {
        public BaseRestRequest(string resource) : base(resource)
        {
            //TODO Version/DEVICEID should retrieve from application.
            this.AddParameter("version", "1.0", ParameterType.HttpHeader);
            this.AddParameter("DEVICEID", "1.0", ParameterType.HttpHeader);

            //ar cultureInfo = DependencyService.Get<ILocale>().GetCurrentCultureInfo();
        }

        public RequestAuthenticationType AuthenticationType { get; set; }

        public bool WantCommonHanldingOfException { get; set; } = true;

        public void AddAuthenticationParameter()
        {

        }
    }
}
