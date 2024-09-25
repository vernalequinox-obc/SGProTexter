using Newtonsoft.Json;
using RestSharp;
using SGProTexter.SgProAPI;
using System;
using System.Diagnostics;

/*
    SgGetCameraTemp

    http://localhost:59590/json/reply/SgGetCameraTemp?format=json
    results: {"Success":false,"Message":"Cannot get temperature because no camera is connected!","Temperature":0}

    Retrieve the camera's temperature.
    The following routes are available for this service: All Verbs /cameratemp   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback
*/

namespace SGProTexter
{
    internal class SgGetCameraTemp
    {
        public SgGetCameraTempResponse PtrResponse = new SgGetCameraTempResponse();
        public SgGetCameraTempPlaceHolder PtrPlaceHolder = new SgGetCameraTempPlaceHolder();

        public SgGetCameraTempResponse GetCameraTemp(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetCameraTemp.GetCameraTemp()::(Caller->" + WhoCalled + ")\n"));
            DoGetCameraTemp(WhoCalled);
            return PtrResponse;
        }

        private void DoGetCameraTemp(string WhoCalled)
        {
            string aWhoCalled = "SgGetCameraTemp.GetCameraTemp().DoGetCameraTemp()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetCameraTempResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Position: \t" + PtrResponse.Temperature + "\n");
            }
            else
            {
                PtrResponse.Success = false;
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }

        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetCameraTemp.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Position: \t" + PtrResponse.Temperature + "\n");
        }
    }



    /*  
    Responses POST Json {}
    All Verbs /cameratemp   

    POST /json/reply/SgGetCameraTemp HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}
    */

    internal class SgGetCameraTempPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/cameratemp";
    }

    /*
    Request GET 
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Temperature":0}

    */

    internal class SgGetCameraTempResponse : SgBaseResponse
    {
        public float Temperature { get; set; }
    }

}
