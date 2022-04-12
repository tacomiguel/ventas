Imports MySql.Data.MySqlClient
Imports libreriaVentas

Public Class mesas
    Dim isDown As Boolean
    Dim isEdit As Boolean = False
    Dim cod_salon As String = "S001"
    

    Private Sub mesas_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.Control And e.KeyCode = Keys.E) Then
            If isEdit = True Then
                isEdit = False
            Else
                isEdit = True
            End If
        End If
        If (e.Control And e.KeyCode = Keys.A) Then
            If FormularioActivo("p_mesas") = False Then
                p_mesas.datossalon(cod_salon)
                p_mesas.Show()
            End If
        End If
    End Sub
    Private Sub mesas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        mostrarmesas(cod_salon)
    End Sub

    Private Sub mostrarmesas(ByVal cod_salon As String)
        Dim objconexion As MySqlConnection
        objconexion = Conexion.obtenerConexion()
        Dim da As New MySqlDataAdapter
        Dim strSQL As String
        'If cod_salon = "S999" Then
        '    strSQL = "Select ope_ped as cod_mesa,concat(dsc_mesa,'-',ope_ped) as dsc_mesa,cod_salon,b_ancho,b_alto,b_color,pos_x,pos_y,isnull(foto) as vnul,foto,activo from salida s inner join mesas m on m.cod_mesa=s.caux2 where  activo and cod_salon='" & cod_salon & "' and nro_doc= '' "
        'Else
        strSQL = "Select cod_mesa,dsc_mesa,cod_salon,b_ancho,b_alto,b_color,pos_x,pos_y,isnull(foto) as vnul,if(estado,foto,fotoX) as foto,activo from mesas " _
                & " where activo and cod_mesa<>'999' and cod_salon='" & cod_salon & "'"
        'End If

        Dim mycomand As New MySqlCommand(strSQL, objconexion)
        Dim myreader As MySqlDataReader
        Controls.Clear()

        Try
            myreader = mycomand.ExecuteReader()
            While myreader.Read
                'Dim botong As New DevComponents.DotNetBar.ButtonX
                Dim botong As New Button
                'Dim botong As New PictureBox
                Dim Imag As Byte() = Nothing
                With botong
                    .Width = 50
                    .Height = 30
                    .Visible = True
                    '.TextAlign = ContentAlignment.MiddleCenter
                    .ForeColor = Color.Transparent
                    .Text = myreader("dsc_mesa")
                    .Name = myreader("cod_mesa")
                    .Width = myreader("b_ancho")
                    .Height = myreader("b_alto")
                    If myreader("vnul") = 0 Then
                        Imag = myreader("foto")
                        .Image = Bytes_Imagen(Imag)
                    Else
                        .Image = Bytes_Imagen(Imag)
                    End If
                    .BackColor = System.Drawing.ColorTranslator.FromOle(CInt(myreader("b_color")))
                    .Location = New Point(myreader("pos_x"), myreader("pos_y"))

                End With

                Controls.Add(botong)
                AddHandler botong.MouseMove, AddressOf Me.botong_MouseMove
                AddHandler botong.MouseDown, AddressOf Me.botong_MouseDown
                AddHandler botong.MouseUp, AddressOf Me.botong_MouseUp
                AddHandler botong.Click, AddressOf Me.botong_Click

            End While
            myreader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'dbConex.Close()
        End Try
        Dim orden As String = cod_salon.Substring(1, 3)
        Me.BackgroundImage = New System.Drawing.Bitmap(Application.StartupPath & "\salon" & orden & ".jpg")
        mostrarsalones()
    End Sub
    Private Sub botong_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado
        Dim btn As Button = DirectCast(sender, Button)
        'Dim btn As PictureBox = DirectCast(sender, PictureBox)
        Dim cadena As String = btn.Name.Substring(0, 1)
        Select Case cadena
            Case "S"
                cod_salon = btn.Name
                mostrarmesas(cod_salon)

            Case Else
                Dim _pventa As New p_ventas_00
                ' Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
                If cod_salon = "S999" Then
                    _pventa.actualizapedido(btn.Name)
                Else
                    p_ventas_00.actualizamesa(btn.Name)
                End If
                
                Me.Close()
        End Select
    End Sub
    Private Sub mostrarsalones()
        Dim objconexion As MySqlConnection
        objconexion = Conexion.obtenerConexion()
        Dim da As New MySqlDataAdapter
        Dim strSQL As String = "select * from salones"
        Dim mycomand As New MySqlCommand(strSQL, objconexion)
        Dim myreader As MySqlDataReader

        ' Controls.Clear()
        Try
            myreader = mycomand.ExecuteReader()
            While myreader.Read
                'Dim botong As New DevComponents.DotNetBar.ButtonX
                Dim botong As New Button

                With botong
                    .Width = 50
                    .Height = 30
                    .Visible = True
                    .TextAlign = ContentAlignment.BottomCenter
                    .ForeColor = Color.White
                    .Text = myreader("dsc_salon")
                    .Name = myreader("cod_salon")
                    .Width = myreader("b_ancho")
                    .Height = myreader("b_alto")
                    .BackColor = System.Drawing.ColorTranslator.FromOle(CInt(myreader("color")))
                    .Location = New Point(myreader("pos_x"), myreader("pos_y"))

                End With
                Controls.Add(botong)
                'AddHandler botong.MouseMove, AddressOf Me.botong_MouseMove
                'AddHandler botong.MouseDown, AddressOf Me.botong_MouseDown
                'AddHandler botong.MouseUp, AddressOf Me.botong_MouseUp
                AddHandler botong.Click, AddressOf Me.botonS_Click

            End While
            myreader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'dbConex.Close()
        End Try
    End Sub
    Private Sub botonS_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado
        Dim btn As Button = DirectCast(sender, Button)
        'Dim btn As PictureBox = DirectCast(sender, PictureBox)
        Dim cadena As String = btn.Name.Substring(0, 1)
        Select Case cadena
            Case "S"
                cod_salon = btn.Name
                mostrarmesas(cod_salon)

            Case Else
                Dim _pventa As New p_ventas_00
                '  Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
                If cod_salon = "S999" Then
                    _pventa.actualizapedido(btn.Name)
                Else

                    '    _pventa.txtcod_mesa.Text = btn.Name
                    '   _pventa.txtcod_mesa.Focus()
                    ' _pventa.actualizamesa()
                End If

                Me.Close()
        End Select
    End Sub

    Private Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
        Try
            'si hay imagen
            If Not Imagen Is Nothing Then
                'caturar array con memorystream hacia Bin
                Dim Bin As New IO.MemoryStream(Imagen)
                'con el método FroStream de Image obtenemos imagen
                Dim Resultado As Image = Image.FromStream(Bin)
                'y la retornamos
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Private Sub botong_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If isEdit = True Then
            Dim btn As Button = DirectCast(sender, Button)
            'Dim btn As PictureBox = DirectCast(sender, PictureBox)
            If isDown = True Then
                btn.Location = getPoint(e, btn)
                ' Me.Cursor = Cursors.SizeAll
            End If
        End If
    End Sub
    Private Sub botong_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If isEdit = True Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                isDown = True
            End If
        End If
    End Sub
    Private Sub botong_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If isEdit = True Then
            Dim btn As Button = DirectCast(sender, Button)
            'Dim btn As PictureBox = DirectCast(sender, PictureBox)
            Dim mutilidades As New utilidades
            isDown = False
            Dim posx As Integer = btn.Location.X
            Dim posy As Integer = btn.Location.Y
            mutilidades.ActualizarUbicacion_Mesas(btn.Name, posx, posy)
        End If
    End Sub
    Protected Function getPoint(ByVal e As Windows.Forms.MouseEventArgs, ByVal Ctl As Windows.Forms.Control) As Point
        Dim ptox As Integer = Ctl.Left \ 2 + e.X
        Dim ptoy As Integer = Ctl.Top \ 2 + e.Y
        Dim punto As New Point(ptox, ptoy)
        Return punto
    End Function


End Class
