using Microsoft.EntityFrameworkCore;
using ParkingManagement.Domain;

namespace ParkingManagement.Persistence
{
    public class ParkingContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
    }
}