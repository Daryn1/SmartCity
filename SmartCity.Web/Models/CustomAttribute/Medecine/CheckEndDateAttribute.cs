using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCity.Web.Models.CustomAttribute.Medecine
{
    public class CheckEndDateAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"{name} Необходимо установить дату больше сегодняшней"
                : ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value != null && !(value is DateTime))
            {
                throw new Exception("CheckStartDataAttribute must be applied only for DateTime fields");
            }

            if (value == null)
            {
                return false;
            }

            var date = (DateTime)value;
            var dateNow = DateTime.Today;
            var check = date.CompareTo(dateNow);
            if (check != 1)
            {
                return false;
            }

            return true;
        }
    }
}
