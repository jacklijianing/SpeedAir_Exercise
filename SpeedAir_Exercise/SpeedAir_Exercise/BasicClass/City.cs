using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedAir_Exercise.BasicClass
{
    class City
    {
        string abbr;

        private string DEFAULT_ABBR = "";

        private Dictionary<string, string> CITYNAME;

        public City()
        {
            CITYNAME = new Dictionary<string, string>();
            // load the dictionary, in here we just set it one by one in code
            CITYNAME.Add("YUL", "Montreal");
            CITYNAME.Add("YYZ", "Toronto");
            CITYNAME.Add("YYC", "Calgary");
            CITYNAME.Add("YVR", "Vancouver");
            abbr = DEFAULT_ABBR;
        }

        public City(string abbr): this()
        {
            setAbbr(abbr);
        }

        public bool checkAbbrValid(string abbr)
        {
            if (CITYNAME.ContainsKey(abbr))
                return true;
            else return false;
        }

        public void setAbbr(string abbr)
        {
            if (checkAbbrValid(abbr))
                this.abbr = abbr;
            else throw new Exception("City abbreviation does not exist.");
        }

        public string getAbbr()
        {
            return this.abbr;
        }

        public string getCityName()
        {
            if (abbr == DEFAULT_ABBR)
            {
                throw new Exception("City abbreviation was not set correctly.");
            }
            return CITYNAME[abbr];
        }
    }
}
