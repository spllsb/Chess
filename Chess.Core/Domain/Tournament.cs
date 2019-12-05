using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Core.Domain
{
    public class Tournament
    {
        private ISet<User> _users = new HashSet<User>();

        public Guid Id { get; protected set;}
        public string Name { get; protected set; }
        public int MaxPlayers { get; protected set; }
        public DateTime UpdateAt {get; private set;}
        public IEnumerable<User> Users 
        { 
            get { return _users;} 
            set { _users = new HashSet<User>(value);} 
        }
        protected Tournament()
        {
        }

        public Tournament(string name, int maxPlayer)
        {
            Name = name;
            MaxPlayers = maxPlayer;
        }

        public void AddPlayer(User newUser)
        {
            var user = Users.SingleOrDefault(x => x.Id == newUser.Id);
            if (user != null)
            {
                throw new Exception($"User with id: '{newUser.Id}' and email '{newUser.Email}' already exists for tournament. You can't add him again");
            }
            _users.Add(newUser);
            UpdateAt = DateTime.UtcNow;
        }

        public void DeletePlayer(User deleteUser)
        {
            var user = Users.SingleOrDefault(x => x.Id == deleteUser.Id);
            if (user == null)
            {
                throw new Exception($"User with id: '{deleteUser.Id}' and email '{deleteUser.Email}' was not found");
            }
            _users.Remove(deleteUser);
            UpdateAt = DateTime.UtcNow;
        }

    }
}