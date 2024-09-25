using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgSetFlipDelay



    http://localhost:59590/json/reply/SgSetFlipDelay?format=json
    results: {"ResponseStatus":{"ErrorCode":"NotImplementedException","Message":"Could not find method named Get(SgSetFlipDelay) or Any(SgSetFlipDelay) on Service SgApiService"}}

    The following routes are available for this service: 
        POST /flipdelay   

    Parameters: 
    Name            Parameter   Data Type   Required    Description 
    DelayInMinutes  path        number      Yes         The delay, in minutes past the meridan to flip (can be a negative number) 

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

*/

namespace SGProTexter.SgProAPI
{
    internal class SgSetFlipDelay
    {

        SgSetFlipDelayResponse PtrResponse = new SgSetFlipDelayResponse();
        SgSetFlipDelayPlaceHolder PtrPlaceHolder = new SgSetFlipDelayPlaceHolder();
        SgSetFlipDelayPostBody PtrPostBody = new SgSetFlipDelayPostBody();

        public SgSetFlipDelayResponse SetFlipDelay(SgSetFlipDelayPostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgSetFlipDelay.SetFlipDelay()::(Caller->" + WhoCalled + ")\n"));
            DoSetFlipDelay(WhoCalled);
            return PtrResponse;
        }

        private void DoSetFlipDelay(string WhoCalled)
        {
            string aWhoCalled = "SgSetFlipDelay.SetFlipDelay().DoSetFlipDelay()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSetFlipDelayResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t DelayInMinutes:   \t" + PtrResponse.DelayInMinutes + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSetFlipDelay.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t DelayInMinutes:   \t" + PtrResponse.DelayInMinutes + "\n");
        }
    }

    /*
    Responses POST
    
    POST /flipdelay   

    POST /json/reply/SgSetFlipDelay HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"DelayInMinutes":0}

    */

    internal class SgSetFlipDelayPostBody
    {
        public string DelayInMinutes { get; set; }
    }

    internal class SgSetFlipDelayPlaceHolder : SgBasePlaceHolder
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

    internal class SgSetFlipDelayResponse : SgBaseResponse
    {
        public int DelayInMinutes { get; set; }
    }
}