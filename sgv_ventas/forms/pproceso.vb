Public Class panel_proceso


    Private Sub cmdfecha_Click(sender As System.Object, e As System.EventArgs) Handles cmdfecha.Click

        If FormularioActivo("u_CambioFecha") = False Then
            u_CambioFecha.Show()
        End If

    End Sub

    Private Sub cmdCerrar_Click(sender As System.Object, e As System.EventArgs) Handles cmdCerrar.Click
        ' Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Sub ButtonX1_Click(sender As System.Object, e As System.EventArgs)
        If FormularioActivo("mesas") = False Then
            mesas.Show()
        End If
    End Sub
End Class
