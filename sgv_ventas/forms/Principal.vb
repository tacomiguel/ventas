Imports System.Windows.Forms
Imports System.Data

Public Class principal

    Private Sub principal_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub principal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tit As String = General.tituloVentanaPrincipal()
        Me.Text = tit
        lblusuario1.Text = "Usuario Activo: " & pDatosUser
        p_ventas_00.MdiParent = Me
        p_ventas_00.Show()
    End Sub

    Private Sub mp_salir_Click(sender As System.Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles mp_salir.Click
        Me.Close()
        End
    End Sub

    Private Sub bubbleButton2_Click(sender As Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles bubbleButton2.Click
     
        
    End Sub

    Private Sub mp_recursos_Click(sender As Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles mp_recursos.Click
        If validaperfil(pPerfil) Then
            mp_eventos.Enabled = False
            u_CambioFecha.MdiParent = Me
            u_CambioFecha.Show()
        End If
        
    End Sub

    Private Sub bubbleButton3_Click(sender As Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles bubbleButton3.Click
 
    End Sub

    Private Sub mp_eventos_Click(sender As Object, e As DevComponents.DotNetBar.ClickEventArgs) Handles mp_eventos.Click
        If validaperfil(pPerfil) Then
            mp_eventos.Enabled = False
            c_ventas.MdiParent = Me
            c_ventas.Show()
        End If
    End Sub
End Class
