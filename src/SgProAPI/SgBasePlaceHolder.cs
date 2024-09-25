using RestSharp;
using SGProTexter.SgProAPI;

namespace SGProTexter
{
    internal class SgBasePlaceHolder
    {
        public string RequestResourceString { get; set; }
        public IRestClient client = new RestClient();

        public RestRequest GetPlaceHolder(string PtrSbVerbPath)
        {
            // IRestRequest request = new(Method.GET);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.Resource = FileHostData.SGproURL + PtrSbVerbPath;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            return request;
        }

        public RestRequest PostPlaceHolder(string PtrSbVerbPath, object AddBody)
        {
            // RestRequest request = new(Method.POST);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.Resource = FileHostData.SGproURL + PtrSbVerbPath;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(AddBody);
            return request;
        }

    }
}
