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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        private IOperationClaimService _operationClaimService;
        private IUserOperationClaimService _userOperationClaimService;

        public CustomerManager(ICustomerDal customerDal, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _customerDal = customerDal;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        [TransactionScopeAspect]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfIDNoExists(customer.IDNo));

            if (result != null)
            {
                return result;
            }

            _customerDal.Add(customer);

            _userOperationClaimService.Add(new UserOperationClaim { UserId = customer.UserId, OperationClaimId = _operationClaimService.GetByName("customer").Data.OperationClaimId });

            return new SuccessResult(Messages.CustomerAdded);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);

            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomersListed);
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == customerId), Messages.CustomersListed);
        }

        public IDataResult<Customer> GetByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId), Messages.CustomersListed);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfIDNoExists(customer.IDNo));

            if (result != null)
            {
                return result;
            }

            _customerDal.Update(customer);

            return new SuccessResult(Messages.CustomerUpdated);
        }

        private IResult CheckIfIDNoExists(string idNo)
        {
            var result = _customerDal.GetAll(c => c.IDNo == idNo).Any();

            return result ? new ErrorResult(Messages.IDNoAlreadyExists) : new SuccessResult();
        }
    }
}
