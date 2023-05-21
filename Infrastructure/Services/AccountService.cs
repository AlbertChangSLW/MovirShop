using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResponseModel> LoginUser(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new Exception("Email does not exist");

            }

            var hashedPassword = GetHashedPassword(password, dbUser.Salt);
            if(hashedPassword == dbUser.HashedPassword)
            {
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<bool> RegisterUser(UserRegisterModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser != null)
            {
                throw new ConflictException("Email already exists");
            }

            var salt = GetRandomSalt();
            var hashPassword = GetHashedPassword(model.Password, salt);
            var user = new User
            {
                Email = model.Email,
                Salt = salt,
                HashedPassword = hashPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            var createdUser = _userRepository.Add(user);

            if(createdUser.Id > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ProfileModel> GetProfile(int id)
        {
            var dbUser = await _userRepository.GetUserById(id);

            var profile = new ProfileModel
            {
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                DateOfBirth = dbUser.DateOfBirth,
                Email = dbUser.Email,
                OldPassword = dbUser.HashedPassword,
                PhoneNumber = dbUser.PhoneNumber                
            };
            return profile;
        }
        public async Task<ProfileModel> UpdateProfile(ProfileModel model, int id)
        {
            var dbUser = await _userRepository.GetUserById(id);
            var hashedPassword = GetHashedPassword(model.OldPassword, dbUser.Salt);

            if(hashedPassword == dbUser.HashedPassword)
            {
                if(model.NewPassword != null) 
                {
                    var newHashedPassword = GetHashedPassword(model.NewPassword, dbUser.Salt);
                    dbUser.HashedPassword = newHashedPassword;
                }

                dbUser.FirstName = model.FirstName;
                dbUser.LastName = model.LastName;
                dbUser.DateOfBirth = model.DateOfBirth;
                dbUser.Email = model.Email;
                dbUser.PhoneNumber = model.PhoneNumber;

                var updateProfile = _userRepository.Update(dbUser);

                return null;
            }

            throw new ConflictException("The old password is wrong!");                
        }

        private string GetRandomSalt()
        {
            var randomByte = new byte[128/8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomByte);
            }

            return Convert.ToBase64String(randomByte);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2
                (
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }
    }
}
