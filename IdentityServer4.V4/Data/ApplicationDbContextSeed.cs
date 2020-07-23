using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.V4.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4.V4.Data
{
    public class ApplicationDbSeedData
    {
        private UserManager<Users> _userManager;
        private RoleManager<Roles> _roleManager;
        private ApplicationDbContext _applicationDb;

        public ApplicationDbSeedData(UserManager<Users> userManager, RoleManager<Roles> roleManager, ApplicationDbContext applicationDb)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDb = applicationDb;
        }

        public async Task Init()
        {
            await AddUser();
        }

        public async Task AddUser()
        {
            List<AddUser> users = new List<AddUser>()
                {
                    new AddUser(){UserName="admin",Password="Admin123456!",NickName="哈哈哈",Name="admin" },
                };
            foreach (var item in users)
            {
                var alice = await _userManager.FindByNameAsync(item.UserName);
                if (alice == null)
                {
                    alice = new Users
                    {
                        UserName = item.UserName,
                    };
                    var result = await _userManager.CreateAsync(alice, item.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
                else
                {
                    //Log.Debug("alice already exists");
                }
            }
        }
    }

    public class AddUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }

        public List<Claim> Claims { get; set; }
        public List<string> Roles { get; set; }
    }
}