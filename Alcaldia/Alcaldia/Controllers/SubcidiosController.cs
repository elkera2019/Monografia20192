using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alcaldia.Models;

namespace Alcaldia.Controllers
{
    public class SubcidiosController : Controller
    {
        private AlcaldiaEntities1 db = new AlcaldiaEntities1();

        // GET: Subcidios
        public ActionResult Index()
        {
            var subcidio = db.Subcidio.Include(s => s.Empleado).Include(s => s.TipoSubcidio);
            return View(subcidio.ToList());
        }

        // GET: Subcidios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcidio subcidio = db.Subcidio.Find(id);
            if (subcidio == null)
            {
                return HttpNotFound();
            }
            return View(subcidio);
        }

        // GET: Subcidios/Create
        public ActionResult Create()
        {
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula");
            ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "Subcidio");
            return View();
        }

        // POST: Subcidios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSubcidio,Inss,IdTipoSubcido,ImagenSubcidio,FechaIniciosubcidio,Dias,Estado")] Subcidio subcidio)
        {
            if (ModelState.IsValid)
            {
                db.Subcidio.Add(subcidio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", subcidio.Inss);
            ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "Subcidio", subcidio.IdTipoSubcido);
            return View(subcidio);
        }

        // GET: Subcidios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcidio subcidio = db.Subcidio.Find(id);
            if (subcidio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", subcidio.Inss);
            ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "Subcidio", subcidio.IdTipoSubcido);
            return View(subcidio);
        }

        // POST: Subcidios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSubcidio,Inss,IdTipoSubcido,ImagenSubcidio,FechaIniciosubcidio,Dias,Estado")] Subcidio subcidio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcidio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", subcidio.Inss);
            ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "Subcidio", subcidio.IdTipoSubcido);
            return View(subcidio);
        }

        // GET: Subcidios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcidio subcidio = db.Subcidio.Find(id);
            if (subcidio == null)
            {
                return HttpNotFound();
            }
            return View(subcidio);
        }

        // POST: Subcidios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcidio subcidio = db.Subcidio.Find(id);
            db.Subcidio.Remove(subcidio);
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
