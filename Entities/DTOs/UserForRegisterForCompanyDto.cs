using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class UserForRegisterForCompanyDto : UserForRegisterDto, IDto
    {
        public int CityId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string MersisNo { get; set; }
    }
}
