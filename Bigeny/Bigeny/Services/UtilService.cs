using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
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

        public static byte[] GetMD5Checksum(object obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                using (var hashMethod = SHA384.Create())
                {
                    binaryFormatter.Serialize(stream, obj);
                    return stream.ToArray();
                    //return hashMethod.ComputeHash(stream);
                }
            }
        }
    }
}
