using System;
using System.Collections.Generic;
using System.Linq;
using Splitwise.Interfaces;
using Splitwise.Models;

namespace Splitwise.Managers
{
    public class UserManager : IUserManager
    {
        public UserManager()
        {
            this.UsersMap = new Dictionary<int, User>();
        }

        public Dictionary<int, User> UsersMap { get; private set; }

        public void AddUser(User user)
        {
            if (!this.UsersMap.ContainsKey(user.Id))
                this.UsersMap.Add(user.Id, user);
        }

        public User GetUserById(int id)
        {
            return this.UsersMap[id];
        }
    }
}
