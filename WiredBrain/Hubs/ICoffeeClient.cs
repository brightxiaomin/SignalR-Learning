using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrain.Models;

namespace WiredBrain.Hubs
{
    public interface ICoffeeClient
    {
        Task NewOrder(Order order);
        Task ReceiveOrderUpate(UpdateInfo info);
        Task Finished(Order order);
    }
}
