using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Common.Dtos
{
    public class InsuranceDto
    {
        public int InsuranceId { get; set; }
        public DateTime InsuranceStartDate { get; set; }
        public DateTime InsuranceEndDate { get; set; }
        public double Price { get; set; }
        public bool IsActive { get { return InsuranceEndDate >= DateTime.Now ? true : false; } }
        public double RemainingTime { get { return Math.Round((InsuranceEndDate - DateTime.Now ).TotalDays) < 1.0 ? 0.00 : Math.Round((InsuranceEndDate - DateTime.Now).TotalDays); } }
        public UserDto? User { get; set; }
        public VehicleDto? Vehicle { get; set; }
    }
}
