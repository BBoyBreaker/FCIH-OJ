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
    public class testDifficultiesController : Controller
    {
        private contestAndProblemContext db = new contestAndProblemContext();

        //
        // GET: /testDifficulties/

        public ActionResult Index()
        {
            return View(db.problemDifficulties.ToList());
        }

        //
        // GET: /testDifficulties/Details/5

        public ActionResult Details(int id = 0)
        {
            problemDifficulty problemdifficulty = db.problemDifficulties.Find(id);
            if (problemdifficulty == null)
            {
                return HttpNotFound();
            }
            return View(problemdifficulty);
        }

        //
        // GET: /testDifficulties/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /testDifficulties/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(problemDifficulty problemdifficulty)
        {
            if (ModelState.IsValid)
            {
                db.problemDifficulties.Add(problemdifficulty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(problemdifficulty);
        }

        //
        // GET: /testDifficulties/Edit/5

        public ActionResult Edit(int id = 0)
        {
            problemDifficulty problemdifficulty = db.problemDifficulties.Find(id);
            if (problemdifficulty == null)
            {
                return HttpNotFound();
            }
            return View(problemdifficulty);
        }

        //
        // POST: /testDifficulties/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(problemDifficulty problemdifficulty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(problemdifficulty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(problemdifficulty);
        }

        //
        // GET: /testDifficulties/Delete/5

        public ActionResult Delete(int id = 0)
        {
            problemDifficulty problemdifficulty = db.problemDifficulties.Find(id);
            if (problemdifficulty == null)
            {
                return HttpNotFound();
            }
            return View(problemdifficulty);
        }

        //
        // POST: /testDifficulties/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            problemDifficulty problemdifficulty = db.problemDifficulties.Find(id);
            db.problemDifficulties.Remove(problemdifficulty);
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