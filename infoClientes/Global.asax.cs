using infoClientes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace infoClientes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Context db = new Context();

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Context, Migrations.Configuration>()); //Cada vez que ejecuto la aplicacion verifica si la base de datos cambio


            //Cuando iniciel sitio, verificar si existe el usuario alexandromunera@gmail.com, sino existe se crea
            this.CheckSuperUser();

            //Cuando iniciel sitio, verificar si existen paises,departamentos,ciudades,clientes o usuarios
            this.CheckDatos();            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }   

        private void CheckDatos()
        {
            #region Verificar los paises

            var colombia = db.Paises.Where(p => p.nomPais == "Colombia").FirstOrDefault();

            if (colombia == null) //Si no existe el pais colombia, crearlo
            {
                colombia = new Pais
                {
                    nomPais = "Colombia"
                };

                db.Paises.Add(colombia);
                db.SaveChanges();
            }

            #endregion

            #region Verificar los departamentos

            var antioquia = db.Departamentos.Where(p => p.nomDepartamento == "Antioquia").FirstOrDefault();

            if (antioquia == null) 
            {
                antioquia = new Departamento
                {
                    nomDepartamento = "Antioquia",
                    idPais = colombia.idPais
                };

                db.Departamentos.Add(antioquia);
                db.SaveChanges();
            }

            var cundinamarca = db.Departamentos.Where(p => p.nomDepartamento == "Cundinamarca").FirstOrDefault();

            if (cundinamarca == null) 
            {
                cundinamarca = new Departamento
                {
                    nomDepartamento = "Cundinamarca",
                    idPais = colombia.idPais
                };

                db.Departamentos.Add(cundinamarca);
                db.SaveChanges();
            }

            #endregion

            #region Verificar las ciudades

            var medellin = db.Ciudades.Where(p => p.nomCiudad == "Medellín").FirstOrDefault();

            if (medellin == null) 
            {
                medellin = new Ciudad
                {
                    nomCiudad = "Medellín",
                    idDepartamento = antioquia.idDepartamento
                };

                db.Ciudades.Add(medellin);
                db.SaveChanges();
            }

            var bogota = db.Ciudades.Where(p => p.nomCiudad == "Bogotá").FirstOrDefault();

            if (bogota == null) 
            {
                bogota = new Ciudad
                {
                    nomCiudad = "Bogotá",
                    idDepartamento = cundinamarca.idDepartamento
                };

                db.Ciudades.Add(bogota);
                db.SaveChanges();
            }


            #endregion

            #region Verificar los clientes

            var alexandro = db.Clientes.Where(p => p.Nit == "123").FirstOrDefault();

            var sgi = new Sgi.Encrypter.Encrypter();


            if (alexandro == null) 
            {
                
                alexandro = new Cliente
                {
                    Nit = sgi.DESEncrypt("123"),
                    Nombre = "Alexandro",
                    Direccion = "Cr 54 # 56",
                    Telefono = "45322",
                    idCiudad = medellin.idCiudad,
                    Cupo = 500000,
                    SaldoCupo = 500000,
                    PorcentajeVisitas = 30
                };

                db.Clientes.Add(alexandro);
                db.SaveChanges();
            }

            #endregion

            #region Verificar los vendedores

            var javier = db.Vendedores.Where(p => p.Nit == "987").FirstOrDefault();

            if (javier == null) 
            {
                javier = new Vendedor
                {
                    Nit = sgi.DESEncrypt("987"),
                    Nombre = "Javir",
                    Direccion = "Cr 88 # 99",
                    Telefono = "9874512"
                };

                db.Vendedores.Add(javier);
                db.SaveChanges();
            }

            #endregion
        }

        private void CheckSuperUser()
        {
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));                        

            this.CheckRole("Admin", userContext);
            this.CheckRole("User", userContext);
            
            var userASP = userManager.FindByName("alexandromunera@gmail.com");

            if (userASP == null)
            {
                userASP = new ApplicationUser
                {
                    UserName = "alexandromunera@gmail.com",
                    Email = "alexandromunera@gmail.com",
                    PhoneNumber = "4532258"
                };

                userManager.Create(userASP, "@Admin123");
            }

            userManager.AddToRole(userASP.Id, "Admin");
            userManager.AddToRole(userASP.Id, "User");
        }

        private void CheckRole(string roleName, ApplicationDbContext userContext)
        {
            //user managment
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            //Check to see if role exist if it doesn't create it
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }

        }
    }
}
