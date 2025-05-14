using Concycle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Concycle.Data.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(Guid id);
        Task AddPostAsync(Post post);
        void DeletePost(Post post);
        Task SavePostAsync();

        Task<List<Post>> GetPostByUserAsync(Guid userId);
        Task<List<Post>> GetPostByTypeAsync(string type);
        Task<List<Post>> GetPostByCategoryAsync(string category);
    }
}
