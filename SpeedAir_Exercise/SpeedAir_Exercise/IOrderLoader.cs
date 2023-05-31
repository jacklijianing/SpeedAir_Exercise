using System;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;

namespace SpeedAir_Exercise
{
    interface IOrderLoader
    {
        public List<Order> LoadOrders();
    }
}
