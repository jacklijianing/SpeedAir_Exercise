using System;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;

namespace SpeedAir_Exercise
{
    class FlightPrinter
    {
        private Flight flight;
        public FlightPrinter(Flight flight)
        {
            this.flight = flight;
        }

        public void PrintFlightOnCMD()
        {
            Console.WriteLine("Flight: {0}, departure: {1}, arrival: {2}, day: {3}", flight.GetID(), flight.GetFromCity().getAbbr(), flight.GetToCity().getAbbr(), flight.GetDay());
        }
    }
}
