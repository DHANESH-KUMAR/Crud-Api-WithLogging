
using System.Collections.Generic;
using System.Threading.Tasks;

namespace school.bll.Student
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public async Task<StudentModel> Create(StudentModel student)
        {
            return await _studentRepository.Create(student);
        }

        public async Task<IEnumerable<StudentModel>> Get()
        {
            return await _studentRepository.Get();
        }

        public async Task<StudentModel> Get(long id)
        {
            return await _studentRepository.Get(id);
        }

        public async Task<StudentModel> Update(StudentModel student)
        {
            return await _studentRepository.Update(student);
        }
        public async Task<bool> Delete(StudentModel student)
        {
            return await _studentRepository.Delete(student);
        }
    }
}
