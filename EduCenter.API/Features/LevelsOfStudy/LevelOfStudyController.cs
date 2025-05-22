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
    public async Task<IActionResult> CreateLevelOfStudy(CreateLevelOfStudyCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateLevelOfStudy(UpdateLevelOfStudyCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpGet("get-alls")]
    public async Task<IActionResult> GetAllLevelsOfStudy()
    {
        var request = new GetAllLevelsOfStudyQuery();
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
