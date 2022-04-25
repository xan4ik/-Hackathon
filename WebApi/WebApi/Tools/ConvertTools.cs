using System;

namespace WebApi.Tools
{
    public static class ConvertTools 
    {
        public static TimeSpan GetTimeSpanOrDefault(this string value)
        {
            if (TimeSpan.TryParse(value, out TimeSpan result))
            {
                return result;
            }
            return TimeSpan.Zero;
        }

        public static int GetIntOrDefault(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return 0;
        }

        public static float GetFloatOrDefault(this string value)
        {
            if (float.TryParse(value, out float result))
            {
                return result;
            }
            return 0f;
        }

        public static DateTime GetDateTimeOrDefault(this string value)
        {
            if (DateTime.TryParse(value, out DateTime result))
            {
                return result;
            }
            return new DateTime(0);
        }
    }

}
