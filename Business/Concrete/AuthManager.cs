using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ICompanyService _companyService;
        private ICustomerService _customerService;
        private IOperationClaimService _operationClaimService;
        private IUserOperationClaimService _userOperationClaimService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ICompanyService companyService, ICustomerService customerService, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _companyService = companyService;
            _customerService = customerService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            var userToCheckActivation = CheckIfUserISActivated(userForLoginDto.Email);
            if (!userToCheckActivation.Success)
            {
                return new ErrorDataResult<User>(userToCheckActivation.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password, bool status)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = status
            };

            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [TransactionScopeAspect]
        public IDataResult<User> RegisterForAdmin(UserForRegisterDto userForRegisterDto, string password)
        {
            var result = Register(userForRegisterDto, password, true);

            _userOperationClaimService.Add(new UserOperationClaim { UserId = result.Data.UserId, OperationClaimId = _operationClaimService.GetByName("admin").Data.OperationClaimId });

            return new SuccessDataResult<User>(result.Data, Messages.UserRegistered);
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(CompanyValidator))]
        public IDataResult<User> RegisterForCompany(UserForRegisterForCompanyDto userForRegisterForCompanyDto, string password)
        {
            var result = Register(userForRegisterForCompanyDto, password, false);

            _companyService.Add(new Company { UserId = result.Data.UserId, CityId = userForRegisterForCompanyDto.CityId, CompanyName = userForRegisterForCompanyDto.CompanyName, Address = userForRegisterForCompanyDto.Address, MersisNo = userForRegisterForCompanyDto.MersisNo });

            return new SuccessDataResult<User>(result.Data, Messages.UserRegistered);
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(CustomerValidator))]
        public IDataResult<User> RegisterForCustomer(UserForRegisterForCustomerDto userForRegisterForCustomerDto, string password)
        {
            var result = Register(userForRegisterForCustomerDto, password, false);

            _customerService.Add(new Customer { UserId = result.Data.UserId, DateOfBirth = userForRegisterForCustomerDto.DateOfBirth, IDNo = userForRegisterForCustomerDto.IDNo });

            return new SuccessDataResult<User>(result.Data, Messages.UserRegistered);
        }

        public IResult CheckIfUserExists(string email)
        {
            var result = _userService.GetByEmail(email).Data;

            return result != null ? new ErrorResult(Messages.UserAlreadyExists) : new SuccessResult();
        }

        private IResult CheckIfUserISActivated(string email)
        {
            return _userService.GetByEmail(email).Data.Status ? new SuccessResult() : new ErrorResult(Messages.UserIsNotActivated);
        }
    }
}
