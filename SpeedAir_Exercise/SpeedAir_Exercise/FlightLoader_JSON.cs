using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;
using Newtonsoft.Json;

namespace SpeedAir_Exercise
{
    class FlightLoader_JSON : IFlightLoader
    {
        private string fileLocation;
        public FlightLoader_JSON(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }
        public List<Flight> LoadFlights()
        {
            List<Flight> result = new List<Flight>();
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                List<FlightRaw> items = JsonConvert.DeserializeObject<List<FlightRaw>>(json);
                foreach (FlightRaw raw in items)
                {
                    result.Add(ConvertFlight(raw));
                }
                r.Close();
                return result;
            }
        }

        private Flight ConvertFlight(FlightRaw raw)
        {
            return new Flight(raw.id, raw.departure, raw.arrival, raw.day);
        }
    }
}
