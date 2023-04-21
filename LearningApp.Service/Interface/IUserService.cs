using LearningApp.DTO;
using LearningApp.Model;
using System.Threading.Tasks;

namespace LearningApp.Service.Interface
{
    public interface IUserService
    {
        Task<UserDetailDto> VerifyUser(LoginModel user);
    }
}
