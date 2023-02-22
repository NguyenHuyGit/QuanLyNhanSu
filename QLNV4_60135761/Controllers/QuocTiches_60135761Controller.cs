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
    public class QuocTiches_60135761Controller : Controller
    {
        private QLNV4_60135761Entities db = new QLNV4_60135761Entities();
        string LayMaQuocTich()
        {
            var maMax = db.QuocTiches.ToList().Select(n => n.MaQT).Max();
            int maQT = int.Parse(maMax.Substring(2)) + 1;
            string QuocTich = String.Concat("0", maQT.ToString());
            return "QT" + QuocTich.Substring(maQT.ToString().Length - 1);
        }

        // GET: QuocTiches_60135761
        public ActionResult Index()
        {
            return View(db.QuocTiches.ToList());
        }

        // GET: QuocTiches_60135761/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuocTich quocTich = db.QuocTiches.Find(id);
            if (quocTich == null)
            {
                return HttpNotFound();
            }
            return View(quocTich);
        }

        // GET: QuocTiches_60135761/Create
        public ActionResult Create()
        {
            ViewBag.MaQT = LayMaQuocTich();
            return View();
        }

        // POST: QuocTiches_60135761/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQT,TenQuocTich")] QuocTich quocTich)
        {
            if (ModelState.IsValid)
            {
                quocTich.MaQT = LayMaQuocTich();
                db.QuocTiches.Add(quocTich);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quocTich);
        }

        // GET: QuocTiches_60135761/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuocTich quocTich = db.QuocTiches.Find(id);
            if (quocTich == null)
            {
                return HttpNotFound();
            }
            return View(quocTich);
        }

        // POST: QuocTiches_60135761/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQT,TenQuocTich")] QuocTich quocTich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quocTich).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quocTich);
        }

        // GET: QuocTiches_60135761/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuocTich quocTich = db.QuocTiches.Find(id);
            if (quocTich == null)
            {
                return HttpNotFound();
            }
            return View(quocTich);
        }

        // POST: QuocTiches_60135761/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuocTich quocTich = db.QuocTiches.Find(id);
            db.QuocTiches.Remove(quocTich);
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
