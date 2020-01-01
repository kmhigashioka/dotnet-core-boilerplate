﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;
using Domain.Entities;
using Omu.ValueInjecter;

namespace Application.Todos.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<List<Todo>>
    {

    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<Todo>>
    {
        private readonly IAppContext _context;

        public GetTodosQueryHandler(IAppContext context)
        {
            _context = context;
        }

        public Task<List<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = _context.Todos.Select(t => Mapper.Map<Todo>(t, null)).ToList();

            return Task.FromResult(todos);
        }
    }
}
