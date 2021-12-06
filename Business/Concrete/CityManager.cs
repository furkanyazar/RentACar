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
    public class CityManager : ICityService
    {
        private ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICityService.Get")]
        [ValidationAspect(typeof(CityValidator))]
        public IResult Add(City city)
        {
            IResult result = BusinessRules.Run(CheckIfCityNameExists(city.CityName));

            if (result != null)
            {
                return result;
            }

            _cityDal.Add(city);

            return new SuccessResult(Messages.CityAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICityService.Get")]
        public IResult Delete(City city)
        {
            _cityDal.Delete(city);

            return new SuccessResult(Messages.CityDeleted);
        }

        [CacheAspect]
        public IDataResult<List<City>> GetAll()
        {
            return new SuccessDataResult<List<City>>(_cityDal.GetAll(), Messages.CitysListed);
        }

        public IDataResult<City> GetById(int cityId)
        {
            return new SuccessDataResult<City>(_cityDal.Get(b => b.CityId == cityId), Messages.CitysListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICityService.Get")]
        [ValidationAspect(typeof(CityValidator))]
        public IResult Update(City city)
        {
            IResult result = BusinessRules.Run(CheckIfCityNameExists(city.CityName));

            if (result != null)
            {
                return result;
            }

            _cityDal.Update(city);

            return new SuccessResult(Messages.CityUpdated);
        }

        private IResult CheckIfCityNameExists(string cityName)
        {
            var result = _cityDal.GetAll(b => b.CityName == cityName).Any();

            return result ? new ErrorResult(Messages.CityNameAlreadyExists) : new SuccessResult();
        }
    }
}
