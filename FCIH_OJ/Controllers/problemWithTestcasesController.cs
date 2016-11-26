using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCIH_OJ.Models.contestAndProblem;
namespace FCIH_OJ.Controllers
{
    public class problemWithTestcasesController : Controller
    {
        //
        // GET: /problemWithTestcases/
        private contestAndProblemContext db = new contestAndProblemContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult create(int id) {
            ////to initialize data (problem types and problem difficulties)
            //problemDifficulty propDiffi1 = new problemDifficulty() { difficultyLetter = 'A' };
            //problemType propTyp1 = new problemType() { type = "sorting" };

            //db.problemDifficulties.Add(propDiffi1);
            //db.problemTypes.Add(propTyp1);
            //////problemType pt = db.problemTypes.Find(4);
            //////problemDifficulty pd = db.problemDifficulties.Find(5);
            //////db.problemDifficulties.Remove(pd);
            ////db.problemTypes.Remove(pt);
            //db.SaveChanges();

            ViewBag.contestId = id;
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter");
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type");
            return View();
        }
        [HttpPost]
        public ActionResult create(problemWithTestcases PTs) {

            PTs.problem.contestId = 1;
           
            db.problems.Add(PTs.problem);
            db.SaveChanges();
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter", PTs.problem.problemDifficultyId);
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type", PTs.problem.problemTypeId);
            return View(PTs);
        }
    }
}
