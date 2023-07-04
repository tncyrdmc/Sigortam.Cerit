using Sigortam.Cerit.Common.Dtos.Setting;
using Sigortam.Cerit.Core.Interfaces;
using Sigortam.Cerit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Core.Services.Setting
{

    public partial class SettingService:ISetting
    {
        public ApplicationDbContext _context;

        public SettingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public SettingDto GetSettings()
        {
            return _context.Setting.Select(x => new SettingDto
            {
                ExpiredInsurancesDay = x.ExpiredInsurancesDay
            })
             .FirstOrDefault();
        }
    }
}
