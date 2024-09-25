using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 
    SgSetCameraTemp


    http://localhost:59590/json/reply/SgSetCameraTemp?format=json
    results: {"Success":false,"Message":"Cannot set temperature because no camera is connected!"}

    
    Set the camera's temperature.
    The following routes are available for this service: 
        All Verbs /setcameratemp/{Temperature}   

    POST /setcameratemp 




*/

namespace SGProTexter.SgProAPI
{
    internal class SgSetCameraTemp
    {
        SgSetCameraTempResponse PtrResponse = new SgSetCameraTempResponse();
        SgSetCameraTempPlaceHolder PtrPlaceHolder = new SgSetCameraTempPlaceHolder();
        SgSetCameraTempPostBody PtrPostBody = new SgSetCameraTempPostBody();

        public SgSetCameraTempResponse SetCameraTemp(SgSetCameraTempPostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            Trace.Write(FileHostData.GetLoggingTime("SgSetCameraTemp.SetCameraTemp()::(Caller->" + WhoCalled + ")\n"));
            DoSetCameraTemp(WhoCalled);
            return PtrResponse;
        }

        private void DoSetCameraTemp(string WhoCalled)
        {
            string aWhoCalled = "SgSetCameraTemp.SetCameraTemp().DoSetCameraTemp()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSetCameraTempResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSetCameraTemp.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /*
    Responses POST 

    All Verbs /setcameratemp/{Temperature} 

    POST /json/reply/SgSetCameraTemp HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length
    
    {"Temperature":0}
    
    */

    internal class SgSetCameraTempPostBody
    {
        public string Temperature { get; set; }
    }

    internal class SgSetCameraTempPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "setcameratemp/";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */

    internal class SgSetCameraTempResponse : SgBaseResponse
    {

    }
}
