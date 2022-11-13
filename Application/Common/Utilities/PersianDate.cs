using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Common
{
    public static class ShamsiDate
    {
        public static string GetShamsiDateNow()
        {
            DateTime dt = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            string c1 = string.Format("{0:0000}/{1:00}/{2:00}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt));
            return (c1);

        }
        public static string GetShamsiDatetimeNow()
        {
            DateTime dt = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            string c1 = string.Format("{0:0000}/{1:00}/{2:00}-{3:00}:{4:00}:{4:00}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt), dt.Hour, dt.Minute, dt.Second);
            return (c1);

        }
        public static string Getdate(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            string c1 = string.Format("{0:0000}/{1:00}/{2:00}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt));
            return (c1);

        }
        public static string Getdatetime(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            string c1 = string.Format("{0:0000}/{1:00}/{2:00}-{3:00}:{4:00}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt), dt.Hour, dt.Minute);
            return (c1);

        }
        public static string Getmount(string dt)
        {
            Dictionary<int, string> LettersDictionary = new Dictionary<int, string>
            {
                [1] = "فروردین",
                [2] = "اردیبهشت",
                [3] = "خرداد",
                [4] = "تیر",
                [5] = "مرداد",
                [6] = "شهریور",
                [7] = "مهر",
                [8] = "آبان",
                [9] = "آذر",
                [10] = "دی",
                [11] = "بهمن",
                [12] = "اسفند"
            };
            //PersianCalendar pc = new PersianCalendar();
            int month = Convert.ToInt32(dt.Substring(5, 2));
            string temp = "";
            temp = LettersDictionary[month];
            return (temp);
        }

        public static string Getday(DateTime dt)
        {

            Dictionary<string, string[]> DayOfWeeks = new Dictionary<string, string[]>();
            DayOfWeeks.Add("en", new string[] { "Saturday", "Sunday", "Monday", "Tuesday", "Thursday", "Wednesday", "Friday" });
            DayOfWeeks.Add("fa", new string[] { "یک شنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" });
            DayOfWeek a = dt.DayOfWeek;
            int temp = (int)a;
            string DayTemp = DayOfWeeks["fa"][temp];
            return (DayTemp);

        }

        public static string GetdayNumber(string dt)
        {
            return dt.Substring(8, 2);
        }

        public static string GetDateShamsiLong(DateTime dt)
        {
            string shamsi = Getdate(dt);
            return Getday(dt) + " " + GetdayNumber(shamsi) + " " + Getmount(shamsi) + " ماه " + shamsi.Substring(0, 4);
        }
        public static DateTime Getmiladi(string dt)
        {
            PersianCalendar pc = new PersianCalendar();
            //string c1 = string.Format("{0:0000}/{1:00}/{2:00}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt));

            int year = Convert.ToInt32(dt.Substring(0, 4));
            int month = Convert.ToInt32(dt.Substring(5, 2));
            int day = Convert.ToInt32(dt.Substring(8, 2));
            DateTime georgianDateTime = new DateTime(year, month, day, new PersianCalendar());
            return georgianDateTime;
        }
    }

}
