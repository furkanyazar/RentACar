using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Company : IEntity
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string MersisNo { get; set; }
    }
}
