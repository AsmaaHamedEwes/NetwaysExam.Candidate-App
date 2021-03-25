using Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Entity.Candidate
{
    public class Tbl_Candidate : EntityBase
    {
        public string FullName { get; set; }
        public float Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public float YearsofExperience { get; set; }
        public ICollection<Tbl_CandidateSkills> Tbl_CandidateSkills { set; get; }
        public ICollection<Tbl_CandidateEmployer> Tbl_CandidateEmployers { set; get; }
    }
}
