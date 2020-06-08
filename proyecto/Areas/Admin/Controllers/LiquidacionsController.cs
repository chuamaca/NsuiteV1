using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace proyecto.Areas.Admin.Controllers
{
    public class LiquidacionsController : Controller
    {
        private ProyectoContext db = new ProyectoContext();


        public Liquidacion liquidacion = new Liquidacion();

        public int afectado = 0;

        // GET: Admin/Liquidacions
        public ActionResult Index()
        {
            return View(db.Liquidacion.ToList());
        }

        public ActionResult configuracion()
        {
            return View();
        }

        // GET: Admin/Liquidacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacion liquidacion = db.Liquidacion.Find(id);
            if (liquidacion == null)
            {
                return HttpNotFound();
            }
            return View(liquidacion);
        }

        // GET: Admin/Liquidacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Liquidacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLiquidaciones,procesos,estado,contratointerno,refacturable,mes,referencia,doc,numerodocumento,c_fact,fechaemision,fechainicio,fechafin,credito,rucempresa,empresa,responsable,sucursal,ruccliente,cliente,usuariofinal,tipousuario,ordenservicio,fechaordenservicio,rqcliente,contrato,guiaremision,tipo,codigoequipo,tipoequipo,serie,marca,modelo,parnumber,bateria,cargador,procesador,velocidad,ram,disco,licencia,nombreequipo,usuariooficce,cableseguridad,mouse,maletin,softwareadicional,accesorios,observaciones,moneda,valor,igv,total,sefacturo")] Liquidacion liquidacion)
        {
            if (ModelState.IsValid)
            {
                db.Liquidacion.Add(liquidacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(liquidacion);
        }

        // GET: Admin/Liquidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacion liquidacion = db.Liquidacion.Find(id);
            if (liquidacion == null)
            {
                return HttpNotFound();
            }
            return View(liquidacion);
        }

        // POST: Admin/Liquidacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

            //PARA MODIFICAR LOS CAMPOS EDITAR **
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLiquidaciones,procesos,estado,contratointerno,refacturable,mes,referencia,doc,numerodocumento,c_fact,fechaemision,fechainicio,fechafin,credito,rucempresa,empresa,responsable,sucursal,ruccliente,cliente,usuariofinal,tipousuario,ordenservicio,fechaordenservicio,rqcliente,contrato,guiaremision,tipo,codigoequipo,tipoequipo,serie,marca,modelo,parnumber,bateria,cargador,procesador,velocidad,ram,disco,licencia,nombreequipo,usuariooficce,cableseguridad,mouse,maletin,softwareadicional,accesorios,observaciones,moneda,valor,igv,total,sefacturo, identificador")] Liquidacion liquidacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liquidacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liquidacion);
        }

        // GET: Admin/Liquidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacion liquidacion = db.Liquidacion.Find(id);
            if (liquidacion == null)
            {
                return HttpNotFound();
            }
            return View(liquidacion);
        }

        // POST: Admin/Liquidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liquidacion liquidacion = db.Liquidacion.Find(id);
            db.Liquidacion.Remove(liquidacion);
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


        public JsonResult GenerarLiquidacion(string valor)
        {
            var rm = new ResponseModel();
            //HAy que validar que estos se genere una sola vez al mes
            //Es decir que si es marzo solo tenga una vez registrada ese mes
            RRR_Liquidacion_mensual();
           

            if (afectado != -1)
            {
                rm.response = true;
                rm.message = "SE AFECTO [ " + afectado + " ]  FILAS";
                rm.href = Url.Content("~/admin/liquidacions/index");
                // ViewBag.rta = "LAs columnas afectadas fueron" + afectado;
            }else
            {
                rm.response = false;
                rm.message = "YA EXISTE LIQUIDACION DEL MES ACTUAL";
                rm.href = "self";
            }
            return Json(rm,JsonRequestBehavior.AllowGet);

          //  return Redirect("~/admin/liquidacion/index");


        }

        public void RRR_Liquidacion_mensual()
        {
            using (var ctx = new ProyectoContext())
            {

                afectado = ctx.Database.ExecuteSqlCommand("RRR_insert_liquidacion");
            }
            //var f = new ProyectoContext();
            ////    SqlParameter param1 = new SqlParameter("@idhardware", idhardware);
            //return f.Database.SqlQuery<Liquidacion>("RRR_insert_liquidacion").AllAsync(estado == true
            //    );
        }
    }
}
