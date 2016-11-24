﻿using System;
using System.Collections.Generic;
using ITI.MANA.DAL;

namespace ITI.MANA.WebApp.Services
{
    public class UserService
    {
        readonly UserGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public bool CreatePasswordUser(string email, string password)
        {
            if (_userGateway.FindByEmail(email) != null) return false;
            _userGateway.CreatePasswordUser(email, _passwordHasher.HashPassword(password));
            return true;
        }

        public bool CreateOrUpdateGoogleUser(string email, string refreshToken)
        {
            User user = _userGateway.FindByEmail(email);
            if (user == null)
            {
                _userGateway.CreateGoogleUser(email, refreshToken);
                return true;
            }
            if (user.GithubAccessToken == string.Empty)
            {
                _userGateway.AddGoogleToken(user.UserId, refreshToken);
            }
            else
            {
                _userGateway.UpdateGoogleToken(user.UserId, refreshToken);
            }
            return false;
        }

        public User FindUser(string email, string password)
        {
            User user = _userGateway.FindByEmail(email);
            if (user != null && _passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;
        }

        public User FindUser(string email)
        {
            return _userGateway.FindByEmail(email);
        }

        public IEnumerable<string> GetAuthenticationProviders(string userId)
        {
            return _userGateway.GetAuthenticationProviders(userId);
        }
    }
}