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
    public class testTestCaseController : Controller
    {
        private contestAndProblemContext db = new contestAndProblemContext();

        //
        // GET: /testTestCase/

        public ActionResult Index()
        {
            var testcases = db.testCases.Include(t => t.Problem);
            return View(testcases.ToList());
        }

        //
        // GET: /testTestCase/Details/5

        public ActionResult Details(int id = 0)
        {
            testCase testcase = db.testCases.Find(id);
            if (testcase == null)
            {
                return HttpNotFound();
            }
            return View(testcase);
        }

        //
        // GET: /testTestCase/Create

        public ActionResult Create()
        {
            ViewBag.problemId = new SelectList(db.problems, "Id", "name");
            return View();
        }

        //
        // POST: /testTestCase/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(testCase testcase)
        {
            if (ModelState.IsValid)
            {
                db.testCases.Add(testcase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.problemId = new SelectList(db.problems, "Id", "name", testcase.problemId);
            return View(testcase);
        }

        //
        // GET: /testTestCase/Edit/5

        public ActionResult Edit(int id = 0)
        {
            testCase testcase = db.testCases.Find(id);
            if (testcase == null)
            {
                return HttpNotFound();
            }
            ViewBag.problemId = new SelectList(db.problems, "Id", "name", testcase.problemId);
            return View(testcase);
        }

        //
        // POST: /testTestCase/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(testCase testcase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testcase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.problemId = new SelectList(db.problems, "Id", "name", testcase.problemId);
            return View(testcase);
        }

        //
        // GET: /testTestCase/Delete/5

        public ActionResult Delete(int id = 0)
        {
            testCase testcase = db.testCases.Find(id);
            if (testcase == null)
            {
                return HttpNotFound();
            }
            return View(testcase);
        }

        //
        // POST: /testTestCase/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            testCase testcase = db.testCases.Find(id);
            db.testCases.Remove(testcase);
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