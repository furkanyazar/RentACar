using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;
        private IModelService _modelService;

        public BrandManager(IBrandDal brandDal, IModelService modelService)
        {
            _brandDal = brandDal;
            _modelService = modelService;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));

            if (result != null)
            {
                return result;
            }

            _brandDal.Add(brand);

            return new SuccessResult(Messages.BrandAdded);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            foreach (var item in _modelService.GetAllByBrandId(brand.BrandId).Data)
            {
                _modelService.Delete(item);
            }

            _brandDal.Delete(brand);

            return new SuccessResult(Messages.BrandDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId), Messages.BrandsListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));

            if (result != null)
            {
                return result;
            }

            _brandDal.Update(brand);

            return new SuccessResult(Messages.BrandUpdated);
        }

        private IResult CheckIfBrandNameExists(string brandName)
        {
            var result = _brandDal.GetAll(b => b.BrandName == brandName).Any();

            return result ? new ErrorResult(Messages.BrandNameAlreadyExists) : new SuccessResult();
        }
    }
}
