using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgSendGuidePulse

    http://localhost:59590/json/reply/SgSendGuidePulse?format=json
    results:  NO RESPONSE

    Used to send guide pulses via the camera relays.
    The following routes are available for this service: 
        All Verbs /guiderguidepulse   
    
    Parameters: 
    Name        Parameter   Data Type                   Required    Description 
    Direction   path        SgGuideCommandDirection     Yes         The direction of the guide correction. North, South, East West 
    Duration    path        int                         Yes         The duration (in milliseconds) of the guide pulse. 


*/

namespace SGProTexter.SgProAPI
{
    internal class SgSendGuidePulse
    {

        SgSendGuidePulseResponse PtrResponse = new SgSendGuidePulseResponse();
        SgSendGuidePulsePlaceHolder PtrPlaceHolder = new SgSendGuidePulsePlaceHolder();
        SgSendGuidePulsePostBody PtrPostBody = new SgSendGuidePulsePostBody();

        public SgSendGuidePulseResponse SendGuidePulse(SgSendGuidePulsePostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            Trace.Write(FileHostData.GetLoggingTime("SgSendGuidePulse.SendGuidePulse()::(Caller->" + WhoCalled + ")\n"));
            DoSendGuidePulse(WhoCalled);
            return PtrResponse;
        }

        private void DoSendGuidePulse(string WhoCalled)
        {
            string aWhoCalled = "SgSendGuidePulse.SendGuidePulse().DoSendGuidePulse()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSendGuidePulseResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSendGuidePulse.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /*
    
    Responses POST

    All Verbs /guiderguidepulse 
    
    POST /json/oneway/SgSendGuidePulse HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Direction":"North","Duration":0}

    */

    internal class SgSendGuidePulsePostBody
    {
        public string Direction { get; set; }
        public string Duration { get; set; }
    }

    internal class SgSendGuidePulsePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "guiderinfo/";
    }

    /*
 
    Request GET 
    NO RESPONSE

    */

    internal class SgSendGuidePulseResponse : SgBaseResponse
    {

    }
}

