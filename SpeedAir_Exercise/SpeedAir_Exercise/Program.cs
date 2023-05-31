using System;
using System.IO;
using System.Collections.Generic;
using SpeedAir_Exercise.BasicClass;

namespace SpeedAir_Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            // read flight information
            string flightDataFileDirectory = Path.Combine(projectDirectory, "Data/flight_info.txt");
            FlightLoader_File flightLoader = new FlightLoader_File(flightDataFileDirectory);
            List<Flight> flights = flightLoader.LoadFlights();
            // show flights out

            foreach (Flight flight in flights)
            {
                FlightPrinter printer = new FlightPrinter(flight);
                printer.PrintFlightOnCMD();
            }

            // read flight information from JSON file
            string flightDataFileDirectory2 = Path.Combine(projectDirectory, "Data/flight_info.json");
            FlightLoader_JSON flightLoader2 = new FlightLoader_JSON(flightDataFileDirectory2);
            List<Flight> flights2 = flightLoader2.LoadFlights();
            // show flight2 information out

            foreach (Flight flight in flights2)
            {
                FlightPrinter printer = new FlightPrinter(flight);
                printer.PrintFlightOnCMD();
            }

            // read orders

            string orderDataFileDirectory = Path.Combine(projectDirectory, "Data/coding-assigment-orders.json");
            OrderLoader_JSON orderLoader = new OrderLoader_JSON(orderDataFileDirectory);
            List<Order> orders = orderLoader.LoadOrders();
            Console.WriteLine("Orders loaded, succeed: {0}, failed: {1}", orders.Count, orderLoader.getLastFailedCount());

            // allocate orders to flights
            OrderAllocator allocator = OrderAllocator.getInstance();
            allocator.Loadflights(flights);
            List<Order> failedOrders = new List<Order>();
            foreach (Order order in orders)
            {
                if (!allocator.AllocateOrder(order))
                {
                    failedOrders.Add(order);
                }
            }

            // output the flight box result
            List<Flight> allocatedFlights = allocator.getFlightsInfo();
            foreach (Flight flight in allocatedFlights)
            {
                Console.WriteLine("Below are the boxes carried by flight from {0} to {1} in day {2}", flight.GetFromCity().getAbbr(), flight.GetToCity().getAbbr(), flight.GetDay());
                foreach (Order order in flight.GetBox())
                {
                    if (order == null) break; // this flight still have empty box, just stop printing
                    Console.Write(order.getName() + ", ");
                }
                Console.WriteLine();
            }

            // output the failed information
            Console.WriteLine("Below are the orders not carried by any flights");
            foreach (Order order in failedOrders)
            {
                Console.Write(order.getName() + ", ");
            }
        }
    }
}
