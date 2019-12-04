using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    public class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}