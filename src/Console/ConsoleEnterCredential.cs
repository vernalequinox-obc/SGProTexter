using System;
using System.Text;

namespace SGProTexter
{
    internal class ConsoleEnterCredential
    {
        private EmailCredentialStructure ptrEmailCredentialStructure;


        public void SetEmailCredentialStructure(EmailCredentialStructure aEmailCredential)
        {
            ptrEmailCredentialStructure = aEmailCredential;
        }

        public EmailCredentialStructure GetEmailCredentialStructure()
        {
            return ptrEmailCredentialStructure;
        }

        public void EnterEmailCredentialsConsole()
        {
            bool doLoop = true;

            // Get sender From
            while (doLoop)
            {
                Console.Write("\nFrom (e.g. johndoe@myIPS.com) : ");
                ptrEmailCredentialStructure.From = Console.ReadLine();
                if (ptrEmailCredentialStructure.From.Length > 0)
                {
                    doLoop = false;
                }
            }
            doLoop = true;

            // Get where to send or Recipient. The cell phone number.
            while (doLoop)
            {
                Console.Write("To Phone Number and their email ISP (e.g. 1234567890@mms.att.net) : ");
                ptrEmailCredentialStructure.Recipient = Console.ReadLine();
                if (ptrEmailCredentialStructure.Recipient.Length > 0)
                {
                    doLoop = false;
                }
            }
            doLoop = true;

            // Get email login Host server name.
            while (doLoop)
            {
                Console.Write("Your email smtp server Host (e.g. smtp.mail.com) : ");
                ptrEmailCredentialStructure.Host = Console.ReadLine();
                if (ptrEmailCredentialStructure.Host.Length > 7)
                {
                    doLoop = false;
                }
            }
            doLoop = true;

            // Get the port number of the email smtp server
            while (doLoop)
            {
                string tempStr;
                Console.Write("Your email smtp server Port (e.g. 587) : ");
                tempStr = Console.ReadLine();
                if (double.TryParse(tempStr, out double portNumber))
                {
                    ptrEmailCredentialStructure.Port = tempStr;
                    if (tempStr.Length > 1 && portNumber > 0)
                    {
                        doLoop = false;
                    }
                    else
                    {
                        Console.Write("Please enter only numbers. ");
                    }
                }
                else
                {
                    Console.Write("Please enter only numbers. ");
                }

            }
            doLoop = true;

            // Get emab;e SSL
            while (doLoop)
            {
                string tempStr = "";
                Console.Write("Enable SSL (True or False) : ");
                tempStr = Console.ReadLine();
                if (tempStr.Length != 0)
                {

                    ptrEmailCredentialStructure.EnableSsL = char.ToUpper(tempStr[0]) + tempStr[1..].ToLower();
                    if (ptrEmailCredentialStructure.EnableSsL == "True" || ptrEmailCredentialStructure.EnableSsL == "False")
                    {
                        if (ptrEmailCredentialStructure.EnableSsL == "False")
                        {
                            ptrEmailCredentialStructure.BoolEnableSSL = false;
                        }
                        else if (ptrEmailCredentialStructure.EnableSsL == "True")
                        {
                            ptrEmailCredentialStructure.BoolEnableSSL = true;
                        }
                        doLoop = false;
                    }
                    else
                    {
                        Console.Write("\nEnter True or False. Please try again.\n");
                    }
                }
            }
            doLoop = true;

            // Get email server login UserName
            while (doLoop)
            {
                Console.Write("Your email login - UserName: ");
                ptrEmailCredentialStructure.UserName = Console.ReadLine();
                if (ptrEmailCredentialStructure.UserName.Length > 0)
                {
                    doLoop = false;
                }
            }
            doLoop = true;

            // Get email login password
            while (doLoop)
            {
                Console.Write("Your email login - Password: ");
                string firstPassword;
                string secondPassword;
                firstPassword = ConsoleGetPassword();
                Console.Write("\nRe enter Password: ");
                secondPassword = ConsoleGetPassword();
                if (firstPassword == secondPassword)
                {
                    ptrEmailCredentialStructure.Password = firstPassword;
                    doLoop = false;
                    Console.Write("\n");
                }
                else
                {
                    Console.Write("\nPassword Mismatch. Please try again.\n");
                }
            }
            doLoop = true;

            // Get Subject
            while (doLoop)
            {
                Console.Write("Subject (e.g. SGPro Sequence Finished) : ");
                ptrEmailCredentialStructure.Subject = Console.ReadLine();
                if (ptrEmailCredentialStructure.Subject.Length > 0)
                {
                    doLoop = false;
                }
            }
            doLoop = true;
        }

        private static string ConsoleGetPassword()
        {
            StringBuilder passwordBuilder = new();
            bool continueReading = true;
            char newLineChar = '\r';
            while (continueReading)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                Console.Write("*");
                char passwordChar = consoleKeyInfo.KeyChar;

                if (passwordChar == newLineChar)
                {
                    continueReading = false;
                }
                else
                {
                    passwordBuilder.Append(passwordChar);
                }
            }
            return passwordBuilder.ToString();
        }



        public void PrintToConsoleEmailCredentialStructure()
        {
            Console.WriteLine("\nEmailCredential");
            Console.WriteLine("\nSGproProfile Exist: " + ptrEmailCredentialStructure.SgSGproEmailCredentialFileExist + "\n");
            Console.WriteLine("\t From: " + ptrEmailCredentialStructure.From);
            Console.WriteLine("\t To Recipient: " + ptrEmailCredentialStructure.Recipient);
            Console.WriteLine("\t Email Server (Host): " + ptrEmailCredentialStructure.Host);
            Console.WriteLine("\t Server Port#: " + ptrEmailCredentialStructure.Port);
            Console.WriteLine("\t SSL Enable: " + ptrEmailCredentialStructure.EnableSsL);
            Console.WriteLine("\t UserName: " + ptrEmailCredentialStructure.UserName);
            Console.WriteLine("\t Password: " + ptrEmailCredentialStructure.Password);
            Console.WriteLine("\t Subject: " + ptrEmailCredentialStructure.Subject);
            Console.WriteLine("\n\n");
        }

    }
}
