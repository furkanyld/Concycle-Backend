using Concycle.Core.Entities;
using Concycle.Data.Context;
using Concycle.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Concycle.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetAllAsync() =>
        await _context.Transactions.ToListAsync();

    public async Task<Transaction?> GetByIdAsync(Guid id) =>
        await _context.Transactions.FindAsync(id);

    public async Task AddAsync(Transaction transaction) =>
        await _context.Transactions.AddAsync(transaction);

    public async Task<List<Transaction>> GetByUserAsync(Guid userId) =>
        await _context.Transactions
            .Where(t => t.FromUserId == userId || t.ToUserId == userId)
            .ToListAsync();

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
