using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class UserServices:IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices()
        {
            _userRepository=new UserRepository();
        }
    }
}
