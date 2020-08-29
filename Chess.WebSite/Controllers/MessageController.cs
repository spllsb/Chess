using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Message;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace Chess.WebSite.Controllers
{
    public class MessageController : WebControllerBase
    {
        private readonly IELOProvider _eloProvider;
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;
        private readonly IEmailSender _emailSender;
        private readonly IClubService _clubService;
        public MessageController(ICommandDispatcher commandDispatcher,
                                IOptions<ChessGameSettings> chessGameSettings,
                                IELOProvider eloProvider,
                                IFileProvider fileProvider,
                                IEmailSender emailSender,
                                IClubService clubService) : base(commandDispatcher)
        {
            _eloProvider = eloProvider;
            _chessGameSettings = chessGameSettings.Value;
            _fileProvider = fileProvider;
            _emailSender = emailSender;
            _clubService = clubService;
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


        
        
        
        
        public async Task<IActionResult> SendMessage()
        {
            var clubs = await _clubService.GetAllClubs();
            ViewBag.ClubsDropDown = new MultiSelectList(clubs,"ContactEmail","Name",clubs.Select(x => x.ContactEmail));
            
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessage sendMessage)
        {
            if(ModelState.IsValid)
            {
                foreach (var item in sendMessage.SendToList)
                {
                    System.Console.WriteLine(item);
                }
                // await _emailSender.SendEmailAsync("bujnolukasz@gmail.com",sendMessage.Title,sendMessage.Content);
            }
            else{
                var clubs = await _clubService.GetAllClubs();
                ViewBag.ClubsDropDown = new MultiSelectList(clubs,"ContactEmail","Name");
            }

            return View(sendMessage);

        }
    }
}