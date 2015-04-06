using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Qiyas.BusinessLogicLayer
{
    public class Tools
    {
        public static string TranslateNumerals(string sIn)
        {
            return TranslateNumerals(sIn, false);
        }
        //this method will convert all english numbers in string into arabic format
        public static string TranslateNumerals(string sIn, bool IsDate)
        {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decoder;
            utf8Decoder = enc.GetDecoder();
            StringBuilder sTranslated = new StringBuilder();
            char[] cTransChar = new char[1];
            byte[] bytes = new byte[] { 217, 160 };
            char[] aChars = sIn.ToCharArray();
            foreach (char c in aChars)
            {
                if (char.IsDigit(c))
                {
                    bytes[1] = Convert.ToByte(160 + Convert.ToInt32((char.GetNumericValue(c))));
                    utf8Decoder.GetChars(bytes, 0, 2, cTransChar, 0);
                    sTranslated.Append(cTransChar[0]);
                }
                else
                {
                    sTranslated.Append(c);
                }
            }
            if (IsDate)
            {
                string finalResult = "";
                string[] strSplit = sTranslated.ToString().Split(new char[] { '/' });
                for (int i = strSplit.Length - 1; i >= 0; i--)
                {
                    finalResult += strSplit[i] + "/";
                }
                return finalResult.Substring(0, finalResult.Length - 1);
            }
            else
                return sTranslated.ToString();


        }

        private static string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
        "dd/MM/yyyy","d/M/yyyy",
        "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
        "yyyy-M-d","dd-MM-yyyy","d-M-yyyy",
        "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
       "yyyy M d","dd MM yyyy","d M yyyy",
        "dd M yyyy","d MM yyyy"};

        public static DateTime HijriToGreg(string hijri)
        {
            CultureInfo enCul = new CultureInfo("en-US");
            CultureInfo arCul = new CultureInfo("ar-SA");
            DateTime tempDate = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            return tempDate;

        }


        public static string HijriToGregs(string hijri)
        {
            CultureInfo enCul = new CultureInfo("en-US");
            CultureInfo arCul = new CultureInfo("ar-SA");
            DateTime tempDate = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            return tempDate.ToString("yyyy/MM/dd", enCul.DateTimeFormat);

        }

        public static string GregToHijri(DateTime dtTime)
        {
            if (dtTime == DateTime.MinValue)
                return "";
            UmAlQuraCalendar calHijri = new UmAlQuraCalendar();
            string weekday = Convert.ToString(calHijri.GetDayOfWeek(dtTime));
            int CurrDayOfMonth = calHijri.GetDayOfMonth(dtTime);
            int CurrMonth = calHijri.GetMonth(dtTime);
            int Curryear = calHijri.GetYear(dtTime);
            return CurrDayOfMonth.ToString() + "/" + CurrMonth.ToString() + "/" + Curryear.ToString();

        }

        public static string GregToHijriCompleteWithDay(DateTime dtTime)
        {
            if (dtTime == DateTime.MinValue)
                return "";
            UmAlQuraCalendar calHijri = new UmAlQuraCalendar();
            CultureInfo info = new CultureInfo("ar-sa");
            info.DateTimeFormat.Calendar = calHijri;
            string weekday = Convert.ToString(calHijri.GetDayOfWeek(dtTime));
            int CurrDayOfMonth = calHijri.GetDayOfMonth(dtTime);
            int CurrMonth = calHijri.GetMonth(dtTime);
            int Curryear = calHijri.GetYear(dtTime);
            return string.Format("{0} {3}/{2}/{1}  هـ ", dtTime.ToString("dddd", info), CurrDayOfMonth.ToString(), CurrMonth.ToString(), Curryear.ToString());

        }

        public static string GregToHijriTime(DateTime dtTime)
        {
            if (dtTime == DateTime.MinValue)
                return "";
            UmAlQuraCalendar calHijri = new UmAlQuraCalendar();
            CultureInfo info = new CultureInfo("ar-sa");
            info.DateTimeFormat.Calendar = calHijri;
            string weekday = Convert.ToString(calHijri.GetDayOfWeek(dtTime));
            int CurrDayOfMonth = calHijri.GetDayOfMonth(dtTime);
            int CurrMonth = calHijri.GetMonth(dtTime);
            int Curryear = calHijri.GetYear(dtTime);
            return dtTime.ToString("hh:mm tt", info);

        }

        public static string GetGreggCompleteByDate(DateTime dtTime)
        {

            return dtTime.ToString("yyyy/MM/dd") + "  م  ";

        }
    }
}