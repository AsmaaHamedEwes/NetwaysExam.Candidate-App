using Domain.Entities.Bases;
using Domain.Entities.Entity.Candidate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Entity.Employer
{
    public class Tbl_Employer : EntityBase
    {
        public string UserId { set; get; }
        [ForeignKey("UserId")]
        public User User { set; get; }
        public string EmployerName { get; set; }
        public ICollection<Tbl_CandidateEmployer> Tbl_CandidateEmployers { set; get; }

    }
}
