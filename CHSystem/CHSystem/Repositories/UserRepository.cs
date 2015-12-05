using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Repositories
{
    public class UserRepository:BaseRepository<User>
    {
        public UserRepository():base (){}
        public UserRepository(UnitOfWork unitOfWork):base(unitOfWork){}

        public User GetByUsernameAndPassword(string username, string password)
        {
            return First(u => u.Username == username && u.Password == password);
        }
    }
}