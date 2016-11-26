using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCIH_OJ.Models;
using FCIH_OJ.Models.contestAndProblem;

namespace FCIH_OJ.Controllers
{
    public class contestCController : Controller
    {
        private contestAndProblemContext db = new contestAndProblemContext();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.contests.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            contest contest = db.contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(contest contest)
        {
            if (ModelState.IsValid)
            {
                db.contests.Add(contest);
                db.SaveChanges();
                ViewBag.contestId = contest.Id;
            }
            return View(contest);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            contest contest = db.contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contest contest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contest);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            contest contest = db.contests.Find(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contest contest = db.contests.Find(id);
            List<problem> problems = (db.problems.Where(p => p.contestId == id)).ToList();
            foreach (problem p in problems) {
                List<testCase> testCases = (db.testCases.Where(t => t.problemId == p.Id)).ToList();
                foreach (testCase t in testCases) 
                    db.testCases.Remove(t);
                db.problems.Remove(p);  
            }
            db.contests.Remove(contest);
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