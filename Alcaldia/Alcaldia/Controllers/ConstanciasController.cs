//extern alias Myalias;

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
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
//using Myalias::Microsoft.Reporting.WebForms;

namespace Alcaldia.Controllers
{
    public class ConstanciasController : Controller
    {
        private AlcaldiaEntities1 db = new AlcaldiaEntities1();

        private string Inss2 = "";
        private string Cedula2 = "";
        private static string Inns4 ;
        private string NombreCompleto2 = "";
        private static DateTime TFecha = DateTime.Now;
        private static byte[] Imagen = null;


        // GET: Constancias
        //public ActionResult Index()
        //{
        //    var constancias = db.Constancias.Include(c => c.Empleado);

        //    return View(constancias.ToList());
        //}
        //AlcaldiaDataSet ds = new AlcaldiaDataSet();

        //public ActionResult Imprimir2()
        //{
        //    string Inss5;
        //    Inss5 = Inns4;
        //    ReportViewer rpt = new ReportViewer();
        //    rpt.ProcessingMode = ProcessingMode.Local;

        //    var consulta = from f in db.Constancias select f;
        //    consulta = consulta.Where(w => w.Inss.Contains(Inss5));

        //    var consulta2 = from f2 in db.Empleado select f2;
        //    consulta2 = consulta2.Where(t => t.Inss.Contains(Inss5));


        //    rpt.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\Report1.rdlc";

        //    ReportDataSource datasourse2 = new ReportDataSource("DataSet2", consulta2);
        //    rpt.LocalReport.DataSources.Add(datasourse2);

        //    ReportDataSource datasourcereport = new ReportDataSource("DataSet1", consulta);
        //    rpt.LocalReport.DataSources.Add(datasourcereport);
        //    rpt.SizeToReportContent = true;
        //    rpt.ShowPrintButton = true;
        //    rpt.ShowZoomControl = true;
        //    ViewBag.RPT = rpt;
        //    return View();
        //}

        public ActionResult Imprimir2()
        {
            string Inss5;
            Inss5 = Inns4;

            byte[] file;
            string Directorio = "~/Reportes/";
            string urlArchivo = string.Format("{0}.{1}", "Report1", "rdlc");
            string FullPathReport = string.Format("{0}{1}", this.HttpContext.Server.MapPath(Directorio), urlArchivo);
            ReportViewer reporte = new ReportViewer();
            reporte.Reset();
            reporte.LocalReport.ReportPath = FullPathReport;
            reporte.Width = Unit.Percentage(900);
            reporte.Height = Unit.Percentage(900);

            var consulta = from f in db.Constancias select f;
            //consulta = consulta.Where(w=>w.Inss.Contains("4"));
            consulta = consulta.Where(w => w.Inss.Contains(Inss5));

            var consulta2 = from f2 in db.Empleado select f2;
            //consulta2 = consulta2.Where(t=>t.Inss.Contains("4"));
            consulta2 = consulta2.Where(t => t.Inss.Contains(Inss5));

            ReportDataSource datasourse2 = new ReportDataSource("DataSet2", consulta2);
            reporte.LocalReport.DataSources.Add(datasourse2);

            ReportDataSource datasourcereport = new ReportDataSource("DataSet1", consulta);
            reporte.LocalReport.DataSources.Add(datasourcereport);
            reporte.LocalReport.Refresh();
            file = reporte.LocalReport.Render("PDF");
            ViewBag.RPT = reporte;
            //ViewData["Reporte"] = reporte;
            //ViewData["Reporte"] = file;
            return File(new MemoryStream(file).ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("{0}{1}", "Documento.", "PDF"));

            //return View();
        }


        public ActionResult IConFecha(string Buscar)
        {
            string mensaje = "";

            var consultafecha = from x2 in db.Constancias select x2;
            if (!string.IsNullOrEmpty(Buscar))
            {
                DateTime fecha1 = Convert.ToDateTime(Buscar).Date;
                //Convert.ToDateTime(Buscar);
                consultafecha = consultafecha.Where(c => c.Fechaconstancias.Value == fecha1);
                if (consultafecha.Count() != 0)
                {
                    mensaje = "encontrado";
                }
                else
                {
                    mensaje = "no encontrado";
                }
            }
            else
            {
                consultafecha = null;
            }

            if (mensaje != "")
            {
                ViewData["MSJ2"] = mensaje;
            }
            return View(consultafecha);
        }
        public ActionResult Index(string buscar)
        {
            string mensaje = "";
            var consultame = from x in db.Constancias select x;
            if (!string.IsNullOrEmpty(buscar))
            {
                Inns4 = buscar;
               
                consultame = consultame.Where(z => z.Inss.Contains(buscar));
                if (consultame.Count() != 0)
                {
                    mensaje = "Empleado econtrado.";
                }
                else
                {
                    mensaje = "Empledo no encontrado";
                }
            }
            else
            {
                consultame = null;
            }
            if (mensaje != "")
            {
                ViewData["MSJ2"] = mensaje;
            }
            return View(consultame);
        }

