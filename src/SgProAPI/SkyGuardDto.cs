using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SkyGuardDto

    http://localhost:59590/json/reply/SkyGuardDto?format=json
    results: {"Success":true,"Message":""}

    Callback used by skyguard, not needed for normal API usage.
    The following routes are available for this service: 
        All Verbs /skyguardcallback   

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback


*/

namespace SGProTexter.SgProAPI
{
    internal class SkyGuardDto
    {

        public static SkyGuardDtoResponse PtrResponse = new SkyGuardDtoResponse();
        public static SkyGuardDtoPlaceHolder PtrPlaceHolder = new SkyGuardDtoPlaceHolder();

        public SkyGuardDtoResponse AbortImage(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SkyGuardDto.AbortImage()::(Caller->" + WhoCalled + ")\n"));
            DoAbortImage(WhoCalled);
            return PtrResponse;
        }

        private void DoAbortImage(string WhoCalled)
        {
            string aWhoCalled = "SkyGuardDto.AbortImage().DoAbortImage()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SkyGuardDtoResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SkyGuardDto.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }

    }

    /*
    Responses POST
    
    POST /json/reply/SkyGuardDto HTTP/1.1 
    Host: localhost
    Content-Type: application/json
    Content-Length: length
    
    {}
    
    */

    internal class SkyGuardDtoPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "guiderinfo/";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}

    */
    internal class SkyGuardDtoResponse : SgBaseResponse
    {

    }
}
