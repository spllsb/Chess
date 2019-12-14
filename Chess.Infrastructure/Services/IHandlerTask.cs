// using System;
// using System.Threading.Tasks;

// namespace Chess.Infrastructure.Services
// {
//     //tworzymy takie fluent api
//     public interface IHandlerTask
//     {
//         //zawsze na sam koniec chcemy wykonać jakąś operacj, niezaleznie co sie dzieje 
//         IHandlerTask Always (Func<Task> always);
//         //osługujemy swoje wyjątki z poziomu usługi
//         IHandlerTask OnCustomError(Func<ChessException, Task> onCustomError,
//             bool propagateExeption = false, bool executeOnError = false);
//         // nie m
//         IHandlerTask OnError(Func<Exception, Task> onCustomError,
//             bool propagateExeption = false, bool executeOnError = false);
        
//         Task OnSuccess(Func<Task> onSuccess);
//         IHandlerTask PropagateException();
//         IHandlerTask DoNotPropagateException();
//         IHandler  Next();
//         Task ExecuteAsync();
//     }
// }