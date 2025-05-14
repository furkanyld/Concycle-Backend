using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Core.Entities
{
    public class ConRequest
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }
        public Post? Post { get; set; }
        public Guid ApplicantId { get; set; }
        public User? Applicant { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Status { get; set; } = "Pending"; // Pending - Accepted - Rejected
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
