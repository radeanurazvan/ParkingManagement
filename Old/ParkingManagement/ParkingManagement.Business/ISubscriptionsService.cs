using System;
using CSharpFunctionalExtensions;
using ParkingManagement.Common;

namespace ParkingManagement.Business
{
    public interface ISubscriptionsService
    {
        Result<decimal> GetPriceForMonth(Guid companyId, Month month, int violations);
    }
}