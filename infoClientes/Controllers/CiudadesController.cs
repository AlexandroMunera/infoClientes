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
    public class CiudadesController : Controller
    {
        private Context db = new Context();

        // GET: Ciudades
        public ActionResult Index()
        {
            var ciudades = db.Ciudades.Include(c => c.Departamento);
            return View(ciudades.OrderBy(p => p.nomCiudad).ToList());
        }

        // GET: Ciudades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudad ciudad = db.Ciudades.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // GET: Ciudades/Create
        public ActionResult Create()
        {
            ViewBag.idDepartamento = new SelectList(db.Departamentos.OrderBy(p => p.nomDepartamento), "idDepartamento", "nomDepartamento");
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCiudad,nomCiudad,idDepartamento")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                db.Ciudades.Add(ciudad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDepartamento = new SelectList(db.Departamentos.OrderBy(p => p.nomDepartamento), "idDepartamento", "nomDepartamento", ciudad.idDepartamento);
            return View(ciudad);
        }

        // GET: Ciudades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudad ciudad = db.Ciudades.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDepartamento = new SelectList(db.Departamentos.OrderBy(p => p.nomDepartamento), "idDepartamento", "nomDepartamento", ciudad.idDepartamento);
            return View(ciudad);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCiudad,nomCiudad,idDepartamento")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDepartamento = new SelectList(db.Departamentos.OrderBy(p => p.nomDepartamento), "idDepartamento", "nomDepartamento", ciudad.idDepartamento);
            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudad ciudad = db.Ciudades.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciudad ciudad = db.Ciudades.Find(id);
            db.Ciudades.Remove(ciudad);
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
