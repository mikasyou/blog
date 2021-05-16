using System.Collections.Generic;

namespace Blog.Domain.Core {
    public interface IDomainEntity {
    }


    public class IDomainAggragationRoot : IDomainEntity {
        public List<IDomainEvent> Events { get; private set; } = new();
    }
}