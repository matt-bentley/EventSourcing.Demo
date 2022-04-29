using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Demo.Core.Abstractions.ExecutionResults;
using EventFlow.Demo.Core.Applications.DomainEvents;
using EventFlow.Demo.Core.Applications.Entities.ValueObjects;

namespace EventFlow.Demo.Core.Applications.Entities
{
    public class ApplicationEnvironment : AggregateRoot<ApplicationEnvironment, Id>,
        IEmit<ApplicationEnvironmentCreatedEvent>,
        IEmit<ComponentRegisteredEvent>,
        IEmit<ComponentRemovedEvent>,
        IEmit<TeamMemberOnboardedEvent>,
        IEmit<TeamMemberOffboardedEvent>,
        IEmit<TeamMemberEmailUpdatedEvent>
    {
        public string ApplicationName { get; private set; }
        public string EnvironmentName { get; private set; }

        private readonly List<Component> _components = new List<Component>();
        public IReadOnlyCollection<Component> Components => _components.AsReadOnly();

        private readonly List<TeamMember> _team = new List<TeamMember>();
        public IReadOnlyCollection<TeamMember> Team => _team.AsReadOnly();

        public ApplicationEnvironment(Id id) : base(id) { }

        public IExecutionResult Create(string applicationName, string environmentName)
        {
            applicationName = applicationName.Trim();
            environmentName = environmentName.Trim();
            
            if (string.IsNullOrEmpty(applicationName))
                return ExecutionResult.Failed("Application Name is required");

            if (string.IsNullOrEmpty(environmentName))
                return ExecutionResult.Failed("Environment Name is required");

            Emit(new ApplicationEnvironmentCreatedEvent(applicationName, environmentName));

            return ExecutionResult.Success();
        }
        
        public IExecutionResult RegisterComponent(Id componentId, string name, string url)
        {
            if (string.IsNullOrEmpty(name))
                return ExecutionResult.Failed("Component Name is required");

            if (string.IsNullOrEmpty(url))
                return ExecutionResult.Failed("Component Url is required");

            Emit(new ComponentRegisteredEvent(componentId, name, url));

            return ExecutionResult.Success();
        }

        public IExecutionResult RemoveComponent(Id componentId)
        {
            if (!_components.Any(x => x.Id == componentId))
                return CustomExecutionResult.NotFound($"Component not found for: {componentId}");

            Emit(new ComponentRemovedEvent(componentId));
            return ExecutionResult.Success();
        }

        public IExecutionResult OnboardTeamMember(Guid userId, string email)
        {
            if (_team.Any(e => e.UserId == userId))
                return ExecutionResult.Failed($"{userId} already has access to the application");

            Emit(new TeamMemberOnboardedEvent(userId, email));

            return ExecutionResult.Success();
        }

        public IExecutionResult OffboardTeamMember(Guid userId)
        {
            if (!_team.Any(e => e.UserId == userId))
                return ExecutionResult.Failed($"{userId} does not have access to the application");

            Emit(new TeamMemberOffboardedEvent(userId));

            return ExecutionResult.Success();
        }

        public IExecutionResult UpdateTeamMemberEmail(Guid userId, string email)
        {
            if (!_team.Any(e => e.UserId == userId))
                return ExecutionResult.Failed($"{userId} does not have access to the application");

            Emit(new TeamMemberEmailUpdatedEvent(userId, email));

            return ExecutionResult.Success();
        }

        public void Apply(ApplicationEnvironmentCreatedEvent @event)
        {
            ApplicationName = @event.ApplicationName;
            EnvironmentName = @event.EnvironmentName;
        }

        public void Apply(ComponentRegisteredEvent @event)
        {
            _components.Add(new Component(@event.ComponentId, @event.Name, @event.Url));
        }

        public void Apply(ComponentRemovedEvent @event)
        {
            var component = _components.FirstOrDefault(x => x.Id == @event.ComponentId);
            _components.Remove(component);
        }

        public void Apply(TeamMemberOnboardedEvent @event)
        {
            _team.Add(new TeamMember(@event.UserId, @event.Email));
        }

        public void Apply(TeamMemberOffboardedEvent @event)
        {
            _team.RemoveAll(x => x.UserId == @event.UserId);
        }

        public void Apply(TeamMemberEmailUpdatedEvent @event)
        {
            var teamMemberIndex = _team.FindIndex(e => e.UserId == @event.UserId);
            _team[teamMemberIndex] = _team[teamMemberIndex].UpdateEmail(@event.Email);
        }
    }
}
