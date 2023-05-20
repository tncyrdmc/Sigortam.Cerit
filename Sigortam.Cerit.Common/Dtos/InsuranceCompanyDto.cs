using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Common.Dtos
{
    public class InsuranceCompanyDto
    {
        public int InsuranceCompanyId { get; set; }
        public string Name { get; set; }
        public Base64FormattingOptions Photo { get; set; }
        public bool IsActive { get; set; }
        public string ImageSvgUrl { get; set; }
        public ICollection<InsuranceDto>? Insurances { get; set; }

    }
}
