using System.IO;
using System;
using System.Configuration;
using System.Text;

namespace Qiyas.BusinessLogicLayer
{
    public class Utilities
    {
        public static string ArabicDateTime(DateTime DT)
        {
            return ArabicDateTime(DT, true);
        }

        public static string ArabicDateTime(DateTime DT, bool showTime)
        {
            System.Globalization.DateTimeFormatInfo DTFormat = new System.Globalization.CultureInfo("ar-eg", false).DateTimeFormat;
            string DayName = DTFormat.GetDayName(DT.DayOfWeek);
            int DayInt = DT.Day;
            string MonthName = DTFormat.GetMonthName(DT.Month);
            int year = DT.Year;
            string ArTime = DT.ToShortTimeString();
            if (ArTime.IndexOf("PM") != -1)
            {
                ArTime = ArTime.Replace("PM", DTFormat.PMDesignator);
            }
            else if (ArTime.IndexOf("AM") != -1)
            {
                ArTime = ArTime.Replace("AM", DTFormat.AMDesignator);
            }
            string arabicD = DayName + " " + DayInt.ToString() + " " + MonthName + " " + year.ToString() + (showTime ? ArTime + " " : "");
            return arabicD;
        }

        public static string GetHashedScript(string hashKey)
        {
            return "<script type='text/javascript' language='javascript'>window.location.hash = '#" + hashKey + "';</script>";
        }
    }
}
