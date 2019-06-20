using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetCoreWebApiPoC.Application.Dto;
using NetCoreWebApiPoC.Application.Interfaces;
using NetCoreWebApiPoC.Domain.Entities;
using Omu.ValueInjecter;

namespace NetCoreWebApiPoC.Application.Todos.Commands.NewTodo
{
    public class NewTodoCommand : IRequest<TodoDto>
    {
        public TodoDto Todo { get; set; }
    }

    public class NewTodoCommandHandler : IRequestHandler<NewTodoCommand, TodoDto>
    {
        private readonly ITodoContext _context;

        public NewTodoCommandHandler(ITodoContext context)
        {
            _context = context;
        }
        public Task<TodoDto> Handle(NewTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = Mapper.Map<Todo>(request.Todo);
            _context.Todos.Add(todo);
            _context.SaveChanges();

            return Task.FromResult(Mapper.Map<TodoDto>(todo));
        }
    }
}
