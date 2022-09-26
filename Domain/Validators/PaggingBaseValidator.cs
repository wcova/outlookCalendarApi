using FluentValidation;
using outlookCalendarApi.Application.Settings;

namespace outlookCalendarApi.Application.Validators
{
    public class PaggingBaseValidator : AbstractValidator<PaggingBase>
    {
        public PaggingBaseValidator()
        {
            RuleFor(x => x.PageSize)
                  .NotNull()
                  .WithMessage(x => string.Format("The pageSize can't be null", nameof(x.PageSize)))
                  .GreaterThan(0)
                  .WithMessage(x => string.Format("The pageSize can't be less than zero", nameof(x.PageSize)));
        }
    }
}
