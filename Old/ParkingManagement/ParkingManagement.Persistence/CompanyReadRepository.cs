using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using ParkingManagement.Domain;

using static System.Int32;

namespace ParkingManagement.Persistence
{
    public class CompanyReadRepository : ICompanyReadRepository
    {
        public Maybe<Company> GetById(Guid id)
        {
            var mockCompany = Company.Create("Centric", 127);
            var range = EmployeesRange.Create(100, MaxValue).Value;

            var mockSubscription = Subscription.Create(range, 100);

            mockCompany.AssignSubscription(new List<Subscription> { mockSubscription });

            return mockCompany;
        }
    }
}