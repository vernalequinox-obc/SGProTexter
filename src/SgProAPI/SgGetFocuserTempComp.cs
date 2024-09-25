using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    SgGetFocuserTempComp

    http://localhost:59590/json/reply/SgGetFocuserTempComp?format=json
    results: {"Success":false,"Message":"Unable to query temp comp, focuser is not connected.","TempCompAvailable":false,"TempCompActive":false}

    Retrieve the focus controller's ASCOM temperature compensation state.
    The following routes are available for this service: 
        All Verbs /focuser/tempcomp   
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    
    To embed the response in a jsonp callback, append ?callback=myCallback

    */

namespace SGProTexter.SgProAPI
{
    internal class SgGetFocuserTempComp
    {

        public SgGetFocuserTempCompResponse PtrResponse = new SgGetFocuserTempCompResponse();
        public SgGetFocuserTempCompPlaceHolder PtrPlaceHolder = new SgGetFocuserTempCompPlaceHolder();

        public SgGetFocuserTempCompResponse GetFocuserTempComp(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgGetFocuserTempComp.GetFocuserTempComp()::(Caller->" + WhoCalled + ")\n"));
            DoGetFocuserTempComp(WhoCalled);
            return PtrResponse;
        }

        private void DoGetFocuserTempComp(string WhoCalled)
        {
            string aWhoCalled = "SgGetFocuserTempComp.GetFocuserTempComp().DoGetFocuserTempComp()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgGetFocuserTempCompResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t TempCompAvailable: \t" + PtrResponse.TempCompAvailable + "\n");
                Trace.Write("\t\t\t TempCompActive:    \t" + PtrResponse.TempCompActive + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }


        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgGetFocuserTempComp.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t TempCompAvailable: \t" + PtrResponse.TempCompAvailable + "\n");
            Console.Write("\t\t\t TempCompActive:    \t" + PtrResponse.TempCompActive + "\n");
        }
    }

    /*
    Responses POST 

    All Verbs /focuser/tempcomp 

    POST /json/reply/SgGetFocuserTempComp HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length
    
    {}
   
    */
    internal class SgGetFocuserTempCompPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/focuser/tempcomp";
    }

    /*
    Request GET
    
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"Success":false,"Message":"String","TempCompAvailable":false,"TempCompActive":false}

    Example: Enter into a browser 
    http://localhost:59590/focuser/tempcomp?format=json
    Results: {"Success":false,"Message":"Unable to query temp comp, focuser is not connected.","TempCompAvailable":false,"TempCompActive":false}


    */

    internal class SgGetFocuserTempCompResponse : SgBaseResponse
    {
        public bool TempCompAvailable { get; set; }
        public bool TempCompActive { get; set; }
    }
}
