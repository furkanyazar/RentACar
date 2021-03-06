using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBodyTypeService
    {
        IDataResult<List<BodyType>> GetAll();

        IDataResult<BodyType> GetById(int bodyTypeId);

        IResult Add(BodyType bodyType);

        IResult Update(BodyType bodyType);

        IResult Delete(BodyType bodyType);
    }
}
