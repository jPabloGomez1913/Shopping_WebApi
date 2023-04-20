using System.ComponentModel.DataAnnotations;

namespace ShoppingWebApi.DAL.Entities
{
    public class City : Entity
    {
        [Display(Name = "Ciudad")]// ASÍ ES COMO SE VA A MOSTRAR POR UI
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio ")] // NOT NULL
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public State State { get; set; }

        //FK
        [Required]// NOT NULL
        public Guid StateId { get; set; }
    }
}
