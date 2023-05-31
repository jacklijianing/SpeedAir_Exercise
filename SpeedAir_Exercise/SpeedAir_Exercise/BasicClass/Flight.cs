using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedAir_Exercise.BasicClass
{
    class Flight
    {
        int id;
        City fromCity;
        City toCity;
        Order[] box;
        int currentOrders;
        int day; // we just use number to represent which day. In real life, this could be defined as DateTime. 

        private int BOX_CAPACITY = 20;
        public Flight(int id, string from, string to, int day)
        {
            this.id = id;
            // from and to should be the abbr
            fromCity = new City(from);
            toCity = new City(to);
            box = new Order[BOX_CAPACITY];
            currentOrders = 0;
            this.day = day;
        }

        public City GetFromCity()
        {
            return fromCity;
        }

        public City GetToCity()
        {
            return toCity;
        }

        public int GetDay()
        {
            return day;
        }

        public bool BoxHasRoom()
        {
            if (currentOrders < BOX_CAPACITY)
                return true;
            else return false;
        }

        public Order[] GetBox()
        {
            return box;
        }

        public bool AddOrder(Order order)
        {
            if (BoxHasRoom())
            {
                box[currentOrders] = order;
                currentOrders++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
