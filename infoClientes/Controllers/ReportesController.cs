using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using infoClientes.Models;

namespace infoClientes.Controllers
{
    public class ReportesController : Controller
    {
        private Context db = new Context();

        // GET: Reportes
        public ActionResult MovimientosClientes(int idCliente=0)
        {
            var visitas = db.Visitas.Include(v => v.Vendedor).Include(c => c.Cliente);

            if (idCliente > 0)
            {
                visitas = visitas.Where(p => p.idCliente == idCliente);
            }

            ViewBag.Clientes = new SelectList(db.Clientes, "idCliente", "NitDesencriptado");

            return View(visitas.ToList());
        }

        public ActionResult GraficoVisitasCiudad()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetVisitasPorCiudad()
        {
            #region Consulta de visitas por ciudad

                var visitasConCiudad = from v in db.Visitas.Include(v => v.Cliente)
                                       select new { visitaId = v.idVisitas, nomCiudad = v.NombreCiudad };

                var visitasPorCiudad = visitasConCiudad.GroupBy(n => n.nomCiudad).
                         Select(group =>
                             new
                             {
                                 Ciudad = group.Key,
                                 CantidadVisitas = group.Count()
                             });
            #endregion            

            return Json(visitasPorCiudad.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}