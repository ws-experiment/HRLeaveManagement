using HRLeaveManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);

        Task<bool> Register(RegisterVM register);

        Task Logout();
    }
}
