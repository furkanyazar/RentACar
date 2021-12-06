using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);

        IDataResult<User> GetByEmail(string email);

        IDataResult<User> GetById(int userId);

        IResult Add(User user);

        IResult Update(User user);
    }
}
