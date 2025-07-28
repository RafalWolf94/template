using Microsoft.EntityFrameworkCore;
using Web.Infrastructure.Common.Persistence;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.UseCases.TechnicalStuff.Transactions;

public class TransactionCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> handler,
    AppDbContext dbContext)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task Handle(TCommand command)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            await handler.Handle(command);
            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        });
    }
}

public class TransactionCommandHandlerDecorator<TCommand, TResult>(
    ICommandHandler<TCommand, TResult> handler,
    ITransactionContext transactionContext,
    AppDbContext dbContext)
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
{
    public async Task<TResult> Handle(TCommand command)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        TResult result = default!;

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            result = await handler.Handle(command);
            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        });

        return result;
    }
}