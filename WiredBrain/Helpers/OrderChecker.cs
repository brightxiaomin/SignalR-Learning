using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WiredBrain.Models;

namespace WiredBrain.Helpers
{
    public class OrderChecker
    {
        private readonly Random random;

        private readonly string[] Status =
            {"Grinding beans", "Steamding milk", "Taking a sip", "On transmission", "picked up"};

        private readonly Dictionary<int, int> StatusTracker = new Dictionary<int, int>();

        public OrderChecker(Random random)
        {
            this.random = random;                
        }

        public int GetNextStatusIndex(int orderNo)
        {
            if (!StatusTracker.ContainsKey(orderNo))
                StatusTracker.Add(orderNo, -1);

            StatusTracker[orderNo]++;

            return StatusTracker[orderNo];
        }

        public UpdateInfo GetUpdate(Order order)
        {
            if (random.Next(1, 5) != 4) return new UpdateInfo { New = false };

            var index = GetNextStatusIndex(order.Id);

            if (Status.Length <= index) return new UpdateInfo { New = false };

            var result = new UpdateInfo
            {
                OrderId = order.Id,
                New = true,
                Update = Status[index],
                Finished = Status.Length - 1 == index
            };
            return result;
        }
    }
}