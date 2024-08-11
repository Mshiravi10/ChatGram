using Application.Responses;
using Application.User.Commands;
using Domain.ILifeTime;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.IServices
{
    public interface IUserSignUpService : IScopedService
    {
        public Task<ServiceResponse<IdentityResult>> RegisterUserAsync(UserRegisterCommand command);
    }
}
