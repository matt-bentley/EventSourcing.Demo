using EventFlow.Aggregates;
using EventFlow.Demo.Core.Applications.Commands;
using EventFlow.Demo.Core;
using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Applications.ReadModels;
using EventFlow.Demo.Core.Users.DomainEvents;
using EventFlow.Demo.Core.Users.Entities;
using EventFlow.Subscribers;

namespace EventFlow.Demo.Application.Applications.Subscriptions
{
    public class ApplicationEmailUpdatedSubscription : ISubscribeSynchronousTo<User, Id, EmailUpdatedEvent>
    {
        private readonly IReadModelRepository<ApplicationReadModel> _applicationsRepository;
        private readonly ICommandBus _commandBus;

        public ApplicationEmailUpdatedSubscription(IReadModelRepository<ApplicationReadModel> applicationsRepository,
            ICommandBus commandBus)
        {
            _applicationsRepository = applicationsRepository;
            _commandBus = commandBus;
        }

        public async Task HandleAsync(IDomainEvent<User, Id, EmailUpdatedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var applications = _applicationsRepository.Get()
                                                      .Where(e => e.Team.Any(e => e.UserId == domainEvent.AggregateIdentity.GetGuid()))
                                                      .ToList();

            foreach(var application in applications)
            {
                await _commandBus.PublishAsync(new UpdateTeamMemberEmailCommand(new Guid(application.Id), domainEvent.AggregateIdentity.GetGuid(), domainEvent.AggregateEvent.Email), CancellationToken.None);
            }
        }
    }
}
