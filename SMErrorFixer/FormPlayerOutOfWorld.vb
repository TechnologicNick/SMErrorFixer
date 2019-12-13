Imports System.IO
Imports System.Text
Imports System.Data.SQLite

Public Class FormPlayerOutOfWorld
    Public connection As SQLiteConnection

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
    End Sub

    Public Sub DisposeDB()
        If connection IsNot Nothing Then
            Debug.WriteLine("Disposing of old database connection...")
            Player.ResetPlayerList()

            'connection.Dispose()

            connection.Close()
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Sub GetPlayers()
        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT id, data FROM GenericData WHERE channel = 4 ORDER BY id ASC"

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
        End While

        For Each p As Player In Player.PlayerList.Values
            p.DebugData()
        Next

    End Sub

    Public Sub GetPlayerData(playerId As Integer)
        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT savegameversion FROM Game"
    End Sub
End Class