using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SpeedAir_Exercise.BasicClass;
using Newtonsoft.Json.Linq;


namespace SpeedAir_Exercise
{
    class OrderLoader_JSON : IOrderLoader
    {
        string fileLocation;
        int succeedCount = 0;
        int failedCount = 0;
        List<Order> succeedOrders;
        List<OrderRaw> failedOrders;
        public OrderLoader_JSON(string fileLocation)
        {
            this.fileLocation = fileLocation;
            succeedOrders = new List<Order>();
            failedOrders = new List<OrderRaw>();
        }

        public int getFailedCount()
        {
            return failedCount;
        }
        public int getSucceedCount()
        {
            return succeedCount;
        }

        public List<Order> getSucceedOrders()
        {
            return succeedOrders;
        }

        public List<OrderRaw> getFailedOrders()
        {
            return failedOrders;
        }
        public void LoadOrders()
        {
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                JObject _jObject = JObject.Parse(json);

                foreach (JProperty item in _jObject.Children())
                {
                    try
                    {
                        // item example: "order-001": {"destination" : "YYZ"}
                        // item.Value is  {"destination" : "YYZ"},
                        Order order = new Order(succeedCount, item.Name, item.Value["destination"].ToString());
                        succeedCount++;
                        succeedOrders.Add(order);
                    }
                    catch (Exception e)
                    {
                        // We will get exception when the destination city abbreviation does not exist
                        failedCount++;
                        failedOrders.Add(new OrderRaw(item.Name, item.Value["destination"].ToString()));
                    }
                }

                r.Close();
            }
        }
    }
}
