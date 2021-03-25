using Domain.Entities.Entity.Employer;
using Service.ViewModel.Base;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;

namespace Service.ViewModel.Profiles.Employer
{
    public class EmployerProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ReqEmployerVM, Tbl_Employer>();
        }

        public override void Response()
        {
            CreateMap<Tbl_Employer, ResEmployerVM>().ForMember(dst => dst.Code, otp => otp.MapFrom(src => src.Code.ToString()));
        }
    }
}
