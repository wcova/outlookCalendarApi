using FluentValidation;
using outlookCalendarApi.Application.Settings;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries
{
    public class CreateEventValidator : AbstractValidator<PaggingBase>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.PageSize)
                  .GreaterThan(0)
                  .WithMessage(x => string.Format("The pageSize can't be less than zero", nameof(x.PageSize)));
        }
    }
}
