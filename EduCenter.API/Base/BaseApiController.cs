using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EduCenter.API.Base;
[ApiController]
// [ServiceFilter(typeof(ApiExceptionFilter))]
[Route("api/[controller]")]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class BaseApiController : ControllerBase
{
    [NonAction]
    public int GetSenderId()
    {
        return Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

}
