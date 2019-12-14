// using System;
// using System.Threading.Tasks;

// namespace Chess.Infrastructure.Services
// {
//     public class HandlerTask : IHandlerTask
//     {
//         private readonly IHandler _handler;
//         private readonly Func<Task> _run;
//         private Func<Task> _validate;
//         private Func<Task> _always;
//         private Func<Task> _onSuccess;
//         private Func<Exception, Task> _onError;
//         private Func<ChessException, Task> _onCustomError;
//         private bool _propagateException = true;
//         private bool _executeOnError = true;


//         public HandlerTask(IHandler handler, Func<Task> run, Func<Task> validate = null)
//         {
//             _handler = handler;
//             _run = run;
//             _validate = validate;
//         }
//         public IHandlerTask Always(Func<Task> always)
//         {
//             _always = always;

//             return this;

//         }

//         public IHandlerTask DoNotPropagateException()
//         {
//             throw new NotImplementedException();
//         }

//         public Task ExecuteAsync()
//         {
//             throw new NotImplementedException();
//         }

//         public IHandler Next()
//         {
//             throw new NotImplementedException();
//         }

//         public IHandlerTask OnCustomError(Func<ChessException, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
//         {
//             throw new NotImplementedException();
//         }

//         public IHandlerTask OnError(Func<Exception, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
//         {
//             throw new NotImplementedException();
//         }

//         public Task OnSuccess(Func<Task> onSuccess)
//         {
//             throw new NotImplementedException();
//         }

//         public IHandlerTask PropagateException()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }