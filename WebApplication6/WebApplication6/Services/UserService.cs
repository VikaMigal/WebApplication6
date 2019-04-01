using System.Collections.Generic;
using MongoDB.Driver;
using WebApplication6.Models;
using Microsoft.Extensions.Configuration;

namespace WebApplication6.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        
      
        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("WebApplication"));
            var database = client.GetDatabase("WebApplication");
            _users = database.GetCollection<User>("Users");
        }
        
        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }
        
        public User Get(string login)
        {
            return _users.Find<User>(user => user.Login == login).FirstOrDefault();
        }
    }
    
    
}