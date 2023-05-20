using Sigortam.Cerit.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Core.Interfaces
{
    public interface IInsurance
    {
         void AddOrUpdateInsurance(InsuranceDto insuranceDto);
         List<InsuranceDto> GetInsurances();
    }
}
