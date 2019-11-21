using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface IUserService
    {
        Task<User> AddAsync(string email, string password);
        Task<User> LoginAsync(string email, string password);

    }
}
