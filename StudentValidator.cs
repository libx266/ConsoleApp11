using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class StudentValidator : Validator<StudentModel>
    {
        public StudentValidator(StudentModel value) : base(value) { }

        protected override Dictionary<string, Func<StudentModel, ValidationResult>> ConstructValidations() => new()
        {
            [nameof(StudentModel.Id)] = s => new ValidationResult(s.Id > 0, "Id is invalid"),
            [nameof(StudentModel.Name)] = s => new ValidationResult(s.Name.Length < 32, "Name is invalid"),
            [nameof(StudentModel.Inn)] = s => new ValidationResult(s.Inn.Length == 12, "Inn is invald")
        };
    }
}
