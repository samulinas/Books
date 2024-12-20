namespace Books.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<CategoryBook> Categories { get; set; }
    }
}
