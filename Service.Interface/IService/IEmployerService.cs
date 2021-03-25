using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;
using System.Collections.Generic;

namespace Service.Interface.IService
{
    public interface IEmployerService
    {
        string CreateEmployer(ReqEmployerVM req);
        List<ResCandidateVM> GetEmployerCandidates(string EmployerId);
        ResEmployerVM GetEmployer(string EmployerId);
        List<ResEmployerVM> GetEmployers();
        string UpdateEmployerProfile(ReqEmployerVM req, string EmployerId);
        string DeleteEmployer(string EmployerId);

    }
}
