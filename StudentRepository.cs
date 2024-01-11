using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class StudentRepository : Repository<StudentModel>
    {
        public StudentModel? GetById(int id) => Select($"select * from students where students.id = {id}").FirstOrDefault();

        public void RegisterStudent(StudentModel student)
        {
            var validator = new StudentValidator(student);

            bool valid = validator.IsValid(out var results);

            if (!valid)
            {
                throw new Exception("Data is invalid", new Exception(String.Join("\n", results.Where(kv => !kv.Value.IsValid).Select(kv => $"{kv.Key}:  {kv.Value.ErrorMessage}"))));
            }

            Save(student);
        }
    }
}
