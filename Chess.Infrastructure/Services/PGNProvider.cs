using System.Threading.Tasks;
using Chess.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Chess.Infrastructure.Services
{

    public class PGNProvider : IPGNProvider
    {
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;
        public PGNProvider(IOptions<ChessGameSettings> chessGameSettings,
                        IFileProvider fileProvider
                        )
        {
            _chessGameSettings = chessGameSettings.Value;
            _fileProvider = fileProvider;
        }

        public async Task<string> GetPGNContent(string pgnFileName)
        => await _fileProvider.GetFileContent(_chessGameSettings.PGNFilePath,pgnFileName);
    }
}