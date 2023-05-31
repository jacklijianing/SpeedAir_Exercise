using System;
using System.IO;
using System.Collections.Generic;
using SpeedAir_Exercise.BasicClass;

namespace SpeedAir_Exercise
{
    class Program
    {
        private static string projectDirectory;
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            // read flight information, story 1
            List<Flight> flights = readFlightFILE();
            // change readFlightFILE to readFlightJSON to try to load flight in JSON file

            // show flights out
            foreach (Flight flight in flights)
            {
                FlightPrinter printer = new FlightPrinter(flight);
                printer.PrintFlightOnCMD();
            }

            // read orders

            string orderDataFileDirectory = Path.Combine(projectDirectory, "Data/coding-assigment-orders.json");
            OrderLoader_JSON orderLoader = new OrderLoader_JSON(orderDataFileDirectory);
            orderLoader.LoadOrders();
            List<Order> orders = orderLoader.getSucceedOrders();
            Console.WriteLine("Orders loaded, succeed: {0}, error: {1}", orderLoader.getSucceedCount(), orderLoader.getFailedCount());
            List<OrderRaw> failedOrders = orderLoader.getFailedOrders();

            // end of read orders

            // allocate orders to flights
            OrderAllocator allocator = OrderAllocator.getInstance();
            allocator.Loadflights(flights);
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
            foreach (OrderRaw order in failedOrders)
            {
                Console.Write(order.getRawName() + ", ");
            }

            // Story 2: output orders with flight itinerary
            Console.WriteLine("Below are the orders with flight itinerary");
            FlightItinerary itinerary = new FlightItinerary(allocatedFlights, failedOrders);
            itinerary.ordersShowFlightInfo();
        }

        private static List<Flight> readFlightFILE()
        {
            // read flight information, story 1
            string flightDataFileDirectory = Path.Combine(projectDirectory, "Data/flight_info.txt");
            FlightLoader_File flightLoader = new FlightLoader_File(flightDataFileDirectory);
            List<Flight> flights = flightLoader.LoadFlights();
            return flights;
        }

        private static List<Flight> readFlightJSON()
        {
            // ANOTHER METHOD TO READ, story 1
            // THIS PART IS OF NO USE IN THIS CODE, JUST BECAUSE STORY 1 DIDN'T SAY WHAT KIND OF INPUT FILE IT COULD BE, SO I CREATED 2 WAYS
            // read flight information from JSON file
            string flightDataFileDirectory = Path.Combine(projectDirectory, "Data/flight_info.json");
            FlightLoader_JSON flightLoader = new FlightLoader_JSON(flightDataFileDirectory);
            List<Flight> flights = flightLoader.LoadFlights();
            return flights;

        }
    }
}
