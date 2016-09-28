using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace infoClientes.Models 
{
    public class Context : DbContext
    {
        public Context()
            : base("DefaultConnection")
        {            
        }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Visitas> Visitas { get; set; }
    }
}