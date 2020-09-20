namespace Chess.Infrastructure.Services
{
    public class AwardService
    {
        
    }



    public class AwardParameters{
        public string FileName { get; set; }
        public string Content { get; set; }

        public AwardParameters(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}