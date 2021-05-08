using MVCApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class OrdersJoined
    {
        public Order order { get; set; }
        public OrderDetail orderdetail { get; set; }
        public Customer customer { get; set; }
    }
}
