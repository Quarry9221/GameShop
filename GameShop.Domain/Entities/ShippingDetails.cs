using System.ComponentModel.DataAnnotations;

namespace GameShop.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Inpur your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Input first adress for deliver")]
        [Display(Name = "First adress")]
        public string Line1 { get; set; }

        [Required(ErrorMessage = "Input second adress for deliver")]
        [Display(Name = "Second adress")]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "input your city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Input your country")]
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}