Imports System.IO
Imports System.Text
Imports System.Data.SQLite

Public Class FormUnableToFindMod
    Public connection As SQLiteConnection

    Private Sub FormUnableToFindMod_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FormUnableToFindMod_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        DisposeDB()
    End Sub

    Private Sub ButtonSelectSaveFile_Click(sender As Object, e As EventArgs) Handles ButtonSelectSaveFile.Click
        SelectFile()
    End Sub

    Public Sub SelectFile()
        'ModManager.LoadMods()

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

        Dim saveGameVersion As Integer = DatabaseHelper.GetSaveGameVersion(connection)
        If Not saveGameVersion = 24 Then
            MessageBox.Show(SMErrorFixer, "This fix is made for save game version 24." & vbNewLine & "This save file is version " & saveGameVersion.ToString() & "." & vbNewLine & "The fix might not work.", "Error while trying to find .db file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        GetShapes()

        ModManager.LoadMods()

        Dim usedMods As List(Of SMMod) = New List(Of SMMod)
        Dim unidentifiedUuids As List(Of Guid) = New List(Of Guid)

        For Each s As Shape In Shape.ShapeList.Values
            Dim m As SMMod = ModManager.GetModFromUuid(s.UUID)

            If m IsNot Nothing Then
                If Not usedMods.Contains(m) Then
                    usedMods.Add(m)
                End If
            Else
                If Not unidentifiedUuids.Contains(s.UUID) Then
                    unidentifiedUuids.Add(s.UUID)
                End If
            End If
        Next

        Debug.WriteLine("Used Mods: ({0})", usedMods.Count)
        For Each m As SMMod In usedMods
            Debug.WriteLine("({0}) {1}", m.Description.fileId, m.Description.name)
        Next

        Debug.WriteLine("Unidentified Uuids: ({0})", unidentifiedUuids.Count)
        For Each g As Guid In unidentifiedUuids
            Debug.WriteLine(g)
        Next

    End Sub

    Public Sub DisposeDB()
        If connection IsNot Nothing Then
            Debug.WriteLine("Disposing of old database connection...")
            Shape.ResetShapeList()

            'connection.Dispose()

            connection.Close()
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Sub GetShapes()
        Debug.WriteLine("Getting shapes...")

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
        End While

        'Dim uuidList As Dictionary(Of Guid, String) = New Dictionary(Of Guid, String)
        '
        'For Each s As Shape In Shape.ShapeList.Values
        '    s.DebugData()
        '
        '    uuidList.Item(s.UUID) = "unknown"
        'Next
        '
        'Debug.WriteLine("Used UUIDs: ({0})", uuidList.Count)
        '
        'For Each g As Guid In uuidList.Keys
        '    Debug.WriteLine(g)
        'Next

    End Sub
End Class