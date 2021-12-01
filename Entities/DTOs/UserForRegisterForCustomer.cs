using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class UserForRegisterForCustomerDto : UserForRegisterDto, IDto
    {
        public string IDNo { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
