using Application.Common.DtoValidators;
using FluentValidation;

namespace Application.Todos.Commands.NewTodo
{
    public class NewTodoCommandValidator : AbstractValidator<NewTodoCommand>
    {
        public NewTodoCommandValidator()
        {
            RuleFor(v => v.Todo).SetValidator(new TodoDtoValidator());
        }
    }
}
