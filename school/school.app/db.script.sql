
CREATE DATABASE SchoolDB;

CREATE TABLE Student
(
	ID bigint primary key identity,
	FirstName nvarchar(100) NOT NULL,
	MiddleName nvarchar(100) NULL,
	LastName nvarchar(100) NULL,
	EnrollmentDate datetime NULL,
	EmailID nvarchar(500) NULL,
	MobileNo varchar(20),

	CreateDate datetime default getdate(),
	CreateBy int default 0,
	ModifyDate datetime default null,
	ModifyBy int default 0
)

INSERT INTO Student(FirstName,MiddleName,LastName,EnrollmentDate,EmailID,MobileNo,CreateDate,CreateBy)
VALUES('Dhanesh','Kumar','Kushwaha','2022-01-03','dhanesh-kumar@hotmail.com','1234566',getUTCDate(),1)

INSERT INTO Student(FirstName,MiddleName,LastName,EnrollmentDate,EmailID,MobileNo,CreateDate,CreateBy)
VALUES('Satish','Kumar','','2022-01-03','Satish@gmail','1234566',getUTCDate(),1)


--getting all students
SELECT ID,FirstName,MiddleName,
		LastName,EnrollmentDate,EmailID,
		MobileNo,CreateDate,CreateBy,ModifyDate,ModifyBy FROM Student

--getting particular student
SELECT ID,FirstName,MiddleName,
		LastName,EnrollmentDate,EmailID,
		MobileNo,CreateDate,CreateBy,ModifyDate,ModifyBy FROM Student WHERE ID=@ID