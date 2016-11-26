using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCIH_OJ.Models.contestAndProblem;

namespace FCIH_OJ.Controllers.test
{
    public class testPropTypesController : Controller
    {
        private contestAndProblemContext db = new contestAndProblemContext();

        //
        // GET: /testPropTypes/

        public ActionResult Index()
        {
            return View(db.problemTypes.ToList());
        }

        //
        // GET: /testPropTypes/Details/5

        public ActionResult Details(int id = 0)
        {
            problemType problemtype = db.problemTypes.Find(id);
            if (problemtype == null)
            {
                return HttpNotFound();
            }
            return View(problemtype);
        }

        //
        // GET: /testPropTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /testPropTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(problemType problemtype)
        {
            if (ModelState.IsValid)
            {
                db.problemTypes.Add(problemtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(problemtype);
        }

        //
        // GET: /testPropTypes/Edit/5

        public ActionResult Edit(int id = 0)
        {
            problemType problemtype = db.problemTypes.Find(id);
            if (problemtype == null)
            {
                return HttpNotFound();
            }
            return View(problemtype);
        }

        //
        // POST: /testPropTypes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(problemType problemtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(problemtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(problemtype);
        }

        //
        // GET: /testPropTypes/Delete/5

        public ActionResult Delete(int id = 0)
        {
            problemType problemtype = db.problemTypes.Find(id);
            if (problemtype == null)
            {
                return HttpNotFound();
            }
            return View(problemtype);
        }

        //
        // POST: /testPropTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            problemType problemtype = db.problemTypes.Find(id);
            db.problemTypes.Remove(problemtype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}