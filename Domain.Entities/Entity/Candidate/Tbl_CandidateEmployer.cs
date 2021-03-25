using Domain.Entities.Bases;
using Domain.Entities.Entity.Employer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Entity.Candidate
{
    public class Tbl_CandidateEmployer : EntityBase
    {
        public int CandidateId { set; get; }
        [ForeignKey("CandidateId")]
        public Tbl_Candidate Tbl_Candidate { set; get; }
        public int PreviousEmployerId { set; get; }
        [ForeignKey("PreviousEmployerId")]
        public Tbl_Employer Tbl_PreviousEmployer { set; get; }
        public int CurrentEmployerId { set; get; }
        [ForeignKey("CurrentEmployerId")]
        public Tbl_Employer Tbl_CurrentEmployer { set; get; }
    }
}
