using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data.Entity
{
    public class User: BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public double PhoneNumber { get; set; }
        public double IdentificationNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<Insurance>? Insurances { get; set; }
    }
}
