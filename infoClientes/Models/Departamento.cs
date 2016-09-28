using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace infoClientes.Models
{
    [Table("Departamentos")] //DataAnotation
    public class Departamento
    {
        [Key]
        public int idDepartamento { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} must contain maximum {1} and minimum {2} characteres", MinimumLength = 3)]
        public string nomDepartamento { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public int idPais { get; set; }

        //Relations
        public virtual Pais Pais { get; set; }
        public virtual ICollection<Ciudad> Ciudades { get; set; }
    }
}