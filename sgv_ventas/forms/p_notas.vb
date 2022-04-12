Public Class p_notas
    Private noperacion
    Private tipoAnulacion
    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub

    Private Sub p_notas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Sub datosNotas(ByVal operacion As Integer, ByVal tAnulacion As Integer)
        noperacion = operacion
        tipoAnulacion = tAnulacion
    End Sub

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        'Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
        Dim _pventa As New p_ventas_00
        If txtnotas.Text = "" Then
            MessageBox.Show("Debe Ingresar una Observacion...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            _pventa.anulardocumento(txtnotas.Text, noperacion, tipoAnulacion)
            Me.Close()
        End If



    End Sub
End Class
