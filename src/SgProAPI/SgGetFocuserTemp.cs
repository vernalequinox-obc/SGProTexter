using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetFocuserTemp

    http://localhost:59590/json/reply/SgGetFocuserTemp?format=json
    results: {"Success":false,"Message":"Cannot get temperature because no focuser is connected!","Temperature":0}

    Retrieve the focuser's temperature.
    The following routes are available for this service: All Verbs /focusertemp   
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    
    To embed the response in a jsonp callback, append ?callback=myCallback
    
*/
namespace SGProTexter.SgProAPI
{
    internal class SgGetFocuserTemp
    {
        public SgGetFocuserTempResponse PtrResponse = new SgGetFocuserTempResponse();
        public SgGetFocuserTempPlaceHolder PtrPlaceHolder = new SgGetFocuserTempPlaceHolder();

        public SgGetFocuserTempResponse GetFocuserTemp(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetFocuserTemp.GetFocuserTemp()::(Caller->" + WhoCalled + ")\n"));
            DoGetFocuserTemp(WhoCalled);
            return PtrResponse;
        }

        private void DoGetFocuserTemp(string WhoCalled)
        {
            string aWhoCalled = "SgGetFocuserTemp.GetFocuserTemp().DoGetFocuserTemp()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetFocuserTempResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t Temperature: \t" + PtrResponse.Temperature + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }


        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetFocuserTemp.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t Temperature: \t" + PtrResponse.Temperature + "\n");
        }
    }

    /*
    Responses POST 
    
    All Verbs /focusertemp   

    POST /json/reply/SgGetFocuserTemp HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {}

    */
    internal class SgGetFocuserTempPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/focusertemp";
    }

    /*  
    Request GET
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"Success":false,"Message":"String","Temperature":0}

    */

    internal class SgGetFocuserTempResponse : SgBaseResponse
    {
        public float Temperature { get; set; }
    }
}
