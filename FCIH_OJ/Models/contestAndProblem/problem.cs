using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCIH_OJ.Models.contestAndProblem
{
    public class problem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        
        public string name { set; get; }

        public int problemDifficultyId { set; get; }
        [ForeignKey("problemDifficultyId")]
        public problemDifficulty ProblemDifficulty { set; get; }
        

        public int problemTypeId { set; get; }
        [ForeignKey("problemTypeId")]
        public problemType ProblemType { set; get; }

        public int contestId { set; get; }
        [ForeignKey("contestId")]
        public contest Contest { set; get; }

        public testCase [] TestCases { set; get; }
        //public List<problemSetter> problemSetters { set; get; }   would be activated after problemSetter module 

    }
}