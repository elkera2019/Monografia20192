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
    public class EmpleadoesController : Controller
    {
        private AlcaldiaEntities1 db = new AlcaldiaEntities1();

        //Lista temporal para cargar el dropdonwlist o combobox
       private List<SelectListItem> lst = new List<SelectListItem>();
       private List<SelectListItem> lst2 = new List<SelectListItem>();
       private List<SelectListItem> lst3 = new List<SelectListItem>();
       private static DateTime TFechaNaci=DateTime.Now;
       private static DateTime TFechaIngre=DateTime.Now; 

       
        // GET: Empleadoes
        public ActionResult Index()
        {
           
            var empleado = db.Empleado.Include(e => e.Departamento).Include(e => e.Planilla);
            return View(empleado.ToList());
        }

        public ActionResult ISubcidio(string buscar)
        {
            string msj = "";
            var con = from x in db.Empleado select x;
            if (!string.IsNullOrEmpty(buscar))
            {
                con = con.Where(a => a.Inss.Contains(buscar));
                if (con.Count() != 0)
                {
                    msj = "Empleado encontrado";
                }
                else
                {
                    msj = "Empleado no encontrado";
                }
            }
            else
            {
                con = null;
            }
            ViewData["MSJ4"] = msj;
            return View(con);
        }
        public ActionResult IConstancia(string obj)
        {
            string mensaje2="";
            var Icon = from x in db.Empleado select x;
            if (!string.IsNullOrEmpty(obj))
            {
                Icon = Icon.Where(z => z.Inss.Contains(obj));
                if (Icon.Count() != 0)
                {
                    mensaje2 = "Empleado econtrado.";
                }
                else
                {
                    mensaje2 = "Empledo no encontrado";
                }
            }
            else
            {
                Icon = null;
            }
            ViewData["MSJ2"] = mensaje2;
            return View(Icon);
        }
        // GET: Empleadoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            //Aqui alimento la lista que necesitare en el dropdownlist o combobox
            lst.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
            lst.Add(new SelectListItem() { Text = "Femenino", Value = "Femenino" });
            lst2.Add(new SelectListItem() { Text="Soltero(a)",Value="Soltero(a)"});
            lst2.Add(new SelectListItem() { Text="Casado(a)",Value="Casado(a)"});
            lst3.Add(new SelectListItem() { Text="Primaria",Value="Primaria"});
            lst3.Add(new SelectListItem() { Text = "Secundaria", Value = "Secundaria" });
            lst3.Add(new SelectListItem() { Text = "Tecnico", Value = "Tecnico" });
            lst3.Add(new SelectListItem() { Text = "Universitario", Value = "Universitario" });

            //cargo la lista en un viewbag para utiizarlo en la vista
            ViewBag.Sexo = lst;
            ViewBag.EstadoCivil = lst2;
            ViewBag.NivelAcademico = lst3;

            ViewBag.IdDpt = new SelectList(db.Departamento, "IdDpt", "TpDepartamento");
            ViewBag.IdPlanilla = new SelectList(db.Planilla, "IdPlanilla", "TpPlanilla");
            return View();
        }

        // POST: Empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Inss,Cedula,NombreCompleto,EstadoCivil,FechaNacimiento,FechaIngreso,DatosLaborales,Celular,Telefono,IdPlanilla,IdDpt,Hospital,NivelAcademico,Cargo,Salario,Vacaciones,Estado,Sexo")] Empleado empleado)
        {
            //lst.Add(new SelectListItem() { Text = "Masculino", Value = "1" });
            //lst.Add(new SelectListItem() { Text = "Femenino", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Masculino", Value = "Masculino" });
            lst.Add(new SelectListItem() { Text = "Femenino", Value = "Femenino" });
            lst2.Add(new SelectListItem() { Text = "Soltero(a)", Value = "Soltero(a)" });
            lst2.Add(new SelectListItem() { Text = "Casado(a)", Value = "Casado(a)" });
            lst3.Add(new SelectListItem() { Text = "Primaria", Value = "Primaria" });
            lst3.Add(new SelectListItem() { Text = "Secundaria", Value = "Secundaria" });
            lst3.Add(new SelectListItem() { Text = "Tecnico", Value = "Tecnico" });
            lst3.Add(new SelectListItem() { Text = "Universitario", Value = "Universitario" });
            string mensaje="";
            var consulta = db.Empleado.Where(a=>a.Inss==empleado.Inss);
            if (consulta.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    if (empleado.FechaIngreso == null && empleado.FechaNacimiento == null)
                    {
                        mensaje = "No deje el campo de fecha vacia.";

                    }
                    else
                    {
                        db.Empleado.Add(empleado);
                        db.SaveChanges();
                        mensaje = "Almacenado Correctamente";
                        ViewData["MSJ"] = mensaje;
                        return RedirectToAction("Index");
                    }
                }

            }
            else
            {
                mensaje = "El numero de Inss que deseaa registrar ya existe en la base de datos.";
            }

            ViewData["MSJ"] = mensaje;
            ViewBag.Sexo = lst;
            ViewBag.EstadoCivil = lst2;
            ViewBag.NivelAcademico = lst3;

            ViewBag.IdDpt = new SelectList(db.Departamento, "IdDpt", "TpDepartamento", empleado.IdDpt);
            ViewBag.IdPlanilla = new SelectList(db.Planilla, "IdPlanilla", "TpPlanilla", empleado.IdPlanilla);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(string id,DateTime FechaIngreso,DateTime FechaNacimiento)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            if (FechaIngreso == null && FechaNacimiento == null)
            {
                FechaIngreso = TFechaIngre;
                FechaNacimiento = TFechaNaci;
            }
            else
            {
                TFechaIngre = FechaIngreso;
                TFechaNaci = FechaNacimiento;
            }
            //ViewData["Fecha"] = FechaNacimiento.ToShortDateString();
            ViewBag.FechaNacimiento2 = FechaNacimiento.ToString("dd/MM/yyyy");
            ViewBag.FechaIngreso2 = FechaIngreso.ToString("dd/MM/yyy");
            ViewBag.IdDpt = new SelectList(db.Departamento, "IdDpt", "TpDepartamento", empleado.IdDpt);
            ViewBag.IdPlanilla = new SelectList(db.Planilla, "IdPlanilla", "TpPlanilla", empleado.IdPlanilla);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Inss,Cedula,NombreCompleto,EstadoCivil,FechaNacimiento,FechaIngreso,DatosLaborales,Celular,Telefono,IdPlanilla,IdDpt,Hospital,NivelAcademico,Cargo,Salario,Vacaciones,Estado,Sexo")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
             
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDpt = new SelectList(db.Departamento, "IdDpt", "TpDepartamento", empleado.IdDpt);
            ViewBag.IdPlanilla = new SelectList(db.Planilla, "IdPlanilla", "TpPlanilla", empleado.IdPlanilla);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
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
