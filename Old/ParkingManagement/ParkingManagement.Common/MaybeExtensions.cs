using CSharpFunctionalExtensions;

namespace ParkingManagement.Common
{
    public static class MaybeExtensions
    {
        public static Result<T> ToInverseResult<T>(this Maybe<T> subject, string error)
        {
            if (subject.HasValue)
            {
                return Result.Fail<T>(error);
            }

            return Result.Ok(subject.Value);
        }
    }
}