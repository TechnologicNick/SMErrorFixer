Imports System.IO
Imports System.Text
Imports System.Data.SQLite
Imports System.Threading

Public Class FormPlayerOutOfWorld
    Public connection As SQLiteConnection
    Public PlayerList As Dictionary(Of Integer, Player) = New Dictionary(Of Integer, Player)

    Private Sub FormPlayerOutOfWorld_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FormPlayerOutOfWorld_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        DisposeDB()
    End Sub

    Private Sub ButtonSelectSaveFile_Click(sender As Object, e As EventArgs) Handles ButtonSelectSaveFile.Click
        SelectFile()
    End Sub

    Public Sub SelectFile()
        Dim result As DialogResult = OpenFileDialogDB.ShowDialog()
        Debug.WriteLine(result)

        If result = DialogResult.OK Then
            OpenDB(OpenFileDialogDB.FileName)
        End If
    End Sub

    Public Sub OpenDB(file As String)
        DisposeDB()

        Debug.WriteLine("Opening database: " & file)

        connection = New SQLiteConnection("Data Source = " + file + "; PRAGMA journal_mode = DELETE;") 'journal_mode already defaults to DELETE, but just to make sure
        connection.Open()

        DatabaseHelper.GetSaveGameVersion(connection)

        GetPlayers()

        ShowData()
    End Sub

    Public Sub DisposeDB()
        If connection IsNot Nothing Then
            Debug.WriteLine("Disposing of old database connection...")
            ResetPlayerList()

            'connection.Dispose()

            connection.Close()
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Sub GetPlayers()
        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT id, data FROM GenericData WHERE channel = 4 ORDER BY id ASC;"

        Dim datareader As SQLiteDataReader = command.ExecuteReader()

        While datareader.Read()
            Dim id As Integer = datareader.GetInt32(0)
            Dim buffer(1024) As Byte

            Dim length As Long = datareader.GetBytes(1, 0, buffer, 0, 1024)

            'Dim strTemp As New StringBuilder(buffer.Length * 2)
            'For Each b As Byte In buffer
            '    strTemp.Append(Conversion.Hex(b))
            'Next
            'Debug.WriteLine(strTemp.ToString())

            Dim blob(length) As Byte

            For i As Integer = 0 To length
                blob(i) = buffer(i)
            Next

            'Debug.WriteLine("Reversing Blob")
            Array.Reverse(blob)
            Dim p As Player = New Player(id, blob)
            PlayerList.Add(id, p)
        End While

        For Each p As Player In PlayerList.Values
            p.DebugData()
        Next

    End Sub

    Public Sub ShowData()
        DataGridViewPlayers.Rows.Clear()

        For Each p As Player In PlayerList.Values
            Dim row As DataGridViewRow = New DataGridViewRow()

            row.CreateCells(DataGridViewPlayers)

            ' It's ugly but I don't know how to make it look better
            row.Cells(DataGridViewPlayers.Columns("ColumnPlayerId").Index).Value = p.Id
            row.Cells(DataGridViewPlayers.Columns("ColumnSteamId").Index).Value = p.SteamId
            row.Cells(DataGridViewPlayers.Columns("ColumnPlayerName").Index).Value = ""
            row.Cells(DataGridViewPlayers.Columns("ColumnPosX").Index).Value = p.X
            row.Cells(DataGridViewPlayers.Columns("ColumnPosY").Index).Value = p.Y
            row.Cells(DataGridViewPlayers.Columns("ColumnPosZ").Index).Value = p.Z
            row.Cells(DataGridViewPlayers.Columns("ColumnDelete").Index).Value = "Delete" ' This should be set automatically by the UseColumnTextForButtonValue but that doesn't seem to work for some reason

            DataGridViewPlayers.Rows.Add(row)

            Dim t As New Thread(
                Sub()
                    row.Cells(DataGridViewPlayers.Columns("ColumnPlayerName").Index).Value = p.RequestPlayerName()
                End Sub
            )
            t.Start()

        Next
    End Sub

    Public Function GetPlayer(PlayerId As Integer)
        Return PlayerList.Item(PlayerId)
    End Function

    Public Sub ResetPlayerList()
        PlayerList.Clear()
    End Sub

    Public Function GetRowFromPlayer(p As Player) As DataGridViewRow
        For Each row As DataGridViewRow In DataGridViewPlayers.Rows
            If row.Cells(DataGridViewPlayers.Columns("ColumnPlayerId").Index).Value = p.Id Then
                Return row
            End If
        Next

        Return Nothing
    End Function

    Public Function GetPlayerFromRow(row As DataGridViewRow) As Player
        Dim id As Integer = row.Cells(DataGridViewPlayers.Columns("ColumnPlayerId").Index).Value

        Return GetPlayer(id)
    End Function

    Private Sub DataGridViewPlayers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewPlayers.CellContentClick
        If e.RowIndex < 0 OrElse Not e.ColumnIndex = DataGridViewPlayers.Columns("ColumnDelete").Index Then
            Return
        End If

        Dim p As Player = GetPlayerFromRow(DataGridViewPlayers.Rows(e.RowIndex))
        DeletePlayerFromDB(p)

        PlayerList.Remove(p.Id)
        ShowData()
    End Sub

    Private Sub DataGridViewPlayers_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridViewPlayers.UserDeletingRow
        For Each row As DataGridViewRow In DataGridViewPlayers.SelectedRows
            Dim p As Player = GetPlayerFromRow(row)
            DeletePlayerFromDB(p)
            PlayerList.Remove(p.Id)
        Next

        e.Cancel = True

        ShowData()
    End Sub

    Public Sub DeletePlayerFromDB(p As Player)
        Debug.WriteLine(String.Format("Deleting player data of {0} (id = {1})", p.Name, p.Id))

        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "DELETE FROM GenericData WHERE channel = 4 AND id = @id;"
        command.Parameters.AddWithValue("@id", p.Id)
        command.ExecuteNonQuery()
    End Sub
End Class