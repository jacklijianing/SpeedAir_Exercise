using System;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;
using System.Linq;

namespace SpeedAir_Exercise
{
    class OrderAllocator
    {
        // use SingletonPattern to make sure there is only one allocator

        private static OrderAllocator instance = new OrderAllocator();

        private List<Flight> flights;

        private OrderAllocator() 
        { 
        }

        public static OrderAllocator getInstance()
        {
            return instance;
        }

        public void Loadflights(List<Flight> flights)
        {
            instance.flights = flights;
        }

        public bool AllocateOrder(Order order)
        {
            foreach (Flight flight in instance.flights.Where(x =>
                x.GetFromCity().getAbbr().Equals(order.getDeparture().getAbbr())
                &&
                x.GetToCity().getAbbr().Equals(order.getDestination().getAbbr())
                &&
                x.BoxHasRoom()
                ).OrderBy(x => x.GetDay())
                // find the first flight which has room and has same departure and destination
            )
            {
                // because we have checked the flight box has room, here must return true
                return flight.AddOrder(order);
            }
            // cannot find any flight can hold this order, return false
            return false;
        }

        public List<Flight> getFlightsInfo()
        {
            return instance.flights;
        }
    }
}
