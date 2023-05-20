using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data.Entity
{
    public class InsuranceCompany
    {
        [Key]
        public int InsuranceCompanyId { get; set; }
        public string Name { get; set; }
        public Base64FormattingOptions Photo { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Insurance>? Insurances { get; set; }
    }
}
