using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITractionService
    {
        IDataResult<List<Traction>> GetAll();

        IDataResult<Traction> GetById(int tractionId);

        IResult Add(Traction traction);

        IResult Update(Traction traction);

        IResult Delete(Traction traction);
    }
}
