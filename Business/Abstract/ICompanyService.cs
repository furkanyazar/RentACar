using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        IDataResult<List<Company>> GetAll();

        IDataResult<Company> GetById(int companyId);

        IDataResult<Company> GetByUserId(int userId);

        IResult Add(Company company);

        IResult Update(Company company);

        IResult Delete(Company company);
    }
}
