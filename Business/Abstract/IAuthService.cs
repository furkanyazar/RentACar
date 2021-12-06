using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password, bool status);

        IDataResult<User> RegisterForAdmin(UserForRegisterDto userForRegisterDto, string password);

        IDataResult<User> RegisterForCompany(UserForRegisterForCompanyDto userForRegisterForCompanyDto, string password);

        IDataResult<User> RegisterForCustomer(UserForRegisterForCustomerDto userForRegisterForCustomerDto, string password);

        IDataResult<User> Login(UserForLoginDto userForLoginDto);

        IResult CheckIfUserExists(string email);

        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
