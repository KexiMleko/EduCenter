using Microsoft.AspNetCore.Mvc;

namespace EduCenter.API.Base;
[ApiController]
// [ServiceFilter(typeof(ApiExceptionFilter))]
[Route("api/[controller]")]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class BaseApiController : ControllerBase
{
    // TODO: implement this!
    [NonAction]
    public int GetSenderId()
    {
        return 1;
    }

}
