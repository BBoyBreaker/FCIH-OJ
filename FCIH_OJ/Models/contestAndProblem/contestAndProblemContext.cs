using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FCIH_OJ.Models.contestAndProblem
{
    public class contestAndProblemContext:DbContext
    {
        public contestAndProblemContext() //these four lines are must to connect to visual database 
            : base("DefaultConnection")   // DefaultConnection is the name of the connection string 
        {
        }
        
        public DbSet<contest> contests { set; get; }
       
        public DbSet<problem> problems { set; get; }

        public DbSet<problemDifficulty> problemDifficulties { get; set; }

        public DbSet<testCase> testCases { get; set; }
        
        public DbSet<problemType>problemTypes {set;get;}
   
    }
}