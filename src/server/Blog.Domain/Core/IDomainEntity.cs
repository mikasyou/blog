using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Core {
    public class IDomainEntity {
        public List<IDomainEvent> Events { get; private set; } = new();
    }
}