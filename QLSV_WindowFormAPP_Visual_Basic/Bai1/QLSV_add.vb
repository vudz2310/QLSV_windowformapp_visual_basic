Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms

Public Class QLSV_add
    ' Connection string to connect to your database

    Private connectionString As String = "Server=localhost;Database=QuanLySinhVienVIP;Integrated Security=True;"
    Private connection As New SqlConnection(connectionString)


    Public Sub New()
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
    Private Sub load()
        LoadData()
        DangNhap()
        dangkithi()
        Diem()
        danhsachSinhVien()
        MonHoc()
        ComboBox1.Items.Add("Mã Sinh Viên")
        ComboBox1.Items.Add("Tên Sinh Viên")
        ComboBox1.Items.Add("Khóa Học")
        ComboBox1.Items.Add("Ngành Học")
        ComboBox1.SelectedIndex = 0 ' Chọn mặc định

        'hình ảnh log-out
        Button6.Image = Image.FromFile("C:\Users\nitro\Desktop\Bai1\image\LOG.png")
        Button6.ImageAlign = ContentAlignment.MiddleLeft
        Button6.TextAlign = ContentAlignment.MiddleRight
        'hình dảnh refresh
        Button32.Image = Image.FromFile("C:\Users\nitro\Desktop\Bai1\image\Refresh-button.png")
        Button32.ImageAlign = ContentAlignment.MiddleLeft
        Button32.TextAlign = ContentAlignment.MiddleRight

        'thuật toán hình ảnh 
        Dim menus As Button() = {menu1, menu2, menu3, menu4, menu5, menu6}
        Dim imageNames As String() = {"admin", "student", "book", "exam", "score", "customer"}
        Dim imagePathBase As String = "C:\Users\nitro\Desktop\Bai1\image\"


        For i As Integer = 0 To menus.Length - 1
            SetMenuImage(menus(i), imagePathBase & imageNames(i) & ".png")
        Next
    End Sub
    Private Sub QLSV_add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load()
        'panel 6 search

    End Sub

    ' Hàm đặt ảnh và căn chỉnh cho menu button
    Private Sub SetMenuImage(menu As Button, imagePath As String)
        menu.Image = New Bitmap(Image.FromFile(imagePath), New Size(32, 32))
        menu.ImageAlign = ContentAlignment.MiddleLeft
        menu.TextAlign = ContentAlignment.MiddleRight
    End Sub




    Private Sub LoadData()
        Try
            ' Đóng kết nối nếu nó đang mở
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            connection.Open()
            Dim query As String = "SELECT * FROM Students"
            Dim command As New System.Data.SqlClient.SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            DataGridView1.DataSource = table ' Ensure this matches the DataGridView's Name

            ' Cập nhật tên cột theo ý muốn
            DataGridView1.Columns("StudentID").HeaderText = "Mã Sinh Viên"
            DataGridView1.Columns("StudentName").HeaderText = "Tên Sinh Viên"
            DataGridView1.Columns("DateOfBirth").HeaderText = "Ngày Sinh"
            DataGridView1.Columns("Course").HeaderText = "Khóa Học"
            DataGridView1.Columns("Gender").HeaderText = "Giới Tính"
            DataGridView1.Columns("Major").HeaderText = "Ngành Học"

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub



    ' Hàm tải thông tin sinh viên
    Private Sub LoadStudentInfo()
        Dim studentID As String = masvnew.Text ' Lấy mã sinh viên từ TextBox masvnew

        If String.IsNullOrEmpty(studentID) Then
            MessageBox.Show("Vui lòng nhập mã sinh viên cần tìm.")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "SELECT StudentID, StudentName, DateOfBirth, Course, Gender, Major FROM Students WHERE StudentID = @masvnew"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@masvnew", studentID)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        txtmasv.Text = reader("StudentID").ToString()
                        txtname.Text = reader("StudentName").ToString()

                        ' Xử lý ngày tháng
                        Dim dob As DateTime
                        If DateTime.TryParse(reader("DateOfBirth").ToString(), dob) Then
                            DateTimePicker1.Value = dob
                            ngaysinh.Text = dob.ToString("yyyy-MM-dd")
                        Else
                            ngaysinh.Clear()
                        End If

                        txtkhoahoc.Text = reader("Course").ToString()

                        ' Xử lý giới tính
                        Dim gender As String = reader("Gender").ToString()
                        If gender = "nam" Then
                            Nam.Checked = True
                        ElseIf gender = "nu" Then
                            nu.Checked = True
                        Else
                            Nam.Checked = False
                            nu.Checked = False
                        End If

                        txtnganhhoc.Text = reader("Major").ToString()
                    Else
                        MessageBox.Show("Không tìm thấy thông tin sinh viên.")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Lỗi Không thể load Sinh Viên: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub


    Private Sub masvnew_Leave(sender As Object, e As EventArgs) Handles masvnew.Leave
        LoadStudentInfo()
    End Sub



    Private Sub btneditclear_Click_1(sender As Object, e As EventArgs)
        txtmasv.Text = ""
        txtkhoahoc.Text = ""
        txtname.Text = ""
        txtnganhhoc.Text = ""
        masvnew.Text = ""
        ngaysinh.Text = ""

    End Sub
    Private Sub Button_Click(sender As Object, e As EventArgs)
        txtmasv.Text = ""
        txtkhoahoc.Text = ""
        txtname.Text = ""
        txtnganhhoc.Text = ""
        masvnew.Text = ""
        ngaysinh.Text = ""

    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs)
        GrAdd.Visible = False
        GrEdit.Visible = False
        Grdel.Visible = True
    End Sub


    Private Sub DangNhap()
        Try
            ' Đóng kết nối nếu nó đang mở
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            connection.Open()
            Dim query As String = "SELECT * FROM DangNhap"
            Dim command As New System.Data.SqlClient.SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView7.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

            DataGridView7.DataSource = table ' Ensure this matches the DataGridView's Name

            DataGridView7.Columns("Taikhoan").HeaderText = "Tên tài khoản"
            DataGridView7.Columns("matkhau").HeaderText = "Mật khẩu "


        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub MonHoc()
        Try
            ' Đóng kết nối nếu nó đang mở
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            connection.Open()
            Dim query As String = "SELECT * FROM MonHoc"
            Dim command As New System.Data.SqlClient.SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView6.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            DataGridView6.DataSource = table ' Ensure this matches the DataGridView's Name

            DataGridView6.Columns("MaMonHoc").HeaderText = "Mã Môn Học"
            DataGridView6.Columns("TenMonHoc").HeaderText = "Tên Môn Học"


        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub dangkithi()
        Try
            ' Đóng kết nối nếu nó đang mở
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            connection.Open()
            Dim query As String = "SELECT * FROM DangKiThi"
            Dim command As New System.Data.SqlClient.SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView4.DataSource = table ' 
            DataGridView4.Columns("MaMonHoc").HeaderText = "Mã Môn Học"
            DataGridView4.Columns("MaSinhVien").HeaderText = "Mã Sinh Viên"
            DataGridView4.Columns("StudentName").HeaderText = "Tên Sinh Viên"
            DataGridView4.Columns("NgayDangKi").HeaderText = "Ngày Đăng Kí"
            DataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Diem()
        Try
            ' Đóng kết nối nếu nó đang mở
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            connection.Open()
            Dim query As String = "SELECT * FROM Diem"
            Dim command As New System.Data.SqlClient.SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView3.DataSource = table ' Ensure this matches the DataGridView's Name

            DataGridView3.Columns("MaMonHoc").HeaderText = "Mã Môn Học"
            DataGridView3.Columns("MaSinhVien").HeaderText = "Mã Sinh Viên"
            DataGridView3.Columns("StudentName").HeaderText = "Tên Sinh Viên"
            DataGridView3.Columns("DiemThi").HeaderText = "Điểm Thi"
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub
    Private Sub danhsachSinhVien()
        Try
            ' Đóng kết nối nếu nó đang mở
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If

            connection.Open()
            Dim query As String = "SELECT * FROM DanhSachSinhVien"
            Dim command As New System.Data.SqlClient.SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView2.DataSource = table
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells




        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub btnadd_Click_1(sender As Object, e As EventArgs) Handles btnadd.Click
        Try
            ' Kết nối cơ sở dữ liệu
            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If

            ' Câu lệnh SQL với các tham số
            Dim query = "INSERT INTO Students (StudentID, StudentName, DateOfBirth, Course, Gender, Major) " &
                              "VALUES (@StudentID, @StudentName, @DateOfBirth, @Course, @Gender, @Major)"

            ' Sử dụng câu lệnh SQL với tham số
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@StudentID", txtaddmasv.Text)
                command.Parameters.AddWithValue("@StudentName", txtaddtensv.Text)

                ' Xử lý ngày tháng: Lấy từ TextBox ngaysinh hoặc từ DateTimePicker
                Dim inputDate = txtaddngaysinh.Text.Trim
                Dim parsedDate As Date
                If Date.TryParse(inputDate, parsedDate) Then
                    command.Parameters.AddWithValue("@DateOfBirth", parsedDate)
                Else
                    MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập đúng định dạng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                command.Parameters.AddWithValue("@Course", txtaddkhoahoc.Text)

                ' Xác định giới tính
                Dim gender = If(Raddnam.Checked, "nam", If(Raddnu.Checked, "nu", Nothing))
                command.Parameters.AddWithValue("@Gender", gender)
                command.Parameters.AddWithValue("@Major", txtaddnganhhoc.Text)

                command.ExecuteNonQuery()
            End Using

            MessageBox.Show("Thêm Sinh Viên Thành Công :>")
            LoadData()

        Catch ex As Exception
            MessageBox.Show("Lỗi không thể thêm sinh viên: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        GrEdit.Visible = True


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub add_Click(sender As Object, e As EventArgs) Handles add.Click
        GrAdd.Visible = True
        GrEdit.Visible = False
        Grdel.Visible = False
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If String.IsNullOrEmpty(txtdel.Text) Then
            MessageBox.Show("vui lòng nhập đúng mã sinh viên.")
            Return
        End If

        Try
            connection.Open()
            Dim query = "DELETE FROM Students WHERE StudentID=@StudentID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@StudentID", txtdel.Text)
                Dim rowsAffected = command.ExecuteNonQuery

                If rowsAffected > 0 Then
                    MessageBox.Show("Xóa Sinh Viên Thành Công")
                Else
                    MessageBox.Show("No student found with the given ID.")
                End If
            End Using
            LoadData() ' Refresh the data grid
        Catch ex As Exception
            MessageBox.Show("Error deleting student: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

        GrEdit.Visible = False
        Grdel.Visible = False
        GrAdd.Visible = True
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        GrAdd.Visible = False
        GrEdit.Visible = True
        Grdel.Visible = False
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        ' Kiểm tra nếu ô tìm kiếm rỗng
        If String.IsNullOrEmpty(txtdel.Text) Then
            MessageBox.Show("Vui lòng nhập mã sinh viên cần tìm.")
            Return
        End If

        Try
            connection.Open()
            ' Câu lệnh SQL tìm kiếm với LIKE
            Dim query = "SELECT * FROM Students WHERE StudentID LIKE @StudentID"

            ' Tạo command
            Using command As New SqlCommand(query, connection)
                ' Thêm tham số với ký tự `%` để tìm kiếm một phần
                command.Parameters.AddWithValue("@StudentID", "%" & txtdel.Text & "%")

                ' Tạo adapter để đổ dữ liệu vào DataTable
                Dim adapter As New SqlDataAdapter(command)
                Dim table As New DataTable()

                ' Đổ dữ liệu từ SQL vào DataTable
                adapter.Fill(table)

                ' Kiểm tra nếu có dữ liệu hay không
                If table.Rows.Count > 0 Then
                    ' Gán DataTable cho DataGridView để hiển thị kết quả
                    DataGridView1.DataSource = table
                Else
                    MessageBox.Show("Không tìm thấy sinh viên với mã đã nhập.")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Lỗi tìm kiếm sinh viên: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtdel.Text = ""
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        GrEdit.Visible = False
        Grdel.Visible = True
        GrAdd.Visible = False
    End Sub

    Private Sub delete_Click_1(sender As Object, e As EventArgs) Handles delete.Click
        GrEdit.Visible = False
        Grdel.Visible = True
        GrAdd.Visible = False
    End Sub

    Private Sub Dateadd_ValueChanged(sender As Object, e As EventArgs) Handles Dateadd.ValueChanged
        txtaddngaysinh.Text = Dateadd.Value.ToString("dd-MM-yyyy")
    End Sub



    Private Sub edit_Click(sender As Object, e As EventArgs) Handles edit.Click
        If String.IsNullOrEmpty(masvnew.Text) Then
            MessageBox.Show("Vui lòng nhập mã sinh viên để chỉnh sửa.")
            Return
        End If

        Try
            connection.Open()
            Dim query = "UPDATE Students SET StudentID = @NewStudentID, StudentName = @StudentName, " &
                              "DateOfBirth = @DateOfBirth, Course = @Course, " &
                              "Gender = @Gender, Major = @Major WHERE StudentID = @NewStudentID"

            Using command As New SqlCommand(query, connection)
                ' Thêm các tham số
                command.Parameters.AddWithValue("@NewStudentID", txtmasv.Text) ' Giá trị mới cho StudentID
                command.Parameters.AddWithValue("@CurrentStudentID", masvnew.Text) ' Giá trị hiện tại của StudentID (từ masvnew)
                command.Parameters.AddWithValue("@StudentName", txtname.Text)
                Dim dob As Date
                If Date.TryParse(ngaysinh.Text, dob) Then
                    command.Parameters.AddWithValue("@DateOfBirth", dob)
                Else
                    MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng kiểm tra lại.")
                    Return
                End If

                command.Parameters.AddWithValue("@Course", txtkhoahoc.Text)
                Dim gender = If(Nam.Checked, "nam", If(nu.Checked, "nu", Nothing))
                command.Parameters.AddWithValue("@Gender", gender)

                command.Parameters.AddWithValue("@Major", txtnganhhoc.Text)

                ' Thực thi lệnh SQL
                Dim rowsAffected = command.ExecuteNonQuery

                If rowsAffected > 0 Then
                    MessageBox.Show("Chỉnh sửa thông tin thành công!")
                    LoadData() ' Refresh lại dữ liệu
                Else
                    MessageBox.Show("Không tìm thấy sinh viên với mã đã nhập.")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Lỗi khi chỉnh sửa sinh viên: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        LoadData()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles menu1.Click
        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        DangNhap()
    End Sub

    Private Sub menu2_Click(sender As Object, e As EventArgs) Handles menu2.Click
        Panel1.Visible = False
        Panel2.Visible = True
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        LoadData()
    End Sub

    Private Sub menu3_Click(sender As Object, e As EventArgs) Handles menu3.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = True
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        MonHoc()
    End Sub

    Private Sub menu4_Click(sender As Object, e As EventArgs) Handles menu4.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        dangkithi()
    End Sub

    Private Sub menu5_Click(sender As Object, e As EventArgs) Handles menu5.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = True
        Panel6.Visible = False
        Diem()
    End Sub

    Private Sub menu6_Click(sender As Object, e As EventArgs) Handles menu6.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = True
        danhsachSinhVien()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (user.Text = "" And pass.Text = "") Then
            MessageBox.Show("vui Lòng điền thông tin đầy đủ")
            connection.Close()
        Else

            Try
                ' Kết nối cơ sở dữ liệu
                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If

                ' Câu lệnh SQL với các tham số
                Dim query = "INSERT INTO DangNhap (taikhoan, matkhau) " &
                                      "VALUES (@user, @pass)"

                ' Sử dụng câu lệnh SQL với tham số
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@user", user.Text)
                    command.Parameters.AddWithValue("@pass", pass.Text)
                    command.ExecuteNonQuery()
                End Using

                MessageBox.Show("Thêm Tài Khoản mới thành công :>")
                DangNhap() ' Làm mới bảng dữ liệu

            Catch ex As Exception
                MessageBox.Show("Lỗi không thể thêm tài khoản mới: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If String.IsNullOrEmpty(user.Text) Then
            MessageBox.Show("Vui lòng nhập mã sinh viên để chỉnh sửa.")
            Return
        End If

        Try
            connection.Open()
            Dim query = "UPDATE dangnhap SET taikhoan = @user, matkhau = @pass "

            Using command As New SqlCommand(query, connection)
                ' Thêm các tham số
                command.Parameters.AddWithValue("@user", user.Text)
                command.Parameters.AddWithValue("@pass", pass.Text)


                ' Thực thi lệnh SQL
                Dim rowsAffected = command.ExecuteNonQuery

                If rowsAffected > 0 Then
                    MessageBox.Show("Chỉnh sửa thông tin thành công!")
                    DangNhap() ' Refresh lại dữ liệu
                Else
                    MessageBox.Show("Tài khoản bạn nhập không tồn tại!!!")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Lỗi : " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If String.IsNullOrEmpty(user.Text) Then
            MessageBox.Show("vui lòng nhập đúng tài khoản cần xóa.")
            Return
        End If

        Try
            connection.Open()
            Dim query = "DELETE FROM dangnhap WHERE taikhoan=@user"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@user", user.Text)
                Dim rowsAffected = command.ExecuteNonQuery

                If rowsAffected > 0 Then
                    MessageBox.Show("Xóa tài khoản Thành Công")
                Else
                    MessageBox.Show("No student found with the given ID.")
                End If
            End Using
            DangNhap() ' Refresh the data grid
        Catch ex As Exception
            MessageBox.Show("Error deleting student: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Hide()
        Form1.Show()

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Dim searchColumn As String = ""
        Dim searchValue As String = search.Text

        ' Xác định cột cần tìm kiếm dựa trên ComboBox
        Select Case ComboBox1.SelectedItem.ToString()
            Case "Mã Sinh Viên"
                searchColumn = "MaSinhVien"
            Case "Tên Sinh Viên"
                searchColumn = "StudentName"
            Case "Khóa Học"
                searchColumn = "Course"
            Case "Ngành Học"
                searchColumn = "Major"
        End Select

        If String.IsNullOrEmpty(searchValue) Then
            MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!")
            Return
        End If

        Try
            connection.Open()

            ' Câu lệnh SQL với LIKE để tìm kiếm
            Dim query As String = "SELECT * FROM danhsachsinhvien WHERE " & searchColumn & " LIKE @searchValue"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@searchValue", "%" & searchValue & "%")
            Dim adapter As New SqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView2.DataSource = table


        Catch ex As Exception
            MessageBox.Show("Lỗi khi tìm kiếm: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtmhadd.TextChanged

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        GroupBox1.Visible = True
        GroupBox2.Visible = False
        GroupBox3.Visible = False

    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        GroupBox1.Visible = False
        GroupBox2.Visible = True
        GroupBox3.Visible = False
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        GroupBox3.Visible = True
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If String.IsNullOrEmpty(txtmamh.Text) Then
            MessageBox.Show("Vui lòng nhập mã môn học để chỉnh sửa.")
            Return
        End If

        Try
            connection.Open()
            Dim query = "delete from monhoc where mamonhoc = @mamonhoc "

            Using command As New SqlCommand(query, connection)
                ' Thêm các tham số
                command.Parameters.AddWithValue("@mamonhoc", txtmamh.Text)



                ' Thực thi lệnh SQL
                Dim rowsAffected = command.ExecuteNonQuery

                If rowsAffected > 0 Then
                    MessageBox.Show("Chỉnh sửa thông tin thành công!")
                    MonHoc()
                Else
                    MessageBox.Show("Mã môn học bạn nhập không tồn tại!!!")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Lỗi : " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If (txtmhadd.Text = "" And txttenmhadd.Text = "") Then
            MessageBox.Show("Vui lòng không bỏ trống!!!!")
        Else
            Try
                If connection.State = ConnectionState.Closed Then
                    connection.Open()
                End If
                Dim query = "insert into monhoc(mamonhoc,tenmonhoc)" & "values (@mamonhoc,@tenmonhoc)"
                Using Command As New SqlCommand(query, connection)
                    Command.Parameters.AddWithValue("@mamonhoc", txtmhadd.Text)
                    Command.Parameters.AddWithValue("@tenmonhoc", txttenmhadd.Text)
                    Command.ExecuteNonQuery()
                End Using

                MessageBox.Show("Thêm Môn học mới thành công :>")
                MonHoc()


            Catch ex As Exception
                MessageBox.Show("Lỗi" & ex.Message)
            Finally
                connection.Close()
            End Try
        End If
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        If String.IsNullOrEmpty(mamh.Text) Then
            MessageBox.Show("Vui lòng nhập mã môn học để chỉnh sửa.")
            Return
        End If

        Try
            connection.Open()
            Dim query = "UPDATE monhoc SET tenmonhoc = @tenmh WHERE mamonhoc = @mamh"

            Using command As New SqlCommand(query, connection)
                ' Thêm các tham số
                command.Parameters.AddWithValue("@mamh", mamh.Text)
                command.Parameters.AddWithValue("@tenmh", tenmh.Text)

                Dim rowsAffected = command.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Chỉnh sửa thông tin thành công!")
                    DangNhap() ' Refresh lại dữ liệu
                Else
                    MessageBox.Show("Mã môn học bạn nhập không tồn tại !!!")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try


    End Sub
    'show điểm thi
    Private Sub txtmsv_TextChanged(sender As Object, e As EventArgs) Handles txtmsv.TextChanged
        If String.IsNullOrEmpty(txtmsv.Text) Then
            txttsv.Text = ""
            txtdiem.Text = ""
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "SELECT s.StudentName, d.DiemThi " &
                              "FROM Students s " &
                              "LEFT JOIN Diem d ON s.StudentID = d.MaSinhVien " &
                              "WHERE s.StudentID = @msv"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@msv", txtmsv.Text)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        txttsv.Text = reader("StudentName").ToString()

                        If IsDBNull(reader("DiemThi")) Then
                            txtdiem.Text = "Không có điểm"
                        Else
                            txtdiem.Text = reader("DiemThi").ToString()
                        End If
                    Else
                        txttsv.Text = "Không tìm thấy sinh viên."
                        txtdiem.Text = ""
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub


    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        If String.IsNullOrEmpty(txtmsv.Text) Or String.IsNullOrEmpty(Txtmmh.Text) Or String.IsNullOrEmpty(txtdiem.Text) Then
            MessageBox.Show("Vui lòng nhập đủ thông tin.")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "INSERT INTO Diem (MaMonHoc, MaSinhVien,StudentName, DiemThi) VALUES (@mmh, @msv,@tensv, @diem)"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@mmh", Txtmmh.Text)
                command.Parameters.AddWithValue("@msv", txtmsv.Text)
                command.Parameters.AddWithValue("@tensv", txttsv.Text)
                command.Parameters.AddWithValue("@diem", Convert.ToDouble(txtdiem.Text))

                Dim rows = command.ExecuteNonQuery()
                If rows > 0 Then
                    MessageBox.Show("Thêm điểm thành công!")
                    Diem()
                Else
                    MessageBox.Show("Không thể thêm điểm.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If String.IsNullOrEmpty(txtmsv.Text) Or String.IsNullOrEmpty(Txtmmh.Text) Or String.IsNullOrEmpty(txtdiem.Text) Then
            MessageBox.Show("Vui lòng nhập đủ thông tin.")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "UPDATE Diem SET DiemThi = @diem WHERE MaMonHoc = @mmh , MaSinhVien = @msv,Studentname= @tsv"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@mmh", Txtmmh.Text)
                command.Parameters.AddWithValue("@tsv", txttsv.Text)
                command.Parameters.AddWithValue("@msv", txtmsv.Text)
                command.Parameters.AddWithValue("@diem", Convert.ToDouble(txtdiem.Text))

                Dim rows = command.ExecuteNonQuery()
                If rows > 0 Then
                    MessageBox.Show("Sửa điểm thành công!")
                    Diem()
                Else
                    MessageBox.Show("Không tìm thấy điểm cần sửa.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If String.IsNullOrEmpty(txtmsv.Text) Or String.IsNullOrEmpty(Txtmmh.Text) Then
            MessageBox.Show("Vui lòng nhập mã sinh viên và mã môn học để xóa.")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "DELETE FROM Diem WHERE MaMonHoc = @mmh , MaSinhVien = @msv,Studentname= @tsv"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@mmh", Txtmmh.Text)
                command.Parameters.AddWithValue("@tsv", txttsv.Text)
                command.Parameters.AddWithValue("@msv", txtmsv.Text)

                Dim rows = command.ExecuteNonQuery()
                If rows > 0 Then
                    MessageBox.Show("Xóa điểm thành công!")
                    Diem()
                Else
                    MessageBox.Show("Không tìm thấy điểm để xóa.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        load()
    End Sub



    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If String.IsNullOrEmpty(TextBox2.Text) Then
            TextBox3.Text = ""
            TextBox4.Text = ""
            Return
        End If

        Try
            connection.Open()

            ' Truy vấn SQL hợp lệ
            Dim query As String = "SELECT s.StudentName, dk.NgayDangKi " &
                              "FROM Students s " &
                              "LEFT JOIN DangKiThi dk ON s.StudentID = dk.MaSinhVien " &
                              "WHERE s.StudentID = @msv"

            Using command As New SqlCommand(query, connection)
                ' Thêm tham số
                command.Parameters.AddWithValue("@msv", TextBox2.Text)

                ' Sử dụng SqlDataReader để đọc dữ liệu
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        ' Lấy tên sinh viên
                        TextBox3.Text = If(IsDBNull(reader("StudentName")), "Không tìm thấy tên", reader("StudentName").ToString())

                        ' Lấy ngày đăng ký
                        If IsDBNull(reader("NgayDangKi")) Then
                            TextBox4.Text = "Không có ngày đăng ký"
                        Else
                            Dim ngayDangKi As Date = Convert.ToDateTime(reader("NgayDangKi"))
                            TextBox4.Text = ngayDangKi.ToString("dd/MM/yyyy") ' Định dạng ngày
                        End If
                    Else
                        TextBox3.Text = "Không tìm thấy sinh viên."
                        TextBox4.Text = ""
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        ' Hiển thị giá trị ngày/tháng/năm trong TextBox4
        TextBox4.Text = DateTimePicker2.Value.ToString("dd/MM/yyyy") ' Định dạng ngày
    End Sub

    Private Sub btndk_Click(sender As Object, e As EventArgs) Handles btndk.Click
        If String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox3.Text) Or String.IsNullOrEmpty(TextBox4.Text) Then
            MessageBox.Show("Vui lòng nhập đầy đủ thông tin!")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "INSERT INTO DangKiThi (MaMonHoc, MaSinhVien, StudentName, NgayDangKi) " &
                                  "VALUES (@mamh, @msv, @name, @ngaydk)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@mamh", TextBox1.Text)
                command.Parameters.AddWithValue("@msv", TextBox2.Text)
                command.Parameters.AddWithValue("@name", TextBox3.Text)
                command.Parameters.AddWithValue("@ngaydk", DateTime.Parse(TextBox4.Text))

                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MessageBox.Show("Thêm thành công!")
                    dangkithi() ' Hàm load lại dữ liệu
                Else
                    MessageBox.Show("Không thể thêm dữ liệu.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then
            MessageBox.Show("Vui lòng nhập mã môn học để chỉnh sửa.")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "UPDATE DangKiThi " &
                                  "SET StudentName = @name, NgayDangKi = @ngaydk " &
                                  "WHERE MaMonHoc = @mamh "

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@mamh", TextBox1.Text)
                command.Parameters.AddWithValue("@msv", TextBox2.Text)
                command.Parameters.AddWithValue("@name", TextBox3.Text)
                command.Parameters.AddWithValue("@ngaydk", DateTime.Parse(TextBox4.Text))

                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MessageBox.Show("Chỉnh sửa thành công!")
                    dangkithi()
                Else
                    MessageBox.Show("Không tìm thấy dữ liệu cần chỉnh sửa.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then
            MessageBox.Show("Vui lòng nhập mã môn học để xóa.")
            Return
        End If

        Try
            connection.Open()
            Dim query As String = "DELETE FROM DangKiThi WHERE MaMonHoc = @mamh,Masinhvien = @msv "

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@mamh", TextBox1.Text)
                command.Parameters.AddWithValue("@msv", TextBox2.Text)

                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MessageBox.Show("Xóa thành công!")
                    dangkithi()
                Else
                    MessageBox.Show("Không tìm thấy dữ liệu cần xóa.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub


End Class
