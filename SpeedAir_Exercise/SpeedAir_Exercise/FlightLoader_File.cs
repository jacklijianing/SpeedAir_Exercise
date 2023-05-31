using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;
using Newtonsoft.Json.Linq;

namespace SpeedAir_Exercise
{
    class FlightLoader_File : IFlightLoader
    {
        private string fileLocation;
        public FlightLoader_File(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }
        public List<Flight> LoadFlights()
        {
            // read data from file with this format in each line:
            // id,departure,arrival,day delimited by comma
            // example: 0,YUL,YYZ,1
            List<Flight> result = new List<Flight>();
            using (StreamReader file = new StreamReader(fileLocation))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    string[] data = ln.Split(',');
                    if (data.Length != 4)
                    {
                        // bad data, skip
                        continue;
                    }
                    else
                    {
                        int id;
                        int day;
                        if (!int.TryParse(data[0], out id)) continue;
                        if (!int.TryParse(data[3], out day)) continue;
                        Flight f = new Flight(id, data[1], data[2], day);
                        result.Add(f);
                    }

                }
                file.Close();
                return result;
            }
        }
    }
}
