using System.Transactions;

namespace Web.UseCases.TechnicalStuff.Transactions;

public interface ITransactionContext
{
  TransactionScope CreateTransactionScope();
    Task SaveAsync(CancellationToken cancellationToken = default);
}