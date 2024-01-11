using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public abstract class Validator<T>
    {
        protected readonly T value;
        protected readonly Dictionary<string, Func<T, ValidationResult>> validations;

        public Validator(T value)
        {
            this.value = value;
            validations = ConstructValidations();
        }

        protected abstract Dictionary<string, Func<T, ValidationResult>> ConstructValidations();


        public bool IsValid(out Dictionary<string, ValidationResult> validationResults)
        {
            Type objectType = typeof(T);
            PropertyInfo[] properties = objectType.GetProperties();

            bool valid = true;

            validationResults = new();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;

                var validResult = validations[propertyName](value);
                if (!validResult.IsValid)
                {
                    valid = false;
                }
                validationResults[propertyName] = validResult;
            }

            return valid;
        }
    }
}
