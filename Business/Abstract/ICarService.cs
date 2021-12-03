using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();

        IDataResult<List<Car>> GetAllByModelId(int modelId);

        IDataResult<Car> GetById(int carId);

        IResult Add(Car car);

        IResult Update(Car car);

        IResult Delete(Car car);
    }
}
