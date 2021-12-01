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
    public class TransmissionManager : ITransmissionService
    {
        private ITransmissionDal _transmissionDal;

        public TransmissionManager(ITransmissionDal transmissionDal)
        {
            _transmissionDal = transmissionDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITransmissionService.Get")]
        [ValidationAspect(typeof(TransmissionValidator))]
        public IResult Add(Transmission transmission)
        {
            IResult result = BusinessRules.Run(CheckIfTransmissionNameExists(transmission.TransmissionName));

            if (result != null)
            {
                return result;
            }

            _transmissionDal.Add(transmission);

            return new SuccessResult(Messages.TransmissionAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITransmissionService.Get")]
        public IResult Delete(Transmission transmission)
        {
            _transmissionDal.Delete(transmission);

            return new SuccessResult(Messages.TransmissionDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Transmission>> GetAll()
        {
            return new SuccessDataResult<List<Transmission>>(_transmissionDal.GetAll(), Messages.TransmissionsListed);
        }

        public IDataResult<Transmission> GetById(int transmissionId)
        {
            return new SuccessDataResult<Transmission>(_transmissionDal.Get(t => t.TransmissionId == transmissionId), Messages.TransmissionsListed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITransmissionService.Get")]
        [ValidationAspect(typeof(TransmissionValidator))]
        public IResult Update(Transmission transmission)
        {
            IResult result = BusinessRules.Run(CheckIfTransmissionNameExists(transmission.TransmissionName));

            if (result != null)
            {
                return result;
            }

            _transmissionDal.Update(transmission);

            return new SuccessResult(Messages.TransmissionUpdated);
        }

        private IResult CheckIfTransmissionNameExists(string transmissionName)
        {
            var result = _transmissionDal.GetAll(b => b.TransmissionName == transmissionName).Any();

            return result ? new ErrorResult(Messages.TransmissionNameAlreadyExists) : new SuccessResult();
        }
    }
}
