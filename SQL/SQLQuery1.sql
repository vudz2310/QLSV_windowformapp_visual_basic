-- Create Database QLSV
CREATE DATABASE QLSV;
USE QLSV;
GO

-- Create dangnhap Table
CREATE TABLE dangnhap (
    taikhoan NVARCHAR(50) PRIMARY KEY,
    matkhau NVARCHAR(50)
);

-- Insert Data into dangnhap
INSERT INTO dangnhap(taikhoan, matkhau)
VALUES ('admin', '123');

-- Select Data from dangnhap
SELECT * FROM dangnhap;

-- Create Students Table with a Foreign Key referencing dangnhap
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,         
    StudentName VARCHAR(100),          
    DateOfBirth DATE,                  
    hocluc NVARCHAR(7),
    Course VARCHAR(50),
    AverageScore FLOAT,                
    Gender VARCHAR(3),                 
    Major VARCHAR(100),  
    taikhoan NVARCHAR(50),
    FOREIGN KEY (taikhoan) REFERENCES dangnhap(taikhoan)
)


-- Insert Data into Students
INSERT INTO Students (StudentID, StudentName, DateOfBirth, hocluc, Course, AverageScore, Gender, Major)
VALUES (1, 'John Doe', '2000-01-01', 'Kha', 'Computer Science', 10.0, 'nam', 'Software Engineering'),
       (100, 'Huy Vu', '2005-01-20', 'gioi', 'cntt', 9.0, 'nam', 'cntt')

-- Select Data from Students
SELECT * FROM Students;

ALTER TABLE Students
DROP COLUMN taikhoan;
ALTER TABLE Students
ADD taikhoan NVARCHAR(50);

 INSERT INTO Students (StudentID, StudentName, DateOfBirth, hocluc, Course, AverageScore, Gender, Major, taikhoan, diem1, diem2, diem3)
VALUES (2, 'Nguyen Van A', '2001-02-15', 'Kha', 'Mathematics', 8.5, 'nam', 'Toán học', 'admin', 7, 8, 9);



