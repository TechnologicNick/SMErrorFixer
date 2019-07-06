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
        Dim e As SMError = GetSelectedError()
        RichTextBoxDescription.Text = e.Description
        RichTextBoxOccurance.Text = e.Occurance
        RichTextBoxFix.Text = e.Fix
    End Sub
End Class
