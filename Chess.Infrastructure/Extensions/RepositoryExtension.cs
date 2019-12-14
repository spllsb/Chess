using System;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Repositories;

namespace Chess.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static async Task<Tournament> GetOrFailAsync(this ITournamentRepository repository, string name)
        {
            var tournament = await repository.GetAsync(name);
            if(tournament == null)
            {
                throw new Exception($"Tournament with id: '{name}' was not found exists");
            }

            return tournament;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if(user == null)
            {
                throw new Exception($"User with id: '{id}' was not found exists");
            }

            return user;
        }

        public static async Task<Article> GetOrFailAsync(this IArticleRepository repository, Guid id)
        {
            var article = await repository.GetAsync(id);
            if(article == null)
            {
                throw new Exception($"Article with id: '{id}' was not found");
            }

            return article;
        }

    }
}