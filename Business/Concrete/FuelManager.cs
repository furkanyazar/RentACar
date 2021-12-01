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
    public class FuelManager : IFuelService
    {
        private IFuelDal _fuelDal;

        public FuelManager(IFuelDal fuelDal)
        {
            _fuelDal = fuelDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IFuelService.Get")]
        [ValidationAspect(typeof(FuelValidator))]
        public IResult Add(Fuel fuel)
        {
            IResult result = BusinessRules.Run(CheckIfFuelNameExists(fuel.FuelName));

            if (result != null)
            {
                return result;
            }

            _fuelDal.Add(fuel);

            return new SuccessResult(Messages.FuelAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IFuelService.Get")]
        public IResult Delete(Fuel fuel)
        {
            _fuelDal.Delete(fuel);

            return new SuccessResult(Messages.FuelDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Fuel>> GetAll()
        {
            return new SuccessDataResult<List<Fuel>>(_fuelDal.GetAll(), Messages.FuelsListed);
        }

        public IDataResult<Fuel> GetById(int fuelId)
        {
            return new SuccessDataResult<Fuel>(_fuelDal.Get(f => f.FuelId == fuelId), Messages.FuelsListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IFuelService.Get")]
        [ValidationAspect(typeof(FuelValidator))]
        public IResult Update(Fuel fuel)
        {
            IResult result = BusinessRules.Run(CheckIfFuelNameExists(fuel.FuelName));

            if (result != null)
            {
                return result;
            }

            _fuelDal.Update(fuel);

            return new SuccessResult(Messages.FuelUpdated);
        }

        private IResult CheckIfFuelNameExists(string fuelName)
        {
            var result = _fuelDal.GetAll(f => f.FuelName == fuelName).Any();

            return result ? new ErrorResult(Messages.FuelNameAlreadyExists) : new SuccessResult();
        }
    }
}
