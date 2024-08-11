using Application.Files.IServices;
using Application.Responses;
using Application.User.Commands;
using Application.Users.IServices;
using Domain.Entities.ChatgramCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Services
{
    public class UserSignUpService : IUserSignUpService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileCreateService _fileCreateService;

        public UserSignUpService(UserManager<UserEntity> userManager, IFileCreateService fileCreateService)
        {
            _userManager = userManager;
            _fileCreateService = fileCreateService;
        }

        public async Task<ServiceResponse<IdentityResult>> RegisterUserAsync(UserRegisterCommand command)
        {
            if(command.Password != command.ConfirmPassword)
            {
                return new ServiceResponse<IdentityResult>
                {
                    Data = null,
                    Success = false,
                    Message = "Password and Confirm Password do not match."
                };
            }
            Guid? profileId = null;
            if (!string.IsNullOrEmpty(command.ProfileId))
            {
                var createProfile = await _fileCreateService.CreateFile(command.ProfileId);
                profileId = createProfile.Data.Guid;
            }
            var user = new UserEntity
            {
                UserName = command.UserName,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                ProfileId = profileId
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            var response = new ServiceResponse<IdentityResult>
            {
                Data = result,
                Success = result.Succeeded,
                Message = result.Succeeded ? "User registered successfully." : "User registration failed."
            };

            return response;
        }
    }
}
