using FluentValidation;
using outlookCalendarApi.Application.Dtos;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Commands.Create
{
    public class CreateEventValidator : AbstractValidator<EventDto>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Subject)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage(x => string.Format("Subject must have a value valid distinct of null and of empty", nameof(x.Subject)));

            RuleFor(x => x.Body)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage(x => string.Format("Bod must have a value valid distinct of null and of empty", nameof(x.Body)));

            RuleFor(x => x.Start)
                  .NotNull()
                  .WithMessage(x => string.Format("Start must have a value distinct of null", nameof(x.Start)));

            RuleFor(x => x.Start.DateTime)
                  .NotNull()
                  .NotEmpty()
                  .When(x => x.Start != null)
                  .WithMessage(x => string.Format("DateTime of Start must have distinct of null and empty", nameof(x.Start.DateTime)));

            RuleFor(x => x.Start.TimeZone)
                  .NotNull()
                  .NotEmpty()
                  .When(x => x.Start != null)
                  .WithMessage(x => string.Format("TimeZone of Start must have distinct of null and empty", nameof(x.Start.TimeZone)));

            RuleFor(x => x.End)
                  .NotNull()
                  .WithMessage(x => string.Format("End must have a value distinct of null", nameof(x.End)));

            RuleFor(x => x.End.DateTime)
                  .NotNull()
                  .NotEmpty()
                  .When(x => x.End != null)
                  .WithMessage(x => string.Format("DateTime of End must have distinct of null and empty", nameof(x.End.DateTime)))
                  .GreaterThan(x => x.Start.DateTime)
                  .When(x => x.Start != null && x.Start.DateTime != null)
                  .WithMessage(x => string.Format("DateTime End must have a value greater than Datetime Start", nameof(x.End.DateTime)));

            RuleFor(x => x.End.TimeZone)
                  .NotNull()
                  .NotEmpty()
                  .When(x => x.End != null)
                  .WithMessage(x => string.Format("TimeZone of End must have distinct of null and empty", nameof(x.End.TimeZone)));

            RuleFor(x => x.Attendees)
                  .NotNull()
                  .WithMessage(x => string.Format("Attendees must have a value distinct null", nameof(x.Attendees)));

            RuleFor(x => x.Locations)
                  .NotNull()
                  .WithMessage(x => string.Format("Locations must have a value distinct null", nameof(x.Locations)));
        }
    }
}
