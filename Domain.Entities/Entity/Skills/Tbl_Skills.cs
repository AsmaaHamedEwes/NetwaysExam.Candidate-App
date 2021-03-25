using Domain.Entities.Bases;
using Domain.Entities.Entity.Candidate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entity.Skills
{
    public class Tbl_Skills : EntityBase
    {
        public string SkillName { get; set; }
        public float Rate { get; set; }
        public ICollection<Tbl_CandidateSkills> Tbl_CandidateSkills { set; get; }

    }
}
