Public Class panel_reportes


    Private Sub cmdCerrar_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdCerrar.Click
        'Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Sub panel_reportes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdfecha_Click(sender As System.Object, e As System.EventArgs) Handles cmdfecha.Click

        If FormularioActivo("c_ventas") = False Then
            c_ventas.Show()
        End If

    End Sub
End Class
