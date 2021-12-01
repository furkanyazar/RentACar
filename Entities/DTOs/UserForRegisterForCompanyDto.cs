using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserForRegisterForCompanyDto : UserForRegisterDto, IDto
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string MersisNo { get; set; }
    }
}
