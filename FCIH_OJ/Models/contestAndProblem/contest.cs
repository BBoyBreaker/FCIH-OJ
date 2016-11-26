using FCIH_OJ.Models.contestAndProblem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCIH_OJ.Models
{
    public class contest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string Name { set; get; }
        public DateTime date { set; get; }
        public int numOfProblems { set; get; }

        public List<problem> problems { set; get; }
        public int re { set; get; }
        // public List<solver> subscribers { set; get; }    would be activated after solver module 
    }
}