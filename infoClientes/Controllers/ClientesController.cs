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
    public class ClientesController : Controller
    {
        private Context db = new Context();

        [HttpPost]
        public ActionResult GetDepartamentos(int idPais)
        {

            var departamentos = (from d in db.Departamentos
                                 where d.idPais == idPais
                                 select new
                                 {
                                     Value = d.idDepartamento,
                                     Text = d.nomDepartamento
                                 }).ToList();

            return Json(departamentos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCiudades(int idDepartamento)
        {

            var ciudades = (from c in db.Ciudades
                            where c.idDepartamento == idDepartamento
                            select new
                            {
                                Value = c.idCiudad,
                                Text = c.nomCiudad
                            }).ToList();

            return Json(ciudades, JsonRequestBehavior.AllowGet);
        }

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Ciudad);

            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.Paises = new SelectList(db.Paises, "idPais", "nomPais");
            ViewBag.Departamentos = new SelectList(db.Departamentos, "idDepartamento", "nomDepartamento");
            ViewBag.idCiudad = new SelectList(db.Ciudades, "idCiudad", "nomCiudad");

            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {

            #region Encriptar el NIT
                var sgi = new Sgi.Encrypter.Encrypter();

                cliente.Nit = sgi.DESEncrypt(cliente.Nit);
            #endregion

            if (ModelState.IsValid && cliente.Cupo >= cliente.SaldoCupo)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Paises = new SelectList(db.Paises, "idPais", "nomPais");
            ViewBag.Departamentos = new SelectList(db.Departamentos, "idDepartamento", "nomDepartamento");
            ViewBag.idCiudad = new SelectList(db.Ciudades, "idCiudad", "nomCiudad", cliente.idCiudad);

            ViewBag.errorSaldo = "El saldo cupo no puede superar el cupo que posees";

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ViewBag.Paises = new SelectList(db.Paises, "idPais", "nomPais");
            ViewBag.Departamentos = new SelectList(db.Departamentos, "idDepartamento", "nomDepartamento");
            ViewBag.idCiudad = new SelectList(db.Ciudades, "idCiudad", "nomCiudad", cliente.idCiudad);

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,Nit,Nombre,Direccion,Telefono,idCiudad,Cupo,SaldoCupo,PorcentajeVisitas")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Paises = new SelectList(db.Paises, "idPais", "nomPais");
            ViewBag.Departamentos = new SelectList(db.Departamentos, "idDepartamento", "nomDepartamento");
            ViewBag.idCiudad = new SelectList(db.Ciudades, "idCiudad", "nomCiudad", cliente.idCiudad);

            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
