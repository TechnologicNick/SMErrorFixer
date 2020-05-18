Imports System.IO
Imports System.Windows.Forms
Imports SMHelper.Util

Namespace UGC.Mod
    Public Class SMModManager

        Public ModList As List(Of SMMod) = New List(Of SMMod)
        'Public UuidMap As Dictionary(Of Guid, SMMod) = New Dictionary(Of Guid, SMMod)

        'Public Shared Function GetPaths() As List(Of String)
        '    Dim paths As List(Of String) = New List(Of String)
        '
        '    Dim userPath As String = Environment.ExpandEnvironmentVariables("%APPDATA%\Axolot Games\Scrap Mechanic\User\")
        '    If Directory.Exists(userPath) Then
        '        If Directory.Exists(UserDirectory.GetMods()) Then
        '            paths.Add(UserDirectory.GetMods())
        '        Else
        '            Debug.WriteLine("User doesn't have a mods folder!")
        '        End If
        '
        '    Else
        '        MessageBox.Show("Unable to find user folder at" & vbNewLine & vbNewLine & userPath, "Folder not found", MessageBoxButtons.OK)
        '    End If
        '
        '    paths.Add(Environment.ExpandEnvironmentVariables(Settings.GetInstance().WorkshopDirectory))
        '
        '    Return paths
        'End Function

        Public Sub LoadMods(paths As List(Of String))
            Dim modCount As Integer = ModList.Count

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
            'Dim smShapesetsFolder As String = Path.Combine(Environment.ExpandEnvironmentVariables(Settings.GetInstance().InstallDirectory), "Data\Objects\Database\ShapeSets")
            'If Directory.Exists(smShapesetsFolder) Then
            '
            '    Dim vanillaDescription As UGCDescription = New UGCDescription()
            '    vanillaDescription.name = "Vanilla"
            '    vanillaDescription.description = "All the vanilla blocks and parts"
            '    vanillaDescription.type = "Blocks and Parts"
            '
            '    Dim vanillaMod As SMMod = New SMMod(vanillaDescription, smShapesetsFolder)
            '    If vanillaMod.IsValid() Then
            '        vanillaMod.ParseShapesets()
            '        ModList.Add(vanillaMod)
            '    Else
            '        Debug.WriteLine("Vanilla mod is invalid!")
            '    End If
            '
            'Else
            '    MessageBox.Show("Unable to find Scrap Mechanic shapesets folder at" & vbNewLine & vbNewLine & smShapesetsFolder, "Folder not found", MessageBoxButtons.OK)
            'End If

            'For Each smMod As SMMod In ModList
            '    For Each uuid As Guid In smMod.UuidList
            '        UuidMap.Item(uuid) = smMod
            '    Next
            'Next

            Debug.WriteLine($"Found a total of {ModList.Count - modCount} mods")

        End Sub

        Public Sub AddVanillaMods(installDirectory As String)
            Dim shapesetsDirectories = New List(Of (description As String, dir As String))
            shapesetsDirectories.Add(("Creative", Path.Combine(Environment.ExpandEnvironmentVariables(installDirectory), "Data\Objects\Database\ShapeSets")))
            shapesetsDirectories.Add(("Challenge", Path.Combine(Environment.ExpandEnvironmentVariables(installDirectory), "ChallengeData\Objects\Database\ShapeSets")))
            shapesetsDirectories.Add(("Survival", Path.Combine(Environment.ExpandEnvironmentVariables(installDirectory), "Survival\Objects\Database\ShapeSets")))

            For Each item In shapesetsDirectories

                If Directory.Exists(item.dir) Then

                    Dim vanillaDescription As UGCDescription = New UGCDescription()
                    vanillaDescription.name = $"Vanilla ({item.description})"
                    vanillaDescription.description = $"All the vanilla blocks and parts from {item.description} mode"
                    vanillaDescription.type = "Blocks and Parts"

                    Dim vanillaMod As SMMod = New SMMod(vanillaDescription, item.dir)
                    If vanillaMod.IsValid() Then
                        vanillaMod.ParseShapesets()
                        ModList.Add(vanillaMod)
                    Else
                        Debug.WriteLine($"{vanillaDescription.name} mod is invalid!")
                    End If

                Else
                    MessageBox.Show("Unable to find shapesets folder at" & vbNewLine & vbNewLine & item.dir, "Folder not found", MessageBoxButtons.OK)
                End If
            Next
        End Sub

        'Public Function GetModFromUuid(uuid As Guid) As SMMod
        '    If Not UuidMap.ContainsKey(uuid) Then
        '        Return Nothing
        '    End If
        '
        '    Return UuidMap.Item(uuid)
        'End Function

    End Class
End Namespace
