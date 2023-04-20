using System.ComponentModel.DataAnnotations;

namespace ShoppingWebApi.DAL.Entities
{
    public class State : Entity
    {
        [Display(Name = "State")]// ASÍ ES COMO SE VA A MOSTRAR POR UI
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio ")] // NOT NULL
        public string Name { get; set; }

        [Display(Name = "País")]

        public Country Country { get; set; }

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; }//Clave foranea que se crea en la tabla
                                           //Relacion entre state y country
        [Display(Name = "Ciudades")]
        public ICollection<City> Cities { get; set; }//Un esatdo puede tener N ciudades

        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }

}
