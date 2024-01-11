using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public abstract class Validator<T>
    {
        protected readonly T value;
        protected readonly Dictionary<string, Func<T, ValidationResult>> validations;

        public Validator(T value, Dictionary<string, Func<T, ValidationResult>>? excludeValidations)
        {
            this.value = value;
            validations = ConstructValidations();
            if (excludeValidations is not null)
            {
                foreach(var kv in excludeValidations)
                {
                    validations[kv.Key] = kv.Value;
                }
            }
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

                var validator = validations.GetValueOrDefault(propertyName);
                if (validator is not null)
                {
                    var validResult = validator.Invoke(value);
                    if (!validResult.IsValid)
                    {
                        valid = false;
                    }
                    validationResults[propertyName] = validResult;
                }
            }

            return valid;
        }
    }
}
