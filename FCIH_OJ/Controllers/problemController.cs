using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCIH_OJ.Models.contestAndProblem;
namespace FCIH_OJ.Controllers
{
    public class problemController : Controller
    {
        private contestAndProblemContext db = new contestAndProblemContext();

        //
        // GET: /problem/

        public ActionResult Index()
        {
            var problems = db.problems.Include(p => p.ProblemDifficulty).Include(p => p.ProblemType).Include(p => p.Contest);
            return View(problems.ToList());
        }
        [ActionName("contestProblems")]
        public ActionResult Index(int contestId) {
            ViewBag.contestId = contestId;
            var problems = db.problems.Where(p=>p.contestId==contestId).Include(p => p.ProblemDifficulty).Include(p => p.ProblemType).Include(p => p.Contest);
            return View( "Index" , problems.ToList());
        }
        //
        // GET: /problem/Details/5

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
        // GET: /problem/Create

        public ActionResult Create(int contestId)
        {
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter");
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type");
           //ViewBag.contestId = contestId;  attempt1 also in problem/create >> you need not this baecause this para contestId would automatically passed to the Model.contestId in the view
            ViewData["submit"] = "Create the problem";
            return View();
        }

        //
        // POST: /problem/Create
      public static int sentProblemIdToEdit;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(problem problem , string submit )
        {
            bool test = ModelState.IsValid;
                                          
            if (ModelState.IsValid)
            {
                if (submit == "Create the problem")
                {
                    db.problems.Add(problem);
                    db.SaveChanges();
                    db.Entry(problem).GetDatabaseValues();  //allahakbr2 this line is because the obejct problem would has id =0 and you want the id that this object has form database to send it to Edit action so you would write this line and you must call this fun only after the creation that if you click create the problem then make another postback by clicking update the problem so in this postback you can not get call this fun with problem object because of error that this object id not existed in database context and this mean you can not do allahakbr1
                    sentProblemIdToEdit = problem.Id;
                    if (problem.TestCases != null)
                    {
                        int numOfTestcases = problem.TestCases.Count();
                        for (int k = 0; k < numOfTestcases; k++)
                        {
                            problem.TestCases[k].problemId = problem.Id;
                            db.testCases.Add(problem.TestCases[k]);
                        }
                        db.SaveChanges();     
                    }
                    ViewData["textAfterCreation"] = "problem has been added succeffully";
                    ViewData["submit"] = "Update the problem"; 
                }
                else {
                   // db.Entry(problem).GetDatabaseValues(); // allahakbr1  you can not write this here because of allahakbr2
                    return RedirectToAction("Edit", new { id=sentProblemIdToEdit});
                }
            }
            ViewBag.problemDifficultyId = new SelectList(db.problemDifficulties, "Id", "difficultyLetter", problem.problemDifficultyId);
            ViewBag.problemTypeId = new SelectList(db.problemTypes, "Id", "type", problem.problemTypeId);

            return View(problem);
        }

        //
        // GET: /problem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            problem problem = db.problems.Find(id);
            problem.TestCases  = db.testCases.Where(x => x.problemId == id).ToList().ToArray();

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
        // POST: /problem/Edit/5

        public void saveTestCaseChanges(int id , testCase testCaseIO) {
            testCase t = db.testCases.Find(id);
            t.input = testCaseIO.input;
            t.output = testCaseIO.output;
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void deleteTestCase(int id ) {
            testCase testCase = db.testCases.Find(id);
            db.testCases.Remove(testCase);
            db.SaveChanges();
        }

        public PartialViewResult addNewTestCaseInEdit(int problemId, string newTestCaseInput, string newTestCaseOutput)
        {
            testCase t = new testCase();
            t.input = newTestCaseInput;
            t.output = newTestCaseOutput;
            t.problemId = problemId;
          
            db.testCases.Add(t);
            db.SaveChanges();
            db.Entry(t).GetDatabaseValues();
            
            return PartialView("_editTestCase", t);
        }

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
        // GET: /problem/Delete/5

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
        // POST: /problem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            problem problem = db.problems.Find(id);
            List<testCase> testCases = db.testCases.Where(t => t.problemId == id).ToList();
            foreach (testCase t in testCases)
                db.testCases.Remove(t);
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