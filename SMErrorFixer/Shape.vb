Imports System.Data.SQLite

Public Class Shape

    Public Shared ShapeList As Dictionary(Of Integer, Shape) = New Dictionary(Of Integer, Shape)

    Public Id As Integer
    Public BodyId As Integer
    Public Blob As Byte()
    Public ReverseBlob As Byte()

    Public UUID As Guid = New Guid("00000000-0000-0000-0000-000000000000")

    Public Sub New(ShapeId As Integer, BodyId As Integer, Blob As Byte())
        Me.Id = ShapeId
        Me.BodyId = BodyId
        Me.Blob = Blob
        Me.ReverseBlob = Me.Blob.Clone()

        Array.Reverse(Me.ReverseBlob)

        'Debug.WriteLine("--- New Shape ({0}) ---", Me.Id)
        'Debug.WriteLine("Blob length = {0}", Me.Blob.Length)




        ParseBlob()

        ShapeList.Add(Me.Id, Me)
    End Sub

    Public Sub ParseBlob()
        Dim uuidBytes(15) As Byte
        Array.Copy(Me.Blob, Me.Blob.Length - 27, uuidBytes, 0, 16)
        Array.Reverse(uuidBytes, 0, 4) ' .NET Guid != (RFC4122) UUID
        Array.Reverse(uuidBytes, 4, 2)
        Array.Reverse(uuidBytes, 6, 2)
        'Debug.WriteLine(BitConverter.ToString(uuidBytes))
        Me.UUID = New Guid(uuidBytes)

        'Debug.WriteLine(BitConverter.ToString(Me.Blob))
    End Sub

    Public Sub DebugData()
        Debug.WriteLine("Shape ({0}): Id = {1}, BodyId = {2}, UUID = {3}", Me.Id, Me.Id, Me.BodyId, Me.UUID)
    End Sub




    Public Shared Function GetShape(PlayerId As Integer)
        Return ShapeList.Item(PlayerId)
    End Function

    Public Shared Sub ResetShapeList()
        ShapeList.Clear()
    End Sub

End Class
