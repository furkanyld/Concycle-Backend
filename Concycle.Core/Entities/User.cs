using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int Score { get; set; } = 0;

        public ICollection<Post>? Posts { get; set; }
        public ICollection<ConRequest>? ConRequests { get; set; }
    }
}
