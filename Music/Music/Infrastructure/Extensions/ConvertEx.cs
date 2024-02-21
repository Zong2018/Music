using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.Extensions
{
    /// <summary>
    /// string的扩展方法
    /// </summary>
    public static class ConvertEx
    {
        //String
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        public static int ToInt32(this string s, int defVal = 0)
        {
            int val = 0;
            if (int.TryParse(s.Trim(), out val))
                return val;
            return defVal;
        }

        public static decimal ToDecimal(this string s, decimal defVal = 0)
        {
            decimal val = 0;
            if (decimal.TryParse(s, out val))
                return val;
            return defVal;
        }

        public static bool ToBool(this string s, bool defVal = false)
        {
            bool val = false;
            if (bool.TryParse(s, out val))
                return val;
            return defVal;
        }

        //Jtoken

        public static string ToStr(this JToken s, string defVal = "")
        {
            if (s == null)
                return defVal;

            return s.ToString();
        }

        public static int ToInt32(this JToken s, int defVal = 0)
        {
            if (s == null)
                return defVal;

            return s.ToString().ToInt32(defVal);
        }

        public static decimal ToDecimal(this JToken s, decimal defVal = 0)
        {
            if (s == null)
                return defVal;

            return s.ToString().ToDecimal(defVal);
        }

        public static bool ToBool(this JToken s, bool defVal = false)
        {
            if (s == null)
                return defVal;
            return s.ToString().ToBool();
        }
    }
}
