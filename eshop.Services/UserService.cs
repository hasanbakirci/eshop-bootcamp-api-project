using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models.Entities;

namespace eshop.Services
{
    public class UserService
    {
        private List<User> users = new List<User>{
            new User {Id=1,Name="adminx",Username="admin",Password="123",Role="Admin"},
            new User {Id=1,Name="userx",Username="user",Password="123",Role="User"},
            new User {Id=1,Name="editorx",Username="editor",Password="123",Role="Editor"}
        };
        public User IsValid(string username,string password){
          return users.FirstOrDefault(x => x.Username == username && x.Password == password);  
        }
    }
}