using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data.Entity
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }
        public DateTime InsuranceStartDate { get; set; }
        public DateTime InsuranceEndDate { get; set; }
        public double IdentificationNumber { get; set; }
        public double Price { get; set; }
        public bool IsActive { get { return InsuranceEndDate >= DateTime.Now ? true : false; }}
        public double MyProperty { get {return (DateTime.Now - InsuranceEndDate).TotalDays ;}}
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
