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
    public class testProblemController : Controller
    {
        private contestAndProblemContext db = new contestAndProblemContext();

        //
        // GET: /testProblem/

        public ActionResult Index()
        {
            var problems = db.problems.Include(p => p.ProblemDifficulty).Include(p => p.ProblemType).Include(p => p.Contest);
            return View(problems.ToList());
        }

        //
        // GET: /testProblem/Details/5

        public ActionResult Details(int id = 0)
        {
            problem problem = db.problems.Find(id);
            if (problem == null)
            {
                return HttpNotFound();
            }
            return View(problem);
        }

        //
        // GET: /testProblem/Create

        public ActionResult Create()
        {
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter");
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type");
            ViewBag.contestId = new SelectList(db.contests, "Id", "Name");
            return View();
        }

        //
        // POST: /testProblem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(problem problem)
        {
            if (ModelState.IsValid)
            {
                db.problems.Add(problem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter", problem.problemDifficultyId);
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type", problem.problemTypeId);
            ViewBag.contestId = new SelectList(db.contests, "Id", "Name", problem.contestId);
            return View(problem);
        }

        //
        // GET: /testProblem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            problem problem = db.problems.Find(id);
            if (problem == null)
            {
                return HttpNotFound();
            }
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter", problem.problemDifficultyId);
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type", problem.problemTypeId);
            ViewBag.contestId = new SelectList(db.contests, "Id", "Name", problem.contestId);
            return View(problem);
        }

        //
        // POST: /testProblem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(problem problem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(problem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter", problem.problemDifficultyId);
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type", problem.problemTypeId);
            ViewBag.contestId = new SelectList(db.contests, "Id", "Name", problem.contestId);
            return View(problem);
        }

        //
        // GET: /testProblem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            problem problem = db.problems.Find(id);
            if (problem == null)
            {
                return HttpNotFound();
            }
            return View(problem);
        }

        //
        // POST: /testProblem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            problem problem = db.problems.Find(id);
            db.problems.Remove(problem);
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