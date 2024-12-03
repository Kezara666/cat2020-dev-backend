using System.Globalization;

namespace CAT20.WebApi.Helpers
{
    public static class Utility
    {
        /// <summary> 
        /// Converts a Timestamp to a string . 
        /// </summary> 
        /// <param name="bytes">Array of bytes to be converted.</param> 
        /// <returns>string</returns> 
        public static string TimeStampToString(byte[] t)
        {
            return String.Format("{0}@{1}@{2}@{3}@{4}@{5}@{6}@{7}@",
                t[0].ToString(), t[1].ToString(), t[2].ToString(),
                t[3].ToString(), t[4].ToString(), t[5].ToString(),
                t[6].ToString(), t[7].ToString());
        }

        /// <summary> 
        /// Converts a string to a Timestamp. 
        /// </summary> 
        /// <param name="bytes">string to be converted.</param> 
        /// <returns>byte[] timestamp</returns> 
        public static byte[] StringToTimeStamp(string str)
        {
            byte[] t = new byte[8];
            int index = 0;
            while (!str.Equals(string.Empty))
            {
                t[index] = byte.Parse(str.Substring(0, str.IndexOf("@")));
                str = str.Substring(str.IndexOf("@") + 1);
                index++;
                if (index == 8)
                    break;
            }
            return t;
        }

        /// <summary> 
        /// Converts a client side Timestamp to string. 
        /// </summary> 
        /// <param name="bytes">string to be converted.</param> 
        /// <returns>datetime date</returns> 
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// Parse value to integer. Will return null if not an integer
        /// </summary>
        /// <param name="val">Parse String</param>
        /// <returns>Converted Value</returns>
        public static int? ParseInt(string val)
        {
            int convertuedValue;
            bool res = int.TryParse(val, out convertuedValue);
            if (res == false)
                return null;
            else
                return convertuedValue;
        }

        /// <summary>
        /// Parse value to integer. Will return null if not an decimal
        /// </summary>
        /// <param name="val">Parse String</param>
        /// <returns>Converted Value</returns>
        public static decimal? ParseDecimal(string val)
        {
            decimal convertedValue;
            bool res = decimal.TryParse(val, out convertedValue);
            if (res == false)
                return null;
            else
                return convertedValue;
        }

        /// <summary>
        /// Parse value to Datetime. Will return null if not a date
        /// </summary>
        /// <param name="val">Parse String</param>
        /// <returns>Converted Value</returns>
        public static DateTime? ParseDate(string val)
        {
            DateTime convertedDate;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

            bool res = DateTime.TryParse(val, culture, DateTimeStyles.None, out convertedDate);
            if (res == false)
                return null;
            else
                return convertedDate;
        }

        /// <summary>
        /// Parse value to Datetime. Will return empty if null
        /// </summary>
        /// <param name="date">Parse Datetime</param>
        /// <returns>Converted Date String</returns>
        public static string GetDateString(DateTime? date)
        {
            if (date == null)
                return string.Empty;
            else
                return date.Value.ToString();
        }
    }
}
