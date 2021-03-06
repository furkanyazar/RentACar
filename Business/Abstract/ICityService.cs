using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICityService
    {
        IDataResult<List<City>> GetAll();

        IDataResult<City> GetById(int cityId);

        IResult Add(City city);

        IResult Update(City city);

        IResult Delete(City city);
    }
}
