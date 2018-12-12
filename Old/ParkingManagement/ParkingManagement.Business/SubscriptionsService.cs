using CSharpFunctionalExtensions;
using EnsureThat;
using ParkingManagement.Domain;
using System;
using ParkingManagement.Common;

namespace ParkingManagement.Business
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly ICompanyReadRepository companyRepository;

        public SubscriptionsService(ICompanyReadRepository companyRepository)
        {
            EnsureArg.IsNotNull(companyRepository);
            this.companyRepository = companyRepository;
        }

        private const decimal violationCost = 30;

        public Result<decimal> GetPriceForMonth(Guid companyId, Month month, int violations)
        {
            EnsureArg.IsNotEmpty(companyId);
            EnsureArg.IsGte(violations, 0);

            var companyOrNothing = companyRepository.GetById(companyId);
            if (companyOrNothing.HasNoValue)
            {
                return Result.Fail<decimal>("Company does not exist");
            }

            var company = companyOrNothing.Value;

            var subscriptionOrNothing = company.SubscriptionFor(month);
            return subscriptionOrNothing.ToResult("No subscription for given month!")
                .OnSuccess(subscription =>
                {
                    var employeesCost = company.NumberOfEmployees * subscription.Subscription.Price;
                    var exceedingNumberOfViolations = company.GetExceedingNumberOfViolations(violations);
                    var violationsCost = exceedingNumberOfViolations * violationCost;

                    return employeesCost + violationsCost;
                });
        }
    }
}