using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Common.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public double PhoneNumber { get; set; }
        public double IdentificationNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
