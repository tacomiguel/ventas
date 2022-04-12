Imports MySql.Data.MySqlClient
Imports libreriaVentas
Public Class p_caja
    Private dsformapago As New DataSet
    Private dtforma_pago As New DataTable("forma_pago")
    Private dsSalida As New DataSet
    Private dtpago As New DataTable("pago")
    Private dsArticulo As New DataSet
    Private dtArticulo As New DataTable("articulo")
    Private proceso As String
    Private noperacion
    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.Close()
        'Dim _pventa As New panel_venta
        'Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Sub p_notas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        estructuraPago()
        mostraritems(4, "")
    End Sub

    Sub datos(ByVal operacion As Integer)
        noperacion = operacion
        txtTotal1.Text = noperacion


    End Sub

    Sub estructuraPago()
        Dim prFont As New Font("Arial Narrow", 12, FontStyle.Bold)
        dsSalida = Salida.dsSalida
        dtpago = dsSalida.Tables("pago")
        With dataPago
            .DataSource = dsSalida.Tables("pago")
            .Columns("cod_pago").Visible = False
            .Columns("operacion").Visible = False
            .Columns("column1").HeaderCell.Style.Font = prFont
            .Columns("monto").HeaderCell.Style.Font = prFont
            .Columns("monto").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("monto").HeaderText = "IMPORTE"
            .Columns("monto").Width = 80
            .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("monto").ReadOnly = False
            .Columns("monto").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("monto").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("propina").HeaderCell.Style.Font = prFont
            .Columns("propina").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("propina").HeaderText = "PROPINA"
            .Columns("propina").Width = 80
            .Columns("propina").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("propina").ReadOnly = False
            .Columns("propina").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("propina").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("observacion").HeaderCell.Style.Font = prFont
            .Columns("observacion").HeaderText = "REFERENCIA"
            .Columns("observacion").Width = 90
            .Columns("observacion").ReadOnly = False
            .Columns("imp_vuelto").Visible = False
        End With
    End Sub

    Private Sub mostraritems(ByVal tipoSQL As Integer, ByVal codigo As String)
        Dim objconexion As MySqlConnection
        objconexion = Conexion.obtenerConexion()
        Dim da As New MySqlDataAdapter
        Dim strSQL As String = ""
        Select Case tipoSQL
            
            Case 4

                strSQL = ("select cod_fpago as codigo,nom_fpago as nombre, isnull(foto) as vnul,foto,color,'&HEFFBF2' as f_color,8 as f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                        & " from forma_pago where activo=1 and b_ancho>0")
                proceso = "P"
            
        End Select

        Dim cmd As New MySqlCommand(strSQL, objconexion)
        da.SelectCommand = cmd
        dsArticulo.Clear()
        da.Fill(dsArticulo, "articulo")

        Dim mycomand As New MySqlCommand(strSQL, objconexion)
        Dim myreader As MySqlDataReader
        panelitem.Controls.Clear()
        Try
            myreader = mycomand.ExecuteReader()
            While myreader.Read
                Dim boton As New Button
                Dim Imag As Byte() = Nothing
                With boton
                    .Width = myreader("b_ancho")
                    .Height = myreader("b_alto")
                    .Visible = True
                    .TextAlign = ContentAlignment.BottomCenter
                    If myreader("vnul") = 0 Then
                        Imag = myreader("foto")
                        .Image = Bytes_Imagen(Imag)
                    Else
                        .Image = Bytes_Imagen(Imag)
                    End If
                    .BackColor = System.Drawing.ColorTranslator.FromOle(CInt(myreader("color")))
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(myreader("f_color")))
                    .Font = New System.Drawing.Font("Arial Rounded MT", myreader("f_tamano"), FontStyle.Bold)
                    .Text = myreader("nombre")
                    .Name = myreader("codigo")
                End With


                panelitem.Controls.Add(boton)
                AddHandler boton.Click, AddressOf Me.botoni_Click
                AddHandler boton.GotFocus, AddressOf Me.botonsg_GotFocus
                AddHandler boton.LostFocus, AddressOf Me.botonsg_LostFocus
                AddHandler boton.MouseMove, AddressOf Me.botonsg_MouseMove
                AddHandler boton.MouseLeave, AddressOf Me.botonsg_MouseLeave


            End While
            myreader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'dbConex.Close()
        End Try
    End Sub
    Private Sub botoni_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado


        Dim btn As Button = DirectCast(sender, Button)

        Select Case proceso
            
            Case "P"

                agregapago(btn.Name)

        End Select
        'calculaTotal()
    End Sub

    Private Sub agregapago(ByVal codigo As String)
        Dim fila As DataRow = dtpago.NewRow
        Dim buscafila() As DataRow
        buscafila = dsArticulo.Tables("Articulo").Select("codigo='" & codigo & "'")
        fila.Item("cod_pago") = buscafila(0).Item("codigo").ToString
        fila.Item("nom_pago") = buscafila(0).Item("nombre").ToString
        'fila.Item("monto") = ltotal - lrecibido
        fila.Item("propina") = 0
        fila.Item("observacion") = ""
        dtpago.Rows.Add(fila)
        dataPago.CurrentCell = dataPago(dataPago.Columns("monto").Index, dataPago.RowCount - 1)
        dataPago.Focus()
        'calculaTotal()
    End Sub

    Private Sub botonsg_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = Color.LightBlue
    End Sub
    Private Sub botonsg_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        'btn.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(marticulo.buscarcolor(btn.Name, proceso)))
    End Sub

    Private Sub botonsg_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = Color.LightBlue
    End Sub
    Private Sub botonsg_MouseLeave(sender As Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)

        'btn.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(marticulo.buscarcolor(btn.Name, proceso)))
    End Sub
    Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
        Try
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
End Class
