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
            List<Flight> flights = new List<Flight>();
            // read flight information
            // story 1 will update this to better design
            flights.Add(new Flight(0, "YUL", "YYZ", 1));
            flights.Add(new Flight(1, "YUL", "YYC", 1));
            flights.Add(new Flight(2, "YUL", "YVR", 1));
            flights.Add(new Flight(3, "YUL", "YYZ", 2));
            flights.Add(new Flight(4, "YUL", "YYC", 2));
            flights.Add(new Flight(5, "YUL", "YVR", 2));

            // read orders
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string dataFileDirectory = Path.Combine(projectDirectory, "Data/coding-assigment-orders.json");
            OrderLoader_JSON loader = new OrderLoader_JSON(dataFileDirectory);
            List<Order> orders = loader.LoadOrders();
            Console.WriteLine("Orders loaded, succeed: {0}, failed: {1}", orders.Count, loader.getLastFailedCount());

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

            // Story 2: output orders with flight itinerary
            Console.WriteLine("Below are the orders with flight itinerary");
            FlightItinerary itinerary = new FlightItinerary(allocatedFlights, failedOrders);
            itinerary.ordersShowFlightInfo();
        }
    }
}
