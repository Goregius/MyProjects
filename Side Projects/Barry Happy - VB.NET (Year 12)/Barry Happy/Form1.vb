Imports System.ComponentModel

Public Class Form1

    Dim PB(10) As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        BackgroundWorker2.RunWorkerAsync()
        Try
            If PB(10) = False Then
            End If
        Catch
            MsgBox("Error in accessing PB")
        End Try

        Blobfish_PB.Image = Image.FromFile("Blob Fish_.png")
        PB(2) = True


        BackgroundWorker1.RunWorkerAsync()
        BackgroundWorker1.CancelAsync()
    End Sub

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub


    Dim Vehicle As Vehicle = New Vehicle()
    Dim Laser As Laser = New Laser()
    Dim Blob_Fish As Blob_Fish = New Blob_Fish
    Dim Laser_Fire As Boolean = False
    Dim Laser_X_Pos_BF As Integer


    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Vehicle.Movement(sender, e, Ship)
        If Laser_Fire = False Then
            Laser.Movement(sender, e, Laser_Beam, Ship)
            Laser_X_Pos_BF = Laser_Beam.Location.X
        End If




    End Sub

    Private Sub Mouse_Click(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Laser_Fire = True
        While True
            If Laser_Fire = True Then
                Laser.Movement_AF(Laser_Beam, Laser_Fire, Laser_X_Pos_BF)
                If Laser_Fire = False Then
                    Exit While
                    Laser_Fire = False
                End If
            End If

        End While

    End Sub

    Public Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        While True
            Blob_Fish.Direction_Sub(Blobfish_PB)
            Blob_Fish.Movement(Blobfish_PB)
            Delay(0.01)
        End While


    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        Dim Blob_HP As Integer = 3
        Dim collision As Boolean = False
        While collision = False
            Delay(0.01)
            For Each PictureBox In Me.Controls
                If Laser_Beam.Bounds.IntersectsWith(Blobfish_PB.Bounds) Then
                    Laser_Beam.Location = New Point(Ship.Location.X + 39, 627)
                    Laser_Beam.Visible = False
                    Laser_Fire = False
                    Blob_HP -= 1
                    If Blob_HP = 2 Then
                        Heart_3.Hide()
                    ElseIf Blob_HP = 1 Then
                        Heart_2.Hide()

                    ElseIf Blob_HP = 0 Then
                        Heart_1.Hide()
                        Blobfish_PB.Location = New Point(-50, -100)
                    End If
                    End If
            Next
        End While
        'PictureBox1 = Laser
        'PictureBox2 = BlobFish
    End Sub
End Class

