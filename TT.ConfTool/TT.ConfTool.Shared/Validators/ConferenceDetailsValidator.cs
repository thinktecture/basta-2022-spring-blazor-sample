using FluentValidation;
using TT.ConfTool.Shared.DTO;

namespace TT.ConfTool.Shared.Validators
{
    public class ConferenceDetailsValidator : AbstractValidator<ConferenceDetails>
    {
        public ConferenceDetailsValidator()
        {
            RuleFor(conference => conference.DateTo).GreaterThanOrEqualTo(conference => conference.DateFrom);
        }
    }
}
