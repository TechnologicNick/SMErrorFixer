<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SMErrorFixer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SMErrorFixer))
        Me.ListBoxErrors = New System.Windows.Forms.ListBox()
        Me.LabelErrors = New System.Windows.Forms.Label()
        Me.PanelError = New System.Windows.Forms.Panel()
        Me.LabelErrorType = New System.Windows.Forms.Label()
        Me.ButtonFix = New System.Windows.Forms.Button()
        Me.RichTextBoxDescription = New System.Windows.Forms.RichTextBox()
        Me.LabelErrorDescription = New System.Windows.Forms.Label()
        Me.RichTextBoxOccurance = New System.Windows.Forms.RichTextBox()
        Me.LabelOccurance = New System.Windows.Forms.Label()
        Me.RichTextBoxFix = New System.Windows.Forms.RichTextBox()
        Me.LabelFix = New System.Windows.Forms.Label()
        Me.PanelError.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBoxErrors
        '
        Me.ListBoxErrors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxErrors.FormattingEnabled = True
        Me.ListBoxErrors.Location = New System.Drawing.Point(12, 25)
        Me.ListBoxErrors.Name = "ListBoxErrors"
        Me.ListBoxErrors.Size = New System.Drawing.Size(176, 251)
        Me.ListBoxErrors.TabIndex = 1
        '
        'LabelErrors
        '
        Me.LabelErrors.AutoSize = True
        Me.LabelErrors.Location = New System.Drawing.Point(12, 9)
        Me.LabelErrors.Name = "LabelErrors"
        Me.LabelErrors.Size = New System.Drawing.Size(150, 13)
        Me.LabelErrors.TabIndex = 2
        Me.LabelErrors.Text = "Select the error you want to fix"
        '
        'PanelError
        '
        Me.PanelError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelError.Controls.Add(Me.RichTextBoxFix)
        Me.PanelError.Controls.Add(Me.LabelFix)
        Me.PanelError.Controls.Add(Me.RichTextBoxOccurance)
        Me.PanelError.Controls.Add(Me.LabelOccurance)
        Me.PanelError.Controls.Add(Me.LabelErrorType)
        Me.PanelError.Controls.Add(Me.ButtonFix)
        Me.PanelError.Controls.Add(Me.RichTextBoxDescription)
        Me.PanelError.Controls.Add(Me.LabelErrorDescription)
        Me.PanelError.Location = New System.Drawing.Point(194, 25)
        Me.PanelError.Name = "PanelError"
        Me.PanelError.Size = New System.Drawing.Size(444, 251)
        Me.PanelError.TabIndex = 3
        '
        'LabelErrorType
        '
        Me.LabelErrorType.AutoSize = True
        Me.LabelErrorType.Location = New System.Drawing.Point(4, 4)
        Me.LabelErrorType.Name = "LabelErrorType"
        Me.LabelErrorType.Size = New System.Drawing.Size(77, 13)
        Me.LabelErrorType.TabIndex = 3
        Me.LabelErrorType.Text = "Type: Example"
        '
        'ButtonFix
        '
        Me.ButtonFix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonFix.Location = New System.Drawing.Point(364, 223)
        Me.ButtonFix.Name = "ButtonFix"
        Me.ButtonFix.Size = New System.Drawing.Size(75, 23)
        Me.ButtonFix.TabIndex = 2
        Me.ButtonFix.Text = "Fix Error"
        Me.ButtonFix.UseVisualStyleBackColor = True
        '
        'RichTextBoxDescription
        '
        Me.RichTextBoxDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxDescription.Location = New System.Drawing.Point(3, 45)
        Me.RichTextBoxDescription.Name = "RichTextBoxDescription"
        Me.RichTextBoxDescription.ReadOnly = True
        Me.RichTextBoxDescription.Size = New System.Drawing.Size(436, 46)
        Me.RichTextBoxDescription.TabIndex = 1
        Me.RichTextBoxDescription.Text = resources.GetString("RichTextBoxDescription.Text")
        '
        'LabelErrorDescription
        '
        Me.LabelErrorDescription.AutoSize = True
        Me.LabelErrorDescription.Location = New System.Drawing.Point(4, 29)
        Me.LabelErrorDescription.Name = "LabelErrorDescription"
        Me.LabelErrorDescription.Size = New System.Drawing.Size(60, 13)
        Me.LabelErrorDescription.TabIndex = 0
        Me.LabelErrorDescription.Text = "Description"
        '
        'RichTextBoxOccurance
        '
        Me.RichTextBoxOccurance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxOccurance.Location = New System.Drawing.Point(3, 110)
        Me.RichTextBoxOccurance.Name = "RichTextBoxOccurance"
        Me.RichTextBoxOccurance.ReadOnly = True
        Me.RichTextBoxOccurance.Size = New System.Drawing.Size(436, 46)
        Me.RichTextBoxOccurance.TabIndex = 5
        Me.RichTextBoxOccurance.Text = resources.GetString("RichTextBoxOccurance.Text")
        '
        'LabelOccurance
        '
        Me.LabelOccurance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelOccurance.AutoSize = True
        Me.LabelOccurance.Location = New System.Drawing.Point(4, 94)
        Me.LabelOccurance.Name = "LabelOccurance"
        Me.LabelOccurance.Size = New System.Drawing.Size(60, 13)
        Me.LabelOccurance.TabIndex = 4
        Me.LabelOccurance.Text = "Occurance"
        '
        'RichTextBoxFix
        '
        Me.RichTextBoxFix.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxFix.Location = New System.Drawing.Point(3, 175)
        Me.RichTextBoxFix.Name = "RichTextBoxFix"
        Me.RichTextBoxFix.ReadOnly = True
        Me.RichTextBoxFix.Size = New System.Drawing.Size(436, 46)
        Me.RichTextBoxFix.TabIndex = 7
        Me.RichTextBoxFix.Text = resources.GetString("RichTextBoxFix.Text")
        '
        'LabelFix
        '
        Me.LabelFix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFix.AutoSize = True
        Me.LabelFix.Location = New System.Drawing.Point(4, 159)
        Me.LabelFix.Name = "LabelFix"
        Me.LabelFix.Size = New System.Drawing.Size(20, 13)
        Me.LabelFix.TabIndex = 6
        Me.LabelFix.Text = "Fix"
        '
        'SMErrorFixer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 288)
        Me.Controls.Add(Me.PanelError)
        Me.Controls.Add(Me.LabelErrors)
        Me.Controls.Add(Me.ListBoxErrors)
        Me.Name = "SMErrorFixer"
        Me.Text = "Scrap Mechanic Error Fixer"
        Me.PanelError.ResumeLayout(False)
        Me.PanelError.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBoxErrors As ListBox
    Friend WithEvents LabelErrors As Label
    Friend WithEvents PanelError As Panel
    Friend WithEvents LabelErrorDescription As Label
    Friend WithEvents RichTextBoxDescription As RichTextBox
    Friend WithEvents ButtonFix As Button
    Friend WithEvents LabelErrorType As Label
    Friend WithEvents RichTextBoxFix As RichTextBox
    Friend WithEvents LabelFix As Label
    Friend WithEvents RichTextBoxOccurance As RichTextBox
    Friend WithEvents LabelOccurance As Label
End Class
