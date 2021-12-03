using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ModelManager : IModelService
    {
        private IModelDal _modelDal;
        private ICarService _carService;

        public ModelManager(IModelDal modelDal, ICarService carService)
        {
            _modelDal = modelDal;
            _carService = carService;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IModelService.Get")]
        [ValidationAspect(typeof(ModelValidator))]
        public IResult Add(Model model)
        {
            IResult result = BusinessRules.Run(CheckIfModelNameExists(model.ModelName));

            if (result != null)
            {
                return result;
            }

            _modelDal.Add(model);

            return new SuccessResult(Messages.ModelAdded);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IModelService.Get")]
        public IResult Delete(Model model)
        {
            foreach (var item in _carService.GetAllByModelId(model.ModelId).Data)
            {
                _carService.Delete(item);
            }

            _modelDal.Delete(model);

            return new SuccessResult(Messages.ModelDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Model>> GetAll()
        {
            return new SuccessDataResult<List<Model>>(_modelDal.GetAll(), Messages.ModelsListed);
        }

        public IDataResult<List<Model>> GetAllByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Model>>(_modelDal.GetAll(m => m.BrandId == brandId), Messages.ModelsListed);
        }

        public IDataResult<Model> GetById(int modelId)
        {
            return new SuccessDataResult<Model>(_modelDal.Get(m => m.ModelId == modelId), Messages.ModelsListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IModelService.Get")]
        [ValidationAspect(typeof(ModelValidator))]
        public IResult Update(Model model)
        {
            IResult result = BusinessRules.Run(CheckIfModelNameExists(model.ModelName));

            if (result != null)
            {
                return result;
            }

            _modelDal.Update(model);

            return new SuccessResult(Messages.CompanyAdded);
        }

        private IResult CheckIfModelNameExists(string modelName)
        {
            var result = _modelDal.GetAll(b => b.ModelName == modelName).Any();

            return result ? new ErrorResult(Messages.ModelNameAlreadyExists) : new SuccessResult();
        }
    }
}
