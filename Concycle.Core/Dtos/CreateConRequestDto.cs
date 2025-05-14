using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Core.Dtos
{
    public class CreateConRequestDto
    {
        public Guid PostId { get; set; }
        public Guid ApplicantId { get; set; }
        public string? Message { get; set; }
    }
}
