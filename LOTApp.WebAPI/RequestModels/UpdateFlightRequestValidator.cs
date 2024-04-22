using FluentValidation;

namespace LOTApp.WebAPI.RequestModels
{
    public class UpdateFlightRequestValidator : AbstractValidator<UpdateFlightRequest>
    {
        public UpdateFlightRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FlightNumber).NotEmpty();
            RuleFor(x => x.FlightNumber).MaximumLength(6);
            RuleFor(x => x.FlightNumber)
                    .Matches(@"^([a-zA-Z][\d]|[\d][a-zA-Z]|[a-zA-Z]{2})(\d{1,})$")
                    .WithMessage("required IATA (marketing) code ex. 'LO231'");

            RuleFor(x => x.DepartLocation).NotEmpty();
            RuleFor(x => x.DepartLocation).MaximumLength(3);
            RuleFor(x => x.DepartLocation)
                   .Matches(@"^[a-zA-Z]{3}$$")
                   .WithMessage("required IATA Airport Commercial service mark ex. 'KTW'");

            RuleFor(x => x.ArrivalLocation).NotEmpty();
            RuleFor(x => x.ArrivalLocation).MaximumLength(3);
            RuleFor(x => x.ArrivalLocation)
                  .Matches(@"^[a-zA-Z]{3}$$")
                  .WithMessage("required IATA Airport Commercial service mark ex. 'KTW'");

            RuleFor(x => x.PlaneType).NotEmpty();
        }
    }
}
