using AutoMapper;
using Domain.Entities.Entity.Candidate;
using Domain.Entities.Entity.Employer;
using Infrastructure.IRepository;
using Service.Interface.IService;
using Service.ViewModel.Base;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implementation.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public EmployerService(IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;

        }
        public string CreateEmployer(ReqEmployerVM req)
        {
            if (req is null)
            {
                ExceptionBase.ThrowException(404, "Employer Req is empty , make sure of providing Requird Data!", "Employer Req is empty , make sure of providing Requird Data!");
            }
            var IsEmployerAdded = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.EmployerName.Equals(req.EmployerName));
            if (!(IsEmployerAdded is null))
            {
                return IsEmployerAdded.Code.ToString();
            }
            var NewEmployer = new Tbl_Employer()
            {
                EmployerName = req.EmployerName
            };
            if (NewEmployer is null)
            {
                ExceptionBase.ThrowException(500, "Cannot Parse req object to  Tbl_Employer", "Cannot Parse req object to  Tbl_Employer");
            }
            unitOfWork.GetRepository<Tbl_Employer>().Add(NewEmployer);
            unitOfWork.SaveChanges();
            return NewEmployer.Code.ToString();
        }

        public string DeleteEmployer(string EmployerId)
        {
            if (EmployerId == default || string.IsNullOrWhiteSpace(EmployerId))
            {
                ExceptionBase.ThrowException(404, "Employer ID is null or Empty. ", "Employer ID is null or Empty. ");
            }
            var IsFoundedEmployer = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Code.ToString().Equals(EmployerId));
            if (IsFoundedEmployer is null)
            {
                ExceptionBase.ThrowException(404, "that is not a Employer ID .", "that Employer is not found. ");
            }
            unitOfWork.GetRepository<Tbl_Employer>().SoftDelete(IsFoundedEmployer);
            unitOfWork.SaveChanges();
            return "Success";
        }

        public ResEmployerVM GetEmployer(string EmployerId)
        {
            if (EmployerId == default || string.IsNullOrWhiteSpace(EmployerId))
            {
                ExceptionBase.ThrowException(404, "Employer ID is null or Empty. ", "Employer ID is null or Empty. ");
            }
            var IsFoundedEmployer = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Code.ToString().Equals(EmployerId));
            if (IsFoundedEmployer is null)
            {
                ExceptionBase.ThrowException(404, "that is not a Employer ID .", "that Employer is not found. ");
            }
            return mapper.Map<ResEmployerVM>(IsFoundedEmployer);
        }

        public List<ResEmployerVM> GetEmployers()
        {
            var AllEmployers = unitOfWork.GetRepository<Tbl_Employer>().GetAll();
            if (AllEmployers != null && AllEmployers.Count > 0)
            {
                List<ResEmployerVM> employerVMs = new List<ResEmployerVM>();
                foreach (var item in AllEmployers)
                {
                    employerVMs.Add(GetEmployer(item.Code.ToString()));
                }
                return employerVMs;
            }
            return null;
        }

        public List<ResCandidateVM> GetEmployerCandidates(string EmployerId)
        {
            if (EmployerId == default || string.IsNullOrWhiteSpace(EmployerId))
            {
                ExceptionBase.ThrowException(404, "Employer ID is null or Empty. ", "Employer ID is null or Empty. ");
            }
            var IsFoundedEmployer = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Code.ToString().Equals(EmployerId));
            if (IsFoundedEmployer is null)
            {
                ExceptionBase.ThrowException(404, "that is not a Employer ID .", "that Employer is not found. ");
            }
            var EmpCandidates = unitOfWork.GetRepository<Tbl_CandidateEmployer>().GetAllIncluding(e => e.CurrentEmployerId.Equals(IsFoundedEmployer.Id) || e.PreviousEmployerId.Equals(IsFoundedEmployer.Id));
            if (EmpCandidates == default || EmpCandidates.Count <= 0)
            {
                ExceptionBase.ThrowException(404, "this Employer has no registered Candidates.", "this Employer has no registered Candidates.");
            }
            List<ResCandidateVM> candidateVMs = new List<ResCandidateVM>();
            foreach (var item in EmpCandidates)
            {
                candidateVMs.Add(mapper.Map<ResCandidateVM>(unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(x => x.Id.Equals(item.CandidateId))));
            }
            return candidateVMs;
        }

        public string UpdateEmployerProfile(ReqEmployerVM req, string EmployerId)
        {
            if (req is null)
            {
                ExceptionBase.ThrowException(404, "Employer Req is empty , make sure of providing Requird Data!", "Employer Req is empty , make sure of providing Requird Data!");
            }
            if (EmployerId == default || string.IsNullOrWhiteSpace(EmployerId))
            {
                ExceptionBase.ThrowException(404, "Employer ID is null or Empty. ", "Employer ID is null or Empty. ");
            }
            var IsFoundedEmployer = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Code.ToString().Equals(EmployerId));
            if (IsFoundedEmployer is null)
            {
                ExceptionBase.ThrowException(404, "that is not a Employer ID .", "that Employer is not found. ");
            }
            var UpdatedEmployer = mapper.Map<Tbl_Employer>(req);
            IsFoundedEmployer.EmployerName = UpdatedEmployer.EmployerName;
            unitOfWork.GetRepository<Tbl_Employer>().Update(IsFoundedEmployer);
            unitOfWork.SaveChanges();
            return "Success";

        }
    }
}
