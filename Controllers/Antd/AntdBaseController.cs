using AdminHubApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd")]
    public abstract class AntdBaseController : BaseApiController
    {
        protected AntdBaseController(ILogger logger) : base(logger)
        {
        }
    }
}