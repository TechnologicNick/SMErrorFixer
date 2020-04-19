Imports System.IO

Public Class MissingBlueprintFile
    Public Shared Sub Fix()

        Dim count As Integer = 0

        For Each dir As String In {
            Settings.GetInstance().WorkshopDirectory,
            UserDirectory.GetBlueprints()
        }
            dir = Environment.ExpandEnvironmentVariables(dir)

            If Directory.Exists(dir) Then

                ' Workshop item folder
                For Each itemFolder As String In Directory.GetDirectories(dir)

                    If Not File.Exists(Path.Combine(itemFolder, "description.json")) Then
                        Continue For
                    End If

                    ' In case someone messed up the json and it can't be deserialised
                    Try
                        Dim description As UGCDescription = UGCDescription.FromWorkshopItem(itemFolder)

                        If description.type = "Blueprint" Then
                            If Not File.Exists(Path.Combine(itemFolder, "blueprint.json")) Then
                                Debug.WriteLine(String.Format("Blueprint {0} doesn't have a blueprint.json file", description.name))

                                Directory.Delete(itemFolder, True)

                                count += 1
                            End If
                            'Debug.WriteLine(itemFolder)
                        End If
                    Catch ex As Exception

                    End Try


                Next
            Else
                Debug.WriteLine(String.Format("Blueprints folder {0} doesn't exist!", dir))
            End If

        Next

        ' It's ugly but at least the grammar is correct
        Dim message As String
        If count = 1 Then
            message = "1 blueprint was found to not have a blueprint.json file and has been deleted"
        Else
            message = String.Format("{0} blueprints were found to not have a blueprint.json file and have been deleted", count)
        End If
        MessageBox.Show(message, "Result")

    End Sub
End Class
