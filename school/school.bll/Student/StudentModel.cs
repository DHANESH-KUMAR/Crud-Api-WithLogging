using System;
using System.ComponentModel.DataAnnotations;

namespace school.bll.Student
{
    public class StudentModel
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "First Name Is Required.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [EnrollmentDate]
        public DateTime EnrollmentDate { get; set; }

        [EmailAddress(ErrorMessage = "Please Provide Email-ID")]
        public string EmailID { get; set; }

        [Required]
        public string MobileNo { get; set; }

    }
}
