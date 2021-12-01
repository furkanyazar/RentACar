using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelService
    {
        IDataResult<List<Fuel>> GetAll();

        IDataResult<Fuel> GetById(int fuelId);

        IResult Add(Fuel fuel);

        IResult Update(Fuel fuel);

        IResult Delete(Fuel fuel);
    }
}
