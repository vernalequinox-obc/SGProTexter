using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*

    SgAbortSolve

    http://localhost:59590/json/reply/SgAbortSolve
    Results: {"Success":false,"Message":"API: Error aborting plate solve. Object reference not set to an instance of an object.","Receipt":"00000000000000000000000000000000"}


    Asks SGPro to abort a plate solve that is in progress.
    The following routes are available for this service: All Verbs /abortsolve/{Receipt}   
    

    http://localhost:59590/abortsolve/{14826270-291f-4130-8256-6b8f8a0b4330}?format=json
*/


namespace SGProTexter.SgProAPI
{
    internal class SgAbortSolve
    {
        public SgAbortSolveResponse PtrResponse = new SgAbortSolveResponse();
        public SgAbortSolvePlaceHolder PtrPlaceHolder = new SgAbortSolvePlaceHolder();

        public SgAbortSolveResponse AbortSolve(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgAbortSolve.AbortSolve()::(Caller->" + WhoCalled + ")\n"));
            DoAbortSolve(WhoCalled);
            return PtrResponse;
        }


        private SgAbortSolveResponse DoAbortSolve(string WhoCalled)
        {
            string aWhoCalled = "SgAbortSolve.AbortSolve().DoAbortSolve()::(Caller->" + WhoCalled + ")";
            if (PtrPlaceHolder.Receipt == Guid.Empty)
            {
                PtrResponse.Success = false;
                PtrResponse.Message = aWhoCalled + " Receipt is {" + Guid.Empty + "} and is not a valid GUID";
                return PtrResponse;
            }
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgAbortSolveResponse>(response.Content);
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatLogMessage(PtrPlaceHolder.RequestResourceString, aWhoCalled)));
            }
            else
            {
                Trace.Write(FileHostData.GetLoggingTime(PtrResponse.FormatEorrorMessage(response.ErrorMessage, aWhoCalled, PtrPlaceHolder.RequestResourceString)));
            }
            return PtrResponse;
        }
        public void ConsolePrint_Response(string WhoCalled)
        {
            SgConsolePrint PtrSgConsolePrint = new SgConsolePrint();
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgAbortSolve.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /*
    
    Responses POST 
    All Verbs /abortsolve/{Receipt} 

    POST /json/reply/SgAbortSolve HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"Receipt":"00000000000000000000000000000000"}

    */

    internal class SgAbortSolvePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerb = "/abortsolve";
        public string SbVerbPath { get; set; }

        private Guid _receipt;

        public Guid Receipt
        {
            get
            {
                return _receipt;
            }
            set
            {
                _receipt = value;
                SbVerbPath = SbVerb + _receipt;
            }
        }
    }

    /*
    Request GET
    
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Receipt":"00000000000000000000000000000000"}

    */

    internal class SgAbortSolveResponse : SgBaseResponse
    {
        public Guid Receipt { get; set; }
    }
}
