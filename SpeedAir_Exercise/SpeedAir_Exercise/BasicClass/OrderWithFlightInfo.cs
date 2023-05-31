using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedAir_Exercise.BasicClass
{
    class OrderWithFlightInfo : Order
    {
        int flightId;
        public OrderWithFlightInfo(Order order, int flightId) : base(order.getID(), order.getName(), order.getDestination().getAbbr())
        {
            this.flightId = flightId;
        }

        public int GetFlightID()
        {
            return flightId;
        }
    }
}
