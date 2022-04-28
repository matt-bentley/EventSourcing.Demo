using EventFlow.Demo.Application.Applications.Commands;
using EventFlow.Demo.Application.Applications.Models;
using EventFlow.Demo.Application.Applications.Queries;
using EventFlow.Demo.Core;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.Demo.Core.Applications.ReadModels;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Demo.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ILogger<ApplicationsController> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public ApplicationsController(ILogger<ApplicationsController> logger,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor)
        {
            _logger = logger;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicationReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var application = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<ApplicationReadModel>(id), CancellationToken.None);
            if(application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ApplicationCreateDto applicationCreate)
        {
            var id = Guid.NewGuid();
            await _commandBus.PublishAsync(new CreateApplicationEnvironmentCommand(id, applicationCreate.Name, applicationCreate.EnvironmentName), CancellationToken.None);
            return CreatedAtAction(nameof(Get), new { id = id }, id);
        }

        [HttpGet("{id}/components/{componentId}")]
        [ProducesResponseType(typeof(ComponentReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComponent(Guid id, Guid componentId)
        {
            var component = await _queryProcessor.ProcessAsync(new GetComponentQuery(id, componentId), CancellationToken.None);
            if(component == null)
            {
                return NotFound();
            }
            return Ok(component);
        }

        [HttpPost("{id}/components")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostComponent(Guid id, [FromBody] ComponentCreateDto componentCreate)
        {
            var componentId = Guid.NewGuid();
            await _commandBus.PublishAsync(new RegisterComponentCommand(id, componentId, componentCreate.Name, componentCreate.Url), CancellationToken.None);
            return CreatedAtAction(nameof(GetComponent), new { id = id, componentId = componentId }, componentId);
        }

        [HttpDelete("{id}/components/{componentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComponent(Guid id, Guid componentId)
        {
            await _commandBus.PublishAsync(new RemoveComponentCommand(id, componentId), CancellationToken.None);
            return NoContent();
        }
    }
}