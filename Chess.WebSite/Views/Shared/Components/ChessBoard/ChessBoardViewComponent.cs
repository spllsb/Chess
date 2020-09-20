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
            System.Console.WriteLine(fen);
            return View("ChessboardAsPicture");
        }
    
        // public  IViewComponentResult Invoke(string pgn)
        // {
        //     ViewBag.PGN = pgn;
        //     return View("ChessboardAsRepeatedGame");
        // }
    }
}