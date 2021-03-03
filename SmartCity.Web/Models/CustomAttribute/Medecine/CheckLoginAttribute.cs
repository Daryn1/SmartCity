using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartCity.Data.Repositories;

namespace SmartCity.Web.Models.CustomAttribute.Medecine
{
    public class CheckLoginAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !(value is string))
            {
                throw new Exception("CheckLoginAttribute must be applied only for string fields");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }


            var login = (string)value;

            var userRepo = validationContext.GetService(typeof(CitizenUserRepository))
                as CitizenUserRepository;
            var existingLogin = userRepo.GetUserByLogin(login);
            if(existingLogin == null)
            {
                return new ValidationResult($"Пользователь c {login} не существует.");
            }

            return ValidationResult.Success;
        }
    }
}
