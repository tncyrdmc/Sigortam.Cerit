using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data.Entity
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        public int ExpiredInsurancesDay { get; set; }
    }
}
