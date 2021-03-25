using Domain.Entities.Bases;
using Domain.Entities.Entity.Candidate;
using Domain.Entities.Entity.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entity
{
    public class User : IdentityBase
    {
        public string FullName { set; get; }
        public string Phone { get; set; }
        public Tbl_Candidate Tbl_Candidate { get; set; }
        public Tbl_Employer Tbl_Employer { get; set; }
    }
}
