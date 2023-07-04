using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Common.Dtos.Setting
{
    public class SettingDto
    {
        public int ExpiredInsurancesDay { get; set; }
        public RemainingTime RemainingTimeType { get; set; }
        public InsuranceExcelExportHeaderColor ExcelExportHeaderColor { get; set; }
    }

}

