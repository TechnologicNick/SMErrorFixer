Imports System.Data.SQLite

Public Class ExplodedGlitchweld
    Public connection As SQLiteConnection
    Public dbHelper As DatabaseHelper
    Public ShapeList As Dictionary(Of Integer, Shape) = New Dictionary(Of Integer, Shape)
    Public JointList As Dictionary(Of Integer, Joint) = New Dictionary(Of Integer, Joint)

    Public BodySize As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

    Public Sub Fix()
        Dim dialog As OpenFileDialog = DatabaseHelper.SaveFileDialog()
        If dialog.ShowDialog() = DialogResult.OK Then
            OpenDB(dialog.FileName)
        End If
    End Sub

    Public Sub OpenDB(file As String)
        DisposeDB()

        connection = DatabaseHelper.CreateConnection(file)
        dbHelper = New DatabaseHelper(connection)

        dbHelper.VersionWarning(24)

        ShapeList = dbHelper.GetShapes()
        JointList = dbHelper.GetJoints()

        ' Calculate the size of the bodies
        For Each s As Shape In ShapeList.Values
            If BodySize.ContainsKey(s.BodyId) Then
                BodySize(s.BodyId) = BodySize(s.BodyId) + 1
            Else
                BodySize(s.BodyId) = 1
            End If
        Next

    End Sub

    Public Sub DisposeDB()
        If connection IsNot Nothing Then
            Debug.WriteLine("Disposing of old database connection...")

            'connection.Dispose()

            connection.Close()
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
End Class
