using Application.Common.Dtos;
using FluentValidation;

namespace Application.Common.DtoValidators
{
    public class TodoDtoValidator : AbstractValidator<TodoDto>
    {
        public TodoDtoValidator()
        {
            RuleFor(v => v).NotNull();
            RuleFor(v => v.Task).NotEmpty();
        }
    }
}
