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
            [nameof(StudentModel.Id)] = s => (s.Id > 0).IfError("value must bi positive"),
            [nameof(StudentModel.Name)] = s => (s.Name.Length < 32).IfError("length must be less than 32 characters"),
            [nameof(StudentModel.Inn)] = s => (s.Inn.Length == 12).IfError("length must be equals 12")
        };
    }
}
