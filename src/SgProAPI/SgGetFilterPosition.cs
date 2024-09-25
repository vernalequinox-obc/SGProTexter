using Newtonsoft.Json;
using RestSharp;
using SGProTexter.SgProAPI;
using System;
using System.Diagnostics;

/*
    SgGetFilterPosition


    http://localhost:59590/json/reply/SgGetFilterPosition?format=json
    results: {"Success":false,"Message":"Filter wheel not connected.","Position":0}

    Get the current position of the filter wheel. Filter positions are "1" based.
    The following routes are available for this service: 

        All Verbs /filterwheelpos   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback



*/


namespace SGProTexter
{
    internal class SgGetFilterPosition
    {
        static public SgGetFilterPositionResponse PtrResponse = new SgGetFilterPositionResponse();
        static public SgGetFilterPositionPlaceHolder PtrPlaceHolder = new SgGetFilterPositionPlaceHolder();

        static public SgGetFilterPositionResponse GetFilterWheelPosition(string WhoCalled)
        {
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgGetFilterPosition.GetFilterWheelPosition()::(Caller->" + WhoCalled + ")\n"));
            DoGetFilterWheelPosition(WhoCalled);
            return PtrResponse;
        }

        static private void DoGetFilterWheelPosition(string WhoCalled)
        {

            string aWhoCalled = "SgGetFilterPosition.GetFilterWheelPosition().DoGetFilterWheelPosition()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetFilterPositionResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Position: \t" + PtrResponse.Position + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        static public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgAbortImage.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Position: \t" + PtrResponse.Position + "\n");
        }
    }

    /*
    Responses POST
    All Verbs /filterwheelpos 
    POST /json/reply/SgGetFilterPosition HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length
    
    {}

    */
    internal class SgGetFilterPositionPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/filterwheelpos";
    }

    /*
    Request GET 
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"Success":false,"Message":"String","Position":0}

    */

    internal class SgGetFilterPositionResponse : SgBaseResponse
    {
        public int Position { get; set; }
    }

}
