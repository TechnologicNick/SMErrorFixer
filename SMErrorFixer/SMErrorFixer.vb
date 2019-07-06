Public Class SMErrorFixer
    Private Sub SMErrorFixer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ErrorDatabase.LoadDatabase()

        ListBoxErrors.DataSource = ErrorDatabase.SMErrorList
        ListBoxErrors.DisplayMember = "DisplayName"

        ListBoxErrors.SelectedIndex = 0
    End Sub

    Private Sub ListBoxErrors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxErrors.SelectedIndexChanged
        UpdateSelectedError()
    End Sub

    Public Function GetSelectedError() As SMError
        Return ListBoxErrors.SelectedItem
    End Function

    Public Sub UpdateSelectedError()
        Dim smError As SMError = GetSelectedError()

        LabelErrorType.Text = "Type: " + smError.Type

        RichTextBoxDescription.Text = smError.Description
        RichTextBoxOccurance.Text = smError.Occurance
        RichTextBoxFix.Text = smError.Fix
    End Sub

    Private Sub ButtonFix_Click(sender As Object, e As EventArgs) Handles ButtonFix.Click
        Dim smError As SMError = GetSelectedError()

        Select Case smError.Name
            Case "10.3"
                ' TODO
        End Select
    End Sub
End Class
