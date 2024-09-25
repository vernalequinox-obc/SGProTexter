using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetTelescopePosition


    http://localhost:59590/json/reply/SgGetTelescopePosition?format=json
    results:  {"Success":false,"Message":"Telescope not connected.","Ra":0,"Dec":0}

    Get the current location of the telescope.
    The following routes are available for this service: 
        All Verbs /telescopepos   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

*/

namespace SGProTexter.SgProAPI
{
    internal class SgGetTelescopePosition
    {

        public SgGetTelescopePositionResponse PtrResponse = new SgGetTelescopePositionResponse();
        public SgGetTelescopePositionPlaceHolder PtrPlaceHolder = new SgGetTelescopePositionPlaceHolder();


        public SgGetTelescopePositionResponse GetTelescopeMountPosition(string WhoCalled)
        {
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgGetTelescopePosition.GetTelescopeMountPosition()::(Caller->" + WhoCalled + ")\n"));
            DoGetTelescopeMountPosition(WhoCalled);
            return PtrResponse;
        }

        private void DoGetTelescopeMountPosition(string WhoCalled)
        {
            string aWhoCalled = "SgGetTelescopePosition.GetTelescopeMountPosition().DoGetTelescopeMountPosition()::(Caller->" + WhoCalled;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetTelescopePositionResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Ra:    \t" + PtrResponse.Ra + "\n");
                Trace.Write("\t\t\t Dec:   \t" + PtrResponse.Dec + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, WhoCalled + "->SgGetTelescopePosition.ConsolePrint_Response");
            Console.Write("\t\t\t Ra:    \t" + PtrResponse.Ra + "\n");
            Console.Write("\t\t\t Dec:   \t" + PtrResponse.Dec + "\n");
        }
    }

    /*
    Responses POST

    All Verbs /telescopepos 

    POST /json/reply/SgGetTelescopePosition HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}
    */

    internal class SgGetTelescopePositionPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/telescopepos";
    }

    /*
    Request GET 

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Ra":0,"Dec":0}

    */

    internal class SgGetTelescopePositionResponse : SgBaseResponse
    {
        public float Ra { get; set; }
        public float Dec { get; set; }
    }
}
