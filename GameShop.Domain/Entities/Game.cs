using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace GameShop.Domain.Entities
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "Please input name of your game")]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please input ganre of your game")]
        public string Genre { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Developer")]
        [Required(ErrorMessage = "Please input name of developer of your game")]
        public string Developer { get; set; }

        [Display(Name = "Price")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please input correct positive number")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
