Public Class Menu
    Dim _pventa As New panel_venta1
    Dim _pproceso As New panel_proceso
    Dim _preporte As New panel_reportes
    Private Sub MetroTileItem1_Click(sender As System.Object, e As System.EventArgs)
        '   Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub


    Private Sub MetroTileItem2_Click(sender As System.Object, e As System.EventArgs)
        '   Principal.ShowModalPanel(_pproceso, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Sub MetroTileItem3_Click(sender As System.Object, e As System.EventArgs)
        '  Principal.ShowModalPanel(_preporte, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub


    Private Sub MetroTileItem4_Click(sender As Object, e As System.EventArgs)
        End
    End Sub



    
    Private Sub MetroTilePanel1_ItemClick(sender As System.Object, e As System.EventArgs) Handles MetroTilePanel1.ItemClick

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs)
        ' Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs)

        If FormularioActivo("u_CambioFecha") = False Then
            u_CambioFecha.Show()
        End If
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs)
        If FormularioActivo("c_ventas") = False Then
            c_ventas.Show()
        End If

    End Sub

    Private Sub ButtonX4_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub ButtonX8_Click(sender As Object, e As EventArgs) Handles ButtonX8.Click
        '  Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Sub ButtonX7_Click(sender As Object, e As EventArgs) Handles ButtonX7.Click

        If FormularioActivo("u_CambioFecha") = False Then
            u_CambioFecha.Show()
        End If
    End Sub

    Private Sub ButtonX6_Click(sender As Object, e As EventArgs) Handles ButtonX6.Click
        If FormularioActivo("c_ventas") = False Then
            c_ventas.Show()
        End If
    End Sub

    Private Sub ButtonX5_Click(sender As Object, e As EventArgs) Handles ButtonX5.Click
        End
    End Sub
End Class
