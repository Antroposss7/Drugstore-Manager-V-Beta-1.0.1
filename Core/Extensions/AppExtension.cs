using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Extensions
{
    public static class AppExtension
    {
        public static bool IsEmail(this string text)
        {
            return Regex.IsMatch(text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static bool IsPositiveInt(this int number)
        {
            return number > 0;

        }
        public static bool CheckInt(this int value)
        {
            return value <= 0 && !value.ToString().Any(char.IsDigit); 
        }
        public static bool CheckString(this string str)
        {
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str) && !str.Any(char.IsDigit) && !int.TryParse(str, out _) && !double.TryParse(str, out _); //сохранить этот метод в заметках
        }
        public static bool IsPhoneNumber(this string text)
        {
            return Regex.IsMatch(text, @"^\+(?:[0-9] ?){6,14}[0-9]$");
        }
    }
}
