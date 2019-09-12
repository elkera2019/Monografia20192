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
    public class DeduccionesController : Controller
    {
        private AlcaldiaEntities1 db = new AlcaldiaEntities1();

        // GET: Deducciones
        public ActionResult Index()
        {
            var deducciones = db.Deducciones.Include(d => d.Empleado);
            return View(deducciones.ToList());
        }

        // GET: Deducciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducciones deducciones = db.Deducciones.Find(id);
            if (deducciones == null)
            {
                return HttpNotFound();
            }
            return View(deducciones);
        }

        // GET: Deducciones/Create
        public ActionResult Create()
        {
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula");
            return View();
        }

        // POST: Deducciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDeducciones,Inss,Deducion,FechaDeduccion,Estado")] Deducciones deducciones)
        {
            if (ModelState.IsValid)
            {
                db.Deducciones.Add(deducciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", deducciones.Inss);
            return View(deducciones);
        }

        // GET: Deducciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducciones deducciones = db.Deducciones.Find(id);
            if (deducciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", deducciones.Inss);
            return View(deducciones);
        }

        // POST: Deducciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDeducciones,Inss,Deducion,FechaDeduccion,Estado")] Deducciones deducciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deducciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", deducciones.Inss);
            return View(deducciones);
        }

        // GET: Deducciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducciones deducciones = db.Deducciones.Find(id);
            if (deducciones == null)
            {
                return HttpNotFound();
            }
            return View(deducciones);
        }

        // POST: Deducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deducciones deducciones = db.Deducciones.Find(id);
            db.Deducciones.Remove(deducciones);
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
