using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response.User;
using System.Threading.Tasks;

namespace Service.Interface.IService
{
    public interface IUserService
    {
        Task<ResponseLoginVM> RefreshToken(RefreshToken refreshToken);
    }
}
