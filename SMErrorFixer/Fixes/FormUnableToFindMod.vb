Imports System.IO
Imports System.Text
Imports System.Data.SQLite
Imports SMHelper.Save
Imports SMHelper.UGC.Mod
Imports SMHelper.Util

Public Class FormUnableToFindMod
    Public connection As SQLiteConnection
    Public dbHelper As DatabaseHelper
    Public ShapeList As Dictionary(Of Integer, Shape) = New Dictionary(Of Integer, Shape)
    Public JointList As Dictionary(Of Integer, Joint) = New Dictionary(Of Integer, Joint)
    Public ModManager As SMModManager

    Private Sub FormUnableToFindMod_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FormUnableToFindMod_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        DisposeDB()
    End Sub

    Private Sub ButtonSelectSaveFile_Click(sender As Object, e As EventArgs) Handles ButtonSelectSaveFile.Click
        SelectFile()
    End Sub

    Public Sub SelectFile()
        'ModManager.LoadMods()

        Dim result As DialogResult = OpenFileDialogDB.ShowDialog()
        Debug.WriteLine(result)

        If result = DialogResult.OK Then
            OpenDB(OpenFileDialogDB.FileName)
        End If
    End Sub

    Public Sub OpenDB(file As String)
        DisposeDB()

        connection = DatabaseHelper.CreateConnection(file)
        dbHelper = New DatabaseHelper(connection)

        dbHelper.VersionWarning(24)

        ShapeList = dbHelper.GetShapes()
        JointList = dbHelper.GetJoints()

        ModManager = New SMModManager()
        ModManager.AddVanillaMods(Environment.ExpandEnvironmentVariables(Settings.GetInstance().InstallDirectory))
        ModManager.LoadMods({
            UserDirectory.GetMods(),
            Environment.ExpandEnvironmentVariables(Settings.GetInstance().WorkshopDirectory)
        }.ToList())

        Dim usedMods As List(Of SMMod) = New List(Of SMMod)
        'Dim unidentifiedUuids As List(Of Guid) = New List(Of Guid)

        ' Dictionary(Of uuid, (type, count))
        Dim unidentifiedUuids As Dictionary(Of Guid, (typeName As String, count As Integer)) = New Dictionary(Of Guid, (typeName As String, count As Integer))

        FindUnidentifiedUuids(Me.ShapeList.Values, "shape", usedMods, unidentifiedUuids)
        FindUnidentifiedUuids(Me.JointList.Values, "joint", usedMods, unidentifiedUuids)





        Dim content As String = String.Format("Used Mods: ({0})", usedMods.Count) & vbNewLine
        For Each m As SMMod In usedMods
            content &= String.Format("({0}) {1}", m.Description.fileId, m.Description.name) & vbNewLine
        Next

        content &= vbNewLine & String.Format("Unidentified Uuids: ({0})", unidentifiedUuids.Count)
        For Each pair As KeyValuePair(Of Guid, (typeName As String, count As Integer)) In unidentifiedUuids
            content &= vbNewLine & $"{pair.Key.ToString()} (type = {pair.Value.typeName}, count = {pair.Value.count})"
        Next

        Debug.WriteLine(content)

        MessageBox.Show(SMErrorFixer, content, "Result")

    End Sub

    Public Sub DisposeDB()
        If connection IsNot Nothing Then
            Debug.WriteLine("Disposing of old database connection...")
            Me.ShapeList.Clear()
            Me.JointList.Clear()

            'connection.Dispose()

            connection.Close()
        End If
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Public Sub FindUnidentifiedUuids(ByRef list As IEnumerable(Of IUuidItem), ByVal typeName As String, ByRef usedMods As List(Of SMMod), ByRef unidentifiedUuids As Dictionary(Of Guid, (typeName As String, count As Integer)))
        Dim UuidMap As Dictionary(Of Guid, SMMod) = New Dictionary(Of Guid, SMMod)

        For Each smMod As SMMod In ModManager.ModList
            For Each uuid As Guid In smMod.UuidList
                UuidMap.Item(uuid) = smMod
            Next
        Next

        For Each i As IUuidItem In list
            If UuidMap.ContainsKey(i.UUID) Then
                Dim m As SMMod = UuidMap.Item(i.UUID)

                If Not usedMods.Contains(m) Then
                    usedMods.Add(m)
                End If
            Else
                If Not unidentifiedUuids.ContainsKey(i.UUID) Then
                    unidentifiedUuids(i.UUID) = (typeName, 1)
                Else
                    Dim value As (typeName As String, count As Integer) = unidentifiedUuids.Item(i.UUID)
                    unidentifiedUuids(i.UUID) = (value.typeName, value.count + 1)
                End If
            End If
        Next

    End Sub

End Class