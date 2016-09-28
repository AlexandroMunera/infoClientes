using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace infoClientes.Models
{
    [Table("Paises")] //DataAnotation
    public class Pais
    {
        [Key]
        public int idPais { get; set; }

        [Display(Name = "Pais" )]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} must contain maximum {1} and minimum {2} characteres", MinimumLength = 3)]
        public string nomPais { get; set; }

        //Relations
        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}