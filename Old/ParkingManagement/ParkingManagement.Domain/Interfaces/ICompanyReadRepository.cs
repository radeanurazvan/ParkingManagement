using System;
using CSharpFunctionalExtensions;

namespace ParkingManagement.Domain
{
    public interface ICompanyReadRepository
    {
        Maybe<Company> GetById(Guid id);
    }
}