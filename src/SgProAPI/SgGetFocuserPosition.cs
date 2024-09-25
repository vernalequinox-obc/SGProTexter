using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetFocuserPosition

    http://localhost:59590/json/reply/SgGetFocuserPosition?format=json
    results: {"Success":false,"Message":"Focuser not connected.","Position":0}

    Get the position of the focuser.
    The following routes are available for this service: All Verbs /focuserpos   
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    
    To embed the response in a jsonp callback, append ?callback=myCallback
    
*/

namespace SGProTexter.SgProAPI
{
    internal class SgGetFocuserPosition
    {

        static public SgGetFocuserPositionResponse PtrResponse = new SgGetFocuserPositionResponse();
        static public SgGetFocuserPositionPlaceHolder PtrPlaceHolder = new SgGetFocuserPositionPlaceHolder();

        static public SgGetFocuserPositionResponse GetFocuserPosition(string WhoCalled)
        {
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgGetFocuserPosition.GetFocuserPosition()::(Caller->" + WhoCalled + ")\n"));
            DoGetFocuserPosition(WhoCalled);
            return PtrResponse;
        }

        static private void DoGetFocuserPosition(string WhoCalled)
        {
            string aWhoCalled = "SgGetFocuserPosition.GetFocuserPosition().DoGetFocuserPosition()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetFocuserPositionResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Position: \t" + PtrResponse.Position + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgAbortImage.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Position: \t" + PtrResponse.Position + "\n");
        }
    }

    /*
    Responses POST 

    All Verbs /focuserpos
    POST /json/reply/SgGetFocuserPosition HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length
    
    {}
    
    */

    internal class SgGetFocuserPositionPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/focuserpos";
    }

    /*
     Request GET 

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Position":0}

    */
    internal class SgGetFocuserPositionResponse : SgBaseResponse
    {
        public int Position { get; set; }
    }
}
