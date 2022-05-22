global using FluentValidation;

namespace SunProject_Application.Command.AddStores
{
    public class AddStoresCommandValidator : AbstractValidator<AddStoresCommandRequest>
    {
        public AddStoresCommandValidator()
        {
            RuleFor(x => x.File).NotEmpty().Must(y => y.FileName.EndsWith(".xlsx")).WithMessage("File Cannot Be Empty And Must Have .xlsx Extension");
        }
    }
}
