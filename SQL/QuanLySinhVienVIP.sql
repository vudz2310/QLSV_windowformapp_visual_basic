-- Tạo cơ sở dữ liệu
CREATE DATABASE QuanLySinhVienVIP;
GO

-- Sử dụng cơ sở dữ liệu QuanLySinhVienVIP
USE QuanLySinhVienVIP;
GO

-- Tạo bảng Students
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,          -- Mã sinh viên (khóa chính)
    StudentName NVARCHAR(100),          -- Tên sinh viên
    DateOfBirth DATE,                   -- Ngày sinh
    Course NVARCHAR(50),                -- Khóa học
    Gender NVARCHAR(3),                 -- Giới tính (nam/nữ)
    Major NVARCHAR(100)                 -- Ngành học
);

-- Tạo bảng MonHoc
CREATE TABLE MonHoc (
    MaMonHoc INT PRIMARY KEY,           -- Mã môn học (khóa chính)
    TenMonHoc NVARCHAR(100) NOT NULL    -- Tên môn học
);

-- Tạo bảng DangKiThi
CREATE TABLE DangKiThi (
    MaMonHoc INT,                       -- Mã môn học (khóa ngoại)
    MaSinhVien INT,                     -- Mã sinh viên (khóa ngoại)
    StudentName NVARCHAR(100),          -- Tên sinh viên
    NgayDangKi DATE,                    -- Ngày đăng ký thi
    PRIMARY KEY (MaMonHoc, MaSinhVien), -- Khóa chính kết hợp
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaSinhVien) REFERENCES Students(StudentID)
);

-- Tạo bảng Diem
CREATE TABLE Diem (
    MaMonHoc INT,                       -- Mã môn học (khóa ngoại)
    MaSinhVien INT,                     -- Mã sinh viên (khóa ngoại)
    StudentName NVARCHAR(100),          -- Tên sinh viên
    DiemThi FLOAT,                      -- Điểm thi môn học
    PRIMARY KEY (MaMonHoc, MaSinhVien), -- Khóa chính kết hợp
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaSinhVien) REFERENCES Students(StudentID)
);
-- Thêm dữ liệu vào bảng Students
INSERT INTO Students (StudentID, StudentName, DateOfBirth, Course, Gender, Major)
VALUES 
    (1, 'John Doe', '2000-01-01', '28', 'nam', 'Software Engineering'),
    (100, 'Huy Vu', '2005-01-20', '28', 'nam', 'CNTT'),
    (2, 'Nguyen Van A', '2001-02-15', '26', 'nam', 'Toán học');

-- Thêm dữ liệu vào bảng MonHoc
INSERT INTO MonHoc (MaMonHoc, TenMonHoc)
VALUES 
    (101, 'Toán Cao Cấp'),
    (102, 'Lập Trình C++'),
    (103, 'Cơ Sở Dữ Liệu');

-- Thêm dữ liệu vào bảng DangKiThi
INSERT INTO DangKiThi (MaMonHoc, MaSinhVien, StudentName, NgayDangKi)
VALUES 
    (101, 1, 'John Doe', '2024-12-01'),
    (102, 100, 'Huy Vu', '2024-12-05'),
    (103, 2, 'Nguyen Van A', '2024-12-10');

-- Thêm dữ liệu vào bảng Diem
INSERT INTO Diem (MaMonHoc, MaSinhVien, StudentName, DiemThi)
VALUES 
    (101, 1, 'John Doe', 9.5),     -- John Doe thi Toán Cao Cấp
    (102, 100, 'Huy Vu', 8.0),     -- Huy Vu thi Lập Trình C++
    (103, 2, 'Nguyen Van A', 7.5); -- Nguyen Van A thi Cơ Sở Dữ Liệu
-- Cập nhật StudentName trong bảng DangKiThi
UPDATE DangKiThi
SET StudentName = s.StudentName
FROM DangKiThi dkt
JOIN Students s ON dkt.MaSinhVien = s.StudentID;

-- Cập nhật StudentName trong bảng Diem
UPDATE Diem
SET StudentName = s.StudentName
FROM Diem d
JOIN Students s ON d.MaSinhVien = s.StudentID;

create table dangnhap(
taikhoan nvarchar(50),
matkhau nvarchar(50)
)

insert into dangnhap(taikhoan,matkhau)
values ('admin','123'),
	   ('',''),
	   ('admin','vudz2310')

	   


select * from Students
select * from MonHoc
select * from Diem
select * from dangnhap
select * from DangKiThi

-- Tạo bảng DanhSachSinhVien
CREATE TABLE DanhSachSinhVien (
    MaSinhVien INT,                    -- Mã sinh viên
    StudentName NVARCHAR(100),         -- Tên sinh viên
    DateOfBirth DATE,                  -- Ngày sinh
    Gender NVARCHAR(3),                -- Giới tính
    Major NVARCHAR(50),                -- Ngành học
    Course NVARCHAR(100),              -- Khóa học
    DiemThi FLOAT,                     -- Điểm thi
    PRIMARY KEY (MaSinhVien) -- Khóa chính kết hợp
);
INSERT INTO DanhSachSinhVien (MaSinhVien, StudentName, DateOfBirth, Gender, Major, Course, DiemThi)
SELECT 
    s.StudentID AS MaSinhVien,
    s.StudentName,
    s.DateOfBirth,
    s.Gender,
    s.Major,
    s.Course,
    d.DiemThi
FROM 
    Students s
JOIN 
    Diem d ON s.StudentID = d.MaSinhVien
	SELECT * FROM DanhSachSinhVien;

