using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using EnsureThat;
using ParkingManagement.Common;

namespace ParkingManagement.Domain
{

    public class Company : Entity
    {
        private ICollection<SubscriptionInProgress> subscriptionsInProgress = new List<SubscriptionInProgress>();

        private Company(string name, int numberOfEmployees)
        {
            EnsureArg.IsNotNull(name);
            EnsureArg.IsGt(numberOfEmployees, 0);

            this.Name = name;
            this.NumberOfEmployees = numberOfEmployees;
        }

        public string Name { get; private set; }

        public int NumberOfEmployees { get; private set; }

        public int NumberOfFreeViolations => NumberOfEmployees % 10;

        public IEnumerable<SubscriptionInProgress> SubscriptionsInProgress
        {
            get => subscriptionsInProgress;
            private set => subscriptionsInProgress = value.ToList();
        }

        public static Company Create(string name, int numberOfEmployees)
        {
            return new Company(name, numberOfEmployees);
        }

        public int GetExceedingNumberOfViolations(int violations)
        {
            if (NumberOfFreeViolations >= violations)
            {
                return 0;
            }

            return violations - NumberOfFreeViolations;
        }

        public Result AssignSubscription(IEnumerable<Subscription> subscriptions)
        {
            Maybe<Subscription> matchingSubscriptionOrNothing =
                subscriptions.FirstOrDefault(s => s.EmployeesRange.Contains(this.NumberOfEmployees));

            return matchingSubscriptionOrNothing.ToResult("Company cannot subscribe!")
                .OnSuccess(TryToSubscribe);
        }

        private Result TryToSubscribe(Subscription subscription)
        {
            Maybe<SubscriptionInProgress> currentSubscription = subscriptionsInProgress.FirstOrDefault(s => s.IsInCurrentPeriod(DateTime.UtcNow));

            return currentSubscription.ToInverseResult("Cannot subscribe!")
                .OnSuccess(s => subscriptionsInProgress.Add(SubscriptionInProgress.From(this, subscription)));
        }

        public Maybe<SubscriptionInProgress> SubscriptionFor(Month month)
        {
            return subscriptionsInProgress.FirstOrDefault(s => s.Month == month);
        }
    }
}