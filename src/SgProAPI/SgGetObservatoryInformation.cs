using Newtonsoft.Json;
using RestSharp;
using SGProTexter.SgProAPI;
using System;
using System.Diagnostics;

/*
    
    SgGetObservatoryInformation

    http://localhost:59590/json/reply/SgGetObservatoryInformation?format=json
    results:{"Success":false,"Message":"Unable to get observatory information, observatory is not connected.","Azimuth":0,"IsMoving":false,"IsHome":false,"OpenState":"Unknown"}


    Get the current state of the observatory
    The following routes are available for this service: All Verbs /observatory   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

    http://localhost:59590/observatory?format=json

 */

namespace SGProTexter
{
    internal class SgGetObservatoryInformation
    {
        public SgGetObservatoryInformationResponse PtrResponse = new SgGetObservatoryInformationResponse();
        public SgGetObservatoryInformationPlaceHolder PtrPlaceHolder = new SgGetObservatoryInformationPlaceHolder();

        public SgGetObservatoryInformationResponse GetObservatoryInformation(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetObservatoryInformation.GetObservatoryInformation()::(Caller->" + WhoCalled + ")\n"));
            DoGetObservatoryInformation(WhoCalled);
            return PtrResponse;
        }

        private void DoGetObservatoryInformation(string WhoCalled)
        {
            string aWhoCalled = "SgGetObservatoryInformation.GetObservatoryInformation().DoGetObservatoryInformation()::(Caller->" + WhoCalled + ")";
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetObservatoryInformationResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Azimuth:   \t" + PtrResponse.Azimuth + "\n");
                Trace.Write("\t\t\t IsMoving:  \t" + PtrResponse.IsMoving + "\n");
                Trace.Write("\t\t\t IsHome:    \t" + PtrResponse.IsHome + "\n");
                Trace.Write("\t\t\t OpenState: \t" + PtrResponse.OpenState + "\n");
            }
            else
            {
            Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetObservatoryInformation.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Azimuth:   \t" + PtrResponse.Azimuth + "\n");
            Console.Write("\t\t\t IsMoving:  \t" + PtrResponse.IsMoving + "\n");
            Console.Write("\t\t\t IsHome:    \t" + PtrResponse.IsHome + "\n");
            Console.Write("\t\t\t OpenState: \t" + PtrResponse.OpenState + "\n");

        }
    }



    /*
    Responses POST
    All Verbs /observatory 

    POST /json/reply/SgGetObservatoryInformation HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}

    */
    internal class SgGetObservatoryInformationPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/observatory";
    }

    /* Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Azimuth":0,"IsMoving":false,"IsHome":false,"OpenState":"Unknown"}

    */
    internal class SgGetObservatoryInformationResponse : SgBaseResponse
    {
        public float Azimuth { get; set; }
        public bool IsMoving { get; set; }
        public bool IsHome { get; set; }
        public string OpenState { get; set; }
    }
}
