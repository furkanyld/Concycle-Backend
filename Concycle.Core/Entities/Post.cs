using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Core.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = "skill";  // skill - help
        public string Category { get; set; } = null!;
        public int ScoreCost { get; set; }

        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }

        public ICollection<ConRequest>? ConRequests { get; set; }
    }
}
