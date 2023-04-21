using LearningApp.DTO;
using LearningApp.Model;
using LearningApp.Service.Interface;
using System.Threading.Tasks;

namespace LearningApp.Service.Implementation
{
    public class UserService : IUserService
    {
        public Task<UserDetailDto> VerifyUser(LoginModel user)
        {
            UserDetailDto userDetail = new();
            userDetail.Name = "Raj";
            return Task.FromResult(userDetail);
        }
    }
}
