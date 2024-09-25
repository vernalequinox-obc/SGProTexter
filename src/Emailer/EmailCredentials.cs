using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;


namespace SGProTexter
{
    internal class EmailCredentials
    {

        private string jsonEmailCredential = "";
        private EmailCredentialStructure emailCredential;


        public void SetEmailCredentialStructure(EmailCredentialStructure aEmailCredential)
        {
            emailCredential = aEmailCredential;
        }

        public EmailCredentialStructure GetEmailCredentialStructure()
        {
            return emailCredential;
        }

        public void DeleteFileEmailCredentialStructure()
        {
            if (!System.IO.Directory.Exists(FileHostData.FullPathFolderEmailCredentialFileName))
            {
                try
                {

                    FileAttributes attributes = File.GetAttributes(FileHostData.FullPathFolderEmailCredentialFileName);
                    File.SetAttributes(FileHostData.FullPathFolderEmailCredentialFileName, FileAttributes.Normal);
                    File.Delete(FileHostData.FullPathFolderEmailCredentialFileName);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(FileHostData.GetLoggingTime("EmailCredentials.DeleteFileEmailCredentialStructure() " + ex.ToString()));
                    Console.WriteLine(ex.ToString());
                }

            }
        }


        public void CreateFileEmailCredentialStructure()
        {
            try
            {
                // make our folder if not there
                if (!System.IO.Directory.Exists(FileHostData.FullPathFolder))
                {
                    System.IO.Directory.CreateDirectory(FileHostData.FullPathFolder);
                }
                jsonEmailCredential = JsonSerializer.Serialize(emailCredential);
                //File.WriteAllTextAsync(FileHostData.FullPathFolderEmailCredentialFileName, EncryptDecrypt.EncryptString(jsonEmailCredential));
                File.WriteAllTextAsync(FileHostData.FullPathFolderEmailCredentialFileName, EncryptDecrypt.EncryptString(jsonEmailCredential));
            }
            catch (Exception ex)
            {
                Trace.TraceError(FileHostData.GetLoggingTime("EmailCredentials.CreateFileEmailCredentialStructure() " + ex.ToString()));
                Console.WriteLine(ex.ToString());
            }
        }

        public bool ReadFileEmailCredential()
        {

            if (IsThereEmailCredentialsFile())
            {
                try
                {
                    emailCredential.ResetEmailCredentialStructure();
                    jsonEmailCredential = EncryptDecrypt.DecryptString(File.ReadAllText(FileHostData.FullPathFolderEmailCredentialFileName));
                }
                catch (Exception ex)
                {
                    Trace.TraceError(FileHostData.GetLoggingTime("EmailCredentials.ReadFileEmailCredential() " + ex.ToString()));
                    Console.WriteLine(ex.ToString());
                }
            }
            try
            {
                emailCredential = JsonSerializer.Deserialize<EmailCredentialStructure>(jsonEmailCredential);
                IsThereEmailCredentialsFile();
            }
            catch (Exception ex)
            {
                Trace.TraceError(FileHostData.GetLoggingTime("EmailCredentials.ReadFileEmailCredential() " + ex.ToString()));
                Console.WriteLine(ex.ToString());
                emailCredential.ResetEmailCredentialStructure();
                return false;
            }
            return true;
        }


        public bool IsThereEmailCredentialsFile()
        {
            emailCredential.SgSGproEmailCredentialFileExist = File.Exists(FileHostData.FullPathFolderEmailCredentialFileName);
            return emailCredential.SgSGproEmailCredentialFileExist;
        }
    }

}
