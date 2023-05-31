using System;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;
using System.Linq;

namespace SpeedAir_Exercise
{
    class FlightItinerary
    {
        List<Flight> flights;
        List<OrderRaw> failedOrders;
        public FlightItinerary(List<Flight> flights, List<OrderRaw> failedOrders)
        {
            this.flights = flights;
            this.failedOrders = failedOrders;
        }

        public void ordersShowFlightInfo()
        {
            List<OrderWithFlightInfo> allOrder = new List<OrderWithFlightInfo>();
            foreach (Flight flight in flights)
            {
                Order[] orders = flight.GetBox();
                foreach (Order order in orders)
                {
                    if (order == null) break; // flight box could be not full
                    allOrder.Add(new OrderWithFlightInfo(order, flight.GetID()));
                }
            }
            // print all assigned orders
            foreach (OrderWithFlightInfo order in allOrder.OrderBy(f => f.getID()))
            {
                Flight flight = flights.Where(x => x.GetID() == order.GetFlightID()).FirstOrDefault();
                Console.WriteLine("order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}", order.getName(), flight.GetID(), flight.GetFromCity().getAbbr(), flight.GetToCity().getAbbr(), flight.GetDay());
            }
            // print all orders failed to assign
            foreach (OrderRaw order in failedOrders)
            {
                Console.WriteLine("order: {0}, flightNumber: not scheduled", order.getRawName());
            }
            // print all orders with bad information
        }
    }
}
