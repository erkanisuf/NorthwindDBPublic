using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class OrdersController : Controller
    {

        private readonly NorthwindDB _context;

        public OrdersController(NorthwindDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Order> orders = _context.Orders.ToList();
            List<OrderDetail> orderdetails = _context.OrderDetails.ToList();
            List<Customer> customer = _context.Customers.ToList();
            var innerjoin = from a in orders
                            join b in orderdetails on a.OrderId equals b.OrderId 
                            join c in customer on a.CustomerId equals c.CustomerId
                            select new OrdersJoined{ order = a, orderdetail=b ,customer=c};
            return View(innerjoin.ToList());
        }
    }
}
