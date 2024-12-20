using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class CategoryBook
    {

        [Key]
        public int? CategoryId { get; set; }

        [DisplayName("Назва категорії книги")]
        [Required(ErrorMessage = "Введіть значення для поля.")]
        public string? Name { get; set; }

        [DisplayName("Порядок відображення")]
        [Required(ErrorMessage = "Введіть значення для поля.")]
        [Range(1, int.MaxValue, ErrorMessage = "Число не входить у заданий діапазон.")]
        public int? DisplayOrder { get; set; }

    }
}
