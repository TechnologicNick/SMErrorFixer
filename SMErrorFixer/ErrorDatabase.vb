Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ErrorDatabase
    Public Shared SMErrorList As List(Of SMError)

    Public Shared Sub LoadDatabase()
        SMErrorList = New List(Of SMError)()

        Dim json As JObject = JObject.Parse(My.Resources.ErrorDatabase)

        For Each item As JObject In json.Item("errors")
            Dim smError As SMError = JsonConvert.DeserializeObject(Of SMError)(item.ToString()) ' I don't know how to do this without converting to a string

            SMErrorList.Add(smError)
        Next

    End Sub
End Class