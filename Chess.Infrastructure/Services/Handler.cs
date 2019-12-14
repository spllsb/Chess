// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace Chess.Infrastructure.Services
// {
//     public class Handler : IHandler
//     {
//         private readonly ISet<IHandlerTask> _handlerTask = new HashSet<IHandlerTask>();

//         public IHandlerTask Run(Func<Task> run)
//         {
//         var handlerTask = new HandlerTask(this, run);
//         _handlerTask.Add(handlerTask);
        
//             return handlerTask;
//         }

//         public IHandlerTaskRunner Validate(Func<Task> validate)
//             => new HandlerTaskRunner(this, validate, _handlerTask);
        
//         public async Task ExecuteAllAsync()
//         {
//             foreach(var handlerTask in _handlerTask)
//             {
//                 await handlerTask.ExecuteAsync();
//             }
//             _handlerTask.Clear();
//         }
//     }
// }