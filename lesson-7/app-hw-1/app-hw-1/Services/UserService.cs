using System;
using app_hw_1.Models;

namespace app_hw_1.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();

        public List<User> GetUsers() => _users;

        public void AddUser(User user)
        {
            _users.Add(user);
        }
    }
}
