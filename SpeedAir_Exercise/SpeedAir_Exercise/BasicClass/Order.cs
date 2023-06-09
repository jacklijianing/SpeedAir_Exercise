﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedAir_Exercise.BasicClass
{
    class Order : OrderRaw
    {
        int id;
        string name;
        City departure;
        City destination;

        private string DEFAULT_DEPARTURE_CITY = "YUL";
        public Order(int id, string name, string destination) : base(name, destination)
        {
            this.id = id;
            this.name = name;
            this.departure = new City(DEFAULT_DEPARTURE_CITY); // In this problem all orders are departing from Montreal. Change this line if in future we want to add more possible departures.
            this.destination = new City(destination);
        }

        public int getID()
        {
            return id;
        }

        public City getDeparture()
        {
            return departure;
        }

        public City getDestination()
        {
            return destination;
        }

        public string getName()
        {
            return name;
        }
    }
}
