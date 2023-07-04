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
    public partial class InsuranceService : IInsurance
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

            var insuranceCompany = _context.InsuranceCompany.FirstOrDefault(x => x.InsuranceCompanyId == insuranceDto.InsuranceCompany.InsuranceCompanyId);

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

            if (insuranceDto.Vehicle.VehicleId != default)
            {
                vehicle = _context.Vehicle.FirstOrDefault(x => x.VehicleId == insuranceDto.Vehicle.VehicleId);
            }
            else
            {
                vehicle = new Vehicle
                {
                    Brand = insuranceDto.Vehicle.Brand,
                    Model = insuranceDto.Vehicle.Model,
                    PlateNumber = insuranceDto.Vehicle.PlateNumber,
                    VehicleYear = insuranceDto.Vehicle.VehicleYear,
                };
                _context.Vehicle.Add(vehicle);
            }

            var insurance = new Sigortam.Cerit.Data.Entity.Insurance
            {
                InsuranceId = insuranceDto.InsuranceId,
                InsuranceEndDate = insuranceDto.InsuranceEndDate,
                InsuranceStartDate = insuranceDto.InsuranceStartDate,
                Price = insuranceDto.Price,
                User = user,
                InsuranceCompany = insuranceCompany,
                //Vehicle = vehicle,
            };

            if (insuranceDto.InsuranceId > 0)
            {
                _context.Insurance.Update(insurance);
            }
            else
            {
                _context.Insurance.Add(insurance);
            }
            _context.SaveChanges();

        }
        public List<InsuranceDto> GetInsurances()
        {
            return _context.Insurance.Select(x => new InsuranceDto
            {
                InsuranceEndDate = x.InsuranceEndDate,
                InsuranceId = x.InsuranceId,
                InsuranceStartDate = x.InsuranceStartDate,
                Price = x.Price,
                User = x.User != null ? new UserDto
                {
                    Name = x.User.Name,
                    LastName = x.User.LastName,
                    BirthYear = x.User.BirthYear,
                    IdentificationNumber = x.User.IdentificationNumber,
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
                } : default,
                InsuranceCompany = x.InsuranceCompany != null ? new InsuranceCompanyDto
                {
                    IsActive = x.InsuranceCompany.IsActive,
                    ImageSvgUrl = x.InsuranceCompany.ImageSvgUrl,
                    InsuranceCompanyId = x.InsuranceCompany.InsuranceCompanyId,
                    Name = x.InsuranceCompany.Name,
                    Photo = default,
                } : default
            })
             .ToList();
        }
        public InsuranceDto GetInsuranceInformation(int insuranceId)
        {
            return _context.Insurance.Select(x => new InsuranceDto
            {
                InsuranceEndDate = x.InsuranceEndDate,
                InsuranceId = x.InsuranceId,
                InsuranceStartDate = x.InsuranceStartDate,
                Price = x.Price,
                User = x.User != null ? new UserDto
                {
                    Name = x.User.Name,
                    LastName = x.User.LastName,
                    BirthYear = x.User.BirthYear,
                    IdentificationNumber = x.User.IdentificationNumber,
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
                } : default,
                InsuranceCompany = x.InsuranceCompany != null ? new InsuranceCompanyDto
                {
                    IsActive = x.InsuranceCompany.IsActive,
                    ImageSvgUrl = x.InsuranceCompany.ImageSvgUrl,
                    InsuranceCompanyId = x.InsuranceCompany.InsuranceCompanyId,
                    Name = x.InsuranceCompany.Name,
                    Photo = default,
                } : default
            }).FirstOrDefault(x => x.InsuranceId == insuranceId);
        }
        public List<InsuranceCompanyDto> GetInsuranceCompanys()
        {
            return _context.InsuranceCompany.OrderByDescending(x => x.IsActive).Select(x => new InsuranceCompanyDto
            {
                IsActive = x.IsActive,
                InsuranceCompanyId = x.InsuranceCompanyId,
                Name = x.Name,
                Photo = x.Photo,
                ImageSvgUrl = x.ImageSvgUrl,
            }).ToList();

        }
        public void UpdateInsuranceCompanys(List<InsuranceCompanyDto> insuranceCompanyDtos)
        {
            var insuranceCompany = _context.InsuranceCompany.ToList();
            insuranceCompany.ForEach(x => x.IsActive = insuranceCompanyDtos.First(y => y.InsuranceCompanyId == x.InsuranceCompanyId).IsActive);
            _context.UpdateRange(insuranceCompany);
            _context.SaveChanges();
        }
        public UserDto GetUserInformation(string userIdentity)
        {
            var user = _context.User.FirstOrDefault(x => x.IdentificationNumber == Convert.ToDouble(userIdentity));

            return user != null ? new UserDto
            {
                BirthYear = user.BirthYear,
                IdentificationNumber = user.IdentificationNumber,
                Name = user.Name,
                LastName = user.LastName
            } : null;
        }
    }
}
