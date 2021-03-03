using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartCity.Data.Repositories;

namespace SmartCity.Web.Models.CustomAttribute
{
    public class UniqUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !(value is string))
            {
                throw new Exception("StrongPasswordAttribute must be applied only for string fields");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }

            var login = (string)value;

            var userRepository = validationContext.GetService(typeof(CitizenUserRepository)) 
                as CitizenUserRepository;

            var existingUser = userRepository.GetUserByLogin(login);

            if (existingUser != null)
            {
                return new ValidationResult($"{login} is not uniq. There is user with the same name");
            }

            return ValidationResult.Success;
        }
    }
}
