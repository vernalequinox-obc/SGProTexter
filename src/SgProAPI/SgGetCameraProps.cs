using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetCameraProps

    http://localhost:59590/json/reply/SgGetCameraProps?format=json
    results: {"Success":false,"Message":"Camera not connected.","NumPixelsX":0,"NumPixelsY":0,"SupportsSubframe":false}
   
    Used to get properties for the currently connected camera
    The following routes are available for this service: All Verbs /cameraprops   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback
*/


namespace SGProTexter.SgProAPI
{
    internal class SgGetCameraProps
    {
        public SgGetCameraPropsResponse PtrResponse = new SgGetCameraPropsResponse();
        public SgGetCameraPropsPlaceHolder PtrPlaceHolder = new SgGetCameraPropsPlaceHolder();

        public SgGetCameraPropsResponse GetCameraProps(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetCameraProps.GetCameraProps()::(Caller->" + WhoCalled + ")\n"));
            DoGetCameraProps("GetFocuser()");
            return PtrResponse;
        }

        void DoGetCameraProps(string WhoCalled)
        {
            string aWhoCalled = "SgGetCameraProps.GetCameraProps().DoGetCameraProps()::(Caller->" + WhoCalled + ")";
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetCameraPropsResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetCameraProps.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }

    }

    /* 
    Responses POST 
    All Verbs /cameraprops 
    POST /json/reply/SgGetCameraProps HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}

    */
    internal class SgGetCameraPropsPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/cameraprops";
    }

    /*
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","NumPixelsX":0,"NumPixelsY":0,"SupportsSubframe":false,"IsoValues":["String"],"GainValues":["String"]}

    */

    internal class SgGetCameraPropsResponse : SgBaseResponse
    {

    }
}
