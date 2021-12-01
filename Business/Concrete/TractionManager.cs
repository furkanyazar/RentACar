using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TractionManager : ITractionService
    {
        private ITractionDal _tractionDal;

        public TractionManager(ITractionDal tractionDal)
        {
            _tractionDal = tractionDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITractionService.Get")]
        [ValidationAspect(typeof(TractionValidator))]
        public IResult Add(Traction traction)
        {
            IResult result = BusinessRules.Run(CheckIfTractionNameExists(traction.TractionName));

            if (result != null)
            {
                return result;
            }

            _tractionDal.Add(traction);

            return new SuccessResult(Messages.TractionAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITractionService.Get")]
        public IResult Delete(Traction traction)
        {
            _tractionDal.Delete(traction);

            return new SuccessResult(Messages.TractionDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Traction>> GetAll()
        {
            return new SuccessDataResult<List<Traction>>(_tractionDal.GetAll(), Messages.TractionsListed);
        }

        public IDataResult<Traction> GetById(int tractionId)
        {
            return new SuccessDataResult<Traction>(_tractionDal.Get(t => t.TractionId == tractionId), Messages.TractionsListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITractionService.Get")]
        [ValidationAspect(typeof(TractionValidator))]
        public IResult Update(Traction traction)
        {
            IResult result = BusinessRules.Run(CheckIfTractionNameExists(traction.TractionName));

            if (result != null)
            {
                return result;
            }

            _tractionDal.Update(traction);

            return new SuccessResult(Messages.TractionUpdated);
        }

        private IResult CheckIfTractionNameExists(string tractionName)
        {
            var result = _tractionDal.GetAll(b => b.TractionName == tractionName).Any();

            return result ? new ErrorResult(Messages.TractionNameAlreadyExists) : new SuccessResult();
        }
    }
}
