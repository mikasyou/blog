using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Core;
using MediatR;

namespace Blog.Infrastructure {
    static class MediatorExtension {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BlogDatabaseContext ctx) {
            var domainEntities = ctx.ChangeTracker
                                    .Entries<DomainEntity>()
                                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents ?? new List<INotification>())
                                             .ToList();

            domainEntities.ToList()
                          .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}