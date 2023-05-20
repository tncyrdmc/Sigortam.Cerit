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
                InsuranceEndDate = insuranceDto.InsuranceEndDate,
                InsuranceStartDate = insuranceDto.InsuranceStartDate,
                Price = insuranceDto.Price,
                User = user,
                //Vehicle = vehicle,
            };

            _context.Insurance.Add(insurance);
            _context.SaveChanges();

        }
        public List<InsuranceDto> GetInsurances()
        {
            return _context.Insurance.Select(x=> new InsuranceDto
            { 
            InsuranceEndDate = x.InsuranceEndDate,    
            InsuranceId = x.InsuranceId,    
            InsuranceStartDate = x.InsuranceStartDate,
            Price = x.Price,
            User = x.User != null  ? new UserDto 
            {
                Name = x.User.Name,
                LastName =x.User.LastName,
                BirthYear = x.User.BirthYear,
                IdentificationNumber =x.User.IdentificationNumber,
                PhoneNumber = x.User.PhoneNumber,
                UserId = x.User.UserId,
            } : default,
            Vehicle = x.Vehicle != null ? new VehicleDto 
            { 
                 Brand = x.Vehicle.Brand,
                 Model = x.Vehicle.Model,
                 PlateNumber = x.Vehicle.PlateNumber,
                 VehicleId = x.Vehicle.VehicleId,
                 VehicleYear = x.Vehicle.VehicleYear,
            }: default,
            })
             .ToList();
        }
        public List<InsuranceCompanyDto> GetInsuranceCompanys()
        {
            return _context.InsuranceCompany.Select(x => new InsuranceCompanyDto
            {
                IsActive = x.IsActive,
                InsuranceCompanyId = x.InsuranceCompanyId,
                Name = x.Name,
                Photo = x.Photo
            }).ToList();
                
        }
    }
}
