using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Core.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public Guid PostId { get; set; }

        public int Score { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
