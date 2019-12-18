using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}