namespace ParkingManagement.Domain
{
    public interface ICompanyWriteRepository
    {
        void Create(Company company);

        void Commit();
    }
}