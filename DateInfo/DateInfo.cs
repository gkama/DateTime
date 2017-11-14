using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Amazon.Lambda.Core;

namespace DateInfo
{
    public class DateInfo
    {
        public string input { get; set; }
        public ILambdaContext context { get; set; }
        public int yearInt { get; set; }
        public int monthInt { get; set; }
        public int dayInt { get; set; }
        public DateInfo(string input, ILambdaContext context)
        {
            this.input = input;
            this.context = context;
        }

        //Get the info
        public string getDateInfo()
        {
            //try-catch Makes sure it's exact format
            try
            {
                DateTime inputDateTime = DateTime.ParseExact(input,
                                            "yyyyMMdd",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None);
                this.yearInt = inputDateTime.Year;
                this.monthInt = inputDateTime.Month;
                this.dayInt = inputDateTime.Day;
                //Date formatted to be JSON serialized and returned
                //Day format
                ///
                /// "d"	The day of the month, from 1 through 31.
                /// "dd"	The day of the month, from 01 through 31.
                /// "ddd"	The abbreviated name of the day of the week.
                /// "dddd"	The full name of the day of the week.
                ///
                day d = new day()
                {
                    d = inputDateTime.ToString("%d"),
                    dd = inputDateTime.ToString("dd"),
                    ddd = inputDateTime.ToString("ddd"),
                    dddd = inputDateTime.ToString("dddd")
                };


                //Month format
                ///
                /// "M"	The month, from 1 through 12.
                /// "MM"	The month, from 01 through 12.
                /// "MMM"	The abbreviated name of the month.
                /// "MMMM"	The full name of the month.
                ///
                month M = new month()
                {
                    m = inputDateTime.ToString("%M"),
                    mm = inputDateTime.ToString("MM"),
                    mmm = inputDateTime.ToString("MMM"),
                    mmmm = inputDateTime.ToString("MMMM")
                };


                //Formats
                formats f = new formats()
                {
                    yyyyMMdd = inputDateTime.ToString("yyyyMMdd"),
                    yyyyMM = inputDateTime.ToString("yyyyMM"),
                    yyyy_MM_dd = inputDateTime.ToString("yyyy-MM-dd"),
                    yyyy_MM = inputDateTime.ToString("yyyy-MM")
                };
                DateFormat dt = new DateFormat()
                {
                    en = inputDateTime.ToString("MMMM %d, yyyy"),
                    en_short = inputDateTime.ToString("MMM %d, yyyy"),
                    en_full = inputDateTime.ToString("dddd, MMMM %d, yyyy"),
                    date = inputDateTime.ToString("MM/dd/yyyy"),
                    date_eu = inputDateTime.ToString("dd/MM/yyyy"),
                    era = inputDateTime.ToString("%g"),
                    timezone = inputDateTime.ToString("%K"),
                    day = d,
                    month = M,
                    formats = f
                };

                //Serialized return
                string serializedReturn = JsonConvert.SerializeObject(dt, Formatting.Indented);
                //Log
                context.Logger.Log(serializedReturn);
                //Return
                return serializedReturn;
            }
            catch (Exception e) { throw e; }
        }


        //Formatted date
        public class DateFormat
        {
            public string en;
            public string en_short;
            public string en_full;
            public string date;
            public string date_eu;
            public string era;
            public string timezone;

            public day day;
            public month month;
            public formats formats;

        }
        public class formats
        {
            public string yyyyMMdd;
            public string yyyyMM;
            public string yyyy_MM_dd;
            public string yyyy_MM;
        }
        public class day
        {
            public string d;
            public string dd;
            public string ddd;
            public string dddd;
        }
        public class month
        {
            public string m;
            public string mm;
            public string mmm;
            public string mmmm;
        }

        public class trivia
        {
            public string year_trivia;
            public string day_trivia;
            public string month_trivia;
        }
    }
}
