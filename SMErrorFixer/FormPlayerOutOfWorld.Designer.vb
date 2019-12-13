<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPlayerOutOfWorld
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
        Me.OpenFileDialogDB = New System.Windows.Forms.OpenFileDialog()
        Me.ButtonSelectSaveFile = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OpenFileDialogDB
        '
        Me.OpenFileDialogDB.FileName = "Seebahn_ZMX.db"
        Me.OpenFileDialogDB.Filter = "Save files (*.db)|*.db"
        Me.OpenFileDialogDB.InitialDirectory = "C:\Users\Nick\AppData\Roaming\Axolot Games\Scrap Mechanic\User\User_7656119814252" &
    "7219\Save"
        Me.OpenFileDialogDB.Title = "Select a save file to edit player position(s)."
        '
        'ButtonSelectSaveFile
        '
        Me.ButtonSelectSaveFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectSaveFile.Location = New System.Drawing.Point(698, 415)
        Me.ButtonSelectSaveFile.Name = "ButtonSelectSaveFile"
        Me.ButtonSelectSaveFile.Size = New System.Drawing.Size(90, 23)
        Me.ButtonSelectSaveFile.TabIndex = 0
        Me.ButtonSelectSaveFile.Text = "Select save file"
        Me.ButtonSelectSaveFile.UseVisualStyleBackColor = True
        '
        'FormPlayerOutOfWorld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ButtonSelectSaveFile)
        Me.Name = "FormPlayerOutOfWorld"
        Me.Text = "Fix player out of world"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OpenFileDialogDB As OpenFileDialog
    Friend WithEvents ButtonSelectSaveFile As Button
End Class
