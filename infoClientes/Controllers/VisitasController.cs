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
    [Authorize]
    public class VisitasController : Controller
    {
        private Context db = new Context();

        // GET: Visitas
        public ActionResult Index()
        {
            var visitas = db.Visitas.Include(v => v.Vendedor);
            return View(visitas.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            ViewBag.idVendedor = new SelectList(db.Vendedores, "idVendedor", "NitDesencriptado");
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "NitDesencriptado");

            return View();
        }

        // POST: Visitas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Visitas visitas)
        {
            visitas.Fecha = DateTime.Now;

            var cliente = db.Clientes.Find(visitas.idCliente);

            visitas.ValorVisita = visitas.ValorNeto * (cliente.PorcentajeVisitas / 100);

            var ciudad = db.Ciudades.Find(cliente.idCiudad);

            visitas.NombreCiudad = ciudad.nomCiudad;

            if (ModelState.IsValid && cliente.SaldoCupo >= visitas.ValorVisita)
            {
                cliente.SaldoCupo -= visitas.ValorVisita;

                db.Visitas.Add(visitas);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.idVendedor = new SelectList(db.Vendedores, "idVendedor", "NitDesencriptado", visitas.idVendedor);
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "NitDesencriptado");

            ViewBag.errorSaldo = "No tiene saldo para realizar esta compra";

            return View(visitas);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idVendedor = new SelectList(db.Vendedores, "idVendedor", "Nit", visitas.idVendedor);
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "Nit");

            return View(visitas);
        }

        // POST: Visitas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVisitas,Fecha,idVendedor,idCliente,ValorNeto,ValorVisita,Observaciones")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idVendedor = new SelectList(db.Vendedores, "idVendedor", "Nit", visitas.idVendedor);
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "Nit");

            return View(visitas);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitas visitas = db.Visitas.Find(id);
            db.Visitas.Remove(visitas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
