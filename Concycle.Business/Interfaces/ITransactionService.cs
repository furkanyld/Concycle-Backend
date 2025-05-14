using Concycle.Core.Dtos;

namespace Concycle.Business.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionDto>> GetAllTransactions();
    Task<TransactionDto?> GetTransactionById(Guid id);
    Task<List<TransactionDto>> GetTransactionsByUser(Guid userId);
    Task<TransactionDto> CreateTransaction(TransactionDto dto);
}
