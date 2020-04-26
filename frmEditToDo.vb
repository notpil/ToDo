Imports System.IO

Public Class frmEditToDo
    Private _Index As Integer = 0
    Public Sub Init(Index As Integer)
        _Index = Index
        TextBox1.Text = ToDoList(Index).taskName
        ComboBox3.Text = ToDoList(Index).taskStatus
        ComboBox1.Text = ToDoList(Index).taskImp
        ComboBox2.Text = ToDoList(Index).taskCategory
        DateTimePicker1.Value = DateTime.Parse(ToDoList(Index).taskDeadLine)
        TextBox3.Text = GetMemo(ToDoList(Index).taskMemo)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox1.Text = "" OrElse ComboBox1.SelectedIndex = -1 OrElse ComboBox2.SelectedIndex = -1 Then
            Label6.Text = "必須項目に空欄があります"
        Else
            Label6.Text = ""
            'ToDoList更新
            ToDoList(_Index).taskName = TextBox1.Text
            ToDoList(_Index).taskImp = ComboBox1.Text
            ToDoList(_Index).taskCategory = ComboBox2.Text
            ToDoList(_Index).taskDeadLine = DateTimePicker1.Value.Date.ToString
            ToDoList(_Index).taskStatus = ComboBox3.Text

            'メモファイル更新
            Dim txtPath As String = memoPath & "\" & ToDoList(_Index).taskMemo
            Using writer = New StreamWriter(txtPath, False)
                writer.WriteLine(TextBox3.Text)
            End Using

            'CSVファイル更新
            Dim InputString As String = todoCSVFormat(0)

            Using writer = New StreamWriter(listPath, False)
                writer.WriteLine(InputString)
            End Using

            For i = 1 To ToDoCount - 1
                InputString = todoCSVFormat(i)
                Using writer = New StreamWriter(listPath, True)
                    writer.WriteLine(InputString)
                End Using
            Next i

            If ComboBox2.SelectedIndex = -1 And ComboBox2.Text IsNot "" Then
                ComboBox2.Items.Add(ComboBox2.Text)
                Using writer = New StreamWriter(catePath, True)
                    writer.WriteLine(ComboBox2.Text)
                End Using
            End If

            'テーブル更新
            frmMain.RefreshTable()

        End If
    End Sub

    Private Sub frmEditToDo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CategoryInit(Me.ComboBox2)
    End Sub
End Class