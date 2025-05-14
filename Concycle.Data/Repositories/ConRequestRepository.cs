using Concycle.Core.Entities;
using Concycle.Data.Context;
using Concycle.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Concycle.Data.Repositories
{
    public class ConRequestRepository : IConRequestRepository
    {
        private readonly AppDbContext _context;

        public ConRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConRequest>> GetAllConRequestsAsync() =>
            await _context.ConRequests.ToListAsync();

        public async Task<ConRequest?> GetConRequestByIdAsync(Guid id) =>
            await _context.ConRequests.FindAsync(id);

        public async Task AddConRequestAsync(ConRequest conRequest) =>
            await _context.ConRequests.AddAsync(conRequest);

        public void DeleteConRequest(ConRequest conRequest) =>
            _context.ConRequests.Remove(conRequest);

        public async Task SaveConRequestAsync() =>
            await _context.SaveChangesAsync();

        public async Task<List<ConRequest>> GetConRequestsByPostIdAsync(Guid postId) =>
            await _context.ConRequests
                .Include(r => r.Post)
                .Where(r => r.PostId == postId)
                .ToListAsync();

        public async Task<List<ConRequest>> GetConRequestsByApplicantIdAsync(Guid userId) =>
            await _context.ConRequests
                .Include(r => r.Post)
                .Where(r => r.ApplicantId == userId)
                .ToListAsync();

        public async Task<List<ConRequest>> GetConRequestsByStatusAsync(string status) =>
            await _context.ConRequests
                .Where(r => r.Status.ToLower() == status.ToLower())
                .ToListAsync();

        public async Task<ConRequest?> GetConRequestWithPostByIdAsync(Guid id)
        {
            return await _context.ConRequests
                .Include(r => r.Post)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
