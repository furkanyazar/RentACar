using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BodyTypeManager : IBodyTypeService
    {
        private IBodyTypeDal _bodyTypeDal;

        public BodyTypeManager(IBodyTypeDal bodyTypeDal)
        {
            _bodyTypeDal = bodyTypeDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBodyTypeService.Get")]
        [ValidationAspect(typeof(BodyTypeValidator))]
        public IResult Add(BodyType bodyType)
        {
            IResult result = BusinessRules.Run(CheckIfBodyTypeNameExists(bodyType.BodyTypeName));

            if (result != null)
            {
                return result;
            }

            _bodyTypeDal.Add(bodyType);

            return new SuccessResult(Messages.BodyTypeAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBodyTypeService.Get")]
        public IResult Delete(BodyType bodyType)
        {
            _bodyTypeDal.Delete(bodyType);

            return new SuccessResult(Messages.BodyTypeDeleted);
        }

        [CacheAspect]
        public IDataResult<List<BodyType>> GetAll()
        {
            return new SuccessDataResult<List<BodyType>>(_bodyTypeDal.GetAll(), Messages.BodyTypesListed);
        }

        public IDataResult<BodyType> GetById(int bodyTypeId)
        {
            return new SuccessDataResult<BodyType>(_bodyTypeDal.Get(b => b.BodyTypeId == bodyTypeId), Messages.BodyTypesListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBodyTypeService.Get")]
        [ValidationAspect(typeof(BodyTypeValidator))]
        public IResult Update(BodyType bodyType)
        {
            IResult result = BusinessRules.Run(CheckIfBodyTypeNameExists(bodyType.BodyTypeName));

            if (result != null)
            {
                return result;
            }

            _bodyTypeDal.Update(bodyType);

            return new SuccessResult(Messages.BodyTypeUpdated);
        }

        private IResult CheckIfBodyTypeNameExists(string bodyTypeName)
        {
            var result = _bodyTypeDal.GetAll(b => b.BodyTypeName == bodyTypeName).Any();

            return result ? new ErrorResult(Messages.BodyTypeNameAlreadyExists) : new SuccessResult();
        }
    }
}
