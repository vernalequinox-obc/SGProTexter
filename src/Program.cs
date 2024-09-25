using SGProTexter.SgProAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace SGProTexter
{
    internal class Program
    {

        // SgBase.SGProURL = "http://localhost:59590";

        private static readonly EmailCredentials PtrEmailCredentials = new();
        private static EmailCredentialStructure PtrEmailCredentialStructure = new();
        private static SgGetObservatoryInformationResponse PtrSgObservatoryResponse = new();
        private static SgGetDeviceStatusResponse PtrTelescopeSgGetDeviceStatusResponse = new();
        private static readonly ConsoleEnterCredential PtrConsoleEnterCredential = new();
        private static readonly ConsoleMainMenu PtrConsoleMainMenu = new();


        static public void Main()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(FileHostData.FullPathFolderEmailLoggingErrorFileName));
            Trace.AutoFlush = true;
            Trace.Write(FileHostData.GetLoggingTime("********************************** Program.Menu  Started *************************************\n"));
            int OperationChoice;
            bool KeepLooping = true;
            FileHostData.RemoveOldLoggingFiles();
            GetEmailCredentialStructure();
            Guid g = Guid.NewGuid();

            OperationChoice = CountDownTimerOptions.StartCountDown();
            while (KeepLooping)
            {
                switch (OperationChoice)
                {
                    case FileHostData.RunConsoleMainMenuInt:
                        Trace.Write(FileHostData.GetLoggingTime("Program.Menu switch FileHostData.RunConsoleMainMenuInt:"));
                        OperationChoice = RunFullMenu();
                        break;
                    case FileHostData.GetEmailCredentialStructureInt:
                        Trace.Write(FileHostData.GetLoggingTime("Program.Menu switch FileHostData.GetEmailCredentialStructureInt:"));
                        GetEmailCredentialStructure();
                        break;
                    case FileHostData.RunSilientModeInt:
                        Trace.Write(FileHostData.GetLoggingTime("Program.Menu switch FileHostData.RunSilientModeInt:"));
                        OperationChoice = RunSilientMode();
                        break;
                    case FileHostData.ExitToSystemInt:
                        Trace.Write(FileHostData.GetLoggingTime("Program.Menu switch FileHostData.ExitToSystemInt:"));
                        Trace.Write(FileHostData.GetLoggingTime("\n\n********************************** Program.Menu Stopped *************************************\n"));
                        KeepLooping = false;
                        break;
                }
            }
            Trace.Write(FileHostData.GetLoggingTime("---------------------------------- Program.Menu  End  ---------------------------------------\n"));
            Environment.Exit(0);
        }


        private static void GetEmailCredentialStructure()
        {
            PtrEmailCredentials.SetEmailCredentialStructure(PtrEmailCredentialStructure);
            if (PtrEmailCredentials.IsThereEmailCredentialsFile())
            {
                if (PtrEmailCredentials.ReadFileEmailCredential())
                {
                    PtrEmailCredentialStructure = PtrEmailCredentials.GetEmailCredentialStructure();
                    PtrConsoleEnterCredential.SetEmailCredentialStructure(PtrEmailCredentialStructure);
                }
            }
        }

        private static int RunFullMenu()
        {
            // Arguments so bring up the main menu dialog
            int ReturnResult;
            PtrConsoleMainMenu.SetEmailCredentials(PtrEmailCredentials);
            PtrConsoleMainMenu.SetEmailCredentialStructure(PtrEmailCredentialStructure);
            PtrConsoleMainMenu.SetSgObservatoryResponse(PtrSgObservatoryResponse);
            PtrConsoleMainMenu.SetTelescopeSgGetDeviceStatusResponse(PtrTelescopeSgGetDeviceStatusResponse);
            ReturnResult = PtrConsoleMainMenu.MainMenuConsole();
            return ReturnResult;
        }

        private static int RunSilientMode()
        {
            if (!PtrEmailCredentialStructure.SgSGproEmailCredentialFileExist)
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
                Trace.Write(FileHostData.GetLoggingTime("Program.RunSilientMode() " + OutString1 + "\n"));
                Console.WriteLine(OutString1);
                Thread.Sleep(15000);
                return FileHostData.ExitToSystemInt;
            }
            else if (!PtrEmailCredentialStructure.IsEmailTestSuccesful)
            {
                Console.Clear();
                string OutString2 = "\n"
                    + "\n\t************* Error cannot send Text Message ********************"
                    + "\n\t**                                                             **"
                    + "\n\t**       The current Email Credential was never tested.        **"
                    + "\n\t**  Please run Menu and select (3) 'Send a test Text Message'  **"
                    + "\n\t**                                                             **"
                    + "\n\t**      Program will exit automatically in 15 seconds          **"
                    + "\n\t**                                                             **"
                    + "\n\t*****************************************************************";
                Trace.Write(FileHostData.GetLoggingTime("Program.RunSilientMode() " + OutString2 + "\n"));
                Console.WriteLine(OutString2);
                Thread.Sleep(15000);
                Console.Clear();
                return FileHostData.ExitToSystemInt;
            }

            Emailer PtrEmailer = new();
            PtrEmailer.SetPtrEmailCredentialStructure(PtrEmailCredentialStructure);
            PtrEmailer.SetPtrSgObservatoryResponse(PtrSgObservatoryResponse);
            PtrEmailer.SetPtrTelescopeSgGetDeviceStatusResponse(PtrTelescopeSgGetDeviceStatusResponse);
            PtrEmailer.SendEmailtoTextMessage(PtrEmailCredentialStructure.SendOnlyOnRORorMountIssues);
            Trace.Write(FileHostData.GetLoggingTime("Program.RunSilientMode() SendEmailtoTextMessage()"));
            return FileHostData.ExitToSystemInt;
        }

    }
}
