using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace infoClientes.Models
{
    [Table("Vendedores")]
    public class Vendedor
    {
        [Key]
        public int idVendedor { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Nit { get; set; }

        [NotMapped]
        public string NitDesencriptado
        {
            get
            {
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

        // Relations
        public virtual ICollection<Visitas> Visitas { get; set; }
    }
}