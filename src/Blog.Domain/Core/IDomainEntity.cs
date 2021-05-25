using System;
using System.Collections.Generic;
using MediatR;

namespace Blog.Domain.Core {
    public class DomainEntity {
        public virtual int Id { get; protected set; }


        public List<IDomainEvent> Events { get; private set; } = new();

        private List<INotification>? domainEvents = null;

        public IReadOnlyCollection<INotification>? DomainEvents => domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem) {
            domainEvents ??= new List<INotification>();
            domainEvents.Add(eventItem);
        }


        public void RemoveDomainEvent(INotification eventItem) {
            domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents() {
            domainEvents?.Clear();
        }

        public bool IsTransient() {
            return this.Id == default;
        }
    }
}