using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    internal static class SyntaxSugar
    {
        internal static ValidationResult IfError(this bool value, string message) => new ValidationResult(value, message);

        internal static void Confirm(this bool value, Dictionary<string, ValidationResult> validationResults, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            var callerTypeName = Path.GetFileNameWithoutExtension(callerFilePath);

            if (!value)
            {
                string errors = String.Join("\n", validationResults.Where(kv => !kv.Value.IsValid).Select(kv => $"{kv.Key}:  {kv.Value.ErrorMessage}"));
                throw new Exception($"Validation failed in `{callerTypeName}` by operation `{callerTypeName}`", new Exception(errors));
            }
        }
    }
}
