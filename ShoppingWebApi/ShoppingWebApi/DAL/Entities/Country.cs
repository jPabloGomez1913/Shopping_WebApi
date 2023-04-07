﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingWebApi.DAL.Entities
{
    public class Country : Entity
    {
        [Display(Name = "País")]// ASÍ ES COMO SE VA A MOSTRAR POR UI
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio ")] // NOT NULL
        public string Name { get; set; }
    }
}
