using FluentValidation;
using outlookCalendarApi.Application.Requests;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries
{
    public class GetEventsValidator : AbstractValidator<GetEventsRequest>
    {
        public GetEventsValidator()
        {
            RuleFor(x => x.PageSize)
                  .GreaterThan(0)
                  .WithMessage(x => string.Format("The pageSize can't be less than zero", nameof(x.PageSize)));
        }
    }
}
