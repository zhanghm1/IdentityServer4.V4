using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class UserService : IUserService
    {
        public string GetName(int id)
        {
            return "name_" + id;
        }
    }

    public interface IUserService
    {
        string GetName(int id);
    }
}