using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IActivationService
    {
        IDataResult<List<Activation>> GetAll();

        IDataResult<Activation> GetById(int activationId);

        IDataResult<Activation> GetByUserId(int userId);

        IResult Add(Activation activation);

        IResult Update(Activation activation);

        IResult Delete(Activation activation);

        IResult SetIsActivated(string email, string activationCode);

        IResult SetIsActivatedForCompany(int userId, bool isActivated);
    }
}
