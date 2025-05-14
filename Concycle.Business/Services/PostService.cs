using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Concycle.Business.Interfaces;
using Concycle.Core.Dtos;
using Concycle.Core.Entities;
using Concycle.Data.Context;
using Concycle.Data.Interfaces;
using Concycle.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Concycle.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<PostDto>> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return _mapper.Map<List<PostDto>>(posts);
        }

        public async Task<PostDto?> GetPostById(Guid id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return post is null ? null : _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> CreatePost(PostDto postDto)
        {
            var owner = await _userRepository.GetUserByIdAsync(postDto.OwnerId);
            if (owner is null)
                throw new Exception("Kullanıcı bulunamadı");

            if (postDto.Type.ToLower() == "help" && owner.Score < postDto.ScoreCost)
                throw new InvalidOperationException("Yetersiz katkı puanı ile bu ilan açılamaz.");

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Description = postDto.Description,
                Type = postDto.Type,
                Category = postDto.Category,
                ScoreCost = postDto.ScoreCost,
                OwnerId = postDto.OwnerId 
            };

            await _postRepository.AddPostAsync(post);
            await _postRepository.SavePostAsync();

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto?> UpdatePost(Guid id, PostDto postDto)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post is null) return null;

            post.Title = postDto.Title;
            post.Description = postDto.Description;
            post.Type = postDto.Type;
            post.Category = postDto.Category;
            post.ScoreCost = postDto.ScoreCost;

            await _postRepository.SavePostAsync();

            return _mapper.Map<PostDto>(post);
        }

        public async Task<bool> DeletePost(Guid id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post is null) return false;

            _postRepository.DeletePost(post);
            await _postRepository.SavePostAsync();
            return true;
        }

        public async Task<List<PostDto>> GetPostsByUser(Guid userId)
        {
            var posts = await _postRepository.GetPostByUserAsync(userId);
            return _mapper.Map<List<PostDto>>(posts);
        }

        public async Task<List<PostDto>> GetPostsByType(string type)
        {
            var posts = await _postRepository.GetPostByTypeAsync(type);
            return _mapper.Map<List<PostDto>>(posts);
        }

        public async Task<List<PostDto>> GetPostsByCategory(string category)
        {
            var posts = await _postRepository.GetPostByCategoryAsync(category);
            return _mapper.Map<List<PostDto>>(posts);
        }

    }
}
