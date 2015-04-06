using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qiyas.BusinessLogicLayer
{
    public class Common
    {
        #region Application Settings Keys
        //DefaultLanguageId
        public static string DefaultLanguageId
        {
            get
            {
                return GetSecuredString("DefaultLanguageId"); //return System.Configuration.ConfigurationManager.AppSettings["DefaultLanguageId"]; 
            }
        }
        public static string DefaultLanguageKey
        {
            get
            {
                return GetSecuredString("DefaultLanguage");
                //return System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"]; 
            }
        }


        public static string DefaultCountryKey
        {
            get
            {
                return GetSecuredString("DefaultCountry");
                //return System.Configuration.ConfigurationManager.AppSettings["DefaultCountry"]; 
            }
        }

        public static string PersonContentPath
        {
            get
            {
                return GetSecuredString("PersonContentPath");
                //return System.Configuration.ConfigurationManager.AppSettings["PersonContentPath"]; 
            }
        }

        public static string ReceivingUser
        {
            get
            {
                return GetSecuredString("ReceivingUser");
                //return System.Configuration.ConfigurationManager.AppSettings["ReceivingUser"]; 
            }
        }

        public static string FromEmail
        {
            get
            {
                return GetSecuredString("FromMail");
            }
        }

        public static string MailServer
        {
            get
            {
                return GetSecuredString("MailServer");
            }
        }

        public static string MailUser
        {
            get
            {
                return GetSecuredString("MailUser");
            }
        }

        public static string MailPort
        {
            get
            {
                return GetSecuredString("MailPort");
            }
        }

        public static string MailPassword
        {
            get
            {
                return GetSecuredString("MailPassword");
            }
        }

        public static string EnableSSL
        {
            get
            {
                return GetSecuredString("EnableSSL");
            }
        }

        public static string SendGridUserName
        {
            get
            {
                return GetSecuredString("SendGridUserName");
            }
        }


        public static string SendGridPassword
        {
            get
            {
                return GetSecuredString("SendGridPassword");
            }
        }

        public static bool UseSendGrid
        {
            get
            {
                return Convert.ToBoolean(GetSecuredString("UseSendGrid"));
            }
        }



        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("APZ@T7EC");
        private static string GetSecuredString(string Key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Key];
        }


        private static bool IsStringEncrypted(string str)
        {
            bool result = false;
            int num = 0;
            if (str.Length == 0)
                return result;
            Int32.TryParse(str.Substring(0, 1), out num);
            if (str.Length > 1 && num >= 5 && num <= 9)
            {
                result = true;
            }
            return result;
        }
        #endregion
    }
}
