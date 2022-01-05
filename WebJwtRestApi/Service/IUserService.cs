using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebJwtRestApi.Controllers;

namespace WebJwtRestApi.Service
{
    public interface IUserService
    {
        bool IsValidUserInformation(LoginModel model);
        object GetUserDetails();
    }
}
