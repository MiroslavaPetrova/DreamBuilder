using AutoMapper;
using DreamBuilder.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamBuilder.Models.Products.InputModels
{
    public class ProductsCreateInputModel : IMapTo<Product>, IHaveCustomMappings
    { 
        private const string PriceErrorMessage = "Price must be a positive number!";

        public string Id { get; set; }

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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<ProductsCreateInputModel, Product>()
               .ForMember(dest => dest.Category, opt => opt.MapFrom(origin => new Category { Name = origin.Category }));
        }
    }
}
