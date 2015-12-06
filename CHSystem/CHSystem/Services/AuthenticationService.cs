using CHSystem.Models;
using CHSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Services
{
    public static class AuthenticationService
    {
        public static User LoggedUser { get; set; }

        public static void AuthenticateUser(string username, string password)
        {
            LoggedUser = new UserRepository().First(u => u.Username == username && u.Password == password);
        }

        public static void Logout()
        {
            AuthenticateUser(null, null);
        }
    }
}