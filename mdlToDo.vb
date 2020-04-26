Imports System.IO

'ToDoの構造体宣言
Public Structure ToDo
    Public Index As Integer
    Public taskCategory As String
    Public taskName As String
    Public taskDeadLine As String
    Public taskMemo As String
    Public taskImp As String
    Public taskStatus As String
End Structure

Module mdlToDo
    Public frgAddCte As Integer = 0 'カテゴリ検索ボックスチェック選択フラグ
    Public ToDoCount As Integer = 0
    Public ToDoList(9999) As ToDo
    Public userPath As String = "UserData" 'userdataのメインディレクトリ
    Public listPath As String = userPath & "\todoList.csv"
    Public catePath As String = userPath & "\categoryList.txt"
    Public memoPath As String = userPath & "\MEMO" 'メモデータ保存先ディレクトリ
    Public IndexAs(9999) As Integer　'indexのコピー用


    Public Sub InitToDoList()
        Dim textFile As FileIO.TextFieldParser ' -- 入力するファイル
        ' --- 入力ファイルを開く
        If System.IO.File.Exists(listPath) Then

            textFile = New FileIO.TextFieldParser(listPath) ' -- デフォルト encoding は UTF8
            ' --- デリミターをタブと定義する
            textFile.TextFieldType = FileIO.FieldType.Delimited
            textFile.SetDelimiters(vbTab)    ' -- カンマ区切りの場合はカッコ内を "," にします

            Dim currentRow As String() ' -- 文字型配列
            ' ---▼▼ 行ループ
            While Not textFile.EndOfData
                currentRow = textFile.ReadFields() ' -- １行を文字型配列に格納

                ToDoList(ToDoCount).Index = currentRow(0)
                ToDoList(ToDoCount).taskStatus = currentRow(1)
                ToDoList(ToDoCount).taskCategory = currentRow(2)
                ToDoList(ToDoCount).taskName = currentRow(3)
                ToDoList(ToDoCount).taskImp = currentRow(4)
                ToDoList(ToDoCount).taskDeadLine = currentRow(5)
                ToDoList(ToDoCount).taskMemo = currentRow(6)

                ToDoCount += 1
            End While
            textFile.Close()
        Else
            Using fs As FileStream = File.Create(listPath)
                ' 呼ばなくても Using を抜けた時点で Dispose メソッドが呼び出される
                fs.Close()
            End Using
        End If
        ' --- 入力ファイルを閉じる
    End Sub

    'カテゴリの初期化
    Public Sub CategoryInit(cmbCate As ComboBox)

        ' --- 入力ファイルを開く
        If System.IO.File.Exists(catePath) Then
            'カテゴリをComboboxに追加
            Using reader As New StreamReader(catePath)
                While reader.Peek() > -1
                    cmbCate.Items.Add(reader.ReadLine())
                End While
            End Using
        Else
            'カテゴリファイルがないときは作成
            Using fs As FileStream = File.Create(catePath)
                fs.Close()
            End Using
        End If
    End Sub

    'Memoディレクトリの生成
    Public Sub InitMemoDirectory()
        If Not System.IO.Directory.Exists(memoPath) Then
            Dim Di As System.IO.DirectoryInfo =
            System.IO.Directory.CreateDirectory(memoPath)
        End If
    End Sub

    'userDataディレクトリ作成
    Public Sub InitUserDataDirectory()
        If Not System.IO.Directory.Exists(userPath) Then
            Dim Di As System.IO.DirectoryInfo =
            System.IO.Directory.CreateDirectory(userPath)
        End If
    End Sub

    'csvファイルに書き込まれるテキストの1行の形式
    Public Function todoCSVFormat(index As Integer) As String
        Dim csvLine As String
        csvLine = ToDoList(index).Index.ToString & vbTab &
                  ToDoList(index).taskStatus & vbTab &
                  ToDoList(index).taskCategory & vbTab &
                  ToDoList(index).taskName & vbTab &
                  ToDoList(index).taskImp & vbTab &
                  ToDoList(index).taskDeadLine & vbTab &
                  ToDoList(index).taskMemo
        Return csvLine
    End Function
    'メモファイルからテキストデータを抽出
    Public Function GetMemo(fileName As String) As String
        Dim memo As String
        Using reader = New StreamReader(memoPath & "\" & fileName)
            memo = reader.ReadToEnd
        End Using
        Return memo
    End Function

End Module

