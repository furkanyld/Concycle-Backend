using Concycle.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Business.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetPosts();
        Task<PostDto?> GetPostById(Guid id);
        Task<PostDto> CreatePost(PostDto postDto);
        Task<PostDto?> UpdatePost(Guid id, PostDto postDto);
        Task<bool> DeletePost(Guid id);
        Task<List<PostDto>> GetPostsByUser(Guid userId);
        Task<List<PostDto>> GetPostsByType(string type);
        Task<List<PostDto>> GetPostsByCategory(string category);
    }
}
