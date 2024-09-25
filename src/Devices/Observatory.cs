using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 *      SgGetObservatoryInformation
 * 
 * 
 * 
 * 
 */



namespace SGProTexter.SgProAPI
{
    internal class Observatory
    {
        private readonly SgGetDeviceStatus PtrSgGetDeviceStatus = new();
        public SgGetObservatoryInformationResponse SgGetObservatoryInformation(string WhoCalled)
        {
            SgGetObservatoryInformationResponse PtrResponse = new();
            SgGetObservatoryInformation PtrSgGetObservatoryInformation = new();
            Trace.Write(FileHostData.GetLoggingTime("Observatory.SgGetObservatoryInformatio()::(Caller->" + WhoCalled + ") \n"));
            PtrResponse = PtrSgGetObservatoryInformation.GetObservatoryInformation("Observatory.SgGetObservatoryInformatio()");
            return PtrResponse;
        }
    }
}
