using Domain.Entities.Bases;
using Domain.Entities.Entity.Skills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Entity.Candidate
{
    public class Tbl_CandidateSkills : EntityBase
    {
        public int CandidateId { set; get; }
        [ForeignKey("CandidateId")]
        public Tbl_Candidate Tbl_Candidate { set; get; }
        public int SkillId { set; get; }
        [ForeignKey("SkillId")]
        public Tbl_Skills Tbl_Skills { set; get; }

    }
}
