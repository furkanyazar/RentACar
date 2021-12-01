using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITransmissionService
    {
        IDataResult<List<Transmission>> GetAll();

        IDataResult<Transmission> GetById(int transmissionId);

        IResult Add(Transmission transmission);

        IResult Update(Transmission transmission);

        IResult Delete(Transmission transmission);
    }
}
