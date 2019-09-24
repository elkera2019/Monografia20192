using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alcaldia.Models;
using System.IO;

namespace Alcaldia.Controllers
{
    public class SubcidiosController : Controller
    {
        private AlcaldiaEntities1 db = new AlcaldiaEntities1();

        private List<SelectListItem> lst = new List<SelectListItem>();
        private string Inss2 = "";
        private string Cedula2 = "";
        private string NombreCompleto2 = "";
        private static string Inns4;
        private static DateTime TFecha = DateTime.Now;
        private static byte[] Imagen = null;
        // GET: Subcidios
        public ActionResult Index(string buscar)
        {
            //var subcidio = db.Subcidio.Include(s => s.Empleado).Include(s => s.TipoSubcidio);
            //return View(subcidio.ToList());
            string mensaje ="";
            var consulta =from z in db.Subcidio select z;
            if (!string.IsNullOrEmpty(buscar))
            {
                Inns4 = buscar;
                consulta = consulta.Where(a=>a.Inss.Contains(buscar));
                if (consulta.Count() != 0)
                {
                    mensaje = "Dato econtrado.";
                }
                else
                {
                    mensaje = "Dato no encontrado";
                }
            }
            else
            {
                consulta = null;
            }
            if (mensaje != "")
            {
                ViewData["MSJ2"] = mensaje;
            }
            return View(consulta);
        }

        // GET: Subcidios/Details/5
        public ActionResult Details(int? id,string Inss)
        {
            string imagedataurl = "";
            string imagenbase64 = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcidio subcidio = db.Subcidio.Find(id);
            if (Inss == null)
            {
                Inss = Inss2;
            }
            else
            {
                Inss2 = Inss;
            }
            var consulta = db.Empleado.Where(z => z.Inss.Equals(Inss2)).FirstOrDefault();
            if (consulta != null)
            {
                Cedula2 = consulta.Cedula.ToString();
                NombreCompleto2 = consulta.NombreCompleto.ToString();
            }
            byte[] data;

            if (subcidio.ImagenSubcidio != null)
            {
                data = (byte[])subcidio.ImagenSubcidio.ToArray();
                imagenbase64 = Convert.ToBase64String(data);
                imagedataurl = string.Format("data:image/png;base64,{0}", imagenbase64);
            }
            else
            {
                imagedataurl = "No ingreso imagen";
            }

            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;

            if (imagedataurl != "No ingreso imagen")
            {
                ViewData["Imagen"] = imagedataurl;
            }
            else
            {
                ViewData["Imagen"] = imagedataurl;
            }


            if (subcidio == null)
            {
                return HttpNotFound();
            }
            return View(subcidio);
        }

