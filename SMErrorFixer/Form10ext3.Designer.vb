<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form10ext3
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
        Me.ButtonSelectSaveFiles = New System.Windows.Forms.Button()
        Me.ButtonFixAll = New System.Windows.Forms.Button()
        Me.OpenFileDialogDBJournal = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'ButtonSelectSaveFiles
        '
        Me.ButtonSelectSaveFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectSaveFiles.Location = New System.Drawing.Point(118, 12)
        Me.ButtonSelectSaveFiles.Name = "ButtonSelectSaveFiles"
        Me.ButtonSelectSaveFiles.Size = New System.Drawing.Size(100, 23)
        Me.ButtonSelectSaveFiles.TabIndex = 0
        Me.ButtonSelectSaveFiles.Text = "Select Save Files"
        Me.ButtonSelectSaveFiles.UseVisualStyleBackColor = True
        '
        'ButtonFixAll
        '
        Me.ButtonFixAll.Location = New System.Drawing.Point(12, 12)
        Me.ButtonFixAll.Name = "ButtonFixAll"
        Me.ButtonFixAll.Size = New System.Drawing.Size(100, 23)
        Me.ButtonFixAll.TabIndex = 1
        Me.ButtonFixAll.Text = "Fix All"
        Me.ButtonFixAll.UseVisualStyleBackColor = True
        '
        'OpenFileDialogDBJournal
        '
        Me.OpenFileDialogDBJournal.DefaultExt = "db-journal"
        Me.OpenFileDialogDBJournal.FileName = "OpenFileDialog"
        Me.OpenFileDialogDBJournal.Filter = "db-journal files (*.db-journal)|*.db-journal"
        Me.OpenFileDialogDBJournal.InitialDirectory = "C:\Users\Nick\AppData\Roaming\Axolot Games\Scrap Mechanic\User\User_7656119814252" &
    "7219\Save"
        Me.OpenFileDialogDBJournal.Multiselect = True
        Me.OpenFileDialogDBJournal.Title = "Select .db-journal files to merge with the .db file."
        '
        'Form10ext3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(230, 46)
        Me.Controls.Add(Me.ButtonFixAll)
        Me.Controls.Add(Me.ButtonSelectSaveFiles)
        Me.Name = "Form10ext3"
        Me.Text = "Fix Error"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonSelectSaveFiles As Button
    Friend WithEvents ButtonFixAll As Button
    Friend WithEvents OpenFileDialogDBJournal As OpenFileDialog
End Class
