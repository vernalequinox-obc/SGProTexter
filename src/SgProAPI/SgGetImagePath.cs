using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;

/*
    SgGetImagePath

    http://localhost:59590/json/reply/SgGetImagePath?format=json
    results: {"Success":false,"Message":"No image found for receipt: 00000000-0000-0000-0000-000000000000"}


    Query SGPro for the full path on an image. Starting an exposure will return a GUID. Use this GUID to ask SGPro for the path to your image. 
    If the response is true it means SGPro is done processing the requested image and the "Message" property will contain either a path to your image (on success) or, 
    in the case of failure to capture, "error" or "abort".
    The following routes are available for this service: All Verbs /imagepath/{Receipt}   
    Parameters: Name Parameter Data Type Required Description 
    Receipt path string Yes The receipt (GUID) returned from the "/image" (SgCaptureImage) call. The response will contain a "Success" value of True 
    if the image you requested is complete (in this case success indicates that SGPro is done processing the image request, not necessarily that 
    there is a valid image ready and waiting). If the image is not yet ready, "Success" will contain a value of False. 

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

 
 */

namespace SGProTexter.SgProAPI
{
    internal class SgGetImagePath
    {

        public static SgGetImagePathResponse PtrResponse = new SgGetImagePathResponse();
        public static SgGetImagePathPlaceHolder PtrPlaceHolder = new SgGetImagePathPlaceHolder();

        public SgGetImagePathResponse GetImagePath(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetImagePath.GetImagePath()::(Caller->" + WhoCalled + ")\n"));
            DoGetImagePath(WhoCalled);
            return PtrResponse;
        }

        private void DoGetImagePath(string WhoCalled)
        {
            string aWhoCalled = "SgGetImagePath.GetImagePath().DoGetImagePath()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetImagePathResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetImagePath.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }

    }

    /* 
    Responses POST
    All Verbs /imagepath/{Receipt}   

    POST /json/reply/SgGetImagePath HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Receipt":"00000000000000000000000000000000"}

    */
    internal class SgGetImagePathPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/imagepath";
        public Guid Receipt { get; set; }
    }

    /*
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */
    internal class SgGetImagePathResponse : SgBaseResponse
    {

    }
}
