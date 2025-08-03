using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureFood.BaseApplication.Services;

namespace PureFood.ServiceCMS.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]

    public class CmsBaseController(ILogger<CmsBaseController> logger, ContextService contextService) : BaseApiController(logger, contextService)
    {

    }
}
