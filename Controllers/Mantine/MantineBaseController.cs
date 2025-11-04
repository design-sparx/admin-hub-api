using AdminHubApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine")]
    public abstract class MantineBaseController : BaseApiController
    {
        protected MantineBaseController(ILogger logger) : base(logger)
        {
        }
    }
}