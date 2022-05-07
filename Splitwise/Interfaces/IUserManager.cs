using System;
using System.Collections.Generic;
using Splitwise.Models;

namespace Splitwise.Interfaces
{
    public interface IUserManager
    {
        User GetUserById(int id);
        void AddUser(User user);
        //void AddUserConnection(User user1, User user2);
        //List<User> GetUserConnections(User user);
    }
}
