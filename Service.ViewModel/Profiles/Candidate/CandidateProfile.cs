using Domain.Entities.Entity.Candidate;
using Service.ViewModel.Base;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModel.Profiles.Candidate
{
    public class CandidateProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ReqCandidateVM, Tbl_Candidate>();
        }

        public override void Response()
        {
            CreateMap< Tbl_Candidate , ResCandidateVM>().ForMember(dst => dst.UserId, otp => otp.MapFrom(src => src.Code.ToString()));
        }
    }
}
