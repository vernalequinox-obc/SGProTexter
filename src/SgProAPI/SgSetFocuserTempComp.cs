using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgSetFocuserTempComp


    http://localhost:59590/json/reply/SgSetFocuserPosition?format=json
    results: { "Success":true,"Message":"Setting focuser to position 0..."}

    Turns the focus controller's onboard ASCOM temperature compensation feature on or off.
    
    The following routes are available for this service: POST /focuser/tempcomp   
        All Verbs /focuser/tempcomp/{TempCompActive}   
    
    Parameters:     
    Name            Parameter   Data Type   Required    Description 
    TempCompActive  path        bool        Yes         True to turn ASCOM temperature compensation on; false to turn it off. 

    To override the Content-type in your clients HTTP Accept Header, append ?format=json

    To embed the response in a jsonp callback, append ?callback=myCallback

*/

namespace SGProTexter.SgProAPI
{
    internal class SgSetFocuserTempComp
    {

        SgSetFocuserTempCompResponse PtrResponse = new SgSetFocuserTempCompResponse();
        SgSetFocuserTempCompPlaceHolder PtrPlaceHolder = new SgSetFocuserTempCompPlaceHolder();
        SgSetFocuserTempCompPostBody PtrPostBody = new SgSetFocuserTempCompPostBody();

        public SgSetFocuserTempCompResponse SetFocuserTempComp(SgSetFocuserTempCompPostBody aPostBody, string WhoCalled)
        {
            PtrPostBody = aPostBody;
            PtrPlaceHolder.RequestResourceString = PtrPlaceHolder.SbVerbPath;
            Trace.Write(FileHostData.GetLoggingTime("SgSetFocuserTempComp.SetFocuserTempComp()::(Caller->" + WhoCalled + ")\n"));
            DoSetFocuserTempComp(WhoCalled);
            return PtrResponse;
        }

        private void DoSetFocuserTempComp(string WhoCalled)
        {
            string aWhoCalled = "SgSetFocuserTempComp.SetFocuserTempComp().DoSetFocuserTempComp()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.PostPlaceHolder(PtrPlaceHolder.RequestResourceString, PtrPostBody));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSetFocuserTempCompResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(aWhoCalled, PtrPlaceHolder.RequestResourceString)));
                Trace.Write("\t\t\t TempCompActive:   \t" + PtrResponse.TempCompActive + "\n");
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
        }

        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSetFocuserTempComp.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
            Console.Write("\t\t\t TempCompActive:   \t" + PtrResponse.TempCompActive + "\n");
        }

    }

    /*
    Responses POST

    All Verbs /focuser/tempcomp/{TempCompActive} 

    POST /json/reply/SgSetFocuserTempComp HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"TempCompActive":false}

    Example: Enter url in browser
    http://localhost:59590/focuser/tempcomp/false?format=json
    Results:  {"Success":true,"Message":"Focuser tempo comp active set to False"}


    */

    internal class SgSetFocuserTempCompPostBody
    {
        public string TempCompActive { get; set; }
    }

    internal class SgSetFocuserTempCompPlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/focuser/tempcomp/";
    }

    /*
    Request GET

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String"}
    
    */
    internal class SgSetFocuserTempCompResponse : SgBaseResponse
    {
        public bool TempCompActive { get; set; }
    }
}