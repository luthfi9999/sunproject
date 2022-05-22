namespace SunProject_Application.Command.AddPromotions
{
    public class AddPromotionsCommandValidator : AbstractValidator<AddPromotionsCommandRequest>
    {
        public AddPromotionsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().Length(13);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Type).NotEmpty().Must(y => y.Equals("S") || y.Equals("C"));
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.EndDate).NotEmpty().GreaterThanOrEqualTo( x => x.StartDate );
        }
    }
}
