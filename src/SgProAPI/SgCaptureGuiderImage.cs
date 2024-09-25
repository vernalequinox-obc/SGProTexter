using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
    SgCaptureGuiderImage

    http://localhost:59590/json/reply/SgCaptureGuiderImage
    Results: {"Success":true,"Message":"Guider image capture started successfully.","Receipt":"bb765c5498b54a059045a6bf570ad53f"}

    Capture an image with a camera's onboard guider (this is not normally used external to SGPro).
    The following routes are available for this service:

    POST /guiderimage
    Parameters:
    Name                Parameter   Data Type	Required	Description
    BinningMode	        path	    int	        Yes	        The binning value for the guide image. A value of 1 means 1x1 and a value of 4 means 4x4.
    ExposureLengthSec	path	    float	    Yes	        The exposure length of the guide frame in seconds.
    OpenShutter	        path	    bool        No	        Controls the status of the camera shutter for light or dark frames. 
                                                            This will only be used if the camera being controlled has a shutter.
    StartX	            path	    int	        No	        Used for specification of guider subframes. The upper left X coordinate of the subframe.
    StartY	            path	    int	        No	        Used for specification of guider subframes. The upper left Y coordinate of the subframe.
    NumX	            path	    int	        No	        Used for specification of guider subframes. The width of the subframe.
    NumY	            path	    int	        No	        Used for specification of guider subframes. The height of the subframe.
    
    To override the Content-type in your clients HTTP Accept Header, append ?format=json
    To embed the response in a jsonp callback, append ?callback=myCallback

     http://localhost:59590/guiderimage/SgGuiderImage?format=json
*/

namespace SGProTexter.SgProAPI
{
    internal class SgCaptureGuiderImage
    {
        public SgCaptureGuiderImageResponse PtrResponse = new SgCaptureGuiderImageResponse();
        public SgCaptureGuiderImagePlaceHolder PtrPlaceHolder = new SgCaptureGuiderImagePlaceHolder();


        public SgCaptureGuiderImageResponse CaptureGuiderImage(string WhoCalled)
        {
            DoCaptureGuiderImage(WhoCalled);
            Trace.Write(FileHostData.GetLoggingTime("SgCaptureGuiderImage.CaptureGuiderImage()::(Caller->" + WhoCalled + ")\n"));
            return PtrResponse;
        }

        private void DoCaptureGuiderImage(string WhoCalled)
        {
            string aWhoCalled = "SgCaptureGuiderImage.CaptureGuiderImage().DoCaptureGuiderImage::(Caller->" + WhoCalled + ")";
            RestResponse response = PtrPlaceHolder.client.Execute(PtrPlaceHolder.GetPlaceHolder(PtrPlaceHolder.RequestResourceString));
            if (response.IsSuccessful)
            {
                PtrResponse = JsonConvert.DeserializeObject<SgCaptureGuiderImageResponse>(response.Content);
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
            PtrSgConsolePrint.ConsolePrint(PtrPlaceHolder, PtrResponse, "SgCaptureGuiderImage.ConsolePrint_Response()::(Caller->" + WhoCalled + ")");
        }
    }

    /*

    Responses POST
    All Verbs /guiderimage 

    POST /json/reply/SgCaptureGuiderImage HTTP/1.1 
    Host: localhost 
    Content-Type: application/json
    Content-Length: length

    {"BinningMode":0,"ExposureLengthSec":0,"OpenShutter":false,"StartX":0,"StartY":0,"NumX":0,"NumY":0}

    Enter this url into web browser 
    http://localhost:59590/json/reply/SgCaptureGuiderImage?format=json
    Results:  {"Success":true,"Message":"Guider image capture started successfully.","Receipt":"242b3e85c8434ed0a80699685d455c40"}

    */

    internal class SgCaptureGuiderImagePlaceHolder : SgBasePlaceHolder
    {
        public string SbVerbPath = "/guiderimage";

        public int BinningMode { get; set; }
        public float ExposureLengthSec { get; set; }
        public bool OpenShutter { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int MumX { get; set; }
        public int MumY { get; set; }
    }

    /* 
    Request GET
    POST /guiderimage 

    HTTP/1.1 200 OK
    Content-Type: application/json
    Content-Length: length

    {"Success":false,"Message":"String","Receipt":"00000000000000000000000000000000"}

    Example of output if pasting url link below to browser:
    http://localhost:59590/json/reply/SgCaptureGuiderImage
    {"Success":true,"Message":"Guider image capture started successfully.","Receipt":"014d895d57af4ced95a5752b7bdbb404"}

    http://localhost:59590/guiderimage/?BinningMode=1&ExposureLengthSec=1&OpenShutter=true&StartX=0&StartY=0&MumX=200&MumY=200


    */

    internal class SgCaptureGuiderImageResponse : SgBaseResponse
    {
        public Guid Receipt { get; set; }
    }
}
