﻿using ECommerce.Models.DataModels.AuthDataModels;
using ECommerce.Models.ResponseModel;

namespace ECommerce.Services.Interfaces.OtherServicesInterfaces.PasswordHasherServiceInterface
{
    public interface IPasswordHasher
    {
        Task<Response<string>> GenerateHashAsync(string password);
        Task<Response<bool>> VerifyPasswordAsync(string password, string passwordHash);
    }
}
