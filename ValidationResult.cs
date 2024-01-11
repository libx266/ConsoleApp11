using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public struct ValidationResult
    {
        public readonly bool IsValid;
        public readonly string ErrorMessage;

        public ValidationResult(bool IsValid, string Message)
        {
            this.IsValid = IsValid;
            this.ErrorMessage = Message;
        }
    }
}
