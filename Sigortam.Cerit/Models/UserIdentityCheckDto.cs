namespace Sigortam.Cerit.Models
{
    public class UserIdentityCheckDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int BirthDate { get; set; }
        public DateTime InsuranceStartDate { get; set; }
        public DateTime InsuranceEndDate { get; set; }
        public double IdentificationNumber { get; set; }
        public string PlateNumber { get; set; }
    }
}
