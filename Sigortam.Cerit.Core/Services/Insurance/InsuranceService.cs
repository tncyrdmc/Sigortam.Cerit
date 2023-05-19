using Sigortam.Cerit.Common.Dtos;
using Sigortam.Cerit.Core.Interfaces;
using Sigortam.Cerit.Data;
using Sigortam.Cerit.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sigortam.Cerit.Core.Services.Insurance
{
    public partial class InsuranceService:IInsurance
    {
        public ApplicationDbContext _context;
        public InsuranceService(ApplicationDbContext context)
        {
            _context = context;
        }
        //[HandleTransaction]
        public void AddOrUpdateInsurance(InsuranceDto insuranceDto)
        {
            var user = new Sigortam.Cerit.Data.Entity.User();
            var vehicle = new Sigortam.Cerit.Data.Entity.Vehicle();

            if (insuranceDto.User.UserId != default)
            {
                 user = _context.User.FirstOrDefault(x => x.UserId == insuranceDto.User.UserId);
            }
            else
            {
                user = new User
                {
                    BirthYear = insuranceDto.User.BirthYear,
                    IdentificationNumber = insuranceDto.User.IdentificationNumber,
                    IsActive = insuranceDto.User.IsActive,
                    IsDelete = insuranceDto.User.IsDelete,
                    LastName = insuranceDto.User.LastName,
                    Name = insuranceDto.User.Name,
                    PhoneNumber = insuranceDto.User.PhoneNumber,
                };
                _context.User.Add(user);
            }

            //if (insuranceDto.Vehicle.VehicleId != default)
            //{
            //    vehicle = _context.Vehicle.FirstOrDefault(x => x.VehicleId == insuranceDto.Vehicle.VehicleId);
            //}
            //else
            //{
            //    vehicle = new Vehicle
            //    {
            //        Brand = insuranceDto.Vehicle.Brand,
            //        Model = insuranceDto.Vehicle.Model,
            //        PlateNumber = insuranceDto.Vehicle.PlateNumber,
            //        VehicleYear = insuranceDto.Vehicle.VehicleYear,
            //    };
            //    _context.Vehicle.Add(vehicle);
            //}

            var insurance = new Sigortam.Cerit.Data.Entity.Insurance
            {
                IdentificationNumber = insuranceDto.IdentificationNumber,
                InsuranceEndDate = insuranceDto.InsuranceEndDate,
                InsuranceStartDate = insuranceDto.InsuranceStartDate,
                Price = insuranceDto.Price,
                User = user,
                //Vehicle = vehicle,
            };

            _context.Insurance.Add(insurance);
            _context.SaveChanges();

        }
    }
}
