Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class SMMod

    Public Description As UGCDescription
    Public ShapesetsFolder As String
    Public UuidList As List(Of Guid)

    Private Valid As Boolean = True

    Public Sub New(path As String)
        If Not File.Exists(System.IO.Path.Combine(path, "description.json")) Then
            Debug.WriteLine("Mod at location """ + path + """ doesn't have a description file! Ignoring mod...")
            Return
        End If

        Try
            Me.Description = UGCDescription.FromWorkshopItem(path)
        Catch ex As Exception
            Me.Valid = False
        End Try
        Me.ShapesetsFolder = path & "\Objects\Database\ShapeSets"

    End Sub

    Public Sub New(description As UGCDescription, shapesetsFolder As String)
        Me.Description = description
        Me.ShapesetsFolder = shapesetsFolder
    End Sub

    Public Function IsValid() As Boolean
        If Not Me.Valid Then
            Return False
        ElseIf Me.Description Is Nothing Then 'see if this works
            Return False
        ElseIf Not Me.Description.type.Equals("Blocks and Parts") Then
            'Debug.WriteLine("Wrong type")
            Return False
        End If

        Return True
    End Function

    Public Sub ParseShapesets()
        Me.UuidList = New List(Of Guid)


        For Each shapesetFile In Directory.GetFiles(Me.ShapesetsFolder)
            Try
                Dim json As JObject = JObject.Parse(File.ReadAllText(shapesetFile))

                'Debug.WriteLine(json.ToString())
                Dim partList As JToken = json.Item("partList")
                If partList IsNot Nothing Then
                    For Each part As JObject In partList
                        Me.UuidList.Add(Guid.Parse(part.Item("uuid")))
                    Next
                End If

                Dim blockList As JToken = json.Item("blockList")
                If blockList IsNot Nothing Then
                    For Each block As JObject In blockList
                        Me.UuidList.Add(Guid.Parse(block.Item("uuid")))
                    Next
                End If
            Catch ex As JsonReaderException
                Debug.WriteLine("Failed to parse json {0}", ex.Path)
            End Try
        Next



        'For Each uuid As Guid In Me.UuidList
        '    Debug.WriteLine(uuid)
        'Next

        Debug.WriteLine("Added " & Me.UuidList.Count & " uuids for the mod """ & Me.Description.name & """")
    End Sub

End Class
