using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class CategoryBookController : Controller
    {
        private readonly AppDbContext? _context;
        public CategoryBookController(AppDbContext? context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            IEnumerable<CategoryBook>? objList = _context?.CategoryBook;
            return View(objList);
        }
        //До відправки форми
        public IActionResult Create()
        {
            return View();
        }
        //Після відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryBook obj)
        {
            if (ModelState.IsValid)
            {
                _context?.CategoryBook.Add(obj);
                _context?.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //До відправки форми
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            var obj = _context?.CategoryBook.Find(id);
            if (obj == null) { 
                return NotFound();
            }
            return View(obj);
        }
        //Після відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CategoryBook obj)
        {
            if (ModelState.IsValid)
            {
                _context?.CategoryBook.Update(obj);
                _context?.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //До відправки форми
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context?.CategoryBook.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Після відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CategoryBook obj)
        {
            _context?.CategoryBook.Remove(obj);
            _context?.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
