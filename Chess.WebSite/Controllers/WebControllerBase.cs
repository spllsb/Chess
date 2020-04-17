using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers

{
    [ApiController]
    [Route("[controller]")]

    public class WebControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public WebControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}