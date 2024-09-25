using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*

    SgGetFlipDelay


    http://localhost:59590/json/reply/SgGetFlipDelay?format=json
    results: {"Success":true,"Message":"Success","DelayInMinutes":0}

    Used to get the meridian flip delay, in minutes
    The following routes are available for this service: GET /flipdelay   
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    
    To embed the response in a jsonp callback, append ?callback=myCallback
 
 */

namespace SGProTexter.SgProAPI
{
    internal class SgSgGetFlipDelay
    {
        public static SgGetFlipDelayResponse PtrResponse = new SgGetFlipDelayResponse();
        public static SgGetFlipDelayPlaceHolder PtrPlaceHolder = new SgGetFlipDelayPlaceHolder();


        public SgGetFlipDelayResponse GetFlipDelay(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetFlipDelay.GetFlipDelay()::(Caller->" + WhoCalled + ")\n"));
            DoGetFlipDelay(WhoCalled);
            return PtrResponse;
        }

        private void DoGetFlipDelay(string WhoCalled)
        {
            string aWhoCalled = "SgGetFlipDelay.GetFlipDelay().DoGetFlipDelay()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetFlipDelayResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Position: \t" + PtrResponse.DelayInMinutes + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }


        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetFlipDelay.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Position: \t" + PtrResponse.DelayInMinutes + "\n");
        }
    }

    /*
   
    Responses POST 
    All Verbs /flipdelay
    
    POST /json/reply/SgGetFlipDelay HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}

    */
    internal class SgGetFlipDelayPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/flipdelay";
    }

    /*
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"Success":false,"Message":"String","DelayInMinutes":0}

    */
    internal class SgGetFlipDelayResponse : SgBaseResponse
    {
        public int DelayInMinutes { get; set; }
    }
}
