using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace SGProTexter
{
    internal class Emailer
    {
        EmailCredentialStructure PtrEmailCredentialStructure;
        SgGetObservatoryInformationResponse PtrSgObservatoryResponse;
        SgGetDeviceStatusResponse PtrTelescopeSgGetDeviceStatusResponse;
        SgGetDeviceStatusResponse PtrCameraSgGetDeviceStatusResponse;

        public void SetPtrEmailCredentialStructure(EmailCredentialStructure aPtrEmailCredentialStructure)
        {
            PtrEmailCredentialStructure = aPtrEmailCredentialStructure;
        }

        public void SetPtrSgObservatoryResponse(SgGetObservatoryInformationResponse aPtrSgObservatoryResponse)
        {
            PtrSgObservatoryResponse = aPtrSgObservatoryResponse;
        }

        public void SetPtrTelescopeSgGetDeviceStatusResponse(SgGetDeviceStatusResponse aPtrTelescopeSgGetDeviceStatusResponse)
        {
            PtrTelescopeSgGetDeviceStatusResponse = aPtrTelescopeSgGetDeviceStatusResponse;
        }

        public void SetPtrCameraSgGetDeviceStatusResponse(SgGetDeviceStatusResponse aPtrCameraSgGetDeviceStatusResponse)
        {
            PtrCameraSgGetDeviceStatusResponse = aPtrCameraSgGetDeviceStatusResponse;
        }

        public bool SendEmailtoTextMessage(bool aSendOnlyOnRORorMountIssues)
        {


            SgGetDeviceStatus PtrSgGetDeviceStatus = new SgGetDeviceStatus();
            SgGetObservatoryInformation PtrSgGetObservatoryInformation = new SgGetObservatoryInformation();
            Emailer PtrEmailer = new();
            bool ReturnResult = false;
            bool SendText = true;
            Console.Clear();
            Console.CursorVisible = false;
            Trace.Write(FileHostData.GetLoggingTime("Emailer.SendEmailtoTextMessage() Getting Mount and Observatory Status"));
            Console.WriteLine("\n\tGetting Mount and Observatory Status");
            PtrSgObservatoryResponse = PtrSgGetObservatoryInformation.GetObservatoryInformation("Emailer.SendEmailtoTextMessage()");
            if (PtrSgObservatoryResponse.Message == null)
            {
                PtrSgObservatoryResponse.Message = "";
            }
            if (PtrSgObservatoryResponse.Message.IndexOf("Or SGPro is not responding and may not be running.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                PtrSgObservatoryResponse.Message = "SGPro is not responding and may not be running.";
            }
            PtrTelescopeSgGetDeviceStatusResponse = PtrSgGetDeviceStatus.GetTelescopeStatus("Emailer.SendEmailtoTextMessage()");
            if (PtrTelescopeSgGetDeviceStatusResponse.Message == null)
            {
                PtrTelescopeSgGetDeviceStatusResponse.Message = "";
            }
            if (PtrTelescopeSgGetDeviceStatusResponse.Message.IndexOf("Or SGPro is not responding and may not be running.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                PtrTelescopeSgGetDeviceStatusResponse.Message = "SGPro is not responding and may not be running.";
            }
            string strBody = "SGPro Is Not Running.";
            if (PtrSgObservatoryResponse != null && PtrSgObservatoryResponse != null)
            {
                strBody = "Roof State: " + PtrSgObservatoryResponse.OpenState + "\n"
                        + "Roof Message: " + PtrSgObservatoryResponse.Message + "\n\n************\n\n"
                        + "Mount State: " + PtrTelescopeSgGetDeviceStatusResponse.State + "\n"
                        + "Mount Message: " + PtrTelescopeSgGetDeviceStatusResponse.Message;
                PtrEmailCredentialStructure.Body = strBody;
            }
            if (aSendOnlyOnRORorMountIssues)
            {
                bool aObservatoryStatus = PtrSgObservatoryResponse.OpenState.IndexOf("Closed", StringComparison.OrdinalIgnoreCase) >= 0;
                bool aTelescopeStatus = PtrTelescopeSgGetDeviceStatusResponse.State.IndexOf("Parked", StringComparison.OrdinalIgnoreCase) >= 0;

                if (!aObservatoryStatus || !aTelescopeStatus)
                {
                    SendText = true;
                }
                else
                {
                    SendText = false;
                }
            }
            if (SendText)
            {
                Trace.Write(FileHostData.GetLoggingTime("Emailer.SendEmailtoTextMessage() Getting Mount and Observatory Status\n" + strBody + "\n"));
                Console.WriteLine("\n\tGetting Email Text Ready.");
                if (PtrEmailCredentialStructure.SgSGproEmailCredentialFileExist)
                {
                    PtrEmailer.PtrEmailCredentialStructure = PtrEmailCredentialStructure;
                    DoDelayWhileCameraWarmsUp();
                    Console.WriteLine("\n\tPlease Wait Busy Sending\n");
                    ReturnResult = PtrEmailer.SendEmail();
                    string[] JustPhoneNumber = PtrEmailCredentialStructure.Recipient.Split('@');

                    if (JustPhoneNumber.Length > 1)
                    {
                        string PhoneNumber = JustPhoneNumber[0];
                        Console.WriteLine("\n\tText Sent to " + PhoneNumber + "\n");
                        Trace.Write(FileHostData.GetLoggingTime("Emailer.SendEmailtoTextMessage() Getting Mount and Observatory Status\n" + "\n\tText Sent to " + PhoneNumber + "\n"));
                    }
                    else
                    {
                        Console.WriteLine("\n\tEmail Sent to " + PtrEmailCredentialStructure.Recipient + "\n");
                        Trace.Write(FileHostData.GetLoggingTime("Emailer.SendEmailtoTextMessage() Getting Mount and Observatory Status\n" + "\n\tEmail Sent to " + PtrEmailCredentialStructure.Recipient + "\n"));
                    }
                }
                else
                {
                    Console.Clear();

                    string OutString1 = "\n"
                        + "\n\t**************   Error Sending Text Message *********************"
                        + "\n\t**                                                             **"
                        + "\n\t**  Email Credential is missing and Text Message was not sent. **"
                        + "\n\t**  Please run Menu to create new Email Credential.            **"
                        + "\n\t**                                                             **"
                        + "\n\t**      Program will exit automatically in 15 seconds          **"
                        + "\n\t**                                                             **"
                        + "\n\t*****************************************************************";
                    Console.WriteLine(OutString1);
                    Trace.TraceError(FileHostData.GetLoggingTime("Emailer.SendEmail() " + OutString1));
                    Thread.Sleep(12000);
                    ReturnResult = false;

                }
            }
            Thread.Sleep(3000);
            Console.Clear();
            Console.CursorVisible = true;
            return ReturnResult;
        }

        private bool SendEmail()
        {
            try
            {
                Trace.Write(FileHostData.GetLoggingTime("Emailer.SendEmail()"));
                int portNum = Int32.Parse(PtrEmailCredentialStructure.Port);
                MailMessage message = new();
                message.From = new MailAddress(PtrEmailCredentialStructure.From);
                message.To.Add(new MailAddress(PtrEmailCredentialStructure.Recipient));
                message.Subject = PtrEmailCredentialStructure.Subject;
                // message.IsBodyHtml = true; //to make message body as html  
                message.Body = PtrEmailCredentialStructure.Body;

                SmtpClient SmtpClient = new(PtrEmailCredentialStructure.Host)
                {
                    Port = portNum,
                    Credentials = new NetworkCredential(PtrEmailCredentialStructure.UserName, PtrEmailCredentialStructure.Password),
                    // UseDefaultCredentials = true,
                    EnableSsl = PtrEmailCredentialStructure.BoolEnableSSL
                };

                Trace.Write(FileHostData.GetLoggingTime("Emailer.SendEmail() message:"
                    + "\n\t " + SmtpClient.Host
                    + "\n\t " + SmtpClient.Port
                    + "\n\t " + message.From
                    + "\n\t " + message.To
                    + "\n\t " + message.Subject
                    + "\n\t " + message.Body
                    + "\n"));
                SmtpClient.Send(message);
                message.Dispose();
            }
            catch (Exception ex)
            {
                PtrEmailCredentialStructure.IsEmailTestSuccesful = false;
                Trace.TraceError(FileHostData.GetLoggingTime("Emailer.SendEmail() " + ex.ToString()));
                Console.WriteLine(ex.ToString());
                return false;
            }
            PtrEmailCredentialStructure.IsEmailTestSuccesful = true;
            return true;
        }

        private void DoDelayWhileCameraWarmsUp()
        {
            bool doLoop = true;
            int seconds;

            if (string.IsNullOrEmpty(PtrEmailCredentialStructure.WarmUpDelay))
            {
                PtrEmailCredentialStructure.WarmUpDelay = "0";
            }

            seconds = int.Parse(PtrEmailCredentialStructure.WarmUpDelay);
            Console.Clear();
            Console.WriteLine("\nCamera warm-up delay. 'X' key to exit delay and continue sending Text Message.\n");
            // Start a task to monitor for user input
            Task.Run(() =>
            {
                while (doLoop)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.X)
                    {
                        doLoop = false; // Exit the timer loop
                        Console.WriteLine("\nCamera warm-up delay stopped by user.\"");
                    }
                }
            });

            // Delay countdown
            while (doLoop)
            {
                if (seconds == 0)
                {
                    Console.WriteLine("No camera warm-up delay set, continuing...");
                    break; // Exit the loop if there's no delay
                }

                for (int i = 0; i < seconds && doLoop; i++)
                {
                    Console.Write($"\rDelay Elapsed: {i + 1} second(s), Time Left: {seconds - (i + 1)} seconds");
                    Thread.Sleep(1000); // Delay for 1 second
                }

                doLoop = false; // Exit the loop after the delay if not stopped
            }

            return;
        }


    }
}
