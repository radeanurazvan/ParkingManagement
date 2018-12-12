using System;
using ParkingManagement.Common;

namespace ParkingManagement.Domain
{
    public class SubscriptionInProgress : Entity
    {
        private SubscriptionInProgress()
        {
        }

        public static SubscriptionInProgress From(Company company, Subscription subscription)
        {
            return new SubscriptionInProgress
            {
                Subscription = subscription,
                Month = Enum.Parse<Month>(DateTime.Now.Month.ToString()),
                CompanyId = company.Id,
                Year = DateTime.Now.Year
            };
        }

        public Guid CompanyId { get; private set; }

        public Subscription Subscription { get; private set; }

        public Month Month { get; private set; }

        public int Year { get; private set; }

        public bool IsInCurrentPeriod(DateTime period)
        {
            var periodMonth = Enum.Parse<Month>(period.Month.ToString());

            return periodMonth == Month && Year == period.Year;
        }
    }
}