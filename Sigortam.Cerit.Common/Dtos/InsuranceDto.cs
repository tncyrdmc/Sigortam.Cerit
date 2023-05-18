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
        public double IdentificationNumber { get; set; }
        public double Price { get; set; }
        public bool IsActive { get { return InsuranceEndDate >= DateTime.Now ? true : false; } }
        public double RemainingTime { get { return (DateTime.Now - InsuranceEndDate).TotalDays; } }
        public UserDto User { get; set; }
        public VehicleDto Vehicle { get; set; }
    }
}
