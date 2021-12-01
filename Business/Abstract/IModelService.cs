using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IModelService
    {
        IDataResult<List<Model>> GetAll();

        IDataResult<List<Model>> GetAllByBrandId(int brandId);

        IDataResult<Model> GetById(int modelId);

        IResult Add(Model model);

        IResult Update(Model model);

        IResult Delete(Model model);
    }
}
