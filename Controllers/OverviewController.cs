using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using northwind_app.Library.Models;
using northwind_app.ViewModels.Overview;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace northwind_app.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class OverviewController : Controller
    {
        CustomerService customerService = new CustomerService();
        OrderService orderService = new OrderService();

        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View(new CustomersViewModel{
                Customers = customerService
                    .AllCustomers()
                    .Select(c => new CustomerViewModel{
                        ContactName = c.ContactName,
                        City = c.City,
                        CustomerId = c.CustomerId
                    })
                    .ToList(),
                TitlePage = "Customers",
                TitleCity = "City",
                TitleName = "ContactName"
            });
        }

        [Route("Customers/{customerId}/Orders")]
        public IActionResult GetOrdersByCustomerId(string customerId)
        {
            return View(new OrdersViewModel{
                Orders = orderService
                    .GetOrdersByCustomerId(customerId)
                    .OrderBy(o => o.OrderDate)
                    .Select(c => new OrderViewModel{
                        OrderDate = c.OrderDate,
                        Address = c.ShipAddress
                    })
                    .ToList(),
                TitlePage = "Orders",
                TitleDate = "Date",
                TitleAddress = "Address"
            });
        }

        [Route("CreateOrder")]
        public IActionResult CreateOrder()
        {
            return View();
        }
        
        [Route("")]
        private IActionResult StoreSearchQuery(string query){
            List<string> previousQueries = GetPreviousQueries();
            if (!previousQueries.Contains(query)) {
                previousQueries.Add(query);
            }
            HttpContext.Session.SetString("SearchQueries", JsonSerializer.Serialize(previousQueries));
            return View(new CustomersViewModel{
                Customers = customerService
                    .AllCustomers()
                    .Select(c => new CustomerViewModel{
                        ContactName = c.ContactName,
                        City = c.City,
                        CustomerId = c.CustomerId
                    })
                    .ToList(),
                TitlePage = "Customers",
                TitleCity = "City",
                TitleName = "ContactName",
                Searched = previousQueries
            });
        }

        private List<string> GetPreviousQueries() {
            string previousQueriesJson = HttpContext.Session.GetString("SearchQueries");
            List<string> previousQueries = new List<string>();
            if (!string.IsNullOrEmpty(previousQueriesJson)) {
                previousQueries = JsonSerializer.Deserialize<List<string>>(previousQueriesJson);
            }
            return previousQueries;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[action]")]
        public IActionResult SaveProduct(OrderViewModel order) {
            if (ModelState.IsValid) {
                OrderService service = new OrderService();
                service.Add(order.OrderDate, order.Address, order.CustomerId);
            }
            ModelState.AddModelError("", "Please fill in all required fields.");
            return View("CreateOrder", order);
        }
    }
}