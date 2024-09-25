using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgSetFocuserPosition

    http://localhost:59590/json/reply/SgSetFocuserPosition?format=json
    results: {"Success":true,"Message":"Setting focuser to position 0..."}

    Set the absolute position of the focuser. This call is asynchronous. To check status, you can either 
    call "/devicestatus/Focuser" or periodically check position with "/focuserpos". If this call is successful, 
    it means that the focuser successfully received the message, not that the change is position was successful.

    The following routes are available for this service: POST /setfocuserpos   
        All Verbs /setfocuserpos/{Position}   

    Parameters: 
    Name        Parameter   Data Type   equired     Description 
    Position    path        int         Yes         The focuser position to set (absolute steps). 

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback


*/



namespace SGProTexter.SgProAPI
{
    internal class SgSetFocuserPosition
    {

        private SgSetFocuserPositionResponse PtrResponse = new SgSetFocuserPositionResponse();
        private SgSetFocuserPositionPlaceHolder PtrPlaceHolder = new SgSetFocuserPositionPlaceHolder();
        private SgSetFocuserPositionPostBody PtrPostBody = new SgSetFocuserPositionPostBody();


        public SgSetFocuserPositionResponse SetFocuserPosition(SgSetFocuserPositionPostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgSetFocuserPosition.SetFocuserPosition()::(Caller->" + WhoCalled + ")\n"));
            DoSetFocuserPosition(WhoCalled);
            return PtrResponse;
        }

        private void DoSetFocuserPosition(string WhoCalled)
        {
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                string aWhoCalled = "SgSetFocuserPosition.SetFocuserPosition().DoSetFocuserPosition::(Caller->" + WhoCalled + ")";
                PtrResponse = JsonConvert.DeserializeObject<SgSetFocuserPositionResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Position:   \t" + PtrResponse.Position + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, WhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSetFocuserPosition.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Position:   \t" + PtrResponse.Position + "\n");
        }
    }

    /*
    Responses POST

    All Verbs /setfocuserpos/{Position} 

    POST /json/reply/SgSetFocuserPosition HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Position":0}

    */

    internal class SgSetFocuserPositionPostBody
    {
        public string Position { get; set; }
    }

    internal class SgSetFocuserPositionPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/setfocuserpos";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */
    internal class SgSetFocuserPositionResponse : SgBaseResponse
    {
        public int Position { get; set; }
    }
}