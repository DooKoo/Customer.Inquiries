using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Inquiries.Core.Extensions
{
    public class RequiredIfNullAttribute : ValidationAttribute
    {
        private String PropertyName { get; set; }
        private String ErrorMessage { get; set; }

        public RequiredIfNullAttribute(String propertyName, String errormessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errormessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
