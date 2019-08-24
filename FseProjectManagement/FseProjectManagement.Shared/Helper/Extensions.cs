using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Shared.Helper
{
    public static class Extensions
    {
        public static string ToNullString(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value;

            return string.Empty;
        }

        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static DateTime? YYYYMMDDToDate(this string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr)) return null;
            return DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public static string DateToYYYYMMDD(this DateTime? date)
        {
            if (date == null) return string.Empty;

            return ((DateTime)date).ToString("yyyyMMdd");
        }
    }
}
