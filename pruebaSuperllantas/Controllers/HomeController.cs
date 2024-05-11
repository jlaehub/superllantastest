using Microsoft.AspNetCore.Mvc;
using pruebaSuperllantas.Models;
using System.Diagnostics;
using pruebaSuperllantas.List;

namespace pruebaSuperllantas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly genericList<Branch> _branches;
        private readonly genericList<Company> _companies;
        private readonly genericList<Customer> _customers;
        private readonly genericList<Product> _products;
        private readonly genericList<Sale> _sales;
        private readonly genericList<User> _users;
        public HomeController(ILogger<HomeController> logger,
            genericList<Branch> brachCrud,
            genericList<Company> companyCrud,
            genericList<Customer> customerCrud,
            genericList<Product> productCrud,
            genericList<Sale> saleCrud,
            genericList<User> userCrud
            )
            
        {
            _logger = logger;
            _branches = brachCrud;
            _companies = companyCrud;
            _customers = customerCrud;
            _products = productCrud;
            _sales = saleCrud;
            _users = userCrud;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
