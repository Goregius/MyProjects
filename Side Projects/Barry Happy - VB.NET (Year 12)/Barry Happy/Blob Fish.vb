Public Class Blob_Fish

    'Left = 12, 12
    'Right = 875, 12

    Public Direction As Char = "R"
    Public Speed As Integer = 4

    Public Sub Direction_Sub(ByRef Blobby As PictureBox)

        If Blobby.Location.X >= 875 Then
            Direction = "L"
        ElseIf Blobby.Location.X <= 12 Then
            Direction = "R"
        End If

    End Sub

    Public Sub Movement(ByRef Blobby As PictureBox)

        If Direction = "R" Then
            Blobby.Location = New Point(Blobby.Location.X + Speed, Blobby.Location.Y)
        ElseIf Direction = "L" Then
            Blobby.Location = New Point(Blobby.Location.X - Speed, Blobby.Location.Y)
        End If

        Randomize()

        Dim value As Integer = CInt(Int((300 * Rnd()) + 1))

        If value = 2 Then
            If Direction = "L" Then
                Direction = "R"
            ElseIf Direction = "R" Then
                Direction = "L"
            End If
        End If

    End Sub





End Class
