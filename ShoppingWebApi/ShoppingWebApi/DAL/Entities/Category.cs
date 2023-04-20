using System.ComponentModel.DataAnnotations;

namespace ShoppingWebApi.DAL.Entities
{
    public class Category : Entity
    {
        [Display(Name = "Categoria")]// ASÍ ES COMO SE VA A MOSTRAR POR UI
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio ")] // NOT NULL
        public string Name { get; set; }

        [Display(Name = "Descripción")]// ASÍ ES COMO SE VA A MOSTRAR POR UI
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Description { get; set; }//Esto indica que la propiedad puede ser null
    }
}
