using EventFlow.Demo.Application.Users.Models;
using EventFlow.Demo.Core.Users.Queries;
using EventFlow.Demo.Application.Users.Services;
using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Users.ReadModels;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            IUsersService usersService)
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _usersService = usersService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var exampleReadModel = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<UserReadModel>(id), CancellationToken.None);
            return Ok(exampleReadModel);
        }

        [HttpGet("{id}/events")]
        [ProducesResponseType(typeof(List<CommittedDomainEvent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEvents(Guid id)
        {
            var events = await _queryProcessor.ProcessAsync(new GetUserEventsQuery(id), CancellationToken.None);
            return Ok(events);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(UserReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _usersService.GetByEmailAsync(email);
            return Ok(user);
        }

        [HttpPut("{id}/email")]
        [ProducesResponseType(typeof(UserReadModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UserReadModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmail(Guid id, [FromBody] EmailUpdateDto emailUpdate)
        {
            await _usersService.UpdateEmailAsync(id, emailUpdate.Email);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserCreateDto userCreate)
        {
            var id = await _usersService.CreateAsync(userCreate);
            return CreatedAtAction(nameof(Get), new { id = id }, id);
        }
    }
}