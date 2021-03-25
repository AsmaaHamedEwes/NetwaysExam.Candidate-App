using Domain.Entities.Entity.Skills;
using Service.ViewModel.Base;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;

namespace Service.ViewModel.Profiles.Skills
{
    public class SkillProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ReqSkillVM, Tbl_Skills>();
        }

        public override void Response()
        {
            CreateMap<Tbl_Skills, ResSkillVM>().ForMember(dst => dst.Code, otp => otp.MapFrom(src => src.Code.ToString()));
        }
    }
}
