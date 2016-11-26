using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCIH_OJ.Models;
namespace FCIH_OJ.Models.contestAndProblem
{
    public class problemWithTestcases
    {
        public problem problem { set; get; }
        public testCase [] testcases { get; set; }
    }
}