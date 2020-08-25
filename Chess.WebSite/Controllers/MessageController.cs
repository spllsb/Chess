using System;
using System.Threading.Tasks;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chess.WebSite.Controllers
{
    public class MessageController : WebControllerBase
    {
        private readonly IELOProvider _eloProvider;
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;
        public MessageController(ICommandDispatcher commandDispatcher,
                                IOptions<ChessGameSettings> chessGameSettings,
                                IELOProvider eloProvider,
                                IFileProvider fileProvider) : base(commandDispatcher)
        {
            _eloProvider = eloProvider;
            _chessGameSettings = chessGameSettings.Value;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {

            Console.WriteLine(await _fileProvider.GetFileContent(_chessGameSettings.PGNFilePath,"aa_vs_bb_20201608213558.pgn"));
            float Ra = 0, Rb = 0;
            _eloProvider.CalcELORating(1250,1200,ChessGameResultEnum.WIN, ref Ra, ref Rb);
            _eloProvider.CalcELORating(1250,1200,ChessGameResultEnum.LOSE, ref Ra, ref Rb);
            _eloProvider.CalcELORating(1250,1200,ChessGameResultEnum.DRAW, ref Ra, ref Rb);

            _eloProvider.CalcELORating(2400,2350,ChessGameResultEnum.WIN, ref Ra, ref Rb);
            _eloProvider.CalcELORating(2400,2350,ChessGameResultEnum.LOSE, ref Ra, ref Rb);
            _eloProvider.CalcELORating(2400,2350,ChessGameResultEnum.DRAW, ref Ra, ref Rb);
            return View();
        }
    }
}