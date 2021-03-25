using AutoMapper;
using Domain.Entities.Entity.Candidate;
using Domain.Entities.Entity.Employer;
using Domain.Entities.Entity.Skills;
using Infrastructure.IRepository;
using Service.Interface.IService;
using Service.ViewModel.Base;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementation.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CandidateService(IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;

        }
        public string CreateCandidate(ReqCandidateVM req)
        {
            if (req is null)
            {
                ExceptionBase.ThrowException(404, "Candidate Req is empty , make sure of providing Requird Data!", "Candidate Req is empty , make sure of providing Requird Data!");
            }
            var IsCandidateAdded = unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(e => e.FullName.Equals(req.FullName) && e.Email.Equals(req.Email));
            if (!(IsCandidateAdded is null))
            {
                return IsCandidateAdded.Code.ToString();
            }
            var NewCandidate = new Tbl_Candidate()
            {
                YearsofExperience = req.YearsofExperience,
                Email = req.Email,
                FullName = req.FullName,
                Title = req.Title,
                Address = req.Address,
                Age = req.Age,
                IsDeleted = false,
                DateOfCreate = DateTime.Now

            };
            if (NewCandidate is null)
            {
                ExceptionBase.ThrowException(500, "Cannot Parse req object to  Tbl_Candidate", "Cannot Parse req object to  Tbl_Candidate");
            }
            unitOfWork.GetRepository<Tbl_Candidate>().Add(NewCandidate);
            unitOfWork.SaveChanges();
            if (!(req.ReqSkills is null) && req.ReqSkills.Count > 0)
            {
                List<Tbl_CandidateSkills> candidateSkills = new List<Tbl_CandidateSkills>();
                foreach (var item in req.ReqSkills)
                {
                    candidateSkills.Add(new Tbl_CandidateSkills()
                    {
                        SkillId = unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.Code.ToString().Equals(item.Code)).Id,
                        CandidateId = NewCandidate.Id
                    });
                }
                unitOfWork.GetRepository<Tbl_CandidateSkills>().AddRing(candidateSkills.ToArray());
                unitOfWork.SaveChanges();
            }
            if (!(req.ReqEmployers is null) && req.ReqEmployers.Count > 0)
            {
                var PrevEmployer = req.ReqEmployers.Where(x => x.ReqEmployerType.Equals(ReqEmployerTypeVM.PreviousEmployer)).FirstOrDefault();
                var CurrentEmployer = req.ReqEmployers.Where(x => x.ReqEmployerType.Equals(ReqEmployerTypeVM.CurrentEmployer)).FirstOrDefault();
                var CandidateEmployer = new Tbl_CandidateEmployer();
                if (PrevEmployer != null)
                {
                    var PrevEmp = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Code.ToString().Equals(PrevEmployer.Code));
                    CandidateEmployer.CandidateId = IsCandidateAdded.Id;
                    CandidateEmployer.PreviousEmployerId = PrevEmp.Id;
                }
                if (CurrentEmployer != null)
                {
                    var CurrEmp = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Code.ToString().Equals(CurrentEmployer.Code));
                    CandidateEmployer.CurrentEmployerId = CurrEmp.Id;
                }
                unitOfWork.GetRepository<Tbl_CandidateEmployer>().Add(CandidateEmployer);
                unitOfWork.SaveChanges();
            }
            return NewCandidate.Code.ToString();
        }

        public string DeleteCandidate(string CandidateId)
        {
            if (CandidateId == default || string.IsNullOrWhiteSpace(CandidateId))
            {
                ExceptionBase.ThrowException(404, "Candidate ID is null or Empty. ", "Candidate ID is null or Empty. ");
            }
            var IsFoundedCandidate = unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(e => e.Code.ToString().Equals(CandidateId));
            if (IsFoundedCandidate is null)
            {
                ExceptionBase.ThrowException(404, "that is not a candidate ID .", "that candidate is not found. ");
            }
            unitOfWork.GetRepository<Tbl_Candidate>().SoftDelete(IsFoundedCandidate);
            unitOfWork.SaveChanges();
            return "Success";
        }

        public ResCandidateVM GetCandidate(string CandidateId)
        {
            if (CandidateId == default || string.IsNullOrWhiteSpace(CandidateId))
            {
                ExceptionBase.ThrowException(404, "Candidate ID is null or Empty. ", "Candidate ID is null or Empty. ");
            }
            var IsFoundedCandidate = unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(e => e.Code.ToString().Equals(CandidateId));
            if (IsFoundedCandidate is null)
            {
                ExceptionBase.ThrowException(404, "that is not a candidate ID .", "that candidate is not found. ");
            }
            var FoundedCandidate = mapper.Map<ResCandidateVM>(IsFoundedCandidate);
            FoundedCandidate.ResEmployers = GetCandidateEmployers(IsFoundedCandidate.Code.ToString());
            FoundedCandidate.ResSkills = GetCandidateSkills(IsFoundedCandidate.Code.ToString());
            return FoundedCandidate;
        }

        public List<ResCandidateVM> GetCandidates()
        {
            var AllCandidates = unitOfWork.GetRepository<Tbl_Candidate>().GetAll();
            if (AllCandidates != null && AllCandidates.Count > 0)
            {
                List<ResCandidateVM> candidateVMs = new List<ResCandidateVM>();
                foreach (var item in AllCandidates)
                {
                    candidateVMs.Add(GetCandidate(item.Code.ToString()));
                }
                return candidateVMs;
            }
            return null;
        }

        public List<ResEmployerVM> GetCandidateEmployers(string CandidateId)
        {
            if (CandidateId == default || string.IsNullOrWhiteSpace(CandidateId))
            {
                ExceptionBase.ThrowException(404, "Candidate ID is null or Empty. ", "Candidate ID is null or Empty. ");
            }
            var IsFoundedCandidate = unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(e => e.Code.ToString().Equals(CandidateId));
            if (IsFoundedCandidate is null)
            {
                ExceptionBase.ThrowException(404, "that is not a candidate ID .", "that candidate is not found. ");
            }
            var CandEmployers = unitOfWork.GetRepository<Tbl_CandidateEmployer>().GetSingle(e => e.CandidateId.Equals(IsFoundedCandidate.Id));
            if (CandEmployers is null)
            {
                ExceptionBase.ThrowException(404, "Candidate has no previous or current employers. ", "Candidate has no previous or current employers. ");

            }
            var PrevEmp = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Id.Equals(CandEmployers.PreviousEmployerId));
            var CurrEmp = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.Id.Equals(CandEmployers.CurrentEmployerId));
            List<ResEmployerVM> employerVMs = new List<ResEmployerVM>();
            employerVMs.Add(mapper.Map<ResEmployerVM>(PrevEmp));
            employerVMs.Add(mapper.Map<ResEmployerVM>(CurrEmp));
            return employerVMs;
        }

        public List<ResSkillVM> GetCandidateSkills(string CandidateId)
        {
            if (CandidateId == default || string.IsNullOrWhiteSpace(CandidateId))
            {
                ExceptionBase.ThrowException(404, "Candidate ID is null or Empty. ", "Candidate ID is null or Empty. ");
            }
            var IsFoundedCandidate = unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(e => e.Code.ToString().Equals(CandidateId));
            if (IsFoundedCandidate is null)
            {
                ExceptionBase.ThrowException(404, "that is not a candidate ID .", "that candidate is not found. ");
            }
            var CandidateSkills = unitOfWork.GetRepository<Tbl_CandidateSkills>().GetAllIncluding(e => e.CandidateId.Equals(IsFoundedCandidate.Id));
            if (CandidateSkills == default || CandidateSkills.Count <= 0)
            {
                ExceptionBase.ThrowException(404, "this candidate has no registed skills.", "this candidate has no registed skills.");
            }
            List<Tbl_Skills> skills = new List<Tbl_Skills>();
            foreach (var item in CandidateSkills)
            {
                skills.Add(unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.Id.Equals(item.SkillId)));
            }
            return mapper.Map<List<ResSkillVM>>(skills);
        }

        public string UpdateCandidateProfile(ReqCandidateVM req, string CandidateId)
        {
            if (req is null)
            {
                ExceptionBase.ThrowException(404, "Candidate Req is empty , make sure of providing Requird Data!", "Candidate Req is empty , make sure of providing Requird Data!");
            }
            if (CandidateId == default || string.IsNullOrWhiteSpace(CandidateId))
            {
                ExceptionBase.ThrowException(404, "Candidate ID is null or Empty. ", "Candidate ID is null or Empty. ");
            }
            var IsFoundedCandidate = unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(e => e.Code.ToString().Equals(CandidateId));
            if (IsFoundedCandidate is null)
            {
                ExceptionBase.ThrowException(404, "that is not a candidate ID .", "that candidate is not found. ");
            }
            var UpdatedCandidate = mapper.Map<Tbl_Candidate>(req);
            unitOfWork.GetRepository<Tbl_Candidate>().Update(UpdatedCandidate);
            if (!(req.ReqSkills is null) && req.ReqSkills.Count > 0)
            {
                List<Tbl_CandidateSkills> candidateSkills = new List<Tbl_CandidateSkills>();
                foreach (var item in req.ReqSkills)
                {
                    candidateSkills.Add(new Tbl_CandidateSkills()
                    {
                        CandidateId = IsFoundedCandidate.Id,
                        SkillId = unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.SkillName.Equals(item.SkillName)).Id
                    });
                }
                unitOfWork.GetRepository<Tbl_CandidateSkills>().UpdateRing(candidateSkills.ToArray());
            }
            if (!(req.ReqEmployers is null) && req.ReqEmployers.Count > 0)
            {
                var PrevEmployer = req.ReqEmployers.Where(x => x.ReqEmployerType.Equals(ReqEmployerTypeVM.PreviousEmployer)).FirstOrDefault();
                var CurrentEmployer = req.ReqEmployers.Where(x => x.ReqEmployerType.Equals(ReqEmployerTypeVM.CurrentEmployer)).FirstOrDefault();
                var CandidateEmployer = new Tbl_CandidateEmployer();
                if (PrevEmployer != null)
                {
                    var PrevEmp = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.EmployerName.Equals(PrevEmployer.EmployerName));
                    CandidateEmployer.CandidateId = IsFoundedCandidate.Id;
                    CandidateEmployer.PreviousEmployerId = PrevEmp.Id;
                }
                if (CurrentEmployer != null)
                {
                    var CurrEmp = unitOfWork.GetRepository<Tbl_Employer>().GetSingle(e => e.EmployerName.Equals(CurrentEmployer.EmployerName));
                    CandidateEmployer.CurrentEmployerId = CurrEmp.Id;
                }
                unitOfWork.GetRepository<Tbl_CandidateEmployer>().Update(CandidateEmployer);
            }

            unitOfWork.SaveChanges();
            return "Success";
        }
    }
}
