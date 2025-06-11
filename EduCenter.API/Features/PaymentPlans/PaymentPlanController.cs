
using EduCenter.API.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EduCenter.API.Features.PaymentPlans.CreatePaymentPlan;
using EduCenter.API.Data.Models;
namespace EduCenter.API.Features.PaymentPlans;
public class PaymentPlanController : BaseApiController
{
    IMediator _mediator;
    public PaymentPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreatePaymentPlan(CreatePaymentPlanCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    [HttpPost("create-multiple")]
    public async Task<IActionResult> CreatePaymentPlans(CreatePaymentPlanCommand request, CancellationToken ct)
    {
        await _mediator.Send(request, ct);
        return Ok();
    }
    // [HttpPost("update")]
    // public async Task<IActionResult> UpdatePaymentPlan(UpdatePaymentPlanCommand request, CancellationToken ct)
    // {
    //     await _mediator.Send(request, ct);
    //     return Ok();
    // }
    // [HttpGet("get-all")]
    // public async Task<IActionResult> GetAllPaymentPlans(CancellationToken ct)
    // {
    //     var request = new GetAllPaymentPlansQuery();
    //     var result = await _mediator.Send(request, ct);
    //     return Ok(result);
    // }
}