        // GET: Subcidios/Create
        public ActionResult Create(string Inss, string Cedula, string NombreCompleto)
        {

            lst.Add(new SelectListItem() { Text = "Enfermedad Comun", Value =" 1" });
            lst.Add(new SelectListItem() { Text = "Accidente Comun", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Maternidad", Value = "3" });
            if (Inss == null)
            {
                Inss = Inss2;
            }
            else
            {
                Inss2 = Inss;
                Cedula2 = Cedula;
                NombreCompleto2 = NombreCompleto;
            }

            
            ViewBag.Inss = Inss2;
            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;
            //ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Inss");
            //ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "IdTipoSubcidio");
            ViewBag.IdTipoSubcido =lst;
            return View();
        }

        // POST: Subcidios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSubcidio,Inss,IdTipoSubcido,ImagenSubcidio,FechaIniciosubcidio,Dias,Estado")] Subcidio subcidio, HttpPostedFileBase img)
        {
            string mensaje = "";
            lst.Add(new SelectListItem() { Text = "Enfermedad Comun", Value = " 1" });
            lst.Add(new SelectListItem() { Text = "Accidente Comun", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Maternidad", Value = "3" });
            //var sub = db.TipoSubcidio.Where(a => a.Subcidio.Contains(subcidio.IdTipoSubcido.ToString())).FirstOrDefault();
            //subcidio.IdSubcidio = sub.IdTipoSubcidio;
            if (ModelState.IsValid)
            {
                if (subcidio.Inss == null && subcidio.FechaIniciosubcidio == null && subcidio.Dias==null)
                {
                    mensaje = "N° Inss,Fecha y dias estan vacios";
                }
                else
                {
                    if (img == null)
                    {
                        mensaje = "Imagen Vacia.";
                    }
                    else
                    {
                        using (var reader = new BinaryReader(img.InputStream))
                        {
                          
                           subcidio.ImagenSubcidio = reader.ReadBytes(img.ContentLength);

                        }
                       
                    }

                    db.Subcidio.Add(subcidio);
                db.SaveChanges();
                mensaje = "Constancia Ingresada";
            }
            return RedirectToAction("Index");
            }

            ViewData["MSJ1"] = mensaje;
           
            ViewBag.Inss = Inss2;
            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;
            //ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "IdTipoSubcidio", subcidio.IdTipoSubcido);
            ViewBag.IdTipoSubcido = lst;
            return View(subcidio);
        }

        // GET: Subcidios/Edit/5
        public ActionResult Edit(int? id, string Inss, DateTime Fecha)
        {

            lst.Add(new SelectListItem() { Text = "Enfermedad Comun", Value = " 1" });
            lst.Add(new SelectListItem() { Text = "Accidente Comun", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Maternidad", Value = "3" });
            string imagenbase64 = "";
            string imagedataurl = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcidio subcidio = db.Subcidio.Find(id);
            if (Inss == null)
            {
                Inss = Inss2;
            }
            if (Fecha == null)
            {
                Fecha = TFecha;
            }
            else
            {
                Inss2 = Inss;
                TFecha = Fecha;
            }
            var consulta = db.Empleado.Where(z => z.Inss.Equals(Inss2)).FirstOrDefault();
            if (consulta != null)
            {
                Cedula2 = consulta.Cedula.ToString();
                NombreCompleto2 = consulta.NombreCompleto.ToString();
                Imagen = subcidio.ImagenSubcidio;
            }

            if (subcidio == null)
            {
                return HttpNotFound();
            }
            byte[] data;

            if (subcidio.ImagenSubcidio != null)
            {
                data = (byte[])subcidio.ImagenSubcidio.ToArray();
                imagenbase64 = Convert.ToBase64String(data);
                imagedataurl = string.Format("data:image/png;base64,{0}", imagenbase64);
            }
            else
            {
                imagedataurl = "No se inserto imagen";
            }


            ViewData["Escaner"] = imagedataurl;

            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Inss3", subcidio.Inss);
            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;

            ViewBag.Fecha2 = Fecha.ToString("dd/MM/yyyy");


            //ViewData["p1"] = "1";
            TempData["p1"] = "si no hace cambio en la fecha se guardara la misma.";
            //ViewBag.IdTipoSubcidio = lst;
            ViewBag.IdTipoSubcido = lst;
            //ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", subcidio.Inss);
            //ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "Subcidio", subcidio.IdTipoSubcido);
            return View(subcidio);
        }

        // POST: Subcidios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSubcidio,Inss,IdTipoSubcido,ImagenSubcidio,FechaIniciosubcidio,Dias,Estado")] Subcidio subcidio, HttpPostedFileBase img)
        {
            string mensaje = "";
            if (ModelState.IsValid)
            {
                if (img == null && Imagen != null)
                {
                    mensaje = "Ingreso el mismo";
                    subcidio.ImagenSubcidio = Imagen;
                }

                else if (img != null)
                {
                    using (var reader = new BinaryReader(img.InputStream))
                    {
                        subcidio.ImagenSubcidio = reader.ReadBytes(img.ContentLength);
                    }
                    mensaje = "Seingreso otro";
                }

                if (img == null && Imagen == null)
                {
                    mensaje = "se inserto vacio";
                }

                if (subcidio.FechaIniciosubcidio == null && TFecha != null)
                {
                    subcidio.FechaIniciosubcidio = TFecha;
                }
                db.Entry(subcidio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", subcidio.Inss);
            //ViewBag.IdTipoSubcido = new SelectList(db.TipoSubcidio, "IdTipoSubcidio", "Subcidio", subcidio.IdTipoSubcido);
            ViewBag.IdTipoSubcido = lst;
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
