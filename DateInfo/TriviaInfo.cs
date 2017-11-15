using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DateInfo
{
    public class TriviaInfo
    {
        //Variables
        public string numbersAPI = "http://numbersapi.com/";
        HttpClient client = new HttpClient();

        private int yearInt { get; set; }
        private int monthInt { get; set; }
        private int dayInt { get; set; }
        private int dayOfYear { get; set; }

        //Constructor
        public TriviaInfo(int yearInt, int monthInt, int dayInt, int dayOfYear)
        {
            this.yearInt = yearInt;
            this.monthInt = monthInt;
            this.dayInt = dayInt;
            this.dayOfYear = dayOfYear;
        }

        public async Task<string> GetTrivia(string Case)
        {
            try
            {
                string url = numbersAPI;
                switch (Case)
                {
                    case "year":
                        url = string.Format(numbersAPI + "{0}/year", this.yearInt);
                        break;
                    case "date":
                        url = string.Format(numbersAPI + "{0}/{1}/date", this.monthInt, this.dayInt);
                        break;
                    case "month":
                        url = string.Format(numbersAPI + "{0}", this.monthInt);
                        break;
                    case "day":
                        url = string.Format(numbersAPI + "{0}", this.dayInt);
                        break;
                    case "dayOfYear":
                        url = string.Format(numbersAPI + "{0}", this.dayOfYear);
                        break;
                }
                var response = await client.GetByteArrayAsync(url);
                string toReturn = Encoding.ASCII.GetString(response, 0, response.Length - 1);
                return toReturn;
            } catch (Exception e) { throw e; }
        }
    }
}
