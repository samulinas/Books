using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Books.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Book> objList = _context.Book;
            foreach (var obj in objList) {
                obj.CategoryBook = _context.CategoryBook.FirstOrDefault(u => u.CategoryId == obj.CategoryId);
            }
            return View(objList);
        }
        //До відправки форми
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM() {
                Book = new Book(),
                CategorySelectList = _context.CategoryBook.Select(i => new SelectListItem { Text = i.Name, Value = i.CategoryId.ToString() })
            };
            return View(bookVM);
        }
        //Після відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (bookVM.Book.BookId != 0) { 
                    string upload = webRootPath + @"\images\books";
                    string fileName = Guid.NewGuid().ToString(); 
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    bookVM.Book.Image = fileName + extension;
                    _context.Book.Add(bookVM.Book);
                }
                _context?.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //До відправки форми
        public IActionResult Update(int? id)
        {
            BookVM bookVM = new BookVM()
            {
                Book = new Book(),
                CategorySelectList = _context.CategoryBook.Select(i => new SelectListItem { Text = i.Name, Value = i.CategoryId.ToString() })
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }
            bookVM.Book = _context.Book.Find(id);
            if (bookVM == null)
            {
                return NotFound();
            }
            return View(bookVM);
        }
        //Після відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(BookVM obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                var objFromDb = _context.Book.AsNoTracking().FirstOrDefault(u => u.BookId == obj.Book.BookId);

                if (files.Count > 0)
                {
                    string upload = webRootPath + @"/images/books/";
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    var oldFile = Path.Combine(upload, objFromDb.Image);

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    obj.Book.Image = fileName + extension;
                }
                else
                {
                    obj.Book.Image = objFromDb.Image;
                }
                _context.Book.Update(obj.Book);
                _context?.SaveChanges();
                return RedirectToAction("Index");
            }
            obj.CategorySelectList = _context.CategoryBook.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
            return View(obj);
        }

        //До відправки форми
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context?.Book.Include(u => u.CategoryBook).FirstOrDefault(u => u.BookId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Після відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Book obj)
        {
            _context?.Book.Remove(obj);
            _context?.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

