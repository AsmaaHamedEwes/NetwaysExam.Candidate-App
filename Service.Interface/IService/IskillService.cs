using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;
using System.Collections.Generic;

namespace Service.Interface.IService
{
    public interface ISkillService
    {
        string CreateSkill(ReqSkillVM req);
        List<ResCandidateVM> GetCandidatesCommonSkills(string SkillId);
        ResSkillVM GetSkill(string SkillId);
        List<ResSkillVM> GetSkills();
        string UpdateSkillRecord(ReqSkillVM req, string SkillId);
        string DeleteSkill(string SkillId);
    }
}
