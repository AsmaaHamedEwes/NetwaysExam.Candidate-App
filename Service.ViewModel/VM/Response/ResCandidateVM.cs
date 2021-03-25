using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.VM.Response
{
    public class ResCandidateVM
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public float Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public float YearsofExperience { get; set; }
        public List<ResSkillVM>  ResSkills { get; set; }
        public List<ResEmployerVM> ResEmployers { get; set; }
    }
}
