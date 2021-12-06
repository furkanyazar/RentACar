using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ActivationManager : IActivationService
    {
        private IActivationDal _activationDal;
        private IUserService _userService;

        public ActivationManager(IActivationDal activationDal, IUserService userService)
        {
            _activationDal = activationDal;
            _userService = userService;
        }

        public IResult Add(Activation activation)
        {
            activation = GenerateActivationCode(activation);

            _activationDal.Add(activation);

            return new SuccessResult(Messages.ActivationAdded);
        }

        public IResult Delete(Activation activation)
        {
            _activationDal.Delete(activation);

            return new SuccessResult(Messages.ActivationDeleted);
        }

        public IDataResult<List<Activation>> GetAll()
        {
            return new SuccessDataResult<List<Activation>>(_activationDal.GetAll(), Messages.ActivationsListed);
        }

        public IDataResult<Activation> GetById(int activationId)
        {
            return new SuccessDataResult<Activation>(_activationDal.Get(a => a.ActivationId == activationId), Messages.ActivationsListed);
        }

        public IDataResult<Activation> GetByUserId(int userId)
        {
            return new SuccessDataResult<Activation>(_activationDal.Get(a => a.UserId == userId), Messages.ActivationsListed);
        }

        public IResult Update(Activation activation)
        {
            _activationDal.Update(activation);

            return new SuccessResult(Messages.ActivationUpdated);
        }

        [TransactionScopeAspect]
        public IResult SetIsActivated(string email, string activationCode)
        {
            User user = _userService.GetByEmail(email).Data;
            Activation activation = GetByUserId(user.UserId).Data;

            if (activation.ActivationCode == activationCode)
            {
                user.Status = true;
                _userService.Update(user);
                Delete(activation);

                return new SuccessResult(Messages.UserActivated);
            }

            return new ErrorResult(Messages.UserNotActivated);
        }

        public IResult SetIsActivatedForCompany(int userId, bool isActivated)
        {
            User user = _userService.GetById(userId).Data;
            user.Status = isActivated;
            _userService.Update(user);

            return new SuccessResult(Messages.UserActivated);
        }

        private Activation GenerateActivationCode(Activation activation)
        {
            Random random = new Random();

            activation.ActivationCode = random.Next(100000, 1000000).ToString();

            return activation;
        }
    }
}
