using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Mediators
{
    public class AppMediator : Mediator, IMediator
    {
        private readonly ILogger<AppMediator> _logger;

        public AppMediator(ServiceFactory serviceFactory, ILogger<AppMediator> logger) : base(serviceFactory)
        {
            _logger = logger;
        }

        public new async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await base.Send(request, cancellationToken);
                return response;
            }
            catch (Exception exception)
            {
                var message = @$"
=============================== START ===============================
Message: {exception.GetBaseException().Message}
StackTrace: {exception.GetBaseException().StackTrace}
=============================== END ===============================";
                _logger.LogError(message);
                throw;
            }
        }
    }
}
