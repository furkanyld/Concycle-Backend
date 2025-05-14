using Concycle.Core.Entities;
using Concycle.Data.Context;
using Concycle.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync() =>
            await _context.Posts.Include(p => p.Owner).ToListAsync();

        public async Task<Post?> GetPostByIdAsync(Guid id) =>
            await _context.Posts.FindAsync(id);

        public async Task AddPostAsync(Post post) =>
            await _context.Posts.AddAsync(post);

        public void DeletePost(Post post) =>
            _context.Posts.Remove(post);

        public async Task SavePostAsync() =>
            await _context.SaveChangesAsync();

        public async Task<List<Post>> GetPostByUserAsync(Guid userId) =>
            await _context.Posts
                .Include(p => p.Owner)
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

        public async Task<List<Post>> GetPostByTypeAsync(string type) =>
            await _context.Posts.Where(p => p.Type.ToLower() == type.ToLower()).ToListAsync();

        public async Task<List<Post>> GetPostByCategoryAsync(string category) =>
            await _context.Posts.Where(p => p.Category.ToLower() == category.ToLower()).ToListAsync();
    }
}
