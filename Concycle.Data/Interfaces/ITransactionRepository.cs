using Concycle.Core.Entities;

namespace Concycle.Data.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(Guid id);
    Task AddAsync(Transaction transaction);
    Task<List<Transaction>> GetByUserAsync(Guid userId);
    Task SaveAsync();
}
