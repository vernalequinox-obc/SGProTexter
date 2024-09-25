using SGProTexter;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SGProTexter
{
    internal class ConsoleMainMenu
    {
        private EmailCredentialStructure PtrEmailCredentialStructure;
        private EmailCredentials PtrEmailCredentials;

        private SgGetObservatoryInformationResponse PtrSgObservatoryResponse = new();
        private SgGetDeviceStatusResponse PtrTelescopeSgGetDeviceStatusResponse = new();


        public void SetEmailCredentials(EmailCredentials aEmailCredentials)
        {
            PtrEmailCredentials = aEmailCredentials;
        }
        public void SetEmailCredentialStructure(EmailCredentialStructure aEmailCredential)
        {
            PtrEmailCredentialStructure = aEmailCredential;
        }

        public void SetSgObservatoryResponse(SgGetObservatoryInformationResponse aSgObservatoryResponse)
        {
            PtrSgObservatoryResponse = aSgObservatoryResponse;
        }

        public void SetTelescopeSgGetDeviceStatusResponse(SgGetDeviceStatusResponse aTelescopeSgGetDeviceStatusResponse)
        {
            PtrTelescopeSgGetDeviceStatusResponse = aTelescopeSgGetDeviceStatusResponse;
        }

        public int MainMenuConsole()
        {
            int ReturnOperation = 0;
            bool keepDoingIt = true;
            Console.Clear();
            while (keepDoingIt)
            {
                Console.WriteLine("\tNOTE: Email Credential must be Tested before using with SGPRO.");
                Console.WriteLine("\n\tWas the Email Credential tested? : " + PtrEmailCredentialStructure.IsEmailTestSuccesful);
                Console.WriteLine("\n\tCHOOSE AN OPTION:\n");
                Console.WriteLine("\t (1) Display Current Email Credential");
                Console.WriteLine("\t (2) Create a New Email Credentials (Note: Will overwrite current credentials)");
                Console.WriteLine("\t (3) Send a test Text Message to verify the Email Credentials");
                Console.WriteLine("\t (4) Send Text Message when:  " + (PtrEmailCredentialStructure.SendOnlyOnRORorMountIssues ? "Only when there is an Issues" : "Always"));
                Console.WriteLine("\t (5) Enter delay while camera warms up, then send Text Message (0 = no delay, 0-900 sec):  " + PtrEmailCredentialStructure.WarmUpDelay);
 
                Console.WriteLine("\n");
                Console.WriteLine("\t (D) Delete Email Credential");
                Console.WriteLine("\t (R) Run Silent Mode");
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t (X) Exit to System");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (1) Display Current Email Credential."));
                        DisplayCurrentEmailCredentialStructure();
                        break;
                    case ConsoleKey.D2:
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (2) Create a New Email Credentials (Note: Will overwrite current credentials)."));
                        CreateNewEmailCredential();
                        break;
                    case ConsoleKey.D3:
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (3) Send a test Text Message to verify the Email Credentials."));
                        TestByTextingEmailCredential();
                        break;
                    case ConsoleKey.D4:
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (4) Send Text Message when."));
                        StatusSendModeSubmenu();
                        Console.Clear(); // Clear the console after returning from the submenu
                        break;
                    case ConsoleKey.D5:
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (5) Enter delay while camera warms up."));
                        DelayWhileCameraWarmsUp();
                        Console.Clear(); // Clear the console after returning from the submenu
                        break;

                    case ConsoleKey.D:
                        DeleteEmailCredentialStructure();
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (D) Delete Email Credential."));
                        break;
                    case ConsoleKey.R:
                        Trace.Write(FileHostData.GetLoggingTime("ConsoleMainMenu.MainMenuConsole user selected (R) Run Silent Mode."));
                        ReturnOperation = FileHostData.RunSilientModeInt;
                        keepDoingIt = false;
                        break;
                    case ConsoleKey.X:
                        Trace.Write(FileHostData.GetLoggingTime("Main Menu user selected (X) Exit to System."));
                        ReturnOperation = FileHostData.ExitToSystemInt;
                        keepDoingIt = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\n\n");
                        break;
                }
            }
            return ReturnOperation;
        }


        private void StatusSendModeSubmenu()
        {
            bool keepDoingIt = true;
            while (keepDoingIt)
            {
                Console.Clear();
                Console.WriteLine("\n\tSelect Status Send Mode:");
                Console.WriteLine("\t (1) Send ROR and Mount Status Always");
                Console.WriteLine("\t (2) Send Status Only on ROR or Mount Issues");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        PtrEmailCredentialStructure.SendOnlyOnRORorMountIssues = false;
                        Console.WriteLine("\nSelected: Send ROR and Mount Status Always");
                        keepDoingIt = false;
                        break;

                    case ConsoleKey.D2:
                        PtrEmailCredentialStructure.SendOnlyOnRORorMountIssues = true;
                        Console.WriteLine("\nSelected: Send Status Only on ROR or Mount Issues");
                        keepDoingIt = false;
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
                SaveEmailCredentialFile();
            }
        }

        private void DelayWhileCameraWarmsUp()
        {
            Console.Clear();
            ConsoleEnterDelay PtrConsoleEnterDelay = new();
            PtrConsoleEnterDelay.SetEmailCredentialStructure(PtrEmailCredentialStructure);
            PtrConsoleEnterDelay.EnterDelayWhileCameraWarmsUpConsole();
            SaveEmailCredentialFile();
            Console.Clear();
        }

        private void CreateNewEmailCredential()
        {
            Console.Clear();
            ConsoleEnterCredential PtrConsoleEnterCredential = new();
            PtrConsoleEnterCredential.SetEmailCredentialStructure(PtrEmailCredentialStructure);
            PtrConsoleEnterCredential.EnterEmailCredentialsConsole();
            SaveEmailCredentialFile();
            Console.Clear();
        }

        private void SaveEmailCredentialFile()
        {
            EmailCredentials PtrEmailCredentials = new();
            PtrEmailCredentials.SetEmailCredentialStructure(PtrEmailCredentialStructure);
            PtrEmailCredentials.CreateFileEmailCredentialStructure();
            PtrEmailCredentialStructure.SgSGproEmailCredentialFileExist = PtrEmailCredentials.IsThereEmailCredentialsFile();
        }

        private void DeleteEmailCredentialStructure()
        {

            ConsoleKey response;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\tType Y or N to confirm.");
                response = Console.ReadKey().Key;

            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            if (response == ConsoleKey.Y)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\tAre you sure Y or N to confirm.");
                    response = Console.ReadKey().Key;

                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            }

            if (response == ConsoleKey.Y)
            {
                PtrEmailCredentialStructure.ResetEmailCredentialStructure();
                PtrEmailCredentials.DeleteFileEmailCredentialStructure();
            }
            return;

        }

        private void DisplayCurrentEmailCredentialStructure()
        {
            string StringPart1 = "\n\nSProTexter profile located in this folder: " + EncryptDecrypt.userNameKey
                + "\nEmailCredential"
                + "\n\t Tested OK:   \t" + PtrEmailCredentialStructure.IsEmailTestSuccesful
                + "\n\t From:        \t" + PtrEmailCredentialStructure.From
                + "\n\t Recipient:   \t" + PtrEmailCredentialStructure.Recipient
                + "\n\t Server Host: \t" + PtrEmailCredentialStructure.Host
                + "\n\t Server Port#:\t" + PtrEmailCredentialStructure.Port
                + "\n\t SSL Enable:  \t" + PtrEmailCredentialStructure.EnableSsL
                + "\n\t UserName:    \t" + PtrEmailCredentialStructure.UserName;
            string ConsolePart2 = StringPart1
                + "\n\t Password:    \t" + PtrEmailCredentialStructure.Password
                + "\n\t Subject:     \t" + PtrEmailCredentialStructure.Subject;
            string FilePart2 = StringPart1
                + "\n\t Password:    \t" + "**********"
                + "\n\t Subject:     \t" + PtrEmailCredentialStructure.Subject + "\n";



            Console.Clear();
            Console.WriteLine(ConsolePart2);
            Trace.Write(FileHostData.GetLoggingTime(FilePart2));
            Console.WriteLine("\n\n\t\t Any Key To Continue:");
            Console.ReadKey();
            Console.Clear();
        }

        public void TestByTextingEmailCredential()
        {
            Emailer PtrEmailer = new();
            PtrEmailer.SetPtrEmailCredentialStructure(PtrEmailCredentialStructure);
            PtrEmailer.SetPtrSgObservatoryResponse(PtrSgObservatoryResponse);
            PtrEmailer.SetPtrTelescopeSgGetDeviceStatusResponse(PtrTelescopeSgGetDeviceStatusResponse);
            if (PtrEmailer.SendEmailtoTextMessage(false))
            {
                SaveEmailCredentialFile();
            }
        }
    }
}
