using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Views.Shared.Components.ChessBoard
{
    public class ChessBoardViewComponent : ViewComponent
    {
        public ChessBoardViewComponent()
        {
                
        }

        public  IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}