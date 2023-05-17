using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data.Entity
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int VehicleYear { get; set; }
        public string PlateNumber { get; set; }

    }
}
