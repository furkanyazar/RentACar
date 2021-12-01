using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;
        private IUserOperationClaimService _userOperationClaimService;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IUserOperationClaimService userOperationClaimService)
        {
            _operationClaimDal = operationClaimDal;
            _userOperationClaimService = userOperationClaimService;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(CheckIfOperationClaimNameExists(operationClaim.OperationClaimName));

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Add(operationClaim);

            return new SuccessResult(Messages.OperationClaimAdded);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin")]
        public IResult Delete(OperationClaim operationClaim)
        {
            foreach (var item in _userOperationClaimService.GetAllByOperationClaimId(operationClaim.OperationClaimId).Data)
            {
                _userOperationClaimService.Delete(item);
            }

            _operationClaimDal.Delete(operationClaim);

            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll(), Messages.OperationClaimsListed);
        }

        public IDataResult<OperationClaim> GetById(int operationClaimId)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(o => o.OperationClaimId == operationClaimId), Messages.OperationClaimsListed);
        }

        public IDataResult<OperationClaim> GetByName(string operationClaimName)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(o => o.OperationClaimName == operationClaimName), Messages.OperationClaimsListed);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(CheckIfOperationClaimNameExists(operationClaim.OperationClaimName));

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Update(operationClaim);

            return new SuccessResult(Messages.OperationClaimUpdated);
        }

        private IResult CheckIfOperationClaimNameExists(string operationClaimName)
        {
            var result = _operationClaimDal.GetAll(o => o.OperationClaimName == operationClaimName).Any();

            return result ? new ErrorResult(Messages.OperationClaimNameAlreadyExists) : new SuccessResult();
        }
    }
}
