using System;

namespace school.dal.Student
{
    public class StudentDto
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }

        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
    }
}
