using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(CheckIfUserOperationClaimExists(userOperationClaim));

            if (result != null)
            {
                return result;
            }

            _userOperationClaimDal.Add(userOperationClaim);

            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);

            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(), Messages.UserOperationClaimsListed);
        }

        public IDataResult<List<UserOperationClaim>> GetAllByOperationClaimId(int operationClaimId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(u => u.OperationClaimId == operationClaimId), Messages.UserOperationClaimsListed);
        }

        public IDataResult<List<UserOperationClaim>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(u => u.UserId == userId), Messages.UserOperationClaimsListed);
        }

        public IDataResult<UserOperationClaim> GetById(int userOperationClaimId)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(u => u.UserOperationClaimId == userOperationClaimId), Messages.UserOperationClaimsListed);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(CheckIfUserOperationClaimExists(userOperationClaim));

            if (result != null)
            {
                return result;
            }

            _userOperationClaimDal.Update(userOperationClaim);

            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }

        private IResult CheckIfUserOperationClaimExists(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(u => u.OperationClaimId == userOperationClaim.OperationClaimId && u.UserId == userOperationClaim.UserId);

            return result != null ? new ErrorResult(Messages.UserOperationClaimAlreadyExists) : new SuccessResult();
        }
    }
}
