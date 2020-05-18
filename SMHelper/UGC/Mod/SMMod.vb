Imports System.IO
Imports System.Drawing
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace UGC.Mod
    Public Class SMMod

        Public Description As UGCDescription
        Public ModDirectory As String
        Public ShapesetsFolder As String
        Public UuidList As List(Of Guid)

        Private _Preview As Image = Nothing
        Public ReadOnly Property Preview As Image
            Get
                If _Preview Is Nothing Then
                    For Each extension As String In {"png", "jpg"}
                        Dim f As String = Path.Combine(ModDirectory, "preview." & extension)
                        If File.Exists(f) Then
                            Try
                                _Preview = Image.FromFile(f)
                                Return _Preview
                            Catch ex As Exception
                                ' Failed loading the image, searching for other preview image
                            End Try
                        End If
                    Next

                    Debug.WriteLine($"Failed to load preview image for mod {Description.name}")
                End If

                Return _Preview
            End Get
        End Property


        Private Valid As Boolean = True

        Public Sub New(modDir As String)
            ModDirectory = modDir

            If Not File.Exists(Path.Combine(ModDirectory, "description.json")) Then
                Debug.WriteLine($"Mod at location ""{ModDirectory}"" doesn't have a description file! Ignoring mod...")
                Return
            End If

            Try
                Me.Description = UGCDescription.FromWorkshopItem(ModDirectory)
            Catch ex As Exception
                Me.Valid = False
            End Try
            Me.ShapesetsFolder = Path.Combine(ModDirectory, "Objects\Database\ShapeSets")

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
End Namespace
