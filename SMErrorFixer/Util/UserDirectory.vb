Imports System.IO

Public Class UserDirectory
    Private Shared UserDirectory As String = Nothing

    Private Shared BaseUserPath As String = Environment.ExpandEnvironmentVariables("%APPDATA%\Axolot Games\Scrap Mechanic\User\")

    Public Shared Function GetUserDirectory() As String
        If IsNothing(UserDirectory) Then
            UserDirectory = Directory.GetDirectories(BaseUserPath).OrderByDescending(Function(d) New DirectoryInfo(d).LastWriteTime).First()
        End If

        Return UserDirectory
    End Function

    Public Shared Function GetBlueprints() As String
        Return Path.Combine(GetUserDirectory(), "Blueprints")
    End Function

    Public Shared Function GetChallenges() As String
        Return Path.Combine(GetUserDirectory(), "Challenges")
    End Function

    Public Shared Function GetMods() As String
        Return Path.Combine(GetUserDirectory(), "Mods")
    End Function

    Public Shared Function GetProgress() As String
        Return Path.Combine(GetUserDirectory(), "Progress")
    End Function

    Public Shared Function GetSave() As String
        Return Path.Combine(GetUserDirectory(), "Save")
    End Function

    Public Shared Function GetTiles() As String
        Return Path.Combine(GetUserDirectory(), "Tiles")
    End Function

    Public Shared Function GetWorlds() As String
        Return Path.Combine(GetUserDirectory(), "Worlds")
    End Function

End Class
