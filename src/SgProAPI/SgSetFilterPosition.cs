using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgSetFilterPosition


    http://localhost:59590/json/reply/SgSetFilterPosition?format=json
    results: {"Success":false,"Message":"Filter wheel not connected."}

    Set the position of the filter wheel. This call is asynchronous. To check status, you can either 
    call "/devicestatus/FilterWheel" or periodically check position with "/filterwheelpos". If this call is successful, 
    it means that the filter wheel successfully received the message, not that the change is position was successful.

    The following routes are available for this service: 
        All Verbs /setfilterwheelpos/{Position}   

    POST /setfilterwheelpos   
    Parameters: 
    Name        Parameter   Data Type   Required    Description 
    Position    path        int         Yes         The filter position to set (filter positions are "1" based). 

    
*/

namespace SGProTexter.SgProAPI
{
    internal class SgSetFilterPosition
    {

        SgSetFilterPositionResponse PtrResponse = new SgSetFilterPositionResponse();
        SgSetFilterPositionPlaceHolder PtrPlaceHolder = new SgSetFilterPositionPlaceHolder();
        SgSetFilterPositionPostBody PtrPostBody = new SgSetFilterPositionPostBody();


        public SgSetFilterPositionResponse SetFilterPosition(SgSetFilterPositionPostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgSetFilterPosition.SetFilterPosition()::(Caller->" + WhoCalled + ")\n"));
            DoSetFilterPosition(WhoCalled);
            return PtrResponse;
        }

        private void DoSetFilterPosition(string WhoCalled)
        {
            string aWhoCalled = "SgSetFilterPosition.SetFilterPosition().DoSetFilterPosition()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSetFilterPositionResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Position:   \t" + PtrResponse.Position + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSetFilterPosition.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Position:   \t" + PtrResponse.Position + "\n");
        }
    }

    /*
    Responses POST

    All Verbs /setfilterwheelpos/{Position} 


    POST /json/reply/SgSetFilterPosition HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Position":0}

    */

    internal class SgSetFilterPositionPostBody
    {
        public string Position { get; set; }
    }

    internal class SgSetFilterPositionPlaceHolder : SgBasePlaceHolder
    {

        public string SbVerbPath = "/setfilterwheelpos";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}
    
    */
    internal class SgSetFilterPositionResponse : SgBaseResponse
    {
        public int Position { get; set; }
    }
}

