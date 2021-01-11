using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceCommon.Infrastructure.Helper
{
    public class TimeHelper
    {
        public static string GetDayOfWeek(DateTime date)
        {
            string dayOfWeek = date.DayOfWeek.ToString();
            switch (dayOfWeek)
            {
                case "Monday": return "Thứ hai";
                case "Tuesday": return "Thứ Ba";
                case "Wednesday": return "Thứ tư";
                case "Thursday": return "Thứ năm";
                case "Friday": return "Thứ sáu";
                case "Saturday": return "Thứ bảy";
                case "Sunday": return "Chủ nhật";
                default: return "";
            }
        }
        public static string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
