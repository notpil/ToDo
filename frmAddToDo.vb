Imports System.IO

Public Class frmAddToDo
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox1.Text = "" OrElse ComboBox1.SelectedIndex = -1 OrElse ComboBox2.Text = "" Then
            Label6.Text = "必須項目に空欄があります"
        Else
            Label6.Text = ""
            Dim taskName As String = TextBox1.Text
            Dim taskImp As String = ComboBox1.Text
            Dim taskCate As String = ComboBox2.Text
            Dim taskDLine As String = DateTimePicker1.Value.Date.ToString
            Dim taskMemo As String = TextBox3.Text
            frmMain.addToDo(taskName, taskImp, taskCate, taskDLine, taskMemo)
        End If


        If ComboBox2.SelectedIndex = -1 And ComboBox2.Text IsNot "" Then
            Dim Dup As Integer = 0

            For i As Integer = 0 To ComboBox2.Items.Count - 1
                If ComboBox2.Items(i) = ComboBox2.Text Then
                    Dup = 1
                    Exit For
                End If
            Next

            If Dup = 0 Then
                ComboBox2.Items.Add(ComboBox2.Text)
                Using writer = New StreamWriter(catePath, True)
                    writer.WriteLine(ComboBox2.Text)
                End Using
            End If

        End If

    End Sub

    Private Sub frmAddToDo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CategoryInit(Me.ComboBox2)
    End Sub
End Class