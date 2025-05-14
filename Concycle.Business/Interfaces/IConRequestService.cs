using Concycle.Core.Dtos;

namespace Concycle.Business.Interfaces
{
    public interface IConRequestService
    {
        Task<List<ConRequestDto>> GetRequests();
        Task<ConRequestDto?> GetRequestById(Guid id);
        Task<List<ConRequestDto>> GetRequestsByPost(Guid postId);
        Task<List<ConRequestDto>> GetRequestsByApplicant(Guid applicantId);
        Task<List<ConRequestDto>> GetRequestsByStatus(string status);
        Task<ConRequestDto?> CompleteRequest(Guid id);
        Task<ConRequestDto> CreateRequest(CreateConRequestDto dto);
        Task<bool> DeleteRequest(Guid id);
        Task<ConRequestDto?> UpdateStatus(Guid id, string newStatus);
    }
}
