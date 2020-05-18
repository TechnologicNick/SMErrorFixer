Imports System.Data.SQLite
Imports System.Text.RegularExpressions

Namespace Save
    Public Class Player

        Public Id As Integer
        Public Blob As Byte()
        Public ReverseBlob As Byte()

        Public IdFromBlob As Integer
        Public SteamId As Long
        Public X As Single
        Public Y As Single
        Public Z As Single

        Public Name As String

        Public Sub New(PlayerId As Integer, Blob As Byte())
            Me.Id = PlayerId
            Me.Blob = Blob
            Me.ReverseBlob = Me.Blob.Clone()

            Array.Reverse(Me.ReverseBlob)

            Debug.WriteLine("--- New Player ({0}) ---", Me.Id)
            Debug.WriteLine("Blob length = {0}", Me.Blob.Length)

            For i As Integer = 0 To Me.Blob.Length - 5
                'For i As Integer = 17 To 17 + 20 - 4
                Dim aSingle As Single = BitConverter.ToSingle(Me.Blob, i)

                'If Math.Abs(aSingle) > 0.001 And Math.Abs(aSingle) < 2000 Then
                If (Math.Abs(aSingle) > 0.00001 And Math.Abs(aSingle) < 2000) Or Math.Abs(aSingle) = 0 Or False Then
                    Debug.Write(i)
                    Debug.Write(" ")
                    Debug.Write(Me.Blob.Length - i)
                    Debug.Write(" ")

                    Debug.Write(BitConverter.ToString(Me.Blob, i, 4))

                    Debug.Write(" ")

                    Debug.WriteLine(aSingle)
                End If
            Next

            'For i As Integer = 0 To Me.Blob.Length - 1 - 8
            '    Dim aLong As Long = BitConverter.ToInt64(Me.Blob, i)
            '
            '    Debug.Write(i)
            '    Debug.Write(" ")
            '    Debug.Write(Me.Blob.Length - i)
            '    Debug.Write(" ")
            '
            '    Debug.Write(BitConverter.ToString(Me.Blob, i, 4))
            '
            '    Debug.Write(" ")
            '
            '    Debug.WriteLine(aLong)
            'Next

            'For i As Integer = 0 To Me.Blob.Length - 1 - 8
            '    Dim aDouble As Double = BitConverter.ToDouble(Me.Blob, i)
            '
            '    Debug.Write(i)
            '    Debug.Write(" ")
            '    Debug.Write(Me.Blob.Length - i)
            '    Debug.Write(" ")
            '
            '    Debug.Write(BitConverter.ToString(Me.Blob, i, 4))
            '
            '    Debug.Write(" ")
            '
            '    Debug.WriteLine(aDouble)
            'Next

            ParseBlob()
        End Sub

        Public Sub ParseBlob()
            Me.IdFromBlob = BitConverter.ToInt32(Me.Blob, Me.Blob.Length - 4)
            Me.SteamId = BitConverter.ToInt64(Me.Blob, 9)

            ' Blob lengths:
            '   54 = Player never moved, never looked around
            '   55 = Player did move, never looked around
            '   62 = Player never moved, did look around
            '   64 = Normal

            If Me.Blob.Length = 64 Then ' YesMoveYesLook
                Me.X = BitConverter.ToSingle(Me.Blob, 32)
                Me.Y = BitConverter.ToSingle(Me.Blob, 36)
                Me.Z = BitConverter.ToSingle(Me.Blob, 40)
            ElseIf Me.Blob.Length = 62 Then ' NoMoveYesLook
                Me.X = BitConverter.ToSingle(Me.Blob, 35)
                Me.Y = BitConverter.ToSingle(Me.Blob, 35)
                Me.Z = BitConverter.ToSingle(Me.Blob, 39)
            ElseIf Me.Blob.Length = 55 Then ' YesMoveNoLook
                Me.X = BitConverter.ToSingle(Me.Blob, 23)
                Me.Y = BitConverter.ToSingle(Me.Blob, 27)
                Me.Z = BitConverter.ToSingle(Me.Blob, 31)
            ElseIf Me.Blob.Length = 54 Then ' NoMoveNoLook
                Me.X = BitConverter.ToSingle(Me.Blob, 27)
                Me.Y = BitConverter.ToSingle(Me.Blob, 27)
                Me.Z = BitConverter.ToSingle(Me.Blob, 31)
            End If

            Debug.WriteLine(BitConverter.ToString(Me.Blob))
        End Sub

        Public Sub DebugData()
            Debug.WriteLine("Player ({0}): IdFromBlob = {1}, SteamId = {2}, X = {3}, Y = {4}, Z = {5}", Me.Id, Me.IdFromBlob, Me.SteamId, Me.X, Me.Y, Me.Z)
        End Sub



        Public Function RequestPlayerName() As String
            If IsNothing(Me.Name) Then
                Dim webClient As New System.Net.WebClient
                Dim result As String = webClient.DownloadString("https://steamcommunity.com/profiles/" & Me.SteamId.ToString())

                Dim regex As Regex = New Regex("<span class=""actual_persona_name"">(.*?)<\/span>")
                Dim match As Match = regex.Match(result)

                Me.Name = match.Groups(1).Value

                Debug.WriteLine(Me.Name)
                Return Me.Name
            Else
                Return Me.Name
            End If
        End Function

    End Class
End Namespace
