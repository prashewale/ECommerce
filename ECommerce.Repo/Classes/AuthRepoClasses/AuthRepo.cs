﻿
using ECommerce.Data;
using ECommerce.Models.DataModels.AuthDataModels;
using ECommerce.Models.ResponseModel;
using ECommerce.Repo.Interfaces.AuthRepoInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Repo.Classes.AuthRepoClasses
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<User>> FindByEmailAsync(string email)
        {
            try
            {
                //check if input email id is null.
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Response<User>.Failure("email id is blank.");
                }

                //find if database is having email id or not.
                User? foundUserInDatabase = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

                //check if found user is null.
                if (foundUserInDatabase is null)
                {
                    return Response<User>.Failure("User not Found");
                }

                return Response<User>.Success(foundUserInDatabase);
            }
            catch (Exception ex)
            {
                return Response<User>.Failure(ex.Message);
            }
        }

        public async Task<Response<User>> FindByUserNameAsync(string userName)
        {
            try
            {
                //check if input userName is null.
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return Response<User>.Failure("UserName is blank.");
                }

                //find if database is having email id or not.
                User? foundUserInDatabase = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);

                //check if found user is null.
                if (foundUserInDatabase is null)
                {
                    return Response<User>.Failure("User not Found");
                }

                return Response<User>.Success(foundUserInDatabase);
            }
            catch (Exception ex)
            {
                return Response<User>.Failure(ex.Message);
            }
        }

        public async Task<Response<User>> CreateUserAsync(User userModel)
        {
            //check if user is null or not.
            if (userModel is null)
            {
                return Response<User>.Failure("input user is blank");
            }

            try
            {
                //save the user in database.
                EntityEntry<User> addedUserInDatabaseResponse = await _dbContext.Users.AddAsync(userModel);

                //extract saved User form response.
                User addedUserInDatabase = addedUserInDatabaseResponse.Entity;

                //check if user saved in database or not.
                if (addedUserInDatabase is null)
                {
                    return Response<User>.Failure("error saving user in database.");
                }

                await SaveAsync();

                return Response<User>.Success(addedUserInDatabase);
            }
            catch (Exception ex)
            {
                return Response<User>.Failure(ex.Message);
            }
        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}