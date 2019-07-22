using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamBuilder.Models.Products.InputModels
{
    public class ProductCreateInputModel
    {
        private const string PriceErrorMessage = "Price must be a positive number!";

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Make")]
        public string Make { get; set; }

        [Required]
        [DisplayName("Model")]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [DisplayName("File image")]
        public IFormFile Image { get; set; }

        [Required]
        [DisplayName("Price")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = PriceErrorMessage)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Manufactured On")]
        public DateTime ManufacturedOn { get; set; }

        [Required]
        [DisplayName("Product category")]
        public string Category { get; set; }

        //      Product
        //- Id(string)
        //- Name(string)(Backhoe loader, Mini digger, Bucket-chain excavator etc. . .)
        //- Make(string)(JCB, Caterpillar, Hidromek)
        //- Model(enum)(3CX, 4CX, ) 
        //- Description(string)
        //- Image(string)
        //- Price(decimal)
        //- ManufactoredOn(DateTime)
        //- Category(Category) /new || old/
        //- Inquiries<List of Inquiry>
        //- Orders<List of Order>
    }
}
