using Application.Features.Agents.Commands;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
public class AgentsController : BaseController
{
    [HttpPost("add")]
    public async Task<IActionResult> CreateAgentAsync([FromBody] CreateAgentRequest createAgent)
    {
        var response = await Sender.Send(new CreateAgentCommand { CreateAgent = createAgent});

        if (response.IsSuccessful)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAgentAsync([FromBody] UpdateAgentRequest updateAgent)
    {
        var response = await Sender.Send(new UpdateAgentCommand { UpdateAgent = updateAgent });

        if (response.IsSuccessful)
            return Ok(response);

        return BadRequest(response);
    }
}
