Public Class Vehicle

    Public Sub Movement(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs, ByRef Barry As PictureBox)
        Barry.Left = CInt((e.X - (Barry.Width / 2)))

    End Sub

End Class
