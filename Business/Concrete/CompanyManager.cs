using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private ICompanyDal _companyDal;
        private IOperationClaimService _operationClaimService;
        private IUserOperationClaimService _userOperationClaimService;

        public CompanyManager(ICompanyDal companyDal, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _companyDal = companyDal;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        [TransactionScopeAspect]
        public IResult Add(Company company)
        {
            IResult result = BusinessRules.Run(CheckIfMersisNoExists(company.MersisNo));

            if (result != null)
            {
                return result;
            }

            _companyDal.Add(company);

            _userOperationClaimService.Add(new UserOperationClaim { UserId = company.UserId, OperationClaimId = _operationClaimService.GetByName("company").Data.OperationClaimId });

            return new SuccessResult(Messages.CompanyAdded);
        }

        public IResult Delete(Company company)
        {
            _companyDal.Delete(company);

            return new SuccessResult(Messages.CompanyDeleted);
        }

        public IDataResult<List<Company>> GetAll()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetAll(), Messages.CompaniesListed);
        }

        public IDataResult<Company> GetById(int companyId)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(c => c.CompanyId == companyId), Messages.CompaniesListed);
        }

        public IDataResult<Company> GetByUserId(int userId)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(c => c.UserId == userId), Messages.CompaniesListed);
        }

        [ValidationAspect(typeof(CompanyValidator))]
        public IResult Update(Company company)
        {
            IResult result = BusinessRules.Run(CheckIfMersisNoExists(company.MersisNo));

            if (result != null)
            {
                return result;
            }

            _companyDal.Update(company);

            return new SuccessResult(Messages.CompanyUpdated);
        }

        private IResult CheckIfMersisNoExists(string mersisNo)
        {
            var result = _companyDal.GetAll(c => c.MersisNo == mersisNo).Any();

            return result ? new ErrorResult(Messages.MersisNoAlreadyExists) : new SuccessResult();
        }
    }
}
