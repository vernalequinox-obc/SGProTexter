using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgGetGuiderInfo

    http://localhost:59590/json/reply/SgGetGuiderInfo?format=json
    results: {"IsConnected":false,"HasShutter":false,"xSize":0,"ySize":0}


    Retrieve if the guider has a shutter
    The following routes are available for this service: All Verbs /guiderinfo   
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    
    To embed the response in a jsonp callback, append ?callback=myCallback
    
    http://localhost:59590/guiderinfo?format=json

*/

namespace SGProTexter.SgProAPI
{
    internal class SgGetGuiderInfo
    {

        public static SgGetGuiderInfoResponse PtrResponse = new SgGetGuiderInfoResponse();
        public static SgGetGuiderInfoPlaceHolder PtrPlaceHolder = new SgGetGuiderInfoPlaceHolder();

        public SgGetGuiderInfoResponse GetGuiderInfo(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetGuiderInfo.GetGuiderInfo()::(Caller->" + WhoCalled + ")\n"));
            DoGetGuiderInfo(WhoCalled);
            return PtrResponse;
        }

        private void DoGetGuiderInfo(string WhoCalled)
        {
            string aWhoCalled = "SgGetGuiderInfo.GetGuiderInfo().DoGetGuiderInfo()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetGuiderInfoResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t HasShutter:  \t" + PtrResponse.HasShutter + "\n");
                Trace.Write("\t\t\t IsConnected: \t" + PtrResponse.IsConnected + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetGuiderInfo.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t HasShutter:  \t" + PtrResponse.HasShutter + "\n");
            Console.Write("\t\t\t IsConnected: \t" + PtrResponse.IsConnected + "\n");
        }
    }

    /*
    Responses POST 
    
    All Verbs /guiderinfo 

    POST /json/reply/SgGetGuiderInfo HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length
    
    {}

 
     */

    internal class SgGetGuiderInfoPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/guiderinfo";
    }

    /*
    Request GET 
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"IsConnected":false,"HasShutter":false,"xSize":0,"ySize":0}

    Example: Enter into browser     
    http://localhost:59590/guiderinfo?format=json
    Results: {"IsConnected":false,"HasShutter":false,"xSize":0,"ySize":0}


    */
    internal class SgGetGuiderInfoResponse : SgBaseResponse
    {
        public bool HasShutter { get; set; }
        public bool IsConnected { get; set; }
    }
}
