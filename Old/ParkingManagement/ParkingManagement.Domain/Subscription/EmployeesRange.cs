using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EnsureThat;

namespace ParkingManagement.Domain
{
    public class EmployeesRange : ValueObject
    {
        private EmployeesRange(int inferiorLimit, int superiorLimit)
        {
            EnsureArg.IsGt(inferiorLimit, 0);
            EnsureArg.IsGt(superiorLimit, 0);

            InferiorLimit = inferiorLimit;
            SuperiorLimit = superiorLimit;
        }

        public int InferiorLimit { get; private set; }

        public int SuperiorLimit { get; private set; }

        public static Result<EmployeesRange> Create(int inferiorLimit, int superiorLimit)
        {
            if (superiorLimit <= inferiorLimit)
            {
                return Result.Fail<EmployeesRange>("Superior limit should be higher than inferior limit!");
            }

            var range = new EmployeesRange(inferiorLimit, superiorLimit);
            return Result.Ok(range);
        }

        public bool Contains(int subject)
        {
            return InferiorLimit <= subject && SuperiorLimit >= subject;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InferiorLimit;
            yield return SuperiorLimit;
        }
    }
}