using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedAir_Exercise.BasicClass
{
    class OrderRaw
    {
        string rawName;
        string rawDestination;
        public OrderRaw(string name, string destination)
        {
            this.rawName = name;
            this.rawDestination = destination;
        }

        public string getRawName()
        {
            return rawName;
        }
        public string getDestinationRaw()
        {
            return rawDestination;
        }
    }
}
