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
        public string RemaningYear { get { return Math.Round(RemainingTime/365.0) > 0 ? Math.Round(RemainingTime / 365.0).ToString() + " Yıl, " : string.Empty ; } }
        public string RemaningMonth { get { return Math.Round((RemainingTime % 365) / 30) > 0 ? Math.Round((RemainingTime % 365) / 30).ToString() + " Ay, " : string.Empty ; } }
        public string RemaningDay { get { return Math.Round((RemainingTime % 365) % 30) > 0 ? Math.Round((RemainingTime % 365) % 30).ToString() + " Gün" : "0 Gün"; } }
        public string RemainingTimeYearMonthDayType { get { return string.Join(' ' , RemaningYear , RemaningMonth , RemaningDay) ;}}
        public UserDto? User { get; set; }
        public VehicleDto? Vehicle { get; set; }
        public InsuranceCompanyDto? InsuranceCompany { get; set; }
        public string PermitNumber { get; set; }
        public string SearchableText { get {return string.Join(" ",User?.FullName.ToLower() ?? string.Empty , User?.IdentificationNumber.ToString().ToLower() ?? string.Empty ,Vehicle?.PlateNumber.ToLower() ?? string.Empty)  ;} }
    }
}
