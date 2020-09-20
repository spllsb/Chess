using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Club;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Handlers.Club
{
    public class CreateClubHandler : ICommandHandler<CreateClub>
    {  
        private readonly IClubService _clubService;
        private readonly IFileProvider _fileProvider;

        public CreateClubHandler(IClubService ClubService,
                                IFileProvider fileProvider)
        {
            _clubService = ClubService;
            _fileProvider = fileProvider;
        }
        public async Task HandleAsync(CreateClub command)
        {
            var uniqueFileName = await _fileProvider.UploadedFile(command.ProfileImage, command.UploadsFolder);
            await _clubService.CreateAsync(command.Name, command.ContactEmail, uniqueFileName, command.Informaction);
        }
    }
}