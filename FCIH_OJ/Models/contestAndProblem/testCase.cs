using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCIH_OJ.Models.contestAndProblem
{
    public class testCase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int problemId { set; get; }
        [ForeignKey("problemId")]
        public problem Problem { set; get; }

        public string input{set;get;}
        public string output { set; get; }
    }
}