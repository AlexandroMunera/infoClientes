using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace infoClientes.Models
{
    [Table("Ciudades")] //DataAnotation
    public class Ciudad
    {
        [Key]
        [Display(Name = "Ciudad")]
        public int idCiudad { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} must contain maximum {1} and minimum {2} characteres", MinimumLength = 3)]
        public string nomCiudad { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public int idDepartamento { get; set; }

        //Relations
        public virtual Departamento Departamento { get; set; }
        
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}