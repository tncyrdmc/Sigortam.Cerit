using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Common.Dtos.Filter
{
    public class FilterDto
    {
        public string SearchText { get; set; }
        public int InsuranceCompanyId { get; set; }
        public StatusType StatusType { get; set; }
        public string FilterSort { get; set; }
        public bool IsResetCasheFilter { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
    public enum StatusType
    {
        All = 0,
        Enable = 1,
        Disable = 2,
    }
}
