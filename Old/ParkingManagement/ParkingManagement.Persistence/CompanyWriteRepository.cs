using EnsureThat;
using ParkingManagement.Domain;

namespace ParkingManagement.Persistence
{
    public class CompanyWriteRepository : ICompanyWriteRepository
    {
        private ParkingContext dbContext;

        public CompanyWriteRepository(ParkingContext dbContext)
        {
            EnsureArg.IsNotNull(dbContext);

            this.dbContext = dbContext;
        }

        public void Create(Company company)
        {
            dbContext.Companies.Add(company);
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}