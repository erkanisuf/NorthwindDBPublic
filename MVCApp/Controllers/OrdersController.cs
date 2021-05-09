using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            var northwindDB = _context.Orders.Include(el => el.Customer).Include(el => el.Employee).Include(el => el.ShipViaNavigation);


            return View(await northwindDB.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = _context.OrderDetails.Include(el=>el.Order).Include(el=>el.Product).FirstOrDefaultAsync(el => el.OrderId == id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return View(await orderdetail);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Orders.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "ContactName", product.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", product.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", product.ShipVia);
            return View(product);
        }
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "ContactName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName");

            return View();
        }

    }
}