        // GET: Constancias/Details/5
        public ActionResult Details(int? id, string Inss)
        {
            string imagedataurl = "";
            string imagenbase64 = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Constancias constancias = db.Constancias.Find(id);

            DateTime fecha2 = Convert.ToDateTime(constancias.Fechaconstancias).Date;
            //TFecha = fecha2;

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

            if (constancias.Escaner != null)
            {
                data = (byte[])constancias.Escaner.ToArray();
                imagenbase64 = Convert.ToBase64String(data);
                imagedataurl = string.Format("data:image/png;base64,{0}", imagenbase64);
            }
            else
            {
                //string Directorio = "~/Imagenes/";
                //string Archvio = string.Format("{0}.{1}","Imagen1","png");
                imagedataurl = "No ingreso imagen";
            }

            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;
            ViewBag.FechaD = fecha2.ToString("dd/MM/yyyy");
            if (imagedataurl != "No ingreso imagen")
            {
                ViewData["Escaner"] = imagedataurl;
            }
            else
            {
                ViewData["Escaner"] = imagedataurl;
            }

            //ViewBag.Escaner = data;

            if (constancias == null)
            {
                return HttpNotFound();
            }
            return View(constancias);
        }

        // GET: Constancias/Create
        public ActionResult Create(string Inss, string Cedula, string NombreCompleto)
        {
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

            //ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula");
            ViewBag.Inss = Inss2;
            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;
            return View();
        }

        // POST: Constancias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConstaniacia,Inss,Fechaconstancias,Horaconstancias,ServicioMedico,Especialidad,Observaciones,NumeroConstancia,Escaner,Estado")] Constancias constancias, HttpPostedFileBase img)
        {
            string mensaje = "";

            if (ModelState.IsValid)
            {
                if (constancias.Inss == null && constancias.Fechaconstancias == null)
                {
                    mensaje = "N° Inss y Fecha estan vacios";
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
                            //recepcion_documentos.Documento = reader.ReadBytes(img.ContentLength);
                            constancias.Escaner = reader.ReadBytes(img.ContentLength);

                        }
                    }



                    db.Constancias.Add(constancias);
                    db.SaveChanges();
                    mensaje = "Constancia Ingresada";
                }
                return RedirectToAction("Index");
            }
            ViewData["MSJ1"] = mensaje;
            //ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", constancias.Inss);
            ViewBag.Inss = Inss2;
            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;
            return View(constancias);
        }

        // GET: Constancias/Edit/5
        public ActionResult Edit(int? id, string Inss, DateTime Fecha)
        {
            string imagenbase64 = "";
            string imagedataurl = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Constancias constancias = db.Constancias.Find(id);
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
                Imagen = constancias.Escaner;
            }

            if (constancias == null)
            {
                return HttpNotFound();
            }

            byte[] data;

            if (constancias.Escaner != null)
            {
                data = (byte[])constancias.Escaner.ToArray();
                imagenbase64 = Convert.ToBase64String(data);
                imagedataurl = string.Format("data:image/png;base64,{0}", imagenbase64);
            }
            else
            {
                imagedataurl = "No se inserto imagen";
            }


            ViewData["Escaner"] = imagedataurl;

            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Inss3", constancias.Inss);
            ViewBag.Cedula = Cedula2;
            ViewBag.NombreCompleto = NombreCompleto2;

            ViewBag.Fecha2 = Fecha.ToString("dd/MM/yyyy");


            //ViewData["p1"] = "1";
            TempData["p1"] = "si no hace cambio en la fecha se guardara la misma.";

            return View(constancias);
        }

        // POST: Constancias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdConstaniacia,Inss,Fechaconstancias,Horaconstancias,ServicioMedico,Especialidad,Observaciones,NumeroConstancia,Escaner,Estado")] Constancias constancias, HttpPostedFileBase img)
        {
            string mensaje = "";
            if (ModelState.IsValid)
            {
                if (img == null && Imagen != null)
                {
                    mensaje = "Ingreso el mismo";
                    constancias.Escaner = Imagen;
                }

                else if (img != null)
                {
                    using (var reader = new BinaryReader(img.InputStream))
                    {
                        constancias.Escaner = reader.ReadBytes(img.ContentLength);
                    }
                    mensaje = "Seingreso otro";
                }

                if (img == null && Imagen == null)
                {
                    mensaje = "se inserto vacio";
                }

                if (constancias.Fechaconstancias == null && TFecha != null)
                {
                    constancias.Fechaconstancias = TFecha;
                }

                db.Entry(constancias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", constancias.Inss);

            //ViewBag.Cedula = Cedula2;
            //ViewBag.NombreCompleto = NombreCompleto2;
            //ViewBag.Fecha = TFecha.ToString("dd/MM/yyyy");
            //ViewBag.Fecha = TFecha.ToString("dd/MM/yyyy");
            return View(constancias);
        }


        // GET: Constancias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Constancias constancias = db.Constancias.Find(id);
            if (constancias == null)
            {
                return HttpNotFound();
            }
            return View(constancias);
        }

        // POST: Constancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Constancias constancias = db.Constancias.Find(id);
            db.Constancias.Remove(constancias);
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

