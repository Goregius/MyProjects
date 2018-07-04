<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.Heart_1 = New System.Windows.Forms.PictureBox()
        Me.Blobfish_PB = New System.Windows.Forms.PictureBox()
        Me.Ship = New System.Windows.Forms.PictureBox()
        Me.Laser_Beam = New System.Windows.Forms.PictureBox()
        Me.Heart_3 = New System.Windows.Forms.PictureBox()
        Me.Heart_2 = New System.Windows.Forms.PictureBox()
        CType(Me.Heart_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Blobfish_PB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ship, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Laser_Beam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Heart_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Heart_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'BackgroundWorker2
        '
        '
        'Heart_1
        '
        Me.Heart_1.Image = Global.Barry_Happy.My.Resources.Resources.Heart
        Me.Heart_1.Location = New System.Drawing.Point(868, 699)
        Me.Heart_1.Name = "Heart_1"
        Me.Heart_1.Size = New System.Drawing.Size(44, 42)
        Me.Heart_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Heart_1.TabIndex = 10
        Me.Heart_1.TabStop = False
        '
        'Blobfish_PB
        '
        Me.Blobfish_PB.Image = Global.Barry_Happy.My.Resources.Resources.Blob_Fish_
        Me.Blobfish_PB.Location = New System.Drawing.Point(441, 12)
        Me.Blobfish_PB.Name = "Blobfish_PB"
        Me.Blobfish_PB.Size = New System.Drawing.Size(88, 53)
        Me.Blobfish_PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Blobfish_PB.TabIndex = 1
        Me.Blobfish_PB.TabStop = False
        '
        'Ship
        '
        Me.Ship.Image = Global.Barry_Happy.My.Resources.Resources.spaceship_
        Me.Ship.Location = New System.Drawing.Point(441, 567)
        Me.Ship.Name = "Ship"
        Me.Ship.Size = New System.Drawing.Size(88, 78)
        Me.Ship.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Ship.TabIndex = 0
        Me.Ship.TabStop = False
        '
        'Laser_Beam
        '
        Me.Laser_Beam.Image = Global.Barry_Happy.My.Resources.Resources.Laser_
        Me.Laser_Beam.Location = New System.Drawing.Point(479, 599)
        Me.Laser_Beam.Name = "Laser_Beam"
        Me.Laser_Beam.Size = New System.Drawing.Size(10, 28)
        Me.Laser_Beam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Laser_Beam.TabIndex = 9
        Me.Laser_Beam.TabStop = False
        Me.Laser_Beam.Visible = False
        '
        'Heart_3
        '
        Me.Heart_3.Image = Global.Barry_Happy.My.Resources.Resources.Heart
        Me.Heart_3.Location = New System.Drawing.Point(968, 699)
        Me.Heart_3.Name = "Heart_3"
        Me.Heart_3.Size = New System.Drawing.Size(44, 42)
        Me.Heart_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Heart_3.TabIndex = 11
        Me.Heart_3.TabStop = False
        '
        'Heart_2
        '
        Me.Heart_2.Image = Global.Barry_Happy.My.Resources.Resources.Heart
        Me.Heart_2.Location = New System.Drawing.Point(918, 699)
        Me.Heart_2.Name = "Heart_2"
        Me.Heart_2.Size = New System.Drawing.Size(44, 42)
        Me.Heart_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Heart_2.TabIndex = 12
        Me.Heart_2.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(1050, 753)
        Me.Controls.Add(Me.Heart_2)
        Me.Controls.Add(Me.Heart_3)
        Me.Controls.Add(Me.Heart_1)
        Me.Controls.Add(Me.Blobfish_PB)
        Me.Controls.Add(Me.Ship)
        Me.Controls.Add(Me.Laser_Beam)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Heart_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Blobfish_PB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ship, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Laser_Beam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Heart_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Heart_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ship As System.Windows.Forms.PictureBox
    Friend WithEvents Blobfish_PB As System.Windows.Forms.PictureBox
    Friend WithEvents Laser_Beam As PictureBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Heart_1 As System.Windows.Forms.PictureBox
    Friend WithEvents Heart_3 As System.Windows.Forms.PictureBox
    Friend WithEvents Heart_2 As System.Windows.Forms.PictureBox
End Class
