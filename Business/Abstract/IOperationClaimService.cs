using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();

        IDataResult<OperationClaim> GetById(int operationClaimId);

        IDataResult<OperationClaim> GetByName(string operationClaimName);

        IResult Add(OperationClaim operationClaim);

        IResult Update(OperationClaim operationClaim);

        IResult Delete(OperationClaim operationClaim);
    }
}
