using Microsoft.AspNetCore.Mvc.Rendering;

namespace Books.Models.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem>? CategorySelectList { get; set; }
    }
}
