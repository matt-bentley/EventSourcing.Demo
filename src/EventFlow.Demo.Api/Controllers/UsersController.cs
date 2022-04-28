using EventFlow.Demo.Application.Users.Commands;
using EventFlow.Demo.Application.Users.Models;
using EventFlow.Demo.Application.Users.Queries;
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

        public UsersController(ILogger<UsersController> logger,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor)
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var exampleReadModel = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<UserReadModel>(id), CancellationToken.None);
            return Ok(exampleReadModel);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(UserReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var exampleReadModel = await _queryProcessor.ProcessAsync(new GetByEmailQuery(email), CancellationToken.None);
            return Ok(exampleReadModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserCreateDto userCreate)
        {
            var id = Guid.NewGuid();
            var executionResult = await _commandBus.PublishAsync(new CreateUserCommand(id, userCreate.FirstName, userCreate.LastName, userCreate.Email), CancellationToken.None);
            return CreatedAtAction(nameof(Get), new { id = id }, id);
        }
    }
}