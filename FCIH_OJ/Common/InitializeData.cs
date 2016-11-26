using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCIH_OJ.Models.contestAndProblem;
using System.Data.Entity;
using FCIH_OJ.Models;
namespace FCIH_OJ.Common
{
    public class InitializeData:DropCreateDatabaseIfModelChanges<contestAndProblemContext>
    {
        protected override void Seed(contestAndProblemContext context)
        {
            problemDifficulty propDiffi1 = new problemDifficulty() {  Id=1 , difficultyLetter = "A" };
            problemDifficulty propDiffi2 = new problemDifficulty() {  Id=2 ,  difficultyLetter = "B" };
            problemType propTyp1 = new problemType() { Id=1 , type = "constructive algorithms" };
            problemType propTyp2 = new problemType() { Id=2 ,  type = "sorting" };
            contest cont1 = new contest() { Id=1, Name="contest1" , date= new DateTime(1995,5,1) , numOfProblems =3 };
          
            context.problemDifficulties.Add(propDiffi1);
            context.problemDifficulties.Add(propDiffi2);
            context.problemTypes.Add(propTyp1);
            context.problemTypes.Add(propTyp2);
            context.contests.Add(cont1);

            base.Seed(context);
        }
    }
}