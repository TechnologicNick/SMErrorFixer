Imports System.Data.SQLite

Public Class DatabaseHelper

    Public Shared Function GetSaveGameVersion(connection As SQLiteConnection) As Integer
        Dim command As SQLiteCommand = connection.CreateCommand()
        command.CommandText = "SELECT savegameversion FROM Game" 'This can be any command. Don't know if it even has to be valid

        'command.ExecuteNonQuery()
        Dim datareader As SQLiteDataReader = command.ExecuteReader()

        While datareader.Read()
            Return datareader.GetInt32(0)
        End While

        Return -1
    End Function

End Class
