Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Settings

    Private Shared Instance As Settings

    Public InstallDirectory As String = "%ProgramFiles(x86)%\Steam\steamapps\common\Scrap Mechanic"
    Public WorkshopDirectory As String = "%ProgramFiles(x86)%\Steam\steamapps\workshop\content\387990"

    Public Shared Sub Load()
        If File.Exists("settings.json") Then
            Dim json As String = File.ReadAllText("settings.json")

            Instance = JsonConvert.DeserializeObject(Of Settings)(json)
            Debug.WriteLine("Settings loaded!")
        Else
            Debug.WriteLine("Settings not found! Creating new file...")
            Instance = New Settings()
            Save()
        End If
    End Sub

    Public Shared Sub Save()
        Dim json As String = JsonConvert.SerializeObject(Instance, Formatting.Indented)

        Debug.WriteLine("Settings saved!")
        File.WriteAllText("settings.json", json)
    End Sub

    Public Shared Function GetInstance() As Settings
        Return Instance
    End Function
End Class
