<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        thoat = New Button()
        lbl1 = New Label()
        lbl2 = New Label()
        txtUsername = New TextBox()
        txtPassword = New TextBox()
        PictureBox1 = New PictureBox()
        Button1 = New Button()
        Label1 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' thoat
        ' 
        thoat.Location = New Point(600, 283)
        thoat.Name = "thoat"
        thoat.Size = New Size(111, 51)
        thoat.TabIndex = 1
        thoat.Text = "Thoát"
        thoat.UseVisualStyleBackColor = True
        ' 
        ' lbl1
        ' 
        lbl1.AutoSize = True
        lbl1.Font = New Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl1.ForeColor = SystemColors.ActiveCaptionText
        lbl1.Location = New Point(338, 149)
        lbl1.Name = "lbl1"
        lbl1.Size = New Size(109, 23)
        lbl1.TabIndex = 2
        lbl1.Text = "User Name "
        ' 
        ' lbl2
        ' 
        lbl2.AutoSize = True
        lbl2.Font = New Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl2.Location = New Point(338, 225)
        lbl2.Name = "lbl2"
        lbl2.Size = New Size(95, 23)
        lbl2.TabIndex = 3
        lbl2.Text = "Password "
        ' 
        ' txtUsername
        ' 
        txtUsername.Font = New Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtUsername.Location = New Point(504, 149)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(207, 41)
        txtUsername.TabIndex = 4
        ' 
        ' txtPassword
        ' 
        txtPassword.BorderStyle = BorderStyle.None
        txtPassword.Font = New Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtPassword.Location = New Point(504, 205)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(207, 34)
        txtPassword.TabIndex = 5
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(74, 130)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(212, 204)
        PictureBox1.TabIndex = 6
        PictureBox1.TabStop = False
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(397, 283)
        Button1.Name = "Button1"
        Button1.Size = New Size(143, 51)
        Button1.TabIndex = 7
        Button1.Text = "Đăng Nhập"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 25.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(114, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(597, 57)
        Label1.TabIndex = 8
        Label1.Text = "Phần Mềm Quản Lí Sinh Viên"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(842, 391)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Controls.Add(PictureBox1)
        Controls.Add(txtPassword)
        Controls.Add(txtUsername)
        Controls.Add(lbl2)
        Controls.Add(lbl1)
        Controls.Add(thoat)
        Name = "Form1"
        Text = "Quản Lý Sinh Viên"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents login As Button
    Friend WithEvents thoat As Button
    Friend WithEvents lbl1 As Label
    Friend WithEvents lbl2 As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label

End Class
