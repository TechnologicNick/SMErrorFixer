Imports System.IO
Imports Newtonsoft.Json

Public Class UGCDescription

    Public Property description As String = "(Empty Description)"
    Public Property fileId As Integer = -1
    Public Property localId As String = "00000000-0000-0000-0000-000000000000"
    Public Property name As String = "(Unnamed Mod)"
    Public Property type As String = "(Unknown Type)"
    Public Property version As Integer = 0

    Public Shared Function FromWorkshopItem(itemDirectory As String) As UGCDescription
        Return JsonConvert.DeserializeObject(Of UGCDescription)(File.ReadAllText(Path.Combine(itemDirectory, "description.json")))
    End Function

End Class
