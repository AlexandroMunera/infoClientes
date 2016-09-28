using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace infoClientes.Models
{
    [Table("Clientes")] //DataAnotation
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Nit { get; set; }
             
        [NotMapped]
        public string NitDesencriptado
        {
            get {
                var sgi = new Sgi.Encrypter.Encrypter();

                return sgi.DESDecrypt(this.Nit);
                
            }
            set { NitDesencriptado = value; }
        }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Ciudad")]
        public int idCiudad { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public double Cupo { get; set; }

        [Display(Name = "Saldo cupo")]
        [Required(ErrorMessage = "The field {0} is required")]
        public double SaldoCupo { get; set; }

        [Display(Name = "% de visitas")]
        [Range(0,100)]
        public double PorcentajeVisitas { get; set; }

        // Relations
        public virtual Ciudad Ciudad { get; set; }
        public virtual ICollection<Visitas> Visitas { get; set; }
    }
}