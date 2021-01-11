using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace EcommerceCommon.Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class UserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            var username = value.ToString();

            if (username != null && username.Length >= 6 && username.Length<=32 && char.IsLetter(username[0]) )
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

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name + "1234");
        }

    }
}
