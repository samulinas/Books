using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class Book
    {
        [Key]
        public int? BookId { get; set; }

        [DisplayName("Назва книги")]
        [Required(ErrorMessage = "Введіть значення для поля.")]
        public string? Title { get; set; }

        [DisplayName("Автори")]
        [Required(ErrorMessage = "Введіть значення для поля.")]
        public string? Authors { get; set; }

        [DisplayName("Опис")]
        public string? Description { get; set; }

        [DisplayName("Зображення")]
        public string? Image { get; set; }

        [DisplayName("Рік видання")]
        [Required(ErrorMessage = "Введіть значення для поля.")]
        [Range(1000, 3000, ErrorMessage = "Число не входить у заданий діапазон.")]
        public int? Year { get; set; }

        [DisplayName("Категорія")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryBook? CategoryBook { get; set; }

    }
}
