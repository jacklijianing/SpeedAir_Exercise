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
        int succeedCount = 0; // save the succeed count so that if we want to load multiple files in future, the ID can continue
        int lastFailedCount = 0;
        int totalFailedCount = 0;
        public OrderLoader_JSON(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public int getLastFailedCount()
        {
            return lastFailedCount;
        }
        public int getTotalFailedCount()
        {
            return totalFailedCount;
        }
        public List<Order> LoadOrders()
        {
            lastFailedCount = 0;
            List<Order> result = new List<Order>();
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
                        result.Add(order);
                    }
                    catch (Exception e)
                    {
                        // We will get exception when the destination city abbreviation does not exist
                        lastFailedCount++;
                        totalFailedCount++;
                    }
                }

                return result;
            }
        }
    }
}
