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
    public class VendedoresController : Controller
    {
        private Context db = new Context();

        // GET: Vendedores
        public ActionResult Index()
        {
            return View(db.Vendedores.ToList());
        }

        // GET: Vendedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedor vendedor = db.Vendedores.Find(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        // GET: Vendedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVendedor,Nit,Nombre,Direccion,Telefono")] Vendedor vendedor)
        {
            #region Encriptar el NIT
                var sgi = new Sgi.Encrypter.Encrypter();

                vendedor.Nit = sgi.DESEncrypt(vendedor.Nit);
            #endregion

            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vendedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendedor);
        }

        // GET: Vendedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedor vendedor = db.Vendedores.Find(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVendedor,Nit,Nombre,Direccion,Telefono")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        // GET: Vendedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedor vendedor = db.Vendedores.Find(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedor);
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
