using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data.Entity
{
    public class Insurance:BaseEntity
    {
        [Key]
        public int InsuranceId { get; set; }
        public DateTime InsuranceStartDate { get; set; }
        public DateTime InsuranceEndDate { get; set; }
        public double Price { get; set; }
        public User? User { get; set; }
        public Vehicle? Vehicle { get; set; }
        public InsuranceCompany? InsuranceCompany { get; set; }
    }
}
