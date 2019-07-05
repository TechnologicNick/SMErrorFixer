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
        Me.lbErrors = New System.Windows.Forms.ListBox()
        Me.labelErrors = New System.Windows.Forms.Label()
        Me.panelError = New System.Windows.Forms.Panel()
        Me.labelErrorDescription = New System.Windows.Forms.Label()
        Me.rtbErrorDescription = New System.Windows.Forms.RichTextBox()
        Me.btnFix = New System.Windows.Forms.Button()
        Me.panelError.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbErrors
        '
        Me.lbErrors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbErrors.FormattingEnabled = True
        Me.lbErrors.Items.AddRange(New Object() {"Error code: 10. Extended: 3", "Error code: 8"})
        Me.lbErrors.Location = New System.Drawing.Point(12, 25)
        Me.lbErrors.Name = "lbErrors"
        Me.lbErrors.Size = New System.Drawing.Size(176, 160)
        Me.lbErrors.TabIndex = 1
        '
        'labelErrors
        '
        Me.labelErrors.AutoSize = True
        Me.labelErrors.Location = New System.Drawing.Point(12, 9)
        Me.labelErrors.Name = "labelErrors"
        Me.labelErrors.Size = New System.Drawing.Size(150, 13)
        Me.labelErrors.TabIndex = 2
        Me.labelErrors.Text = "Select the error you want to fix"
        '
        'panelError
        '
        Me.panelError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelError.Controls.Add(Me.btnFix)
        Me.panelError.Controls.Add(Me.rtbErrorDescription)
        Me.panelError.Controls.Add(Me.labelErrorDescription)
        Me.panelError.Location = New System.Drawing.Point(194, 25)
        Me.panelError.Name = "panelError"
        Me.panelError.Size = New System.Drawing.Size(444, 159)
        Me.panelError.TabIndex = 3
        '
        'labelErrorDescription
        '
        Me.labelErrorDescription.AutoSize = True
        Me.labelErrorDescription.Location = New System.Drawing.Point(4, 4)
        Me.labelErrorDescription.Name = "labelErrorDescription"
        Me.labelErrorDescription.Size = New System.Drawing.Size(60, 13)
        Me.labelErrorDescription.TabIndex = 0
        Me.labelErrorDescription.Text = "Description"
        '
        'rtbErrorDescription
        '
        Me.rtbErrorDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbErrorDescription.Location = New System.Drawing.Point(3, 20)
        Me.rtbErrorDescription.Name = "rtbErrorDescription"
        Me.rtbErrorDescription.ReadOnly = True
        Me.rtbErrorDescription.Size = New System.Drawing.Size(436, 105)
        Me.rtbErrorDescription.TabIndex = 1
        Me.rtbErrorDescription.Text = resources.GetString("rtbErrorDescription.Text")
        '
        'btnFix
        '
        Me.btnFix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFix.Location = New System.Drawing.Point(364, 131)
        Me.btnFix.Name = "btnFix"
        Me.btnFix.Size = New System.Drawing.Size(75, 23)
        Me.btnFix.TabIndex = 2
        Me.btnFix.Text = "Fix Error"
        Me.btnFix.UseVisualStyleBackColor = True
        '
        'SMErrorFixer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 196)
        Me.Controls.Add(Me.panelError)
        Me.Controls.Add(Me.labelErrors)
        Me.Controls.Add(Me.lbErrors)
        Me.Name = "SMErrorFixer"
        Me.Text = "Scrap Mechanic Error Fixer"
        Me.panelError.ResumeLayout(False)
        Me.panelError.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbErrors As ListBox
    Friend WithEvents labelErrors As Label
    Friend WithEvents panelError As Panel
    Friend WithEvents labelErrorDescription As Label
    Friend WithEvents rtbErrorDescription As RichTextBox
    Friend WithEvents btnFix As Button
End Class
