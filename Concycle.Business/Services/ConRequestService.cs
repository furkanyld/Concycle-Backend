using AutoMapper;
using Concycle.Business.Interfaces;
using Concycle.Core.Dtos;
using Concycle.Core.Entities;
using Concycle.Data.Interfaces;

namespace Concycle.Business.Services
{
    public class ConRequestService : IConRequestService
    {
        private readonly ITransactionService _transactionService;
        private readonly IConRequestRepository _repository;
        private readonly IMapper _mapper;

        public ConRequestService(IConRequestRepository repository, IMapper mapper, ITransactionService transactionService)
        {
            _repository = repository;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        public async Task<List<ConRequestDto>> GetRequests()
        {
            var data = await _repository.GetAllConRequestsAsync();
            return _mapper.Map<List<ConRequestDto>>(data);
        }

        public async Task<ConRequestDto?> GetRequestById(Guid id)
        {
            var req = await _repository.GetConRequestByIdAsync(id);
            return req is null ? null : _mapper.Map<ConRequestDto>(req);
        }

        public async Task<List<ConRequestDto>> GetRequestsByPost(Guid postId)
        {
            var data = await _repository.GetConRequestsByPostIdAsync(postId);
            return _mapper.Map<List<ConRequestDto>>(data);
        }

        public async Task<List<ConRequestDto>> GetRequestsByApplicant(Guid applicantId)
        {
            var data = await _repository.GetConRequestsByApplicantIdAsync(applicantId);
            return _mapper.Map<List<ConRequestDto>>(data);
        }

        public async Task<List<ConRequestDto>> GetRequestsByStatus(string status)
        {
            var data = await _repository.GetConRequestsByStatusAsync(status);
            return _mapper.Map<List<ConRequestDto>>(data);
        }

        public async Task<ConRequestDto> CreateRequest(CreateConRequestDto dto)
        {
            var request = new ConRequest
            {
                Id = Guid.NewGuid(),
                PostId = dto.PostId,
                ApplicantId = dto.ApplicantId,
                Message = dto.Message,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddConRequestAsync(request);
            await _repository.SaveConRequestAsync();

            return _mapper.Map<ConRequestDto>(request);
        }

        public async Task<ConRequestDto?> UpdateStatus(Guid id, string newStatus)
        {
            var request = await _repository.GetConRequestByIdAsync(id);
            if (request is null) return null;

            request.Status = newStatus;
            await _repository.SaveConRequestAsync();

            return _mapper.Map<ConRequestDto>(request);
        }

        public async Task<ConRequestDto?> CompleteRequest(Guid id)
        {
            var request = await _repository.GetConRequestWithPostByIdAsync(id);
            if (request is null || request.Status.ToLower() != "accepted") return null;

            if (request.IsCompleted)
                return _mapper.Map<ConRequestDto>(request);

            var transactionDto = new TransactionDto
            {
                FromUserId = request.ApplicantId,
                ToUserId = request.Post.OwnerId,
                PostId = request.PostId,
                Score = request.Post.ScoreCost
            };

            var created = await _transactionService.CreateTransaction(transactionDto);
            if (created is null)
                throw new InvalidOperationException("Transaction could not be created.");

            request.IsCompleted = true;
            await _repository.SaveConRequestAsync();

            return _mapper.Map<ConRequestDto>(request);
        }

        public async Task<bool> DeleteRequest(Guid id)
        {
            var request = await _repository.GetConRequestByIdAsync(id);
            if (request is null) return false;

            _repository.DeleteConRequest(request);
            await _repository.SaveConRequestAsync();
            return true;
        }
    }
}
