Public Class SMErrorFixer
    Private Sub SMErrorFixer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Settings.Load()

        ErrorDatabase.LoadDatabase()

        ListBoxErrors.DataSource = ErrorDatabase.SMErrorList
        ListBoxErrors.DisplayMember = "DisplayName"

        ListBoxErrors.SelectedIndex = 0
    End Sub

    Private Sub SMErrorFixer_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        Settings.Save()
    End Sub

    Private Sub ListBoxErrors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxErrors.SelectedIndexChanged
        UpdateSelectedError()
    End Sub

    Public Function GetSelectedError() As SMError
        Return ListBoxErrors.SelectedItem
    End Function

    Public Sub UpdateSelectedError()
        Dim smError As SMError = GetSelectedError()

        LabelErrorType.Text = "Type: " & smError.Type

        RichTextBoxDescription.Text = smError.Description
        RichTextBoxOccurance.Text = smError.Occurance
        RichTextBoxFix.Text = smError.Fix

        Button0.Text = smError.Button0
        Button1.Text = smError.Button1

        Button0.Visible = smError.Button0 IsNot ""
        Button1.Visible = smError.Button1 IsNot ""
    End Sub

    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
        Dim smError As SMError = GetSelectedError()

        Select Case smError.Name
            Case "10ext3"
                'Form10ext3.ShowDialog(Me)
                Form10ext3.SelectFiles()
            Case "player_out_of_world"
                FormPlayerOutOfWorld.Show()
            Case "unable_to_find_mod"
                FormUnableToFindMod.SelectFile()
            Case "access_denied"
                AccessDenied.Fix()
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim smError As SMError = GetSelectedError()

        Select Case smError.Name
            Case "10ext3"
                Form10ext3.FixAll()
        End Select
    End Sub
End Class
