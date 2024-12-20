using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Books = _context.Book,
                Categories = _context.CategoryBook
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            DetailsVM detailsVM = new DetailsVM()
            {
                Book = _context.Book.FirstOrDefault(b => b.BookId == id),
                ExistsInCart = false
            };
            return View(detailsVM);
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
