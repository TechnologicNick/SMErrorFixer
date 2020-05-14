Imports System.IO
Imports System.Data.SQLite

Public Class Form10ext3
    Private Sub ButtonSelectSaveFiles_Click(sender As Object, e As EventArgs) Handles ButtonSelectSaveFiles.Click
        SelectFiles()
    End Sub

    Private Sub ButtonFixAll_Click(sender As Object, e As EventArgs) Handles ButtonFixAll.Click

    End Sub



    Public Sub SelectFiles()
        Dim result As DialogResult = OpenFileDialogDBJournal.ShowDialog()
        Debug.WriteLine(result)

        If result = DialogResult.OK Then
            For Each dbjournal As String In OpenFileDialogDBJournal.FileNames

                Dim db As String = dbjournal.Replace(".db-journal", ".db")

                While Not File.Exists(db)
                    Dim errorResult As DialogResult = MessageBox.Show(SMErrorFixer, "Database file does not exist!", "Error while trying to find .db file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                    If errorResult = DialogResult.Cancel Then
                        Continue For
                    End If
                End While

                Debug.WriteLine("Open database " & db)

                Fix(db)
            Next
        End If
    End Sub

    Public Sub FixAll()


    End Sub

    Public Sub Fix(location As String)

        Using connection As SQLiteConnection = DatabaseHelper.CreateConnection(location)

            ' Execute a random command to force SQLite to merge the journal into the database.
            Dim dbHelper As DatabaseHelper = New DatabaseHelper(connection)
            dbHelper.GetSaveGameVersion()

            connection.Close()
            connection.Dispose()
        End Using
        GC.Collect()
        GC.WaitForPendingFinalizers()

    End Sub
End Class