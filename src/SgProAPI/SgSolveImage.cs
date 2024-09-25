using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    SgSolveImage


    http://localhost:59590/json/reply/SgSolveImage?format=json
    results: {"Success":false,"Message":"There is no plate solver selected!","Receipt":"367b9e4eeddf4b899cbe28af6e708484"}

    Ask SGPro to plate solve an image. This is an asynchronous operation and will return immediately. 
    The return type (SgSolveResponse) will contain a GUID and this can be used to retrieve the solved image data when it is complete (using /solvedata/{Receipt}).
    
    The following routes are available for this service: 
        POST /solve   

    Parameters: 
    Name        Parameter   Data Type   Required    Description 
    ImagePath   path        string      Yes         The path to the 16-bit unsigned FITS image to solve. 
    RaHint      path        float       No          The RA hint location given in decimal hours; Required if not using hint data from headers, 
                                                        if directed to use hint data from the headers and this value is provided, 
                                                        it will override the FITS header data. Also not required for blind solving. 

    DecHint     path        float       No          The Dec hint location given in decimal degrees (-90 to 90); Required if not using hint data from headers, 
                                                        if directed to use hint data from the headers and this value is provided, it will override the FITS header data. 
                                                        Also not required for blind solving. 

    ScaleHint   path        float       No          The Scale hint location given in arc-minutes / second; Required if not using hint data from headers, 
                                                        if directed to use hint data from the headers and this value is provided, it will override the FITS header data. 
                                                        Also not required for blind solving. 

    BlindSolve  path        boolean     Yes         A boolean value indicating if SGPro should attempt to blind solve the specified image. This value is required. 
                                                        If set to true, providing a scale hint may result in a faster solve. 

    UseFitsHeadersForHints path boolean Yes         A boolean value indicating if SGPro should attempt to gather hint data from the image's FITS headers. 
                                                        This value is required. If set to true, header hint data may still be overriden by setting hint values in this data structure. 


To override the Content-type in your clients HTTP Accept Header, append ?format=json

To embed the response in a jsonp callback, append ?callback=myCallback


*/

namespace SGProTexter.SgProAPI
{
    internal class SgSolveImage
    {

        public SgSolveImageResponse PtrResponse = new SgSolveImageResponse();
        public SgSolveImagePlaceHolder PtrPlaceHolder = new SgSolveImagePlaceHolder();

        public SgSolveImageResponse SolveImage(string WhoCalled)
        {
            Trace.Write(FileHostData.GetLoggingTime("SgSolveImage.SolveImage()::(Caller->" + WhoCalled + ")\n"));
            DoSolveImage(WhoCalled);
            return PtrResponse;
        }

        private void DoSolveImage(string WhoCalled)
        {
            string aWhoCalled = "SgSolveImage.SolveImage().DoSolveImage()::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgSolveImageResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgSolveImage.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }

    }

    /*
    Responses POST

    POST /json/reply/SgSolveImage HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"ImagePath":"String","RaHint":0,"DecHint":0,"ScaleHint":0,"BlindSolve":false,"UseFitsHeadersForHints":false}




    */

    internal class SgSolveImagePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "guiderinfo/";
    }

    /*
    Request GET
      
    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length
    
    {"Success":false,"Message":"String","Receipt":"00000000000000000000000000000000"}


    */
    internal class SgSolveImageResponse : SgBaseResponse
    {
        public Guid Receipt { get; set; }
    }
}
