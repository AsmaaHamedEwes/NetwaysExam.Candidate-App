using Domain.Entities.Entity;
using Domain.Entities.Entity.Candidate;
using Domain.Entities.Entity.Developer;
using Domain.Entities.Entity.Employer;
using Domain.Entities.Entity.Skills;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Persistance
{
    public partial class AppDbContext
    {

        #region Developer

        public DbSet<Tbl_Exception> Tbl_Exceptions { get; set; }

        #endregion

        #region Candidate

        public DbSet<Tbl_Candidate> Tbl_Candidates { get; set; }

        public DbSet<Tbl_CandidateEmployer>  Tbl_CandidateEmployers { get; set; }
        public DbSet<Tbl_CandidateSkills> Tbl_CandidateSkills { get; set; }
        #endregion
        #region Employer

        public DbSet<Tbl_Employer> Tbl_Employers { get; set; }

        #endregion
        #region Skills

        public DbSet<Tbl_Skills>  Tbl_Skills { get; set; }

        #endregion
        #region Tokens
        public DbSet<Tbl_Token> Tbl_Token { set; get; }
        #endregion

    }
}
