using System.Collections.Generic;
using System.Threading.Tasks;

namespace school.bll.Student
{
    public interface IStudentService
    {
        Task<StudentModel> Create(StudentModel student);
        Task<IEnumerable<StudentModel>> Get();
        Task<StudentModel> Get(long id);
        Task<StudentModel> Update(StudentModel student);
        Task<bool> Delete(StudentModel student);
    }
}
