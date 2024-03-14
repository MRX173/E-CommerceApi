using Application.Abstractions;
using Infrastructure.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> :  // registar this in DI
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactional
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IDbContext _dbContext;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = default;
        try
        {
            await _dbContext.RetryOnExceptionAsync(async () =>
            {
                _logger.LogInformation($"Transaction started: {typeof(TRequest).Name}");
                await _dbContext.BegainTransactionAsync(cancellationToken);
                response = await next();
                await _dbContext.CommitTransactionAsync(cancellationToken);
                _logger.LogInformation($"Transaction ended: {typeof(TRequest).Name}");
            });
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Transaction failed: {typeof(TRequest).Name}");
            await _dbContext.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(e.Message, e.StackTrace);
            throw;
        }

        return response;
    }
}