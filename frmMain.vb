Imports System.IO

Public Class frmMain
    '-- 検索ボックスが選択されたら説明文を削除
    Private Sub txtAddCate_TextChanged(sender As Object, e As EventArgs) Handles txtAddCate.MouseClick
        If frgAddCte = 0 Then
            txtAddCate.Text = ""
            txtAddCate.ForeColor = Color.Black
            frgAddCte = 1
        End If
    End Sub

    '--　検索ボックスが空で選択されていないとき説明文を表示
    Private Sub txtAddCate_TextChanged2(sender As Object, e As EventArgs) Handles txtAddCate.Leave
        If frgAddCte = 1 And txtAddCate.Text = "" Then
            txtAddCate.Text = "カテゴリを検索"
            txtAddCate.ForeColor = Color.Silver
            frgAddCte = 0
        End If
    End Sub

    '-- ToDo追加ダイアログを表示
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim frmTemp As New frmAddToDo()
        frmTemp.ShowDialog()
    End Sub

    '-- ToDoリストにデータを追加し、ToDo表示テーブルを更新
    Public Sub addToDo(taskName As String, taskImp As String, taskCate As String, taskDLine As String, taskMemo As String)

        Dim fileName As String = "memo" & DateTime.Now.ToBinary.ToString & ".txt"　'メモファイル名
        Dim txtPath As String = memoPath & "\" & fileName 'メモファイルの保存先

        'メモ保存用のテキストファイル作成
        Using fs As FileStream = File.Create(txtPath)
            fs.Close()
        End Using

        'ToDoListへ追加
        ToDoList(ToDoCount).taskName = taskName
        ToDoList(ToDoCount).taskImp = taskImp
        ToDoList(ToDoCount).taskCategory = taskCate
        ToDoList(ToDoCount).taskDeadLine = taskDLine
        ToDoList(ToDoCount).taskMemo = fileName
        ToDoList(ToDoCount).taskStatus = "進行中"
        ToDoList(ToDoCount).Index = ToDoCount

        'テキストファイルへメモを書き込み
        Dim inputString As String
        inputString = todoCSVFormat(ToDoCount)
        Using writer = New StreamWriter(listPath, True)
            writer.WriteLine(inputString)
        End Using

        Using writer = New StreamWriter(txtPath, True)
            writer.WriteLine(taskMemo)
        End Using

        'ToDoCountを加算
        ToDoCount += 1

        'テーブル更新
        RefreshTable()

    End Sub

    '-- ToDoを表示するテーブルを現在のToDoListをもとに再読み込み
    Public Sub RefreshTable()

        'テーブルを初期化
        DataGridView1.Rows.Clear()

        Dim dateTime As DateTime '今日の日付データを格納
        Dim remaineder As String '〆切りまでの残り日数

        '再読み込み
        Dim i As Integer = 0

        'ToDoListからテーブルの各列へ値を挿入
        For i = 0 To ToDoCount - 1

            DataGridView1.Rows.Add() ' --列を追加

            ' --残り日数を計算
            dateTime = DateTime.Parse(ToDoList(i).taskDeadLine)
            remaineder = DateDiff("d", DateTime.Today, dateTime).ToString

            '--　各セルに値を代入
            DataGridView1(0, i).Value = ToDoList(i).Index
            DataGridView1(1, i).Value = False
            DataGridView1(2, i).Value = ToDoList(i).taskCategory
            DataGridView1(3, i).Value = ToDoList(i).taskName
            DataGridView1(4, i).Value = ToDoList(i).taskStatus
            DataGridView1(5, i).Value = ToDoList(i).taskImp
            DataGridView1(6, i).Value = dateTime.ToLongDateString
            If Integer.Parse(remaineder) < 0 Then
                DataGridView1(7, i).Value = "超過"
            Else
                DataGridView1(7, i).Value = "あと　" & remaineder & "日"
            End If
            DataGridView1(8, i).Value = GetMemo(ToDoList(i).taskMemo)
        Next i
    End Sub

    '-- 削除ボタンが押されたら選択中されたタスクを削除
    '-- 複数のタスクを1度に削除し、テーブルの更新は1回のみ
    '   とするために、IndexAs(i)を用いて、AddToDoList(j)の
    '   削除に伴うIndexのズレを調整
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim i As Integer = 0

        'IndexAs(i)を変更前のToDoList(i).Indexで初期化
        'IndexAs(テーブルから参照されるIndex) -> ToDoList変更後のIndex
        For i = 0 To ToDoCount - 1
            IndexAs(i) = ToDoList(i).Index
        Next i

        'checkBoxにチェックがついているものを検索し、それらを削除
        For i = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1(1, i).Value Then
                DeleteToDo(DataGridView1(0, i).Value) 'ToDoを1つ削除
            End If
        Next i


        ' --todoList.CSV書き込み処理
        Dim inputString As String
        If ToDoCount = 0 Then
            InputString = ""
        Else
            InputString = todoCSVFormat(0)
        End If

        '一行目書き込み時にテキストをリセット
        Using writer = New StreamWriter(listPath, False)
            writer.WriteLine(InputString)
        End Using

        '2行目以降は随時追加
        For i = 1 To ToDoCount - 1
            InputString = todoCSVFormat(i)
            Using writer = New StreamWriter(listPath, True)
                writer.WriteLine(InputString)
            End Using
        Next i

        'テーブル更新
        RefreshTable()

    End Sub

    'ToDoListからToDoを1つ削除し、Indexを修正
    Private Sub DeleteToDo(Index As Integer)
        Dim i As Integer = 0

        Dim CurrentIndex As Integer = IndexAs(Index)
        'Indexに紐づいたメモのテキストファイルを削除
        File.Delete(memoPath & "\" & ToDoList(CurrentIndex).taskMemo)

        For i = CurrentIndex To ToDoCount - 2
            ' --ToDoListを詰める
            ToDoList(i) = ToDoList(i + 1)
            ' --Indexを詰める
            ToDoList(i).Index -= 1
            ' --IndexAsの参照先を詰める
            IndexAs(ToDoList(i + 1).Index) -= 1
        Next i

        ' --ToDoCount減算
        ToDoCount -= 1

    End Sub

    'テーブルの要素をダブルクリックでToDo編集ダイアログが起動
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If 0 <= e.RowIndex Then
            Dim editToDo As New frmEditToDo()
            'クリックされた行のインデックスを渡す
            editToDo.Init(e.RowIndex)
            editToDo.ShowDialog()
        End If
    End Sub

    '初期読み込み
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitUserDataDirectory() 'userdataディレクトリ作成
        InitToDoList() 'CSVファイルからToDoListを作成
        InitMemoDirectory() 'Memoディレクトリのチェック
        RefreshTable() ' テーブル表示
    End Sub

End Class
