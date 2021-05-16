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
            var northwindDB = _context.Orders.Include(el => el.Customer).Include(el => el.Employee).Include(el => el.ShipViaNavigation).Where(el=>el != null);


            return View(await northwindDB.ToListAsync());
        }
        [HttpGet]
        // Fetch for the modal via JS;
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = _context.OrderDetails.Include(el=>el.Order).Include(el=>el.Product).Where(el => el.OrderId == id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            // returns array of order details
            return Ok(await orderdetail.ToListAsync());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Order editorder)
        {
            if (id != editorder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(editorder.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(editorder);
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "ContactName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
       

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OrdersJoined neworder)
        {
            // adds to orders and orders details at same time
            var items = neworder;
            var details = items.orderdetail;
            if (ModelState.IsValid)
            {
                _context.Add(items.order);
               var result =  await _context.SaveChangesAsync();
               var returnedID = items.order.OrderId;
                details.OrderId = returnedID;
                // if its first saved to orders table then it returns id wit the save and then adds the rest to orders detail.
                if (result > 0)
                {
                    
                    _context.Add(details);
                   await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "ContactName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // Edit order Detail Item
        public async Task<IActionResult> OrderDetailEdit(int? id)
        {
            ViewData["OrderId"] = id;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");


            return View();
        }
        // Add more items to Order Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetailEdit(string id, OrderDetail editorderDetail)
        {
            string searchParam = "product";
            string orderParam = "order";
            int index = id.IndexOf(searchParam); // finds index of 'product in href'
            int productid =  0;
            int orderid = 0;
            if (index != -1)
            {
                //DO YOUR LOGIC
               var result = id.Substring(index+ searchParam.Length); // and takes value after 'product' which are product`s id
               var resultOrder = id.Substring(id.IndexOf(orderParam) + orderParam.Length,5);

                int val = 0;
                if (Int32.TryParse(result, out val)){
                    productid = Convert.ToInt32(result);
                    orderid = Convert.ToInt32(resultOrder);
                    editorderDetail.ProductId = productid;
                    editorderDetail.OrderId = orderid;
                }
            }
           
            if (orderid != editorderDetail.OrderId)
            {
                return NotFound();
            }


            ModelState.Clear();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(editorderDetail.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(editorderDetail);
        }
        // Checks if orderDetail exists method
        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetailCreate(int id, OrderDetail newdetail)
        {
            var itemtodb = newdetail;
            itemtodb.OrderId = id;
            if (ModelState.IsValid)
            {
                _context.Add(itemtodb);
               var result =  await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        // Deletes Order
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Action of Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Checks if customer exists method
        private bool CustomerExists(string id)
        {
            return _context.Orders.Any(e => e.CustomerId == id);
        }


        // POST: DEL Order Detail item

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelDetail([FromBody]DeleteDetail deldetail)
        {
            var founditem = await _context.OrderDetails.Where(el=>el.ProductId == deldetail.bodyproductID).Where(el=>el.OrderId == deldetail.bodyorderID)
                .FirstAsync();
            _context.OrderDetails.Remove(founditem);
           var result =  await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(deldetail);
            }
            else {
                return NotFound();
            }

            
        }
        public class DeleteDetail { 
           public int bodyproductID { get; set; }
           public int bodyorderID { get; set; }
        }

    }
}
