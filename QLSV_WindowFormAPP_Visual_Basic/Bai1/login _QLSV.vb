Imports System.Data.SqlClient

Public Class Form1


    Public Sub New()
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub Button1_click(sender As Object, e As EventArgs)

    End Sub

    Private Function ValidateCredentials(username As String, password As String) As Boolean
        ' Connection string to connect to your SQL Server database
        Dim connectionString As String = "Data Source=localhost;Initial Catalog=QuanLySinhVienVIP;Integrated Security=True" ' Adjust as needed
        Dim query As String = "SELECT COUNT(*) FROM dangnhap WHERE taikhoan = @taikhoan AND matkhau = @matkhau"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@taikhoan", username)
                command.Parameters.AddWithValue("@matkhau", password)

                connection.Open()
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                connection.Close()

                Return count > 0
            End Using
        End Using
    End Function

    Private Sub thoat_Click(sender As Object, e As EventArgs) Handles thoat.Click, thoat.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If ValidateCredentials(txtUsername.Text, txtPassword.Text) Then
            MessageBox.Show("Đăng nhập thành công!")
            ' Open the main form or perform other actions
            QLSV_add.Show()
            Hide()
        Else
            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!")
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
