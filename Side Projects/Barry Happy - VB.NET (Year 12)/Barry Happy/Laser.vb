Public Class Laser : Inherits Laser_Speed

    Public Sub Movement(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs, ByRef Laser_Beam As PictureBox, ByRef Ship As PictureBox)
        Laser_Beam.Location = New Point(Ship.Location.X + 39, Ship.Location.Y + 39)
    End Sub


    Public Sub Movement_AF(ByRef Laser_Beam As PictureBox, ByRef Laser_Fire As Boolean, ByVal Laser_X_Pos_BF As Integer)
        Delay(0.01)
        Laser_Beam.Visible = True
        Laser_Beam.Location = New Point(Laser_X_Pos_BF, Laser_Beam.Location.Y - Laser_Speed)

        If Laser_Beam.Location.Y < -10 Then
            Laser_Fire = False
            Laser_Beam.Visible = False
        End If
    End Sub

End Class

Public Class Laser_Speed
    Public Laser_Speed As Integer = 10
End Class
