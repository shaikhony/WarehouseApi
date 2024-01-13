using FluentValidation;

namespace WarehouseApi.Dtos.Validators
{
    public class CustomerDtoValidator : AbstractValidator<Customer>
    {
        public CustomerDtoValidator()
        {
            RuleFor(Customer => Customer.CustomerName).NotNull().Must(beValidName);
            RuleFor(c => c.CustomerEmail).EmailAddress();
        }

        private bool beValidName(string CustomerName)
        {
            return CustomerName.All(char.IsLetter);
        }
    }
}
