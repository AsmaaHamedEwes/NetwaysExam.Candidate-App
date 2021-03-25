using System.Collections.Generic;
namespace Service.ViewModel.VM.Request
{
    public class ReqCandidateVM
    {
        public string FullName { get; set; }
        public float Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public float YearsofExperience { get; set; }
        public List<ReqSkillVM>  ReqSkills { get; set; }
        public List<ReqEmployerVM> ReqEmployers { get; set; }
    }
}
