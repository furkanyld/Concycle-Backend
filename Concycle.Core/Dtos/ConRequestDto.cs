using Concycle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Core.Dtos
{
    public class ConRequestDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid ApplicantId { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Message { get; set; }
        public string? PostTitle { get; set; }    
        public bool IsCompleted { get; set; }          
        public DateTime CreatedAt { get; set; }
    }
}
