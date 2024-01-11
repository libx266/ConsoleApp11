using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class StudentValidator : Validator<StudentModel>
    {
        public StudentValidator(StudentModel value, Dictionary<string, Func<StudentModel, ValidationResult>>? excludes = default) : base(value, excludes) { }

        protected override Dictionary<string, Func<StudentModel, ValidationResult>> ConstructValidations() => new()
        {
            [nameof(StudentModel.Id)] = s => new ValidationResult(s.Id > 0, "Id is invalid"),
            [nameof(StudentModel.Name)] = s => new ValidationResult(s.Name.Length < 32, "Name length must be less than 32 characters"),
            [nameof(StudentModel.Inn)] = s => new ValidationResult(s.Inn.Length == 12, "Inn length must be equals 12")
        };
    }
}
