using System;
using System.ComponentModel.DataAnnotations;
using SmartCity.Data.Repositories;

namespace SmartCity.Web.Models.CustomAttribute.Medecine
{
    public class CheckCoastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !(value is decimal) && !(value is long))
            {
                throw new Exception("CheckCoastAttribute must be applied only for decimal and long fields");
            }

            if (value == null)
            {
                return new ValidationResult("Value can't be null");
            }

            var id = (long)value;
            var coast = (decimal)value;

            var userRepository = validationContext.GetService(typeof(CitizenUserRepository))
                as CitizenUserRepository;
            var existingUser = userRepository.Get(id);
            var solvency = existingUser.Balance.CompareTo(coast);
            if(solvency < 0)
            {
                return new ValidationResult($"На вашем счёте недостаточно средств для покупки.");
            }


            return ValidationResult.Success;
        }
    }
}
