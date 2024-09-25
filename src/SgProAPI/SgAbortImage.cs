using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 
    SgAbortImage

    http://localhost:59590/json/reply/SgAbortImage
    Results: {"Success":true,"Message":"No camera is connected."}

    Abort the current image.
    The following routes are available for this service:
    All Verbs	/abortimage		
    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

    http://localhost:59590/abortimage?format=json
    
    
    

    */

namespace SGProTexter.SgProAPI
{
    internal class SgAbortImage
    {
        public SgAbortImageResponse PtrResponse = new SgAbortImageResponse();
        public SgAbortImagePlaceHolder PtrPlaceHolder = new SgAbortImagePlaceHolder();

        public SgAbortImageResponse AbortImage(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgAbortImage.AbortImage()::(Caller->" + WhoCalled + ")\n"));
            DoAbortImage(WhoCalled);
            return PtrResponse;
        }

        private void DoAbortImage(string WhoCalled)
        {
            string aWhoCalled = "SgAbortImage.AbortImage().DoAbortImage()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgAbortImageResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgAbortImage.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /*

    Responses POST 
    All Verbs /abortimage 
        
    POST /json/reply/SgAbortImage HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}

    */
    internal class SgAbortImagePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/abortimage";
    }

    /* 
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */

    internal class SgAbortImageResponse : SgBaseResponse
    {

    }
}

