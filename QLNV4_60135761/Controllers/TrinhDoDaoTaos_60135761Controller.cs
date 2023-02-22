using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNV4_60135761.Models;

namespace QLNV4_60135761.Controllers
{
    public class TrinhDoDaoTaos_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaTrinhDo()
        {
            var maMax = db.TrinhDoDaoTaos.ToList().Select(n => n.MaTD).Max();
            int maTD = int.Parse(maMax.Substring(2)) + 1;
            string TrinhDo = String.Concat("0", maTD.ToString());
            return "TD" + TrinhDo.Substring(maTD.ToString().Length - 1);
        }


        // GET: TrinhDoDaoTaos_60135761
        public ActionResult Index()
        {
            return View(db.TrinhDoDaoTaos.ToList());
        }

        // GET: TrinhDoDaoTaos_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrinhDoDaoTao trinhDoDaoTao = db.TrinhDoDaoTaos.Find(id);
            if (trinhDoDaoTao == null)
            {
                return HttpNotFound();
            }
            return View(trinhDoDaoTao);
        }

        // GET: TrinhDoDaoTaos_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaTD = LayMaTrinhDo();
            return View();
        }

        // POST: TrinhDoDaoTaos_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTD,TenTrinhDo")] TrinhDoDaoTao trinhDoDaoTao)
        {
            if (ModelState.IsValid)
            {
                trinhDoDaoTao.MaTD = LayMaTrinhDo();
                db.TrinhDoDaoTaos.Add(trinhDoDaoTao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trinhDoDaoTao);
        }

        // GET: TrinhDoDaoTaos_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrinhDoDaoTao trinhDoDaoTao = db.TrinhDoDaoTaos.Find(id);
            if (trinhDoDaoTao == null)
            {
                return HttpNotFound();
            }
            return View(trinhDoDaoTao);
        }

        // POST: TrinhDoDaoTaos_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTD,TenTrinhDo")] TrinhDoDaoTao trinhDoDaoTao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trinhDoDaoTao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trinhDoDaoTao);
        }

        // GET: TrinhDoDaoTaos_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrinhDoDaoTao trinhDoDaoTao = db.TrinhDoDaoTaos.Find(id);
            if (trinhDoDaoTao == null)
            {
                return HttpNotFound();
            }
            return View(trinhDoDaoTao);
        }

        // POST: TrinhDoDaoTaos_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TrinhDoDaoTao trinhDoDaoTao = db.TrinhDoDaoTaos.Find(id);
            db.TrinhDoDaoTaos.Remove(trinhDoDaoTao);
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
