using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Views.Shared.Components.ChessBoard
{
    public class ChessBoardViewComponent : ViewComponent
    {
        public ChessBoardViewComponent()
        {
                
        }

        public  IViewComponentResult Invoke(string fen, string boardName)
        {
            ViewBag.BoardName = boardName;
            ViewBag.Fen = fen;
            return View("ChessboardAsPicture");
        }
    }
}