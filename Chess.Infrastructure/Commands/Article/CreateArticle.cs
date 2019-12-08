namespace Chess.Infrastructure.Commands.Article
{
    public class CreateArticle : ICommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string FullNameAuthor { get; set; }
    }
}