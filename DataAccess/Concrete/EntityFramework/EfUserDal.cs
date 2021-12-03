using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RentACarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RentACarContext())
            {
                var result = from opetationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on opetationClaim.OperationClaimId equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.UserId
                             select new OperationClaim { OperationClaimId = opetationClaim.OperationClaimId, OperationClaimName = opetationClaim.OperationClaimName };

                return result.ToList();
            }
        }
    }
}
