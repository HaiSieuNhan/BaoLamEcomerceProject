using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace EcommerceCommon.Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            var password = value.ToString();

            if (password != null && password.Length >= 6 && password.Length <= 32 )
            {

            }
            else
            {
                result = false;
            }
            return result;
        }

        public override string FormatErrorMessage(string name)
        {

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name );
        }

    }
}
