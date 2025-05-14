using Concycle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Data.Interfaces
{
    public interface IConRequestRepository
    {
        Task<List<ConRequest>> GetAllConRequestsAsync();
        Task<ConRequest?> GetConRequestByIdAsync(Guid id);
        Task AddConRequestAsync(ConRequest conRequest);
        Task SaveConRequestAsync();
        void DeleteConRequest(ConRequest conRequest);

        Task<List<ConRequest>> GetConRequestsByPostIdAsync(Guid postId);
        Task<List<ConRequest>> GetConRequestsByApplicantIdAsync(Guid applicantId);
        Task<List<ConRequest>> GetConRequestsByStatusAsync(string status);
        Task<ConRequest?> GetConRequestWithPostByIdAsync(Guid id);
    }
}
