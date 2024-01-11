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

            validator.IsValid(out var results).Confirm(results);

            Save(student);
        }
    }
}
