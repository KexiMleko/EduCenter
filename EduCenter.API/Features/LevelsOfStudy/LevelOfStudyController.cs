using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.LevelsOfStudy.GetAllLevelsOfStudy;
using EduCenter.API.Features.LevelsOfStudy.UpdateLevelOfStudy;
using EduCenter.API.Features.LevelsOfStudy.CreateLevelOfStudy;
namespace EduCenter.API.Features.LevelOfStudys;
public class LevelOfStudyController : BaseApiController
{
    IMediator _mediator;
    public LevelOfStudyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateLevelOfStudy(CreateLevelOfStudyCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateLevelOfStudy(UpdateLevelOfStudyCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllLevelsOfStudy(CancellationToken ct)
    {
        var request = new GetAllLevelsOfStudyQuery();
        var result = await _mediator.Send(request, ct);
        return Ok(result);
    }
}
