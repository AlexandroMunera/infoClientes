using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace infoClientes.Models
{
    [Table("Visitas")]
    public class Visitas
    {
        [Key]
        public int idVisitas { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int idVendedor { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int idCliente { get; set; }
        public string NombreCiudad { get; set; }
        
        [Display(Name = "Valor Neto")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double ValorNeto { get; set; }

        [Display(Name = "Valor visita")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double ValorVisita { get; set; }

        [Display(Name = "Observaciones visita")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string Observaciones { get; set; }

        // Relations
        public virtual Vendedor Vendedor { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}