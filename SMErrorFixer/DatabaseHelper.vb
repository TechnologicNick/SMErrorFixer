Imports System.IO
Imports System.Data.SQLite

Public Class DatabaseHelper

    Public connection As SQLiteConnection

    Public Sub New(connection As SQLiteConnection)
        Me.connection = connection
    End Sub

    Public Shared Function CreateConnection(path As String) As SQLiteConnection
        Debug.WriteLine("Opening database: " & path)

        Dim connection As SQLiteConnection = New SQLiteConnection(String.Format("Data Source = {0}; PRAGMA journal_mode = DELETE;", path)) 'journal_mode already defaults to DELETE, but just to make sure
        connection.Open()

        Return connection
    End Function

    Public Function GetSaveGameVersion() As Integer
        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT savegameversion FROM Game" 'This can be any command. Don't know if it even has to be valid

        'command.ExecuteNonQuery()
        Dim datareader As SQLiteDataReader = command.ExecuteReader()

        While datareader.Read()
            Return datareader.GetInt32(0)
        End While

        Return -1
    End Function

    Public Sub VersionWarning(version As Integer)
        Dim saveGameVersion As Integer = Me.GetSaveGameVersion()
        If Not saveGameVersion = version Then
            MessageBox.Show(
                SMErrorFixer,
                "This fix is made for save game version " & version & "." & vbNewLine & "This save file is version " & saveGameVersion.ToString() & "." & vbNewLine & "The fix might not work.",
                "Error while trying to find .db file",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
        End If
    End Sub

    Public Shared Function SaveFileDialog() As OpenFileDialog
        Dim dialog As OpenFileDialog = New OpenFileDialog()

        dialog.FileName = Directory.GetFiles(Path.Combine(UserDirectory.GetSave(), "*.db")).OrderByDescending(Function(d) New DirectoryInfo(d).LastWriteTime).First()
        dialog.Filter = "Save files (*.db)|*.db"
        dialog.InitialDirectory = UserDirectory.GetSave()
        dialog.Title = "Select a save file"

        Return dialog
    End Function

    Public Function GetShapes() As Dictionary(Of Integer, Shape)
        Debug.WriteLine("Getting shapes...")

        Dim list = New Dictionary(Of Integer, Shape)

        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT * FROM ChildShape"

        Dim datareader As SQLiteDataReader = command.ExecuteReader()

        While datareader.Read()
            ' id
            Dim id As Integer = datareader.GetInt32(0)

            ' bodyId
            Dim bodyId As Integer = datareader.GetInt32(1)

            ' data
            Dim buffer(1024) As Byte
            Dim length As Long = datareader.GetBytes(2, 0, buffer, 0, 1024)
            Dim blob(length) As Byte

            For i As Integer = 0 To length
                blob(i) = buffer(i)
            Next

            Array.Reverse(blob)


            Dim s As Shape = New Shape(id, bodyId, blob)
            list.Add(s.Id, s)
        End While

        Return list
    End Function

    Public Function GetJoints() As Dictionary(Of Integer, Joint)
        Debug.WriteLine("Getting joints...")

        Dim list = New Dictionary(Of Integer, Joint)

        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT * FROM Joint"

        Dim datareader As SQLiteDataReader = command.ExecuteReader()

        While datareader.Read()
            ' id
            Dim id As Integer = datareader.GetInt32(0)

            ' childShapeIdA
            Dim childShapeIdA As Integer = datareader.GetInt32(1)

            ' childShapeIdB
            Dim childShapeIdB As Integer = datareader.GetInt32(2)

            ' data
            Dim buffer(1024) As Byte
            Dim length As Long = datareader.GetBytes(3, 0, buffer, 0, 1024)
            Dim blob(length) As Byte

            For i As Integer = 0 To length
                blob(i) = buffer(i)
            Next

            Array.Reverse(blob)


            Dim j As Joint = New Joint(id, childShapeIdA, childShapeIdB, blob)
            list.Add(j.Id, j)
        End While

        Return list
    End Function

End Class
