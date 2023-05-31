using System;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;

namespace SpeedAir_Exercise
{
    interface IOrderLoader
    {
        public void LoadOrders();

        public int getFailedCount();
        public int getSucceedCount();

        public List<Order> getSucceedOrders();
        public List<OrderRaw> getFailedOrders();
    }
}
