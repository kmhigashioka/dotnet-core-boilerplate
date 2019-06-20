using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiPoC.Application.Dto;
using NetCoreWebApiPoC.Application.Todos.Commands.NewTodo;
using NetCoreWebApiPoC.Application.Todos.Queries.GetTodos;

namespace NetCoreWebApiPoC.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Todos")]
    [Authorize]
    public class TodosController : Controller
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var result = _mediator.Send(new GetTodosQuery());
            return Ok(result.Result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoDto todo)
        {
            var result = _mediator.Send(new NewTodoCommand {
                Todo = todo
            });

            return Created(string.Empty, result.Result);
        }
    }
}