using EnsureThat;

namespace ParkingManagement.Domain
{
    public class Subscription : Entity
    {
        private Subscription(EmployeesRange employeesRange, decimal price)
        {
            EnsureArg.IsNotNull(employeesRange);
            EnsureArg.IsGt(price, 0);

            EmployeesRange = employeesRange;
            Price = price;
        }

        public EmployeesRange EmployeesRange { get; private set; }

        public decimal Price { get; private set; }

        public static Subscription Create(EmployeesRange range, decimal price)
        {
            return new Subscription(range, price);
        }
    }
}