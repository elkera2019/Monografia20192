using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alcaldia.Models;
using System.Data.SqlClient;

namespace Alcaldia.Controllers
{
    public class AsistenciasController : Controller
    {
        private AlcaldiaEntities1 db = new AlcaldiaEntities1();
        private static DateTime FechaB;
        private static List<Asistencias> listasistencia = new List<Asistencias>();
        

        private static List<string> listainss = new List<string>();
        private static DataSet d = new DataSet();
        // GET: Asistencias
        public ActionResult Index()
        {
            var asistencias = db.Asistencias.Include(a => a.Empleado);
            return View(asistencias.ToList());
        }

        // GET: Asistencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // GET: Asistencias/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula");
        //    return View();
        //}

        //Get: Asistenica/Create
       private DataSet  Correr()
        {
            DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(@"Data Source=RAMCES\MARADIAGA;Initial Catalog=Alcaldia;Integrated Security=True");
                string qwery = "SELECT Empleado.Inss, Empleado.NombreCompleto, Asistencias.FechaAsistencia, Asistencias.HoraEntrada, Asistencias.FirmaEntrada, Asistencias.HoraSalida, Asistencias.FirmaSalida FROM Asistencias right join Empleado ON Asistencias.Inss = Empleado.Inss";
                SqlDataAdapter sda = new SqlDataAdapter(qwery, con);
                sda.Fill(ds);
            return ds;
        }
        public ActionResult Create(string Insertar)
        {
            string horas = "07:00";
            //TimeSpan horasalida=TimeSpan.Parse("05:00");
            string horasalida = "05:00";
            //DataSet d = new DataSet();
            //DataTable dt;
            var consulta = from e in db.Empleado select e;
            if (Insertar != null)
            {
              
                d=Correr();

                //dt = d.Tables[0];

                //foreach (DataRow row2 in d.Tables[0].Rows)
                //{
                //    listasistencia.Add(new Asistencias() { Inss = row2["Inss"].ToString(), FechaAsistencia = Convert.ToDateTime(Insertar), HoraEntrada = TimeSpan.Parse(horas), HoraSalida = TimeSpan.Parse(horasalida) });
                //}

                //foreach (var g in listasistencia)
                //{
                //    Console.Write(g.ToString());

                //}


            }
            else
            {
                //d = Correr();
                d = null;
                
            }

            ViewBag.FechaAsistencia = Insertar;
            ViewBag.HoraEntrada = horas;
            ViewBag.HoraSalida = horasalida;
            
            //DataSet ds = new DataSet();
            //ds = null;
            //if (!string.IsNullOrEmpty(Insertar))
            //{ 
            //ViewBag.FechaAsistencia = Insertar;

            //SqlConnection con = new SqlConnection(@"Data Source=RAMCES\MARADIAGA;Initial Catalog=Alcaldia;Integrated Security=True");
            //string qwery = "SELECT Empleado.Inss, Empleado.NombreCompleto, Asistencias.FechaAsistencia, Asistencias.HoraEntrada, Asistencias.FirmaEntrada, Asistencias.HoraSalida, Asistencias.FirmaSalida FROM Asistencias right join Empleado ON Asistencias.Inss = Empleado.Inss";
            //SqlDataAdapter sda = new SqlDataAdapter(qwery, con);
            //sda.Fill(ds);
            //}
            //return View(ds);
            return View(d);
            
        }

        // POST: Asistencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdAsistencias,Inss,FechaAsistencia,HoraEntrada,FirmaEntrada,HoraSalida,FirmaSalida,Estado")] Asistencias asistencias, string Inss)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.Asistencias.Add(asistencias);
        //        //db.Asistencias.AddRange(listasistencia);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    //ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", asistencias.Inss);
        //    return View(asistencias);
        //}

        public ActionResult Create(DateTime FechaAsistencia, string HoraSalida, string[] HoraEntrada)
        {
            //TimeSpan HoraEntrada=TimeSpan.Parse("01:00");
            //    TimeSpan HoraSalida=TimeSpan.Parse("08:00");

            //TimeSpan hora2 = TimeSpan.Parse(HoraEntrada);

            //TimeSpan hora3 = TimeSpan.Parse(HoraSalida);
            //d = Correr();
            //TimeSpan hora2 = TimeSpan.Parse(HoraEntrada[]);

            //string hoi = "00:01";
            //TimeSpan hora2=TimeSpan.Parse(hoi);

            //for (int i = 0; i < HoraEntrada.Length; i++)
            //{
            //hora2 = TimeSpan.Parse(HoraEntrada[i]);

            //foreach (DataRow row2 in d.Tables[0].Rows)
            //{
            //    listasistencia.Add(new Asistencias() { Inss = row2["Inss"].ToString(), FechaAsistencia = FechaAsistencia, HoraEntrada = TimeSpan.Parse(HoraEntrada[i]), HoraSalida = TimeSpan.Parse(HoraSalida) });
            //}

            //}


            foreach (DataRow row2 in d.Tables[0].Rows)
            {
                listainss.Add(row2["Inss"].ToString());
            }
            string[] arregloinss = listainss.ToArray();

            for (int i = 0; i < HoraEntrada.Length; i++)
            {
               
                for (int j = 0; j < arregloinss.Length; j++)
                {
                   
                    listasistencia.Add(new Asistencias { Inss = arregloinss[j], HoraEntrada = TimeSpan.Parse(HoraEntrada[i]), HoraSalida = TimeSpan.Parse(HoraSalida),FechaAsistencia=FechaAsistencia });
                    i++;
                }
                }

            if (ModelState.IsValid)
            {

                db.Asistencias.AddRange(listasistencia);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            //prueba de recorrido con el foreach
            //foreach (string wtf in listainss)
            //{
            //    foreach (string wtf2 in HoraEntrada)
            //    {
            //        listasistencia.Add(new Asistencias { Inss=wtf,HoraEntrada=TimeSpan.Parse(wtf2),HoraSalida=TimeSpan.Parse(HoraSalida)});
            //    }
            //}


            //foreach (var i in listasistencia)
            //{
            //    if (i.Inss == bi)
            //    {
            //        listasistencia.RemoveAt(i.Inss.Count());
            //        listasistencia.Add(new Asistencias() { Inss=bi,HoraEntrada=null/*hora2*/,HoraSalida=null});
            //    }
            //}
            return View();
        }

        // GET: Asistencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", asistencias.Inss);
            return View(asistencias);
        }

        // POST: Asistencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAsistencias,Inss,FechaAsistencia,HoraEntrada,FirmaEntrada,HoraSalida,FirmaSalida,Estado")] Asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inss = new SelectList(db.Empleado, "Inss", "Cedula", asistencias.Inss);
            return View(asistencias);
        }

        // GET: Asistencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asistencias asistencias = db.Asistencias.Find(id);
            db.Asistencias.Remove(asistencias);
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
