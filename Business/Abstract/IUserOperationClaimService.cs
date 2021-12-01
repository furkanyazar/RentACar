using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll();

        IDataResult<List<UserOperationClaim>> GetAllByOperationClaimId(int operationClaimId);

        IDataResult<List<UserOperationClaim>> GetAllByUserId(int userId);

        IDataResult<UserOperationClaim> GetById(int userOperationClaimId);

        IResult Add(UserOperationClaim userOperationClaim);

        IResult Update(UserOperationClaim userOperationClaim);

        IResult Delete(UserOperationClaim userOperationClaim);
    }
}
