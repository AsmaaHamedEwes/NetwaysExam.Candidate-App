using AutoMapper;
using Domain.Entities.Entity.Candidate;
using Domain.Entities.Entity.Skills;
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
    public class SkillsService : ISkillService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SkillsService(IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;

        }
        public string CreateSkill(ReqSkillVM req)
        {
            if (req is null)
            {
                ExceptionBase.ThrowException(404, "Skill Req is empty , make sure of providing Requird Data!", "Skill Req is empty , make sure of providing Requird Data!");
            }
            var IsSkillAdded = unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.SkillName.Equals(req.SkillName));
            if (!(IsSkillAdded is null))
            {
                return IsSkillAdded.Code.ToString();
            }
            var NewSkill = new Tbl_Skills()
            {
                 SkillName = req.SkillName , 
                 Rate = req.Rate
            };
            if (NewSkill is null)
            {
                ExceptionBase.ThrowException(500, "Cannot Parse req object to  Tbl_Skills", "Cannot Parse req object to  Tbl_Skills");
            }
            unitOfWork.GetRepository<Tbl_Skills>().Add(NewSkill);
            unitOfWork.SaveChanges();
            return NewSkill.Code.ToString();
        }

        public string DeleteSkill(string SkillId)
        {
            if (SkillId == default || string.IsNullOrWhiteSpace(SkillId))
            {
                ExceptionBase.ThrowException(404, "Skill ID is null or Empty. ", "Skill ID is null or Empty. ");
            }
            var IsFoundedSkill = unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.Code.ToString().Equals(SkillId));
            if (IsFoundedSkill is null)
            {
                ExceptionBase.ThrowException(404, "that Skill is not found.", "that Skill is not found. ");
            }
            unitOfWork.GetRepository<Tbl_Skills>().SoftDelete(IsFoundedSkill);
            unitOfWork.SaveChanges();
            return "Success";
        }

        public List<ResCandidateVM> GetCandidatesCommonSkills(string SkillId)
        {
            if (SkillId == default || string.IsNullOrWhiteSpace(SkillId))
            {
                ExceptionBase.ThrowException(404, "Skill ID is null or Empty. ", "Skill ID is null or Empty. ");
            }
            var CandidateSkills = unitOfWork.GetRepository<Tbl_CandidateSkills>().GetAllIncluding(e => e.SkillId.ToString().Equals(SkillId));
            if (CandidateSkills == default || CandidateSkills.Count <= 0)
            {
                ExceptionBase.ThrowException(404, "this Skill has no registered Candidates.", "this Skill has no registered Candidates.");
            }
            List<ResCandidateVM> resCandidateVMs = new List<ResCandidateVM>();
            foreach (var item in CandidateSkills)
            {
                resCandidateVMs.Add(mapper.Map<ResCandidateVM>(unitOfWork.GetRepository<Tbl_Candidate>().GetSingle(x => x.Id.Equals(item.CandidateId))));
            }
            return resCandidateVMs;
        }

        public ResSkillVM GetSkill(string SkillId)
        {
            if (SkillId == default || string.IsNullOrWhiteSpace(SkillId))
            {
                ExceptionBase.ThrowException(404, "Skill ID is null or Empty.", "Skill ID is null or Empty.");
            }
            var IsFoundedSkill = unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.Code.ToString().Equals(SkillId));
            if (IsFoundedSkill is null)
            {
                ExceptionBase.ThrowException(404, "that is not a Skill ID .", "that Skill is not found.");
            }
            return mapper.Map<ResSkillVM>(IsFoundedSkill);
        }

        public List<ResSkillVM> GetSkills()
        {
            var AllSkills = unitOfWork.GetRepository<Tbl_Skills>().GetAll();
            if (AllSkills != null && AllSkills.Count > 0)
            {
                List<ResSkillVM> skillVMs = new List<ResSkillVM>();
                foreach (var item in AllSkills)
                {
                    skillVMs.Add(GetSkill(item.Code.ToString()));
                }
                return skillVMs;
            }
            return null;
        }

        public string UpdateSkillRecord(ReqSkillVM req, string SkillId)
        {
            if (req is null)
            {
                ExceptionBase.ThrowException(404, "Skill Req is empty , make sure of providing Requird Data!", "Skill Req is empty , make sure of providing Requird Data!");
            }
            if (SkillId == default || string.IsNullOrWhiteSpace(SkillId))
            {
                ExceptionBase.ThrowException(404, "Skill ID is null or Empty. ", "Skill ID is null or Empty. ");
            }
            var IsFoundedSkill = unitOfWork.GetRepository<Tbl_Skills>().GetSingle(e => e.Code.ToString().Equals(SkillId));
            if (IsFoundedSkill is null)
            {
                ExceptionBase.ThrowException(404, "that is not a Skill ID .", "that Skill is not found. ");
            }
            var UpdatedSkill = mapper.Map<Tbl_Skills>(req);
            IsFoundedSkill.SkillName = UpdatedSkill.SkillName;
            IsFoundedSkill.Rate = UpdatedSkill.Rate; 
            unitOfWork.GetRepository<Tbl_Skills>().Update(IsFoundedSkill);
            unitOfWork.SaveChanges();
            return "Success";
        }
    }
}
