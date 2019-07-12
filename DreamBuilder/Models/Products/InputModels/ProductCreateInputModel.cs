using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamBuilder.Models.Products.InputModels
{
    public class ProductCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Price must be a positive number!")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Manufactured On")]
        public DateTime ManufacturedOn { get; set; }

        [Required]
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
