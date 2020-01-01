using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiPoC.Application.Dto;
using NetCoreWebApiPoC.Application.Todos.Commands.NewTodo;
using NetCoreWebApiPoC.Application.Todos.Queries.GetTodos;
using NetCoreWebApiPoC.Domain.Entities;
using System.Collections.Generic;

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

        /// <summary>
        /// Get Todo items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTodos()
        {
            var result = _mediator.Send(new GetTodosQuery());
            return Ok(result.Result);
        }

        /// <summary>
        /// Create Todo item.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Create([FromBody] TodoDto todo)
        {
            var result = _mediator.Send(new NewTodoCommand {
                Todo = todo
            });

            return CreatedAtRoute("GetTodos", null, result);
        }
    }
}