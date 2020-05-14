Public Class Joint
    Implements IUuidItem

    'Public Shared JointList As Dictionary(Of Integer, Joint) = New Dictionary(Of Integer, Joint)

    Public Id As Integer
    Public ChildShapeIdA As Integer
    Public ChildShapeIdB As Integer
    Public Blob As Byte()
    Public ReverseBlob As Byte()

    Private _UUID As Guid = New Guid("00000000-0000-0000-0000-000000000000")

    Public Property UUID As Guid Implements IUuidItem.UUID
        Get
            Return _UUID
        End Get
        Set(value As Guid)
            _UUID = value
        End Set
    End Property

    Public Sub New(JointId As Integer, ChildShapeIdA As Integer, ChildShapeIdB As Integer, Blob As Byte())
        Me.Id = JointId
        Me.ChildShapeIdA = ChildShapeIdA
        Me.ChildShapeIdB = ChildShapeIdB
        Me.Blob = Blob
        Me.ReverseBlob = Me.Blob.Clone()

        Array.Reverse(Me.ReverseBlob)

        'Debug.WriteLine("--- New Shape ({0}) ---", Me.Id)
        'Debug.WriteLine("Blob length = {0}", Me.Blob.Length)




        ParseBlob()

        'JointList.Add(Me.Id, Me)
    End Sub

    Public Sub ParseBlob()
        Dim uuidBytes(15) As Byte
        Array.Copy(Me.Blob, Me.Blob.Length - 31, uuidBytes, 0, 16)
        Array.Reverse(uuidBytes, 0, 4) ' .NET Guid != (RFC4122) UUID
        Array.Reverse(uuidBytes, 4, 2)
        Array.Reverse(uuidBytes, 6, 2)
        'Debug.WriteLine(BitConverter.ToString(uuidBytes))
        Me.UUID = New Guid(uuidBytes)

        'Debug.WriteLine(BitConverter.ToString(Me.Blob))
    End Sub

    Public Sub DebugData()
        Debug.WriteLine("Shape ({0}): Id = {1}, ChildShapeIdA = {2}, ChildShapeIdA = {3}, UUID = {4}", Me.Id, Me.Id, Me.ChildShapeIdA, Me.ChildShapeIdB, Me.UUID)
    End Sub

End Class
