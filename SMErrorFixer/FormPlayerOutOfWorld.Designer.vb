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
        Me.DataGridViewPlayers = New System.Windows.Forms.DataGridView()
        Me.ColumnPlayerId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnSteamId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnPlayerName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnPosX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnPosY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnPosZ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnDelete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridViewPlayers, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'DataGridViewPlayers
        '
        Me.DataGridViewPlayers.AllowUserToAddRows = False
        Me.DataGridViewPlayers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewPlayers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnPlayerId, Me.ColumnSteamId, Me.ColumnPlayerName, Me.ColumnPosX, Me.ColumnPosY, Me.ColumnPosZ, Me.ColumnDelete})
        Me.DataGridViewPlayers.Location = New System.Drawing.Point(12, 12)
        Me.DataGridViewPlayers.Name = "DataGridViewPlayers"
        Me.DataGridViewPlayers.Size = New System.Drawing.Size(776, 397)
        Me.DataGridViewPlayers.TabIndex = 1
        '
        'ColumnPlayerId
        '
        Me.ColumnPlayerId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColumnPlayerId.HeaderText = "PlayerId"
        Me.ColumnPlayerId.Name = "ColumnPlayerId"
        Me.ColumnPlayerId.ReadOnly = True
        Me.ColumnPlayerId.Width = 70
        '
        'ColumnSteamId
        '
        Me.ColumnSteamId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColumnSteamId.HeaderText = "SteamId64"
        Me.ColumnSteamId.Name = "ColumnSteamId"
        Me.ColumnSteamId.ReadOnly = True
        Me.ColumnSteamId.Width = 83
        '
        'ColumnPlayerName
        '
        Me.ColumnPlayerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColumnPlayerName.HeaderText = "Player Name"
        Me.ColumnPlayerName.Name = "ColumnPlayerName"
        Me.ColumnPlayerName.ReadOnly = True
        '
        'ColumnPosX
        '
        Me.ColumnPosX.HeaderText = "X"
        Me.ColumnPosX.Name = "ColumnPosX"
        Me.ColumnPosX.ReadOnly = True
        '
        'ColumnPosY
        '
        Me.ColumnPosY.HeaderText = "Y"
        Me.ColumnPosY.Name = "ColumnPosY"
        Me.ColumnPosY.ReadOnly = True
        '
        'ColumnPosZ
        '
        Me.ColumnPosZ.HeaderText = "Z"
        Me.ColumnPosZ.Name = "ColumnPosZ"
        Me.ColumnPosZ.ReadOnly = True
        '
        'ColumnDelete
        '
        Me.ColumnDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColumnDelete.HeaderText = "Delete"
        Me.ColumnDelete.Name = "ColumnDelete"
        Me.ColumnDelete.Text = "Delete"
        Me.ColumnDelete.UseColumnTextForButtonValue = True
        Me.ColumnDelete.Width = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 420)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(190, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Note: the position may not be accurate"
        '
        'FormPlayerOutOfWorld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridViewPlayers)
        Me.Controls.Add(Me.ButtonSelectSaveFile)
        Me.Name = "FormPlayerOutOfWorld"
        Me.Text = "Fix player out of world"
        CType(Me.DataGridViewPlayers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents OpenFileDialogDB As OpenFileDialog
    Friend WithEvents ButtonSelectSaveFile As Button
    Friend WithEvents DataGridViewPlayers As DataGridView
    Friend WithEvents ColumnPlayerId As DataGridViewTextBoxColumn
    Friend WithEvents ColumnSteamId As DataGridViewTextBoxColumn
    Friend WithEvents ColumnPlayerName As DataGridViewTextBoxColumn
    Friend WithEvents ColumnPosX As DataGridViewTextBoxColumn
    Friend WithEvents ColumnPosY As DataGridViewTextBoxColumn
    Friend WithEvents ColumnPosZ As DataGridViewTextBoxColumn
    Friend WithEvents ColumnDelete As DataGridViewButtonColumn
    Friend WithEvents Label1 As Label
End Class
