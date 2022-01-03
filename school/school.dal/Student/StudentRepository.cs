using Dapper;
using school.bll.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace school.dal.Student
{
    public class StudentRepository : IStudentRepository
    {
        private const string connection_string = "Data Source=.;Initial Catalog=SchoolDB;Integrated Security=True";
        public async Task<StudentModel> Create(StudentModel student)
        {
            try
            {
                int createdId = 0;
                var query = $@"INSERT INTO Student (FirstName,MiddleName,LastName,EnrollmentDate,EmailID,MobileNo,CreateDate,CreateBy)
			                    VALUES(@FirstName,@MiddleName,@LastName,@EnrollmentDate,@EmailID,@MobileNo,GETDATE(),@CreateBy);SELECT SCOPE_IDENTITY()";


                using (IDbConnection db = new SqlConnection(connection_string))
                {
                    var parameters = new
                    {
                        FirstName = student.FirstName,
                        MiddleName = student.MiddleName,
                        LastName = student.LastName,
                        EnrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd"),
                        EmailID = student.EmailID,
                        MobileNo = student.MobileNo,
                        CreateBy = 1
                    };
                    createdId = await db.ExecuteScalarAsync<int>(query, parameters);
                }

                if (createdId > 0)
                {
                    return await Get(createdId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(StudentModel student)
        {
            int rowsAffeted = 0;
            var query = $@"Delete Student WHERE ID=@ID";

            using (IDbConnection db = new SqlConnection(connection_string))
            {
                rowsAffeted = await db.ExecuteAsync(query, new { ID = student.ID });
            }
            return rowsAffeted > 0;
        }
        /// <summary>
        /// Here we are transforming studentdto to studentmodel
        /// student model contain only public entity or fixed name.
        /// let assume in future MobileNo change to ContactNo for code perspective only
        /// there is the only place where we need to change so api consumer does't affected
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StudentModel>> Get()
        {
            List<StudentModel> students = new List<StudentModel>();

            foreach (var student in await getStudents())
            {
                students.Add(new StudentModel
                {
                    ID = student.ID,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    EnrollmentDate = student.EnrollmentDate,
                    EmailID = student.EmailID,
                    MobileNo = student.MobileNo
                });
            }
            return students;

        }
        //getting particular student related-property
        private async Task<IEnumerable<StudentDto>> getStudents()
        {
            IEnumerable<StudentDto> students;

            var query = @"SELECT ID,FirstName,MiddleName, LastName,EnrollmentDate,EmailID,
		                        MobileNo,CreateDate,CreateBy,ModifyDate,ModifyBy FROM Student";

            using (IDbConnection db = new SqlConnection(connection_string))
            {
                students = await db.QueryAsync<StudentDto>(query);
            }

            return students;
        }
        private async Task<StudentDto> getStudents(long id)
        {
            StudentDto student;

            var query = $@"SELECT ID,FirstName,MiddleName, LastName,EnrollmentDate,EmailID,
		                        MobileNo,CreateDate,CreateBy,ModifyDate,ModifyBy FROM Student WHERE ID=@ID";

            using (IDbConnection db = new SqlConnection(connection_string))
            {
                student = await db.QueryFirstAsync<StudentDto>(query, new { ID = id });
            }

            return student;
        }
        public async Task<StudentModel> Get(long id)
        {
            var student = await getStudents(id);

            return new StudentModel
            {
                ID = student.ID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
                EmailID = student.EmailID,
                MobileNo = student.MobileNo
            };

        }
        //getting all student related-property
        public async Task<StudentModel> Update(StudentModel student)
        {
            try
            {
                int rowsAffeted = 0;
                var query = $@"UPDATE   Student SET 
                        	    FirstName=@FirstName, MiddleName=@MiddleName,LastName=@LastName,
                                EnrollmentDate=GETDATE(),EmailID=@EmailID,MobileNo=@MobileNo,
                                ModifyDate=GETDATE(),ModifyBy=@ModifyBy WHERE ID=@ID";

                using (IDbConnection db = new SqlConnection(connection_string))
                {
                    var parameters = new
                    {
                        ID = student.ID,
                        FirstName = student.FirstName,
                        MiddleName = student.MiddleName,
                        LastName = student.LastName,
                        EnrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd"),
                        EmailID = student.EmailID,
                        MobileNo = student.MobileNo,
                        ModifyBy = 1
                    };
                    rowsAffeted = await db.ExecuteAsync(query, parameters);
                }

                if (rowsAffeted > 0)
                {
                    return await Get(student.ID);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
