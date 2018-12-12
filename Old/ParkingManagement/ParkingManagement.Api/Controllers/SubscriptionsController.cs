using System;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Business;
using ParkingManagement.Common;

namespace ParkingManagement.Api.Controllers
{
    [Route("api/v1/subscriptions")]
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionsService subscriptionsService;

        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            EnsureArg.IsNotNull(subscriptionsService);
            this.subscriptionsService = subscriptionsService;
        }

        [HttpGet("pricing")]
        public IActionResult GetPriceForMonth([FromQuery] GetPriceModel model)
        {
            var month = Enum.Parse<Month>(model.Month.ToString());

            var pricingResult = this.subscriptionsService.GetPriceForMonth(model.CompanyId, month, model.NumberOfViolations);

            return pricingResult
                .Map(result => new GetPriceResultModel
                {
                    Month = month,
                    CompanyId = model.CompanyId,
                    Pricing = result
                })
                .AsActionResult(NotFound);
        }
    }

    public class GetPriceModel
    {
        public Guid CompanyId { get; set; }

        public int Month { get; set; }

        public int NumberOfViolations { get; set; }
    }

    public class GetPriceResultModel
    {
        public Guid CompanyId { get; set; }

        public Month Month { get; set; }

        public decimal Pricing { get; set; }
    }

    public static class ResultExtensions
    {
        public static IActionResult AsActionResult<T>(this Result<T> result, Func<IActionResult> onFailure)
        {
            if (result.IsFailure)
            {
                return onFailure();
            }

            return new OkObjectResult(result.Value);
        }
    }
}