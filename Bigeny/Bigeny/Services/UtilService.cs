using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Bigeny.Services
{
    public class UtilService
    {
        public static string GetBeautyDate(DateTime date)
        {
            // Сегодня в 20:30
            // Вчера в 18:23
            // 12 декабря в 13:40

            string res = "";

            DateTimeFormatInfo info = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;
            if (date.Day == DateTime.Now.Day)
            {
                res += "Сегодня в ";
            }
            else if (date.Day == DateTime.Now.Day - 1)
            {
                res += "Вчера в ";
            }
            else
            {
                res += $"{date.Day} {info.MonthGenitiveNames[date.Month - 1]} ";
            }

            res += date.ToString("HH:mm");

            return res;
        }
    }
}
