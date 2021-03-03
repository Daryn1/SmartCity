using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartCity.Data.Repositories;

namespace SmartCity.Web.Models.CustomAttribute.Medecine
{
    public class CheckPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !(value is string))
            {
                throw new Exception("CheckPasswordAttribute must be applied only for string fields");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }


            var password = (string)value;

            var userRepo = validationContext.GetService(typeof(CitizenUserRepository))
                as CitizenUserRepository;
            var existingPassword = userRepo.GetUserByPassword(password);
            if (existingPassword == null)
            {
                return new ValidationResult($"Не верный пароль");
            }

            return ValidationResult.Success;
        }
    }
}
