using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SmartCity.Data.Entities;
using SmartCity.Data.Interfaces;

namespace SmartCity.Services.Infrastructure
{
    public class UserValidator
    {
        private ICitizenUserRepository citizenUserRepository;
        private readonly int requiredPasswordLength;
        private string validationMessages = string.Empty;

        public UserValidator(ICitizenUserRepository citizenUserRepository, int requiredPasswordLength = 3)
        {
            this.citizenUserRepository = citizenUserRepository;
            this.requiredPasswordLength = requiredPasswordLength;
        }

        public void Validate(CitizenUser user)
        {
            if (user == null)
            {
                throw new ValidationException("Specified user does not exist.");
            }

            if (string.IsNullOrWhiteSpace(user.Login))
            {
                var invalidUserName = $"Login '{user.Login}' is invalid, can only contain letters or digits.";
                validationMessages += Environment.NewLine + invalidUserName;
            }

            var owner = citizenUserRepository.GetUserByLogin(user.Login);

            if (owner != null && owner.Id != user.Id)
            {
                var duplicateUserName = $"Login {user.Login} is already taken.";
                validationMessages += Environment.NewLine + duplicateUserName;
            }

            ValidatePassword(user.Password);

            if (validationMessages != string.Empty)
            {
                throw new ValidationException(validationMessages);
            }
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ValidationException("Enter the password.");
            }

            if (password.Length < requiredPasswordLength)
            {
                var passwordTooShort = $"Passwords must be at least {requiredPasswordLength} characters.";
                validationMessages += Environment.NewLine + passwordTooShort;
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                var passwordTooShort = $"Passwords must have at least one digit ('0'-'9').";
                validationMessages += Environment.NewLine + passwordTooShort;
            }
        }
    }
}
