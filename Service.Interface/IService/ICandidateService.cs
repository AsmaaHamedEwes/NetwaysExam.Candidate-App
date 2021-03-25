using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;
using System.Collections.Generic;

namespace Service.Interface.IService
{
    public interface ICandidateService
    {
        string CreateCandidate(ReqCandidateVM req);
        List<ResEmployerVM> GetCandidateEmployers(string CandidateId);
        ResCandidateVM GetCandidate(string CandidateId);
        List<ResCandidateVM> GetCandidates();
        List<ResSkillVM> GetCandidateSkills(string CandidateId);
        string UpdateCandidateProfile(ReqCandidateVM req, string CandidateId);
        string DeleteCandidate(string CandidateId);

    }
}
