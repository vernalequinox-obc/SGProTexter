using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgGetTelescopeIsSlewing


    http://localhost:59590/json/reply/SgGetTelescopeIsSlewing?format=json
    results:  {"Success":false,"Message":"Telescope not connected.","IsSlewing":false}

    Check if the telescope is still slewing.
    The following routes are available for this service: 

        All Verbs /TelescopeIsSlewing   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback


*/


namespace SGProTexter.SgProAPI
{
    internal class SgGetTelescopeIsSlewing
    {

        public SgGetTelescopeIsSlewingResponse PtrResponse = new SgGetTelescopeIsSlewingResponse();
        public SgGetTelescopeIsSlewingPlaceHolder PtrPlaceHolder = new SgGetTelescopeIsSlewingPlaceHolder();

        public SgGetTelescopeIsSlewingResponse GetTelescopeIsSlewing(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetTelescopeIsSlewing.GetTelescopeIsSlewing()::(Caller->" + WhoCalled + ")\n"));
            DoGetTelescopeIsSlewing(WhoCalled);
            return PtrResponse;
        }

        private void DoGetTelescopeIsSlewing(string WhoCalled)
        {
            string aWhoCalled = "SgGetTelescopeIsSlewing.GetTelescopeIsSlewing().DoGetTelescopeIsSlewing()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetTelescopeIsSlewingResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t IsSlewing:   \t" + PtrResponse.IsSlewing + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetTelescopeIsSlewing.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t IsSlewing:   \t" + PtrResponse.IsSlewing + "\n");
        }

    }

    /*

    Responses POST Json {}
    
    All Verbs /TelescopeIsSlewing 

    POST /json/reply/SgGetTelescopeIsSlewing HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}

    */

    internal class SgGetTelescopeIsSlewingPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/guiderinfo";
    }

    /*
    Request GET 

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","IsSlewing":false}

    */

    internal class SgGetTelescopeIsSlewingResponse : SgBaseResponse
    {
        public bool IsSlewing { get; set; }
    }
}
