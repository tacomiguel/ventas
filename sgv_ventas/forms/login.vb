Imports libreriaseguridad
Imports libreriaVentas
Imports MySql.Data.MySqlClient


Public Class Login
    'para validar el separador decimal
    Private sepDecimal As Char
    Dim Arrastre As Boolean


    Private Sub Login_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            End
        End If

    End Sub
    Private Sub login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''Formulario Elipse
        'Dim gp As New System.Drawing.Drawing2D.GraphicsPath
        'gp.AddEllipse(0, 0, Me.Width, Me.Height)
        'gp.GetHashCode()
        'Me.Region = New Region(gp)
        'capturamos el separador decimal
        'Aca tiene que cargar su imagen de fondo...
        'Dim imagen As Image = Bitmap.FromFile("C:\loginsga.BMP")
        'Dim imagen As Image = Me.BackgroundImage
        'Me.BackgroundImage = imagen
        'Me.Height = imagen.Height
        'Me.Width = imagen.Width
        'Dim mibitmap As Bitmap = CType(imagen, Bitmap)
        'Le paso a la funcion la imagen el bitmap de fondo y el color transparente ( En este caso tomo el color del pixel 0,0 del bitmap)
        'Dicha función me retorna la región de la imagen para poder asignarla a la región del formulario
        'Me.Region = ObtenerRegionDelBitmap(mibitmap, mibitmap.GetPixel(0, 0))
        'pb_logoemp.BackgroundImage = New System.Drawing.Bitmap(Application.StartupPath & "\logoemp.jpg")
        'Dim dia, mes, anno As Integer
        'Dim NomMes As String
        'dia = Microsoft.VisualBasic.DateAndTime.Day(pFechaSystem)
        'mes = Month(pFechaSystem)
        'NomMes = UCase(MonthName(mes, True))
        'anno = Year(pFechaSystem)
        ''LblPeriodo.Text = "Periodo Activo :" & NomMes & "-" & CType(anno, String)
        'LblPeriodo.Text = "" & dia & " " & NomMes & " del " & CType(anno, String)
        LblPeriodo.Text = general.devuelvefechaImpresion(pFechaSystem)
        sepDecimal = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim mTC As New TipoCambio
        'txtTC.Text = mTC.recupera(pFechaSystem)
        pTC = mTC.recupera(0, pFechaSystem)
        Lbltipcambio.Text = "Tipo de Cambio :" & mTC.recupera(0, pFechaSystem)

        txtClave.Focus()

    End Sub
    Private Sub cmdContinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ingresaSystem()
    End Sub



    Sub ingresaSystem()
        Dim mseguridad As New seguridad
        'Dim cadenae As String = mseguridad.Cifrado(1, 1, "20535135810", "09855425", "12345678")
        'Dim c As String = cadenae
        'MessageBox.Show(cadenae)
        'Dim cadenad As String = mseguridad.Cifrado(2, 1, cadenae, "09855425", "12345678")
        'MessageBox.Show(cadenad)

        If usuario.ingresoSistema(txtUsuario.Text, txtClave.Text) = True Then
            pNombreUser = IIf(txtUsuario.Text = "", usuario.recuperaNombreUsuario(txtClave.Text), txtUsuario.Text)
            pCuentaUser = IIf(txtUsuario.Text = "", usuario.recuperaCuentaUsuario(txtClave.Text), txtUsuario.Text)
            pNivelUser = usuario.recuperaNivelUsuario(pCuentaUser)
            pAlmaUser = usuario.recuperaAlmacenUsuario(pCuentaUser)
            pDatosUser = usuario.recuperaDatosUsuario(pCuentaUser)
            pPerfil = usuario.recuperaPerfilUsuario(pCuentaUser)
            Dim com As New MySqlCommand("select c.*,e.licencia from configuracion c inner join empresa e on c.cod_emp=e.cod_emp where e.activo='1'", dbConex)
            Dim dr As MySqlDataReader
            dr = com.ExecuteReader
            If dr.HasRows = True Then
                While dr.Read
                    pIGV = dr("igv")
                    pEmpresa = dr("cod_emp")
                    'Dim cadenaC As String = mseguridad.Cifrado(1, 1, pEmpresa, "09855425", "12345678")
                    'Dim milicencia As String = dr("licencia")
                    'If milicencia.Trim = cadenaC Then
                    'Else
                    '    validaempresa(pEmpresa)
                    'End If
                    plicencia = dr("licencia")
                    pNempresa = dr("nom_emp")
                    pDirEmpresa = dr("dir_emp")
                    pDecimales = dr("nros_decimales")
                    pDiasModificarIngreso = dr("dias_modificar_ingreso")
                    pDiasModificarPedido = dr("dias_modificar_pedido")
                    pDiasModificarSalida = dr("dias_modificar_salida")
                    pPreciosIncIGV = dr("precios_inc_igv")
                    pDespachoXprecioVenta = dr("despacho_x_pre_venta")
                    pDespachoXtipoCliente = dr("despacho_x_tipo_cliente")
                    pModoPedidos = dr("modo_pedidos")
                    pDiasModificarInventario = dr("dias_modificar_inventario")
                    pMoneda = dr("moneda")
                    pMonedaAbr = dr("monedaAbr")
                    pCatalogoXalmacen = dr("catalogo_x_almacen")
                    pvistaprevia = dr("vistaprevia_rpt")
                    pfechaservidor = dr("fechaServidor")
                    pLogo = dr("Logo")
                    pImpuestoXarticulo = dr("impuesto_x_articulo")
                    pDiasModificarTrans = dr("dias_modificar_trans")
                    pDespachoStock0 = dr("despacho_stock0")
                    pDiasModificarBaja = dr("dias_modificar_baja")
                End While
                ' validaempresa(plicencia)
                'establecemos permisos de usuario
                Dim ds As New DataSet, I As Integer, cod_smenu As String
                Dim musuario As New usuario, activo As Boolean
                ds = musuario.permisos(pCuentaUser)
                If ds.Tables("permisos").Rows.Count > 0 Then
                    For I = 0 To ds.Tables("permisos").Rows.Count - 1
                        cod_smenu = ds.Tables("permisos").Rows(I).Item("cod_smenu").ToString()
                        activo = ds.Tables("permisos").Rows(I).Item("activo").ToString()
                        asignaPermisos(cod_smenu, activo)
                    Next
                End If
            Else
                MessageBox.Show("NO es Posible Cargar la Configuración del Sistema...")
                End
            End If
            dr.Close()
            principal.Show()
            Me.Hide()
        Else
            pNombreUser = ""
            pCuentaUser = ""
            MessageBox.Show("Acceso Denegado...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtClave.Text = ""
            txtClave.Focus()
        End If

    End Sub
    'Sub validaempresa(ByVal codigo As String)
    '    Dim mutilidades As New utilidades
    '    Dim frmNuevo As New Form()
    '    Dim txtBox As New TextBox()
    '    Dim txtBoxLic As New TextBox()

    '    With txtBox
    '        .Text = codigo
    '        .Location = New System.Drawing.Point(80, 25)
    '        .Size() = New System.Drawing.Size(170, 40)
    '        .Font = New Font(New FontFamily("Arial"), 20)
    '        .Visible = True
    '    End With

    '    With txtBoxLic
    '        .Text = ""
    '        .Location = New System.Drawing.Point(80, 80)
    '        .Size() = New System.Drawing.Size(170, 160)
    '        .Font = New Font(New FontFamily("Arial"), 20)
    '        .Multiline = True
    '        .Visible = True
    '    End With

    '    With frmNuevo.Controls
    '        .Add(txtBox)
    '        .Add(txtBoxLic)
    '    End With

    '    With frmNuevo
    '        .Text = "Licencia"
    '        .ShowDialog()
    '        .StartPosition = FormStartPosition.CenterScreen
    '    End With
    '    mutilidades.ActualizarLicencia(txtBox.Text, txtBoxLic.Text)
    '    End
    'End Sub
    Private Sub txtTC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) And Not (e.KeyChar.Equals(sepDecimal)) Then
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtUsuario_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsuario.GotFocus
        general.ingresaTexto(txtUsuario)
    End Sub
    Private Sub txtUsuario_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsuario.LostFocus
        general.saleTexto(txtUsuario)
    End Sub
    Private Sub txtClave_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClave.GotFocus
        general.ingresaTexto(txtClave)
    End Sub
    Private Sub txtClave_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClave.LostFocus
        general.saleTexto(txtClave)
    End Sub
    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End
    End Sub
    Private Function ObtenerRegionDelBitmap(ByVal MiImagen As Bitmap, ByVal ColorTransparente As Color) As Region

        Dim RegionLocal As Region = Nothing

        If MiImagen Is Nothing Then Return RegionLocal

        Dim ColorDeFondo As Color = ColorTransparente

        Dim Largo As Integer = MiImagen.Height - 1

        Dim Ancho As Integer = MiImagen.Width

        Dim Fila As Integer

        Dim Columna As Integer

        RegionLocal = New Region(New Rectangle(0, 0, 0, 0))

        For Fila = 0 To Largo

            Dim ColumnaComienzo As Integer = -1

            Dim ColumnaFin As Integer = -1


            For Columna = 0 To Ancho

                If Columna = Ancho Then

                    If ColumnaComienzo <> -1 Then

                        ColumnaFin = Columna

                        Dim regUnion As New Region(New Rectangle(ColumnaComienzo, Fila, ColumnaFin - ColumnaComienzo, 1))

                        RegionLocal.Union(regUnion)

                        regUnion = Nothing

                    End If

                Else

                    If Not MiImagen.GetPixel(Columna, Fila).Equals(ColorDeFondo) Then

                        If ColumnaComienzo = -1 Then ColumnaComienzo = Columna

                    ElseIf MiImagen.GetPixel(Columna, Fila).Equals(ColorDeFondo) Then

                        If ColumnaComienzo <> -1 Then

                            ColumnaFin = Columna

                            Dim regUnion As New Region(New Rectangle(ColumnaComienzo, Fila, ColumnaFin - ColumnaComienzo, 1))

                            RegionLocal.Union(regUnion)

                            regUnion = Nothing

                            ColumnaComienzo = -1

                            ColumnaFin = -1

                        End If

                    End If

                End If

            Next

        Next

        Return RegionLocal

    End Function

    Private Sub Login_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Arrastre = True

    End Sub

    Private Sub Login_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If Arrastre Then Me.Location = Me.PointToScreen(New Point(e.X, e.Y))
    End Sub

    Private Sub Login_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        Arrastre = False
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub digitatecla(ByVal codigo As String)
        txtClave.Text = txtClave.Text & codigo
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        digitatecla(btn.Text)
    End Sub



    Private Sub txtClave_KeyDown(sender As Object, e As KeyEventArgs) Handles txtClave.KeyDown
        If e.KeyCode = Keys.Enter Then
            ingresaSystem()
        End If
    End Sub

    Private Sub Button1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            ingresaSystem()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ingresaSystem()
    End Sub

    'Private Sub Button1_KeyUp(sender As Object, e As KeyEventArgs) Handles Button1.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        MessageBox.Show(txtClave.Text & "Hola")
    '        ingresaSystem()
    '    End If
    'End Sub


End Class

