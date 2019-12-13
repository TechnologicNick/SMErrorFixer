Imports System.IO

Public Class ModManager

    Public Shared ModList As List(Of SMMod)
    Public Shared UuidMap As Dictionary(Of Guid, SMMod)

    Public Shared Function GetPaths() As List(Of String)
        Dim paths As List(Of String) = New List(Of String)

        Dim userPath As String = Environment.ExpandEnvironmentVariables("%APPDATA%\Axolot Games\Scrap Mechanic\User\")
        If Directory.Exists(userPath) Then
            My.Settings.UserFolder = Directory.GetDirectories(userPath).OrderByDescending(Function(d) New DirectoryInfo(d).LastWriteTime).First()

            If Directory.Exists(My.Settings.UserFolder & "\Mods") Then
                paths.Add(My.Settings.UserFolder & "\Mods")
            Else
                Debug.WriteLine("User doesn't have a mods folder!")
            End If

        Else
            MessageBox.Show(SMErrorFixer, "Unable to find user folder at" & vbNewLine & vbNewLine & userPath, "Folder not found", MessageBoxButtons.OK)
        End If

        paths.Add(Environment.ExpandEnvironmentVariables(My.Settings.WorkshopFolder))

        Return paths
    End Function

    Public Shared Sub LoadMods()
        LoadMods(False)
    End Sub

    Public Shared Sub LoadMods(forceReload As Boolean)
        If Not forceReload Then
            If UuidMap IsNot Nothing Then
                Return
            End If
        End If

        ModList = New List(Of SMMod)
        UuidMap = New Dictionary(Of Guid, SMMod)

        Dim paths As List(Of String) = GetPaths()

        For Each path As String In paths
            Debug.WriteLine(path)

            For Each modFolder As String In Directory.GetDirectories(path)
                Dim smMod As SMMod = New SMMod(modFolder)
                If smMod.IsValid() Then
                    smMod.ParseShapesets()
                    ModList.Add(smMod)
                Else
                    'Debug.WriteLine("Mod is invalid!")
                End If
            Next

        Next

        'TODO
        Dim smShapesetsFolder As String = Environment.ExpandEnvironmentVariables(My.Settings.ScrapMechanicFolder) & "\Data\Objects\Database\ShapeSets"
        If Directory.Exists(smShapesetsFolder) Then

            Dim vanillaDescription As UGCDescription = New UGCDescription()
            vanillaDescription.name = "Vanilla"
            vanillaDescription.description = "All the vanilla blocks and parts"
            vanillaDescription.type = "Blocks and Parts"

            Dim vanillaMod As SMMod = New SMMod(vanillaDescription, smShapesetsFolder)
            If vanillaMod.IsValid() Then
                vanillaMod.ParseShapesets()
                ModList.Add(vanillaMod)
            Else
                Debug.WriteLine("Vanilla mod is invalid!")
            End If

        Else
            MessageBox.Show(SMErrorFixer, "Unable to find Scrap Mechanic shapesets folder at" & vbNewLine & vbNewLine & smShapesetsFolder, "Folder not found", MessageBoxButtons.OK)
        End If

        For Each smMod As SMMod In ModList
            For Each uuid As Guid In smMod.UuidList
                UuidMap.Item(uuid) = smMod
            Next
        Next

        Debug.WriteLine("Found a total of " & UuidMap.Count & " uuids")

    End Sub

    Public Shared Function GetModFromUuid(uuid As Guid) As SMMod
        If Not UuidMap.ContainsKey(uuid) Then
            Return Nothing
        End If

        Return UuidMap.Item(uuid)
    End Function

End Class
