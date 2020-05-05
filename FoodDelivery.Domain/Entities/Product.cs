using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FoodDelivery.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Введите название товара")]
        public string Name { get; set; }

        [Display(Name = "Вес, г")]
        [Required(ErrorMessage = "Введите вес товара")]
        public int Weight { get; set; }

        [Display(Name = "Цена, руб")]
        [Required(ErrorMessage = "Введите цену товара")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное значение для поля цены")]
        public decimal Price { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Введите категорию для товара")]
        public string Category { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание для товара")]
        public string Description { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
