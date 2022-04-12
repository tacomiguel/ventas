Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Math
Imports libreriaVentas
Imports DevComponents.DotNetBar.Keyboard
Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing


Public Class panel_venta1
    'Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Private dsDocumento As New DataSet
    Private dtDocumento_s As New DataTable("documento_s")
    Private dsCliente As New DataSet
    Private dtcliente As New DataTable("cliente")
    Private dsformapago As New DataSet
    Private dtforma_pago As New DataTable("forma_pago")
    Private dsSalida As New DataSet
    Private dtDetalle As New DataTable("detalle")
    Private dsPago As New DataSet
    Private dtpago As New DataTable("pago")
    Private dsArticulo As New DataSet
    Private dtArticulo As New DataTable("articulo")
    Private dsItems As New DataSet
    Private dtitem As New DataTable("items")
    Private virtualKeyboard As KeyboardControl
    Private marticulo As New Articulo
    Private msalida As New Salida, mcliente As New cliente, mdocumento As New Documento
    Private nroOperacion As Integer = 0
    Private fcod_clie As String = "", fcod_vend As String = "", fnro_telefono As String = ""
    Private cod_alma As String = ""
    Private proceso As String
    Private tipoProceso As Char = "A"
    Private estacargando As Boolean = True
    Private codigocombo, codigocombodet, lcodvendedor, lcodAtencion As String
    Private cantidad As Integer = 0
    Private cantidadCombo As Integer = 0
    Private Nmin, Nmax, nroingreso, nropedido As Integer
    Private lvuelto, lrecibido, ltotal As Decimal
    Dim mutilidades As New utilidades

    Private Título As String = "VENTAS"
    Private prtSettings As PrinterSettings
    Private prtDoc As PrintDocument
    Private prtFont As System.Drawing.Font
    Private lineaActual As Integer
    Private formato_tck As Integer = 1
    Private nomImpresora As String
    Private cadenaSQL As String
    '0=Precuenta,1=ticket,2=comanda

    Private Sub cmdCerrar_Click(sender As System.Object, e As System.EventArgs) Handles cmdCerrar.Click
        ' Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
    End Sub

    Private Enum PrinterStatus
        PrinterIdle = 3
        PrinterPrinting = 4
        PrinterWarmingUp = 5
        ' For more states see WMI docs.
    End Enum


    Private Sub pventa_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000  ' Un segundo
        Timer1.Start()
        TabControl1.SelectedTabIndex = 0
        dsDocumento.Tables.Add(dtDocumento_s)
        Dim daDocumento As New MySqlDataAdapter
        Dim comDoc As New MySqlCommand("select cod_doc,nom_doc from documento_s where activo=1 and (esVenta) order by nom_doc", dbConex)
        daDocumento.SelectCommand = comDoc
        daDocumento.Fill(dsDocumento, "documento_s")
        With cboDocumento
            .DataSource = dsDocumento.Tables("documento_s")
            .DisplayMember = dsDocumento.Tables("documento_s").Columns("nom_doc").ToString
            .ValueMember = dsDocumento.Tables("documento_s").Columns("cod_doc").ToString
            .SelectedIndex = 0
        End With
        'dataset cliente
        dsCliente.Tables.Add(dtcliente)
        datasetCliente()
        reiniciaDatos()
        estructuraDetalle()
        estructuraPago()
        AgregaTecladovirtual()
        mostraritems(10, "")
        GeneraNumDocumento()
        estacargando = False




    End Sub
    Sub datasetCliente()
        'dataset cliente
        dsCliente.Clear()
        Dim daCliente As New MySqlDataAdapter, c0, c1, c2 As String
        c1 = "Select cod_clie,nom_clie,raz_soc,dir_clie,fono_clie,dir_ent,nom_cont,nom_rep,cod_tipo,cod_tipo "
        c2 = "from cliente where activo=1 order by raz_soc"
        c0 = c1 + c2
        Dim comClie As New MySqlCommand(c0, dbConex)
        daCliente.SelectCommand = comClie
        daCliente.Fill(dsCliente, "cliente")
        With cboCliente
            .DataSource = dsCliente.Tables("cliente")
            .DisplayMember = dsCliente.Tables("cliente").Columns("raz_soc").ToString
            .ValueMember = dsCliente.Tables("cliente").Columns("cod_clie").ToString
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.ListItems
            .SelectedIndex = -1
        End With
    End Sub
    Sub AgregaTecladovirtual()
        virtualKeyboard = New KeyboardControl()
        With virtualKeyboard
            .Width = 600
            .Height = 200
            teclado.Size = New Size(595, 0)
            '.Location = New Point(0, 0)
            .IsTopBarVisible = False
            .Visible = True
            '.AttachTo(txtRuc)
        End With
        teclado.Controls.Add(virtualKeyboard)
    End Sub


    Private Sub mostraritems(ByVal tipoSQL As Integer, ByVal codigo As String)
        Dim objconexion As MySqlConnection
        objconexion = Conexion.obtenerConexion()
        Dim da As New MySqlDataAdapter
        Dim strSQL As String = ""
        ' PanelSG.Controls.Clear()
        Select Case tipoSQL
            Case 10
                tabventa.Text = "GRUPO DE VENTA"
                strSQL = ("select cod_grupo as codigo,nom_grupo as nombre,isnull(foto) as vnul,foto,b_color,f_color,f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto " _
                          & " from grupo where activo and esVenta")
                proceso = "G"
            Case 20
                tabventa.Text = "GRUPO DE VENTA"
                strSQL = ("select cod_sgrupo as codigo,nom_sgrupo as nombre,isnull(foto) as vnul,foto,b_color,f_color,f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto " _
                          & " from subgrupo where activo and esVenta and cod_grupo='" & codigo & "'")
                proceso = "G"
                panelitem.Controls.Clear()
            Case 0
                tabventa.Text = "GRUPO DE VENTA"
                strSQL = ("select cod_sgrupo as codigo,nom_sgrupo as nombre,isnull(foto) as vnul,foto,b_color,f_color,f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto " _
                          & " from subgrupo where activo and esVenta")
                proceso = "G"
                panelitem.Controls.Clear()
            Case 1
                tabventa.Text = "ITEMS DE VENTA"
                strSQL = ("select cod_art as codigo,nom_art as nombre,isnull(a.foto) as vnul,a.foto,if(length(a.b_color)>0,a.b_color,s.b_color) as b_color,f_color,f_tamano,pre_venta,afecto_igv,a.b_ancho,a.b_alto " _
                          & " from articulo a inner join subgrupo s on a.cod_grupov=s.cod_sgrupo " _
                          & " inner join tipo_articulo t on a.cod_tart=t.cod_tart " _
                          & " where a.activo and (t.esProductoterminado or t.esProduccion or t.esCombo) and a.cod_grupov='" & codigo & "'")
                proceso = "A"
                panelitem.Controls.Clear()
            Case 2
                tabventa.Text = "ITEMS DE VENTA"
                strSQL = ("select cod_art as codigo,nom_art as nombre,isnull(a.foto) as vnul,a.foto,s.b_color,f_color,f_tamano,pre_venta,afecto_igv,a.b_ancho,a.b_alto " _
                          & " from articulo a inner join subgrupo s on a.cod_grupov=s.cod_sgrupo " _
                          & " inner join tipo_articulo t on a.cod_tart=t.cod_tart " _
                          & " where a.activo and (t.esProductoterminado or t.esProduccion or t.esCombo) and nom_art like '%" & txtinput.Text & "%'")
                proceso = "A"
                panelitem.Controls.Clear()
            Case 3
                tabventa.Text = "COMBOS"
                strSQL = "select a.cod_art as codigo,concat(space(5),nom_art) as nombre,isnull(a.foto) as vnul,b_color,f_color,f_tamano,a.foto,0.00 as pre_venta,afecto_igv,a.b_ancho,a.b_alto" _
                    & " from articulo a inner join tipo_articulo t on a.cod_tart=t.cod_tart " _
                    & " inner join subgrupo s on a.cod_grupov=s.cod_sgrupo " _
                    & " inner join receta r on r.cod_art=a.cod_art " _
                    & " where a.activo and (t.esProductoTerminado) and cod_rec='" & codigo & "'"
                proceso = "A"
                panelitem.Controls.Clear()
            Case 4
                tabventa.Text = "FORMAS DE PAGO"
                strSQL = ("select cod_fpago as codigo,nom_fpago as nombre, isnull(foto) as vnul,foto,b_color, f_color,f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                        & " from forma_pago where activo=1 and b_ancho>0")
                proceso = "P"
            Case 40
                tabventa.Text = "FORMAS DE PAGO"
                strSQL = ("select ope_ped as codigo,concat(dsc_mesa,' ',raz_soc) as nombre,isnull(foto) as vnul,foto,b_color,f_color,f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto  " _
                          & " from salida inner join mesas on cod_mesa =cAux2 inner join cliente on salida.cod_clie=cliente.cod_clie where !cancelado ")
                proceso = "P1"

            Case 5
                tabventa.Text = "COMBOS"
                strSQL = "select cod_combo as codigo,dsc_combo as nombre,1 as vnul,foto,b_color,'&HEFFBF2' as f_color,8 as f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto " _
                          & " from art_combo where activo and cod_art='" & codigo & "'"
                proceso = "C"
                panelitem.Controls.Clear()
            Case 6
                tabventa.Text = "DETALLE COMBOS"
                strSQL = "select a.cod_art as codigo,concat(space(5),nom_art) as nombre,isnull(a.foto) as vnul,s.b_color,f_color,f_tamano,a.foto,precio as pre_venta,afecto_igv,a.b_ancho,a.b_alto" _
                & " from articulo a inner join tipo_articulo t on a.cod_tart=t.cod_tart " _
                & " inner join art_combodet c on c.cod_art=a.cod_art " _
                & " inner join subgrupo s on a.cod_sgrupo=s.cod_sgrupo " _
                & " where a.activo and (t.esProductoTerminado) and cod_combo='" & codigo & "'"
                proceso = "B"
                panelitem.Controls.Clear()
            Case 7
                tabventa.Text = "VENDEDOR"
                strSQL = "select cod_vend as codigo,nom_vend as nombre,isnull(foto) as vnul,foto, b_color,'&HEFFBF2' as f_color,8 as f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                           & " from vendedor v inner join usuario u on v.cod_vend=u.cuenta"
                cadenaSQL = strSQL
                proceso = "V1"
                panelitem.Controls.Clear()
            Case 8
                tabventa.Text = "ATENCION"
                strSQL = "select cod_recurso as codigo,dsc_recurso as nombre,isnull(foto) as vnul,foto,b_color,'&HEFFBF2' as f_color,8 as f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                           & " from tipo_recurso where cod_tabla='10'"
                cadenaSQL = strSQL
                proceso = "V2"
                panelitem.Controls.Clear()
            Case 9
                tabventa.Text = "PAX"
                codigo = "P000"
                strSQL = ("select cod_art as codigo,nom_art as nombre,isnull(a.foto) as vnul,a.foto,if(length(a.b_color)>0,a.b_color,s.b_color) as b_color,f_color,f_tamano,pre_venta,afecto_igv,a.b_ancho,a.b_alto " _
                          & " from articulo a inner join subgrupo s on a.cod_grupov=s.cod_sgrupo " _
                          & " inner join tipo_articulo t on a.cod_tart=t.cod_tart " _
                          & " where a.activo and a.cod_grupov='" & codigo & "'")
                proceso = "V3"
                panelitem.Controls.Clear()


            Case Else
        End Select

        Dim cmd As New MySqlCommand(strSQL, objconexion)
        da.SelectCommand = cmd
        If proceso = "P" Then
            dsPago.Clear()
            da.Fill(dsPago, "mipago")
        Else
            dsArticulo.Clear()
            da.Fill(dsArticulo, "articulo")
        End If


        Dim mycomand As New MySqlCommand(strSQL, objconexion)
        Dim myreader As MySqlDataReader
        'panelitem.Controls.Clear()

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
                    .BackColor = System.Drawing.ColorTranslator.FromOle(CInt(myreader("b_color")))
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(myreader("f_color")))
                    .Font = New System.Drawing.Font("Arial Rounded MT", myreader("f_tamano"), FontStyle.Bold)
                    .Text = myreader("nombre")
                    .Name = myreader("codigo")
                End With

                Select Case tipoSQL

                    Case 10
                        PanelG.Controls.Add(boton)
                        AddHandler boton.Click, AddressOf Me.botong_Click
                        AddHandler boton.GotFocus, AddressOf Me.botonsg_GotFocus
                        'AddHandler boton.LostFocus, AddressOf Me.botonA_LostFocus
                        AddHandler boton.MouseMove, AddressOf Me.botonsg_MouseMove
                        'AddHandler boton.MouseLeave, AddressOf Me.botonA_MouseLeave


                    Case 20

                        PanelSG.Controls.Add(boton)
                        AddHandler boton.Click, AddressOf Me.botonsg_Click
                        AddHandler boton.GotFocus, AddressOf Me.botonsg_GotFocus
                        AddHandler boton.LostFocus, AddressOf Me.botonA_LostFocus
                        AddHandler boton.MouseMove, AddressOf Me.botonsg_MouseMove
                        AddHandler boton.MouseLeave, AddressOf Me.botonA_MouseLeave
                    Case 4
                        PanelSG.Controls.Add(boton)
                        AddHandler boton.Click, AddressOf Me.botonp_Click
                        AddHandler boton.GotFocus, AddressOf Me.botonsg_GotFocus
                        'AddHandler botong.LostFocus, AddressOf Me.botonsg_LostFocus
                        AddHandler boton.MouseMove, AddressOf Me.botonsg_MouseMove
                        'AddHandler botong.MouseLeave, AddressOf Me.botonsg_MouseLeave
                    Case 40
                        panelitem.Controls.Add(boton)
                        AddHandler boton.Click, AddressOf Me.botonb_Click
                        AddHandler boton.GotFocus, AddressOf Me.botonsg_GotFocus
                        'AddHandler boton.LostFocus, AddressOf Me.botonsg_LostFocus
                        AddHandler boton.MouseMove, AddressOf Me.botonsg_MouseMove
                        'AddHandler boton.MouseLeave, AddressOf Me.botonsg_MouseLeave
                    Case Else
                        panelitem.Controls.Add(boton)
                        AddHandler boton.Click, AddressOf Me.botoni_Click
                        AddHandler boton.GotFocus, AddressOf Me.botonsg_GotFocus
                        AddHandler boton.LostFocus, AddressOf Me.botonsg_LostFocus
                        AddHandler boton.MouseMove, AddressOf Me.botonsg_MouseMove
                        AddHandler boton.MouseLeave, AddressOf Me.botonsg_MouseLeave
                End Select

            End While
            myreader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'dbConex.Close()
        End Try
    End Sub
    Private Sub botonA_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(marticulo.buscarcolorA(btn.Name)))
    End Sub


    Private Sub botonA_MouseLeave(sender As Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(marticulo.buscarcolorA(btn.Name)))
    End Sub

    Private Sub botonsg_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = Color.LightBlue
    End Sub
    Private Sub botonsg_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(marticulo.buscarcolor(btn.Name, proceso)))
    End Sub

    Private Sub botonsg_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = Color.LightBlue
    End Sub
    Private Sub botonsg_MouseLeave(sender As Object, e As System.EventArgs)
        Dim btn As Button = DirectCast(sender, Button)

        btn.BackColor = System.Drawing.ColorTranslator.FromOle(CInt(marticulo.buscarcolor(btn.Name, proceso)))
    End Sub

    Private Sub botong_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado
        Dim btn As Button = DirectCast(sender, Button)
        'panelitem.Controls.Clear()
        PanelSG.Controls.Clear()
        mostraritems(20, btn.Name)
        calculaTotal()

    End Sub
    Private Sub botonsg_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado
        Dim btn As Button = DirectCast(sender, Button)
        'panelitem.Controls.Clear()
        mostraritems(1, btn.Name)
        calculaTotal()

    End Sub
    Private Sub botonp_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado
        Dim btn As Button = DirectCast(sender, Button)
        'nropedido = btn.Name
        'ubicarpedido()
        If lvuelto <= 0 Then
            agregapago(btn.Name)
        End If
        calculaTotal()

    End Sub

    Private Sub botonb_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado
        Dim btn As Button = DirectCast(sender, Button)
        nropedido = btn.Name
        ubicarpedido()
        calculaTotal()

    End Sub

    Private Sub botoni_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Obtenemos el botón pulsado

        If tipoProceso = "A" Then
            Dim btn As Button = DirectCast(sender, Button)

            Select Case proceso
                Case "A"
                    If buscaitem(btn.Name) = False Then
                        agregaitem(btn.Name)
                    End If
                Case "P"
                    If lvuelto <= 0 Then
                        agregapago(btn.Name)
                    End If
                Case "G"
                    mostraritems(1, btn.Name)
                Case "B"
                    If buscaCombo(btn.Name) = False Then
                        agregacombo(btn.Name)
                    End If
                Case "C"
                    If marticulo.existeCombodet(btn.Name) Then
                        mostraritems(6, btn.Name)
                        Nmin = marticulo.comboMin(btn.Name)
                        Nmax = marticulo.comboMax(btn.Name)
                        codigocombodet = btn.Name
                    End If
                Case "V1"
                    capturaITEM(btn.Name, cadenaSQL, 1)
                    'capturavendedor(btn.Name)
                Case "V2"
                    capturaITEM(btn.Name, cadenaSQL, 2)
                Case "V3"
                    If buscaitem(btn.Name) = False Then
                        agregaitem(btn.Name)
                    End If
            End Select
            calculaTotal()
        End If
    End Sub
    Private Function buscaitem(ByVal codigo As String) As Boolean
        Dim row As DataGridViewRow
        Dim resp As Boolean = False
        For Each row In dataDetalle.Rows
            If marticulo.existeCombo(codigo) = False Then
                If row.Cells("cod_art").Value = codigo Then
                    If marticulo.existeDescuento(codigo) = False Then
                        If row.Cells("estado").Value = "I" Then
                            row.Cells("cantidad").Value = row.Cells("cantidad").Value + 1
                            calculadetalle()
                            calculaTotal()
                            resp = True
                            Exit For
                        End If
                    End If
                End If
            End If
        Next
        Return resp
    End Function
    Private Function buscaCombo(ByVal codigo As String) As Boolean
        Dim row As DataGridViewRow
        Dim resp As Boolean = False
        Dim valor As Integer
        valor = dataDetalle("ingreso", dataDetalle.CurrentRow.Index).Value
        For Each row In dataDetalle.Rows

            If row.Cells("cod_art").Value = codigo And row.Cells("ingreso").Value = valor Then
                'row.Selected = True
                'dataDetalle.FirstDisplayedScrollingRowIndex = row.Index
                If row.Cells("estado").Value = "C" Then
                    row.Cells("cantidad").Value = row.Cells("cantidad").Value + 1
                    cantidad = cantidad + 1
                    If cantidadCombo * Nmax <= cantidad Then
                        marticulo.actualizaEstadoCombo(codigocombodet, 0, True)
                        mostraritems(5, codigocombo)
                        cantidad = 0
                    End If

                    If (marticulo.existeCombo(codigocombo)) = False Then
                        marticulo.actualizaEstadoCombo(codigocombo, 1, False)
                        mostraritems(0, "")
                        modoHabilitado(True)
                    End If
                    calculadetalle()
                    calculaTotal()
                    resp = True
                    Exit For
                End If

            End If

        Next
        Return resp
    End Function
    Private Sub agregaitem(ByVal codigo As String)

        Dim fila As DataRow = dtDetalle.NewRow
        Dim buscafila() As DataRow
        buscafila = dsArticulo.Tables("Articulo").Select("codigo='" & codigo & "'")
        fila.Item("cod_art") = buscafila(0).Item("codigo").ToString
        fila.Item("nom_art") = buscafila(0).Item("nombre").ToString
        fila.Item("afecto_igv") = buscafila(0).Item("afecto_igv")
        If (marticulo.existeDescuento(codigo)) = True Then
            Dim rpta As Integer
            nroingreso = nroingreso + 1
            rpta = MessageBox.Show("Descuento al Item..(SI) o Descuento al Documento..(NO).", "SGA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If rpta = 6 Then
                Dim mfila As Integer = dataDetalle.CurrentRow.Index

                fila.Item("precio") = dataDetalle("precio", mfila).Value * (buscafila(0).Item("pre_venta").ToString / 100)
                fila.Item("cantidad") = -1.0 * dataDetalle("cantidad", mfila).Value
            ElseIf rpta = 7 Then
                fila.Item("precio") = txtTotal.Text * (buscafila(0).Item("pre_venta").ToString / 100)
                fila.Item("cantidad") = -1.0
            End If


        Else
            fila.Item("precio") = buscafila(0).Item("pre_venta").ToString
            fila.Item("cantidad") = IIf(Char.IsDigit(txtinput.Text), txtinput.Text, 1.0)
        End If
        fila.Item("neto") = fila.Item("cantidad") * fila.Item("precio")
        fila.Item("ingreso") = nroingreso
        nroingreso = nroingreso + 1
        If buscafila(0).Item("afecto_igv").ToString = True Then
            fila.Item("igv") = pIGV
        Else
            fila.Item("igv") = 0
        End If
        fila.Item("comi_v") = 1
        fila.Item("comi_jv") = 0
        If proceso = "C" Then
            fila.Item("estado") = "C"
        Else
            fila.Item("estado") = "I"
        End If
        dtDetalle.Rows.Add(fila)
        If (marticulo.existeCombo(codigo)) = True Then
            codigocombo = codigo
            cantidadCombo = fila.Item("cantidad")
            mostraritems(5, codigocombo)
            modoHabilitado(False)
        End If
        txtinput.Text = ""
        dataDetalle.CurrentCell = dataDetalle(dataDetalle.Columns("cantidad").Index, dataDetalle.RowCount - 1)
        dataDetalle.Focus()
        calculaTotal()

    End Sub

    Private Sub agregacombo(ByVal codigo As String)
        Dim fila As DataRow = dtDetalle.NewRow
        Dim buscafila() As DataRow
        Dim cantidad_ing As Decimal

        buscafila = dsArticulo.Tables("Articulo").Select("codigo='" & codigo & "'")
        fila.Item("cod_art") = buscafila(0).Item("codigo").ToString
        fila.Item("nom_art") = buscafila(0).Item("nombre").ToString
        'fila.Item("nom_uni") = buscafila(0).Item("nom_uni").ToString
        fila.Item("precio") = buscafila(0).Item("pre_venta").ToString
        'IIf(txtinput.Text > (cantidadCombo * Nmax) - cantidad, (cantidadCombo * Nmax) - cantidad, txtinput.Text)
        If Char.IsDigit(txtinput.Text) Then
            If txtinput.Text > (cantidadCombo * Nmax) - cantidad Then
                cantidad_ing = (cantidadCombo * Nmax) - cantidad
            Else
                cantidad_ing = txtinput.Text
            End If
        End If
        cantidad_ing = IIf(Char.IsDigit(txtinput.Text), cantidad_ing, 1.0)
        fila.Item("cantidad") = cantidad_ing

        cantidad = cantidad + fila.Item("cantidad")
        fila.Item("neto") = fila.Item("cantidad") * fila.Item("precio")
        fila.Item("afecto_igv") = buscafila(0).Item("afecto_igv")
        fila.Item("ingreso") = nroingreso - 1
        If buscafila(0).Item("afecto_igv").ToString = True Then
            fila.Item("igv") = pIGV
        Else
            fila.Item("igv") = 0
        End If
        fila.Item("comi_v") = 1
        fila.Item("comi_jv") = 0
        fila.Item("estado") = "C"
        dtDetalle.Rows.Add(fila)
        txtinput.Text = ""
        dataDetalle.CurrentCell = dataDetalle(dataDetalle.Columns("cantidad").Index, dataDetalle.RowCount - 1)
        dataDetalle.Focus()

        If cantidadCombo * Nmax <= cantidad Then
            marticulo.actualizaEstadoCombo(codigocombodet, 0, True)
            mostraritems(5, codigocombo)
            cantidad = 0

        End If

        If (marticulo.existeCombo(codigocombo)) = False Then
            marticulo.actualizaEstadoCombo(codigocombo, 1, False)
            ' mostraritems(0, "")
            modoHabilitado(True)
        End If

    End Sub

    Private Sub agregapago(ByVal codigo As String)
        If dataDetalle.RowCount > 0 Then
            Dim fila As DataRow = dtpago.NewRow
            Dim buscafila() As DataRow
            buscafila = dsPago.Tables("mipago").Select("codigo='" & codigo & "'")
            fila.Item("cod_pago") = buscafila(0).Item("codigo").ToString
            fila.Item("nom_pago") = buscafila(0).Item("nombre").ToString
            fila.Item("monto") = ltotal - lrecibido
            fila.Item("propina") = 0
            fila.Item("observacion") = ""
            dtpago.Rows.Add(fila)
            txtinput.Text = ""
            dataPago.CurrentCell = dataPago(dataPago.Columns("monto").Index, dataPago.RowCount - 1)
            dataPago.Focus()
            calculaTotal()
        End If

    End Sub
    Private Sub capturaITEM(ByVal codigo As String, ByVal cadenaSQL As String, ByVal tipobtn As Integer)
        Dim fila As DataRow = dtDetalle.NewRow
        Dim buscafila() As DataRow
        Dim Imag As Byte() = Nothing
        If codigo = "" Then
            lcodvendedor = pCuentaUser
            lcodAtencion = "101"
            cmdmozo.Text = ""
            cmdAtencion.Text = "SALON"
            cmdmozo.Image = Bytes_Imagen(Imag)
        Else
            Dim objconexion As MySqlConnection
            objconexion = Conexion.obtenerConexion()
            Dim da As New MySqlDataAdapter
            Dim cmd As New MySqlCommand(cadenaSQL, objconexion)
            da.SelectCommand = cmd
            dsItems.Clear()
            da.Fill(dsItems, "items")
            buscafila = dsItems.Tables("items").Select("codigo='" & codigo & "'")
            Select Case tipobtn
                Case 1
                    If buscafila(0).Item("vnul") = 0 Then
                        Imag = buscafila(0).Item("foto")
                        cmdmozo.Image = Bytes_Imagen(Imag)
                    Else
                        cmdmozo.Image = Bytes_Imagen(Imag)
                    End If
                    cmdmozo.Text = buscafila(0).Item("nombre")
                    lcodvendedor = codigo
                Case 2
                    If buscafila(0).Item("vnul") = 0 Then
                        Imag = buscafila(0).Item("foto")
                        cmdAtencion.Image = Bytes_Imagen(Imag)
                    Else
                        cmdAtencion.Image = Bytes_Imagen(Imag)
                    End If
                    cmdAtencion.Text = buscafila(0).Item("nombre")
                    lcodAtencion = codigo

            End Select
        End If

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
    Sub estructuraDetalle()
        dsSalida = Salida.dsSalida
        dtDetalle = dsSalida.Tables("detalle")
        With dataDetalle
            .DataSource = dsSalida.Tables("detalle")
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("nom_art").HeaderText = "DESCRIPCION"
            .Columns("nom_art").Width = 350
            .Columns("nom_art").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("nom_art").ReadOnly = True
            .Columns("nom_art").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("cantidad").HeaderText = "CANT."
            .Columns("cantidad").Width = 60
            .Columns("cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cantidad").ReadOnly = False
            .Columns("cantidad").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("cantidad").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("precio").HeaderText = "PRECIO"
            .Columns("precio").Width = 70
            .Columns("precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("precio").ReadOnly = True
            .Columns("precio").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("precio").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("neto").HeaderText = "TOTAL"
            .Columns("neto").Width = 74
            .Columns("neto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("neto").ReadOnly = True
            .Columns("neto").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("neto").SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns("obs").Visible = False
            .Columns("cod_art").Visible = False
            .Columns("afecto_igv").Visible = False
            .Columns("nom_uni").Visible = False
            .Columns("orden").Visible = False
            .Columns("operacion").Visible = False
            .Columns("ingreso").Visible = False
            .Columns("salida").Visible = False
            .Columns("saldo").Visible = False
            .Columns("igv").Visible = False
            .Columns("comi_v").Visible = False
            .Columns("comi_jv").Visible = False
            .Columns("estado").Visible = False
            .Columns("proceso").Visible = False
        End With
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
    Sub calculaTotal()
        Dim nroReg As Integer = 0, I As Integer = 0
        Dim lsubTotal As Decimal = 0, ligv As Decimal = 0, lmTotal As Decimal = 0
        ltotal = 0.0
        lrecibido = 0.0
        lvuelto = 0.0
        nroReg = dataDetalle.RowCount
        For I = 0 To nroReg - 1
            lsubTotal = lsubTotal + (dataDetalle("cantidad", I).Value * dataDetalle("precio", I).Value)
            If dataDetalle("afecto_igv", I).Value = True Then
                lmTotal = lmTotal + (dataDetalle("cantidad", I).Value * dataDetalle("precio", I).Value)
                ligv = dataDetalle("igv", I).Value
            End If
        Next
        'lblItems.Text = "Nro de Items. " & Str(I)
        If chkIGV.Checked = True Then
            txtTotal.Text = Round(lsubTotal, pDecimales)
        Else
            ligv = ligv * lmTotal
            txtTotal.Text = Round(lsubTotal, pDecimales) + Round(ligv, pDecimales)
        End If


        nroReg = dataPago.RowCount
        If nroReg > 0 Then
            For I = 0 To nroReg - 1
                lrecibido = lrecibido + (dataPago("monto", I).Value)
            Next
            txtrecibido.Text = Round(lrecibido, pDecimales)
            txtvuelto.Text = Round((lrecibido - lsubTotal), pDecimales)
        Else
            txtrecibido.Text = 0.0
            txtvuelto.Text = 0.0
        End If
        ltotal = txtTotal.Text
        lrecibido = txtrecibido.Text
        lvuelto = txtvuelto.Text
        txtTotal1.Text = ltotal
    End Sub

    Private Sub cmdinicio_Click(sender As System.Object, e As System.EventArgs) Handles cmdInicio.Click
        mostraritems(0, "")

    End Sub

    Private Sub PbBuscar_Click(sender As System.Object, e As System.EventArgs) Handles pbBuscar.Click
        mostraritems(2, "")
    End Sub
    Private Sub eliminaritem()
        Dim rpta As Integer
        Dim valor As Integer
        Dim periodo As String = general.convierteFechaEspecificadaMes(pFechaSystem)
        If validaDetalleAnulacion() Then
            If dataDetalle.RowCount() > 0 Then
                rpta = MessageBox.Show("Esta Seguro de Eliminar el ITEM Seleccionado" + Chr(13) + "Este Proceso es Irreversible...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2)
                If rpta = 6 Then

                    valor = dataDetalle("ingreso", dataDetalle.CurrentRow.Index).Value

                    For Each row In dataDetalle.Rows
                        If row.Cells("ingreso").Value = valor Then
                            row.Selected = True
                        End If
                    Next

                    Dim value As DataGridViewSelectedRowCollection
                    value = dataDetalle.SelectedRows

                    For Each vfila As DataGridViewRow In value
                        dataDetalle.Rows.Remove(vfila)

                    Next
                    If (msalida.existeOperacion(nroOperacion, 0)) Then
                        msalida.AnularDetalle(False, periodo, nroOperacion, valor, -1, 1)
                        grabadetalle()
                        imprimircomanda(nroOperacion)
                        msalida.AnularDetalle(False, periodo, nroOperacion, valor, 0, 0)

                    End If

                    coloreaitem()
                    marticulo.actualizaEstadoCombo(codigocombo, 1, False)
                    mostraritems(0, "")
                    modoHabilitado(True)

                End If
            End If
        End If
        calculaTotal()
    End Sub
    Private Sub eliminarpago()
        Dim rpta As Integer
        If validaDetalleAnulacion() Then
            If dataPago.RowCount() > 0 Then
                rpta = MessageBox.Show("Esta Seguro de Eliminar el PAGO Seleccionado" + Chr(13) + "Este Proceso es Irreversible...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2)
                If rpta = 6 Then
                    Dim mfila As DataRow = dtpago.Rows(dataPago.CurrentRow.Index)
                    dtpago.Rows.Remove(mfila)
                End If
            End If
        End If
    End Sub

    Sub coloreaitem()
        For Each row As DataGridViewRow In dataDetalle.Rows
            If Not IsDBNull(row.Cells("cod_art").Value) Then
                If row.Cells("cantidad").Value = 0 Then
                    row.DefaultCellStyle.ForeColor = Color.Red
                Else
                    row.DefaultCellStyle.ForeColor = Color.Black
                End If
            End If
        Next
    End Sub
    Private Sub cmdGrabar_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrabar.Click
        Dim continuar As Boolean = True, grabaGuia As Boolean = True
        Dim ser_doc, nro_doc As String
        Dim rpta As Integer = 6

        'validamos el documento
        Try

            If lrecibido >= ltotal Then
                formato_tck = 1

                If tipoProceso = "E" Then 'Edicion
                    SetDefaultPrinter(mdocumento.NombreImpresora(cboDocumento.SelectedValue))
                    imprimirdoc(pvistaprevia)
                    modoHabilitado(True)
                    txtcod_mesa.Text = ""
                Else
                    If validaIngreso() Then
                        GeneraNumDocumento()
                        ser_doc = txtSerDocumento.Text
                        nro_doc = txtNroDocumento.Text
                        grabadocumento(ser_doc, nro_doc)
                        formato_tck = IIf(cboDocumento.SelectedValue = "99", 0, 1)
                        SetDefaultPrinter(mdocumento.NombreImpresora(cboDocumento.SelectedValue))
                        imprimirdoc(pvistaprevia)
                        modoHabilitado(True)
                        txtcod_mesa.Text = ""
                    End If
                End If
                'If ltotal > 20 Then
                '    formato_tck = 3
                '    imprimirdoc(pvistaprevia)
                'End If
                'rpta = MessageBox.Show("Desea Enviar el Pedido a Produccion...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If rpta = 6 Then
                    imprimircomanda(nroOperacion)
                End If
                reiniciaDatos()
                reiniciaCliente()
                GeneraNumDocumento()
            Else
                If Not (msalida.existeOperacion(nroOperacion, 0)) Then
                    MessageBox.Show("Falta Registrar PAGO...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cboCliente.Focus()
                Else
                    rpta = MessageBox.Show("Falta Registrar PAGO..." + Chr(13) + "Desea Imprmimir una Pre-Cuenta...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If rpta = 6 Then
                        formato_tck = 0
                        ser_doc = ""
                        nro_doc = ""
                        grabacomanda(ser_doc, nro_doc)
                        SetDefaultPrinter(mdocumento.NombreImpresora("99"))
                        imprimirdoc(pvistaprevia)
                        ubicarmesa()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Function validaIngreso()
        Dim validado As Boolean = False
        If dataDetalle.RowCount > 0 Then
            validado = True
            If cboDocumento.Text = "FACTURA" Then
                validado = False
                If Len(txtRuc.Text) > 0 Then
                    If Len(cboCliente.Text) > 0 Then
                        validado = True
                    Else
                        MessageBox.Show("Ingrese la Razon Social  del Cliente...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        cboCliente.Focus()
                    End If
                Else
                    MessageBox.Show("Ingrese el RUC del Cliente...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtRuc.Focus()
                End If
            End If
        Else
            MessageBox.Show("Falta Registrar Artículos...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroDocumento.Focus()
        End If
        Return validado
    End Function

    Sub grabacomanda(ByVal ser_doc As String, ByVal nro_doc As String)
        Dim mcliente As New cliente, mutilidades As New utilidades
        Dim cod_doc, cod_fpago, cod_mesa, nom_cliente, tm As String, fec_doc As Date, inc_igv As Integer
        '            'el cliente y vendedor lo recuperamos al cargar el pedido
        cod_doc = cboDocumento.SelectedValue
        fec_doc = pFechaSystem
        fcod_clie = txtRuc.Text
        fcod_vend = IIf(lcodvendedor = "", pCuentaUser, lcodvendedor)
        cod_fpago = "01"
        cod_mesa = IIf(txtcod_mesa.Text = "", "999", txtcod_mesa.Text)
        nom_cliente = cboCliente.Text
        Dim ndias As Integer = mutilidades.devuelvediasFormaPago(cod_fpago)
        cod_alma = pAlmaUser

        If fcod_clie.Length = 0 And nom_cliente.Length > 0 Then
            fcod_clie = mcliente.maxCodigoClie
        End If
        If Not mcliente.existe(fcod_clie, 0) Then
            mcliente.insertar(fcod_clie, cboCliente.Text, cboCliente.Text, TxtDirfiscal.Text, TxtDirfiscal.Text, txtContacto.Text, txtTelefono.Text, "00", 1, 0)
        End If

        If Not (msalida.existeOperacion(nroOperacion, 0)) Then
            nroOperacion = msalida.maxOperacion(0)
            nropedido = msalida.maxPedido
            'registramos la cabecera

            If pPreciosIncIGV Then
                inc_igv = 1
            Else
                inc_igv = 0
            End If
            tm = pMonedaAbr
            msalida.insertar_aux(nroOperacion, nropedido, cod_doc, ser_doc, nro_doc, fec_doc, fcod_vend, fcod_clie, cod_fpago, 0, inc_igv, pDecimales, cod_alma, "0000", txtContacto.Text, pEmpresa, pCuentaUser, 0, "12", cod_mesa, lcodAtencion, txtContacto.Text, 0)
            grabadetalle()
        Else
            msalida.Actualiza_documento(nroOperacion, 0, pFechaSystem, cod_doc, "12", ser_doc, nro_doc, pCuentaUser, fcod_clie)
            grabadetalle()
        End If
        msalida.Actualiza_montopago(nroOperacion, 0)
    End Sub
    Sub grabadocumento(ByVal ser_doc As String, ByVal nro_doc As String)
        Dim mcliente As New cliente, mutilidades As New utilidades
        Dim cod_doc, cod_fpago, nom_cliente, cod_mesa, tm As String, fec_doc As Date, inc_igv As Integer
        '            'el cliente y vendedor lo recuperamos al cargar el pedido

        fec_doc = pFechaSystem
        fcod_clie = txtRuc.Text
        nom_cliente = cboCliente.Text
        fcod_vend = IIf(lcodvendedor = "", pCuentaUser, lcodvendedor)
        cod_fpago = dataPago("cod_pago", 0).Value
        cod_mesa = txtcod_mesa.Text
        Dim ndias As Integer = mutilidades.devuelvediasFormaPago(cod_fpago)
        Dim lcancelado As Integer = IIf(ndias > 0, 0, 1)
        cod_alma = pAlmaUser

        cod_doc = IIf(ndias < 0, "99", cboDocumento.SelectedValue)
        nro_doc = IIf(ndias < 0, Microsoft.VisualBasic.Right("00000000" & Trim(Str(Microsoft.VisualBasic.Val(nropedido))), 8), msalida.maxNroSalida("12", ser_doc))
        cboDocumento.SelectedValue = cod_doc

        If fcod_clie.Length = 0 And nom_cliente.Length > 0 Then
            fcod_clie = mcliente.maxCodigoClie
        End If
        If Not mcliente.existe(fcod_clie, 0) Then
            mcliente.insertar(fcod_clie, cboCliente.Text, cboCliente.Text, TxtDirfiscal.Text, TxtDirfiscal.Text, txtContacto.Text, txtTelefono.Text, "00", 1, 0)
        End If

        If Not (msalida.existeOperacion(nroOperacion, 0)) Then
            nroOperacion = msalida.maxOperacion(0)
            nropedido = msalida.maxPedido()
            'registramos la cabecera
            nro_doc = IIf(ndias < 0, Microsoft.VisualBasic.Right("00000000" & Trim(Str(Microsoft.VisualBasic.Val(nropedido))), 8), msalida.maxNroSalida("12", ser_doc))
            If pPreciosIncIGV Then
                inc_igv = 1
            Else
                inc_igv = 0
            End If

            msalida.insertar_aux(nroOperacion, nropedido, cod_doc, ser_doc, nro_doc, fec_doc, fcod_vend, fcod_clie, "01", lcancelado, inc_igv, pDecimales, cod_alma, "0000", txtObservacion.Text, pEmpresa, pCuentaUser, 0, IIf(cod_doc = "99", "99", "12"), cod_mesa, lcodAtencion, txtContacto.Text, 0)
            tm = pMonedaAbr
            grabadetalle()
            grabarpago()
            'End If
        Else
            msalida.Actualiza_documento(nroOperacion, 1, pFechaSystem, cod_doc, IIf(cod_doc = "99", "99", "12"), ser_doc, nro_doc, pCuentaUser, fcod_clie)
            grabadetalle()
            grabarpago()
        End If

    End Sub
    Sub grabadetalle()
        Dim cod_art, obs As String, I, nroSalida, nroIngreso As Integer, cant, precio, comi_v, comi_jv, igv As Decimal
        For I = 0 To dataDetalle.RowCount - 1
            If dataDetalle("comi_v", I).Value = 1 Then
                cod_art = dataDetalle("cod_art", I).Value
                cant = dataDetalle("cantidad", I).Value
                precio = dataDetalle("precio", I).Value
                comi_v = 0
                comi_jv = dataDetalle("comi_jv", I).Value
                'obs = IIf(IsDBNull(dataDetalle("obs", I).Value), "", dataDetalle("obs", I).Value)
                obs = dataDetalle("nom_art", I).Value
                nroIngreso = dataDetalle("ingreso", I).Value
                If dataDetalle("afecto_igv", I).Value = False Then
                    igv = 0
                Else
                    igv = pIGV
                End If
                nroSalida = msalida.maxSalida
                msalida.insertar_detObs(nroOperacion, nroSalida, nroIngreso, cod_art, cant, precio, igv, comi_v, comi_jv, obs, 1, 0)
            End If
        Next

    End Sub
    Sub grabarpago()
        Dim imp_pago, imp_propina, imp_vuelto As Decimal
        Dim obs, cod_fpago As String
        Dim imp_total = txtTotal.Text

        For I = 0 To dataPago.RowCount - 1
            cod_fpago = dataPago("cod_pago", I).Value
            imp_pago = dataPago("monto", I).Value
            imp_propina = dataPago("propina", I).Value
            If cod_fpago = "06" And lvuelto > 0 Then
                imp_vuelto = lvuelto
                lvuelto = 0
            Else
                imp_vuelto = 0
            End If
            obs = IIf(IsDBNull(dataPago("observacion", I).Value), "", dataPago("observacion", I).Value)
            msalida.insertar_detPago(nroOperacion, cod_fpago, imp_pago, imp_propina, imp_vuelto, obs, 0)
        Next
        msalida.Actualiza_montopago(nroOperacion, txtrecibido.Text)
        mutilidades.ActualizarEstado_Mesas(1, txtcod_mesa.Text, "&H228B22")
    End Sub

    Private Function PrinterStatusToString(ByVal ps As PrinterStatus) As Boolean
        Dim s As String
        Dim resp As Boolean = True
        Select Case ps
            Case PrinterStatus.PrinterIdle
                s = "waiting (idle)"
            Case PrinterStatus.PrinterPrinting
                s = "printing"
            Case PrinterStatus.PrinterWarmingUp
                s = "warming up"
            Case Else ' Vielleicht gibt es noch weitere Fälle...
                s = "Error de Impresora..Verificar"
                resp = False
        End Select
        Return resp
    End Function

    Private Function verificaEstadoPrinter(ByVal nomimpresora As String) As Boolean
        Dim strPrintServer As String
        Dim resp As Boolean = True
        strPrintServer = "localhost"
        Dim WMIObject As String, PrinterSet As Object, Printer As Object
        WMIObject = "winmgmts://" & strPrintServer
        PrinterSet = GetObject(WMIObject).InstancesOf("win32_Printer")
        For Each Printer In PrinterSet
            If Printer.name = nomimpresora Then
                MsgBox(Printer.Name & ": " & PrinterStatusToString(Printer.PrinterStatus))
                resp = PrinterStatusToString(Printer.PrinterStatus)
            End If
        Next Printer
        Return resp
    End Function
    Private Sub imprimirdoc(ByVal esPreview As Boolean)
        ' imprimir o mostrar el PrintPreview

        If prtSettings Is Nothing Then
            prtSettings = New PrinterSettings
        End If

        If prtDoc Is Nothing Then
            prtDoc = New PrintDocument
            AddHandler prtDoc.PrintPage, AddressOf prt_PrintPage
        End If
        '
        ' resetear la línea actual
        lineaActual = 0
        '
        ' la configuración a usar en la impresión
        prtDoc.PrinterSettings = prtSettings
        '
        If esPreview Then
            Dim prtPrev As New PrintPreviewDialog
            prtPrev.Document = prtDoc
            prtPrev.Text = "Previsualizar datos de " & Título
            prtPrev.ShowDialog()
        Else
            prtDoc.Print()
        End If
    End Sub

    Private Sub prt_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        ' Este evento se produce cada vez que se va a imprimir una página
        Dim lineHeight As Single
        'Dim yPos As Single = e.MarginBounds.Top
        Dim yPos As Single = 2
        Dim leftMargin As Single = e.MarginBounds.Left
        Dim printFont As Font
        prtFont = New System.Drawing.Font("Courier ", 8)
        ' Asignar el tipo de letra
        printFont = prtFont
        lineHeight = printFont.GetHeight(e.Graphics)

        Dim fontTitulo As New Font("Arial", 20, FontStyle.Bold)
        yPos += fontTitulo.GetHeight(e.Graphics)


        Select Case formato_tck
            Case 2
                yPos = ImpresionComanda(e, printFont, yPos, nomImpresora)
            Case 3
                yPos = ImpresionPromocion(e, printFont, yPos, nomImpresora)
            Case Else
                yPos = CabeceraImpresion(e, printFont, yPos)
                yPos = lineaImpresion(e, printFont, yPos)
                yPos = DetalleImpresion(e, printFont, yPos, formato_tck)
                yPos = lineaImpresion(e, printFont, yPos)
                yPos = PiedePagina(e, printFont, yPos)

        End Select


        e.HasMorePages = False

    End Sub
    Public Function ImpresionPromocion(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single, ByVal nomimpresora As String) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        Dim leftmargin As Single = 3
        Dim mutilidades As New utilidades
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim midato As String
        Dim importe As Decimal
        Dim ds As DataSet = mutilidades.recuperadetalleComanda(nroOperacion, nomimpresora)
        mitabla = ds.Tables(0)

        sb = New System.Text.StringBuilder
        sb.AppendFormat("{0} ", ajustar(UCase(mitabla.Rows(0)(4)), 40, HorizontalAlignment.Center))
        sb.AppendLine()
        sb.AppendFormat("{0} ", "Chk : " + ajustar(mitabla.Rows(0)(5), 15, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(6), 18, HorizontalAlignment.Right))
        sb.AppendLine()
        sb.AppendFormat("{0} ", ":" + ajustar(mitabla.Rows(0)(14), 15, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(7), 18, HorizontalAlignment.Right))
        'sb.AppendLine()
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
        yPos += (lineHeight * 4)
        yPos = lineaImpresion(e, prFont, yPos)
        sb = New System.Text.StringBuilder
        Dim cabeceras As Integer = 1

        For i As Integer = 0 To mitabla.Rows.Count() - 1

            For j As Integer = 0 To cabeceras
                Select Case j
                    Case 0

                        importe = mitabla.Rows(i)(j)
                        sb.AppendFormat("{0} ", ajustar(importe.ToString("#0.00"), 6, HorizontalAlignment.Right))


                    Case 1
                        'sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(j).ToString(), 30, HorizontalAlignment.Left))

                        Dim micadena() As String = Split(mitabla.Rows(i)(j).ToString(), vbCrLf)
                        Dim anchocolumnas As Integer = 40
                        For x As Integer = 0 To UBound(micadena, 1)
                            sb = New System.Text.StringBuilder
                            sb.AppendFormat("{0} ", ajustar(micadena(x), anchocolumnas, HorizontalAlignment.Left))
                            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                            yPos += lineHeight
                        Next
                        'Case Else
                        '    importe = mitabla.Rows(i)(j)
                        '    sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                End Select

            Next
            'e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            'yPos += (lineHeight * 2)
        Next

        yPos = lineaImpresion(e, prFont, yPos)


        Dim micadena1() As String = Split(mitabla.Rows(0)(8).ToString(), Chr(13))

        Dim anchocolumnas1 As Integer = 120
        For x As Integer = 0 To UBound(micadena1, 1)
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar(micadena1(x), anchocolumnas1, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += lineHeight
        Next
        'End If
        yPos = lineaImpresion(e, prFont, yPos)


        'Datos del delivery
        For x As Integer = 10 To 13
            midato = mitabla.Rows(0)(x)
            If midato.Length > 0 Then
                sb = New System.Text.StringBuilder
                sb.AppendFormat("{0} ", ajustar(": " & midato.ToString, 30, HorizontalAlignment.Left))
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
                yPos += (lineHeight * 2)
            End If
        Next


        Return yPos

    End Function
    Public Function ImpresionComanda(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single, ByVal nomimpresora As String) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        Dim leftmargin As Single = 3
        Dim mutilidades As New utilidades
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim midato As String
        Dim importe As Decimal
        Dim ds As DataSet = mutilidades.recuperadetalleComanda(nroOperacion, nomimpresora)
        mitabla = ds.Tables(0)

        sb = New System.Text.StringBuilder
        sb.AppendFormat("{0} ", ajustar(UCase(mitabla.Rows(0)(4)), 40, HorizontalAlignment.Center))
        sb.AppendLine()
        sb.AppendFormat("{0} ", "Chk : " + ajustar(mitabla.Rows(0)(5), 15, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(6), 18, HorizontalAlignment.Right))
        sb.AppendLine()
        sb.AppendFormat("{0} ", ":" + ajustar(mitabla.Rows(0)(14), 15, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(7), 18, HorizontalAlignment.Right))
        'sb.AppendLine()
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
        yPos += (lineHeight * 4)
        yPos = lineaImpresion(e, prFont, yPos)
        sb = New System.Text.StringBuilder
        Dim cabeceras As Integer = 1

        For i As Integer = 0 To mitabla.Rows.Count() - 1

            For j As Integer = 0 To cabeceras
                Select Case j
                    Case 0

                        importe = mitabla.Rows(i)(j)
                        sb.AppendFormat("{0} ", ajustar(importe.ToString("#0.00"), 6, HorizontalAlignment.Right))


                    Case 1
                        'sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(j).ToString(), 30, HorizontalAlignment.Left))

                        Dim micadena() As String = Split(mitabla.Rows(i)(j).ToString(), vbCrLf)
                        Dim anchocolumnas As Integer = 40
                        For x As Integer = 0 To UBound(micadena, 1)
                            sb = New System.Text.StringBuilder
                            sb.AppendFormat("{0} ", ajustar(micadena(x), anchocolumnas, HorizontalAlignment.Left))
                            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                            yPos += lineHeight
                        Next
                        'Case Else
                        '    importe = mitabla.Rows(i)(j)
                        '    sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                End Select

            Next
            'e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            'yPos += (lineHeight * 2)
        Next

        yPos = lineaImpresion(e, prFont, yPos)

        Dim micadena1() As String = Split(mitabla.Rows(0)(8).ToString(), Chr(13))

        Dim anchocolumnas1 As Integer = 120
        For x As Integer = 0 To UBound(micadena1, 1)
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar(micadena1(x), anchocolumnas1, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += lineHeight
        Next
        'End If
        yPos = lineaImpresion(e, prFont, yPos)


        'sb = New System.Text.StringBuilder
        'sb.AppendFormat("{0} ", ajustar(": " & mitabla.Rows(0)(10).ToString, 30, HorizontalAlignment.Left))
        'e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
        'yPos += (lineHeight * 2)

        'Datos del delivery
        For x As Integer = 10 To 13
            midato = mitabla.Rows(0)(x)
            If midato.Length > 0 Then
                sb = New System.Text.StringBuilder
                sb.AppendFormat("{0} ", ajustar(": " & midato.ToString, 30, HorizontalAlignment.Left))
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
                yPos += (lineHeight * 2)
            End If
        Next


        Return yPos

    End Function


    Public Function CabeceraImpresion(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        'Dim leftMargin As Single = e.MarginBounds.Left
        Dim leftmargin As Single = 5
        Dim mutilidades As New utilidades
        Dim midato As String
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim ds As DataSet = mutilidades.recuperaEmpresa()
        mitabla = ds.Tables(0)

        Dim cabeceras As Integer = 6
        Dim anchocolumnas As Integer = 38

        For i As Integer = 0 To cabeceras
            midato = mitabla.Rows(0)(i)
            If midato.Length > 0 Then
                sb = New System.Text.StringBuilder
                sb.AppendFormat("{0} ", ajustar(midato.ToString(), anchocolumnas, HorizontalAlignment.Center))
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                yPos += (lineHeight * 2)
            End If
        Next

        ds = mutilidades.recuperacabeceraticket(nroOperacion)
        mitabla = ds.Tables(0)
        sb = New System.Text.StringBuilder
        If formato_tck = 1 Then
            sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(0).ToString(), 22, HorizontalAlignment.Left))
        Else
            sb.AppendFormat("{0} ", "Pre Cta :" + ajustar(mitabla.Rows(0)(13).ToString(), 20, HorizontalAlignment.Left))
        End If

        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(2), 10, HorizontalAlignment.Right))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
        yPos += (lineHeight * 2)

        sb = New System.Text.StringBuilder
        sb.AppendFormat("{0} ", "Maq Nro :" + ajustar(mitabla.Rows(0)(1).ToString(), 20, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(0)(3), 10, HorizontalAlignment.Right))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
        yPos += (lineHeight * 2)

        If Len(mitabla.Rows(0)(12).ToString) > 0 Then
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", "*" + ajustar(mitabla.Rows(0)(11).ToString(), 20, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += (lineHeight * 2)

            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", "*" + ajustar(mitabla.Rows(0)(12).ToString(), 30, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += (lineHeight * 2)
        End If
        Return yPos

    End Function

    Public Function DetalleImpresion(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single, ByVal formato_tck As Integer) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        Dim leftmargin As Single = 3
        Dim mutilidades As New utilidades
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim midato As String
        Dim ds As DataSet = mutilidades.recuperadetalle(nroOperacion)
        Dim importe As Decimal
        mitabla = ds.Tables(0)

        Dim cabeceras As Integer = 2
        If Len(txtinput.Text) > 0 Then
            Dim rpta As Integer
            rpta = MessageBox.Show("Esta Seguro, desea Imprimir Resumido...?", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If rpta = 6 Then
                sb = New System.Text.StringBuilder
                sb.AppendFormat("{0} ", txtinput.Text, 14, HorizontalAlignment.Left)
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                yPos += lineHeight
                txtinput.Text = ""
            Else
                For i As Integer = 0 To mitabla.Rows.Count() - 1
                    sb = New System.Text.StringBuilder
                    For j As Integer = 0 To cabeceras
                        Select Case j
                            Case 0
                                importe = mitabla.Rows(i)(j)
                                sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 5, HorizontalAlignment.Left))
                            Case 1
                                sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(j).ToString(), 24, HorizontalAlignment.Left))
                            Case Else
                                importe = mitabla.Rows(i)(j)
                                sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                        End Select

                    Next
                    e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                    yPos += lineHeight
                Next
            End If
        Else
            For i As Integer = 0 To mitabla.Rows.Count() - 1
                sb = New System.Text.StringBuilder
                For j As Integer = 0 To cabeceras
                    Select Case j
                        Case 0
                            importe = mitabla.Rows(i)(j)
                            sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 5, HorizontalAlignment.Left))
                        Case 1
                            sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(j).ToString(), 24, HorizontalAlignment.Left))
                        Case Else
                            importe = mitabla.Rows(i)(j)
                            sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                    End Select

                Next
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                'yPos += lineHeight
                yPos += (lineHeight * 2)
            Next

        End If



        yPos = lineaImpresion(e, prFont, yPos)

        ds = mutilidades.recuperacabeceraticket(nroOperacion)
        mitabla = ds.Tables(0)
        sb = New System.Text.StringBuilder
        importe = mitabla.Rows(0)(4)
        sb.AppendFormat("{0} ", ajustar("Total Neto :", 10, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(importe.ToString("#,##0.00"), 10, HorizontalAlignment.Right))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 180, yPos)
        yPos += (lineHeight * 2)

        sb = New System.Text.StringBuilder
        importe = mitabla.Rows(0)(5)
        sb.AppendFormat("{0} ", ajustar("IGV 18%    :", 10, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(importe.ToString("#,##0.00"), 10, HorizontalAlignment.Right))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 180, yPos)
        yPos += (lineHeight * 2)

        sb = New System.Text.StringBuilder
        importe = mitabla.Rows(0)(6)
        sb.AppendFormat("{0} ", ajustar("TOTAL S/.  :", 10, HorizontalAlignment.Left))
        sb.AppendFormat("{0} ", ajustar(importe.ToString("#,##0.00"), 10, HorizontalAlignment.Right))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 180, yPos)
        yPos += (lineHeight * 2)
        yPos = lineaImpresion(e, prFont, yPos)
        'detalle forma pago
        If formato_tck = 1 Then
            For i As Integer = 0 To mitabla.Rows.Count() - 1
                sb = New System.Text.StringBuilder
                importe = mitabla.Rows(i)(9)
                sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(8), 10, HorizontalAlignment.Left))
                sb.AppendFormat("{0} ", ajustar(importe.ToString("#,##0.00"), 10, HorizontalAlignment.Right))
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 180, yPos)
                yPos += (lineHeight * 2)
            Next

            sb = New System.Text.StringBuilder
            importe = mitabla.Rows(0)(10)
            sb.AppendFormat("{0} ", ajustar("VUELTO       ", 10, HorizontalAlignment.Left))
            sb.AppendFormat("{0} ", ajustar(importe.ToString("#,##0.00"), 10, HorizontalAlignment.Right))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 180, yPos)
            yPos += (lineHeight * 2)
        Else
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar("RUC:___________________________________", 38, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
            yPos += (lineHeight * 2)
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar("EMPRESA:_______________________________ ", 38, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
            yPos += (lineHeight * 2)
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar("TELEFONO:_______________________________", 38, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
            yPos += (lineHeight * 2)
        End If

        sb = New System.Text.StringBuilder
        sb.AppendFormat("{0} ", ajustar("usuario :  " & mitabla.Rows(0)(7).ToString, 30, HorizontalAlignment.Left))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
        yPos += (lineHeight * 2)


        'Datos del delivery
        sb = New System.Text.StringBuilder
        sb.AppendFormat("{0} ", ajustar(": " & mitabla.Rows(0)(17).ToString, 30, HorizontalAlignment.Left))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
        yPos += (lineHeight * 2)


        midato = mitabla.Rows(0)(14)
        If midato.Length > 0 Then
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar(": " & midato.ToString, 30, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
            yPos += (lineHeight * 2)
        End If

        midato = mitabla.Rows(0)(15)
        If midato.Length > 0 Then
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar(": " & midato.ToString, 30, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
            yPos += (lineHeight * 2)
        End If

        midato = mitabla.Rows(0)(16)
        If midato.Length > 0 Then
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar(": " & midato.ToString, 30, HorizontalAlignment.Left))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin + 10, yPos)
            yPos += (lineHeight * 2)
        End If

        Return yPos

    End Function

    Public Function PiedePagina(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        'Dim leftMargin As Single = e.MarginBounds.Left
        Dim leftmargin As Single = 5
        Dim mutilidades As New utilidades
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim ds As DataSet = mutilidades.recuperaEmpresa()
        mitabla = ds.Tables(0)
        Dim micadena() As String = IIf(formato_tck = 1, Split(mitabla.Rows(0)(7).ToString(), "/"), Split(mitabla.Rows(0)(8).ToString(), "/"))
        Dim anchocolumnas As Integer = 40
        For i As Integer = 0 To UBound(micadena, 1)
            sb = New System.Text.StringBuilder
            sb.AppendFormat("{0} ", ajustar(micadena(i), anchocolumnas, HorizontalAlignment.Center))
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += lineHeight
        Next

        Return yPos
    End Function


    Private Sub UbicarCliente(ByVal cod_cliente As String)
        Try
            'Dim fila As DataRow = dsCliente.Tables("cliente").Rows.Find(lcod)
            Dim fila() As DataRow
            Dim cadena As String = "cod_clie ='" & cod_cliente & "'"
            fila = dsCliente.Tables("cliente").Select(cadena)
            txtRuc.Text = fila(0).Item("cod_clie").ToString
            TxtDirfiscal.Text = fila(0).Item("dir_ent").ToString
            txtTelefono.Text = fila(0).Item("fono_clie").ToString
            txtContacto.Text = fila(0).Item("nom_cont").ToString
            'almacenamos el tipo de cliente
            'TipoCliente = fila(0).Item("cod_tipo").ToString
            'cboFPago.Focus()
            cboCliente.SelectedValue = fila(0).Item("cod_clie").ToString
            'cboTipoCliente.SelectedValue = TipoCliente
        Catch err As Exception
            'MessageBox.Show(err.Message, err.Source, MessageBoxButtons.OK)
            'MessageBox.Show("Seleccione un Cliente Registrado...", "SGA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reiniciaCliente()
            'dsArticulo.Clear()
        End Try
    End Sub

    Private Sub UbicarFonoCliente(ByVal nro_telefono As String)
        Try
            'Dim fila As DataRow = dsCliente.Tables("cliente").Rows.Find(lcod)
            Dim fila() As DataRow
            Dim cadena As String = " fono_clie ='" & nro_telefono & "'"
            fila = dsCliente.Tables("cliente").Select(cadena)
            If fila.Count > 0 Then
                txtRuc.Text = fila(0).Item("cod_clie").ToString
                TxtDirfiscal.Text = fila(0).Item("dir_ent").ToString
                txtTelefono.Text = fila(0).Item("fono_clie").ToString
                txtContacto.Text = fila(0).Item("nom_cont").ToString
                cboCliente.SelectedValue = fila(0).Item("cod_clie").ToString
                'Else
                '    txtRuc.Text = ""
                '    txtDirfiscal.Text = ""
                '    txtContacto.Text = ""
                '    cboCliente.SelectedValue = ""
            End If
        Catch err As Exception
            'MessageBox.Show(err.Message, err.Source, MessageBoxButtons.OK)
            'MessageBox.Show("Seleccione un Cliente Registrado...", "SGA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'reiniciaCliente()
            'dsArticulo.Clear()
        End Try
    End Sub

    Private Sub ubicarDocumento()
        'reiniciaDatos()

        'If txtNroDocumento.ReadOnly = False Then
        If cboDocumento.SelectedIndex = -1 Then
            txtSerDocumento.Text = ""
            txtNroDocumento.Text = ""
            cboDocumento.Focus()
        Else
            Dim lserie As String, lnumero As String, ldoc As String
            'lserie = Microsoft.VisualBasic.Right("000" & txtSerDocumento.Text, 4)
            lserie = txtSerDocumento.Text
            lnumero = Microsoft.VisualBasic.Right("00000000" & txtNroDocumento.Text, 8)
            If cboDocumento.SelectedIndex >= 0 Then
                ldoc = cboDocumento.SelectedValue
                'nroOperacion = msalida.existeMesa(cod_mesa)
                nroOperacion = msalida.existeDocumento(ldoc, lserie, lnumero, 0)
                If nroOperacion > 0 Then 'si la factura ya esta registrada
                    'si la factura no esta anulada
                    dtDetalle.Clear()
                    If Not msalida.estaAnulada(nroOperacion) Then
                        modoHabilitado(True)
                        tipoProceso = "E"
                        cmdGrabar.Enabled = True
                        'limpiamos el dataset salida
                        dsSalida.Clear()
                        'cargamos los datos desde la factura
                        cargaCabecera(nroOperacion)
                        ' cargaCabeceraGuia(nroOperacion)
                        'deshabilitaCabecera()
                        cargaDetalle(nroOperacion, False)
                        cargaPago(nroOperacion)
                        'recuperamos el nro de pedido
                        'Dim mPedido As New pedido
                        'nroOperacionPedido = mPedido.existe(txtSerPedido.Text, txtNroPedido.Text)
                    Else
                        MessageBox.Show("La " + cboDocumento.Text + " " + lserie + "-" + lnumero + " esta Anulada" + Chr(13) + "Ingrese otro Número de documento...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        reiniciaControles(False, True)
                        reiniciaDatos()
                        'If Microsoft.VisualBasic.Len(txtSerPedido.Text) > 0 Then
                        '    txtNroPedido.Focus()
                        'Else
                        '    txtSerPedido.Focus()
                        'End If
                    End If
                    'verificamos si cuenta con permiso para anular la factura
                    'If Not esEditable() Then
                    '    cmdAnular.Enabled = False
                    'Else
                    '    cmdAnular.Enabled = True

                    'End If
                    'cmdImprimir.Focus()
                    dataDetalle.ReadOnly = True
                    dataDetalle.Focus()
                Else
                    reiniciaDatos()
                    reiniciaCliente()
                    txtRuc.Text = ""
                    dataDetalle.ReadOnly = False
                    '    'como aun no esta registrada la factura, verificamos si ya se recupero el pedido
                    '    If nroOperacionPedido = 0 Then
                    '        MessageBox.Show("Documento aun NO Registrado" + Chr(13) + "Debe recuperar un pedido...", "CEFE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        txtSerPedido.Focus()
                    '    Else
                    '        txtSerGuia.Focus()
                    'End If
                End If
            Else
                'falta seleccionar el tipo de documento
                MessageBox.Show("Seleccione el Tipo de Documento...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cboDocumento.Focus()
            End If
        End If

    End Sub
    Private Sub ubicarmesa()
        Dim lserie As String, lnumero As String, ldoc As String, lmesa As String
        If Len(txtcod_mesa.Text) > 0 Then
            '   reiniciaDatos()
            lmesa = Microsoft.VisualBasic.Right("000" & IIf(txtcod_mesa.Text = "", "999", txtcod_mesa.Text), 3)
            If mutilidades.existeMesa(lmesa, 0) > 0 Then
                lserie = txtSerDocumento.Text
                lnumero = Microsoft.VisualBasic.Right("00000000" & txtNroDocumento.Text, 8)
                ' lmesa = Microsoft.VisualBasic.Right("000" & IIf(txtcod_mesa.Text = "", "099", txtcod_mesa.Text), 3)
                'If cboDocumento.SelectedIndex >= 0 Then
                ldoc = "12"
                nroOperacion = msalida.buscarMesa(lmesa, pFechaSystem)
                If nroOperacion > 0 Then
                    If Not msalida.estaAnulada(nroOperacion) Then
                        modoHabilitado(True)
                        tipoProceso = "A"
                        cmdGrabar.Enabled = True
                        dsSalida.Clear()
                        cargaCabecera(nroOperacion)
                        cargaDetalle(nroOperacion, False)
                        cargaPago(nroOperacion)
                    Else
                        MessageBox.Show("La " + cboDocumento.Text + " " + lserie + "-" + lnumero + " esta Anulada" + Chr(13) + "Ingrese otro Número de documento...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        reiniciaControles(False, True)
                        reiniciaDatos()
                    End If

                    dataDetalle.ReadOnly = True
                    dataDetalle.Focus()
                Else
                    If Len(txtcod_mesa.Text) > 0 Then
                        reiniciaDatos()
                        reiniciaCliente()
                        txtRuc.Text = ""
                        dataDetalle.ReadOnly = False
                    End If

                End If
                'Else
                '    'falta seleccionar el tipo de documento
                '    MessageBox.Show("Seleccione el Tipo de Documento...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    cboDocumento.Focus()
                'End If
                nroingreso = msalida.maxIngreso(nroOperacion)
                coloreaitem()

            Else
                MessageBox.Show("No Existe Codigo de Mesa...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtcod_mesa.Text = ""
                'txtcod_mesa.Focus()
            End If
        End If

    End Sub
    Public Sub ubicarpedido()
        Dim lserie As String, lnumero As String, ldoc As String
        reiniciaDatos()

        lserie = txtSerDocumento.Text
        lnumero = Microsoft.VisualBasic.Right("00000000" & txtNroDocumento.Text, 8)
        ' lmesa = Microsoft.VisualBasic.Right("000" & IIf(txtcod_mesa.Text = "", "099", txtcod_mesa.Text), 3)
        If cboDocumento.SelectedIndex >= 0 Then
            ldoc = "12"
            nroOperacion = msalida.buscarPedido(nropedido)
            If nroOperacion > 0 Then
                If Not msalida.estaAnulada(nroOperacion) Then
                    modoHabilitado(True)
                    tipoProceso = "A"
                    cmdGrabar.Enabled = True
                    dsSalida.Clear()
                    cargaCabecera(nroOperacion)
                    cargaDetalle(nroOperacion, False)
                    cargaPago(nroOperacion)

                Else
                    MessageBox.Show("La " + cboDocumento.Text + " " + lserie + "-" + lnumero + " esta Anulada" + Chr(13) + "Ingrese otro Número de documento...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    reiniciaControles(False, True)
                    reiniciaDatos()
                End If

                dataDetalle.ReadOnly = True
                dataDetalle.Focus()
            Else
                If Len(txtcod_mesa.Text) > 0 Then
                    reiniciaDatos()
                    reiniciaCliente()
                    txtRuc.Text = ""
                    dataDetalle.ReadOnly = False
                End If

            End If
        Else
            'falta seleccionar el tipo de documento
            MessageBox.Show("Seleccione el Tipo de Documento...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboDocumento.Focus()
        End If

    End Sub

    Function validaAnulacion()
        Dim validado As Boolean = False
        If msalida.documento(nroOperacion) <> "" Then
            If msalida.fechadoc(nroOperacion) = pFechaSystem Then
                If msalida.usuario(nroOperacion) = pCuentaUser Then
                    validado = True
                Else
                    MessageBox.Show("El Usuario no Corresponde ..No es posible Anularlo", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtNroDocumento.Focus()
                End If
            Else
                MessageBox.Show("El Documento esta cerrado..No es posible Anularlo", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNroDocumento.Focus()
            End If
        End If
        Return validado
    End Function

    Function validaDetalleAnulacion()
        Dim validado As Boolean = False
        If nroOperacion > 0 Then
            If msalida.documento(nroOperacion) = "" Then
                If msalida.fechadoc(nroOperacion) = pFechaSystem Then
                    If msalida.usuario(nroOperacion) = pCuentaUser Then
                        validado = True
                    Else
                        MessageBox.Show("El Usuario no Corresponde ..No es posible Anularlo", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtcod_mesa.Focus()
                    End If
                Else
                    MessageBox.Show("El Documento esta cerrado..No es posible Anularlo", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtcod_mesa.Focus()
                End If
            Else
                MessageBox.Show("El Documento esta cerrado..No es posible Anularlo", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtcod_mesa.Focus()
            End If
        Else
            validado = True
        End If

        Return validado
    End Function

    Sub reiniciaControles(ByVal incluirNroPedido As Boolean, ByVal incluirNroDocumento As Boolean)
        cboDocumento.SelectedIndex = 0
        txtrecibido.Text = 0
        txtTotal.Text = 0
        txtvuelto.Text = 0
    End Sub

    Sub reiniciaDatos()
        dtDetalle.Clear()
        dtpago.Clear()
        nroOperacion = 0
        fcod_vend = ""
        fcod_clie = ""
        tipoProceso = "A"
        txtRuc.Text = ""
        txtObservacion.Text = ""
        cmdGrabar.Enabled = True
        capturaITEM("", "", 0)
        cboDocumento.SelectedIndex = 0
        lblperiodo.Text = general.devuelvefechaImpresion(pFechaSystem)
        lblnomUsuario.Text = (pNombreUser)
        lbltipCambio.Text = "Tipo de Cambio :" & pTC
        calculaTotal()
    End Sub

    Sub reiniciaCliente()
        cboCliente.SelectedIndex = -1
        'txtRuc.Text = ""
        TxtDirfiscal.Text = ""
        txtTelefono.Text = ""
        txtContacto.Text = ""
        cboCliente.Text = ""
        datasetCliente()
        pn_clientes.Visible = False

        'cboCliente.Focus()
    End Sub
    Sub modoHabilitado(ByVal estado As Boolean)
        cmdInicio.Enabled = estado
        cmdpago.Enabled = estado
        cmdComanda.Enabled = estado
        cmdGrabar.Enabled = estado
        cmdAnular.Enabled = estado

    End Sub

    Sub cargaCabecera(ByVal nro_ope As Integer)
        Dim dt As New DataTable
        dt = msalida.recuperaCabecera(nro_ope, True)
        cboCliente.SelectedValue = dt.Rows(0).Item("cod_clie").ToString
        txtcod_mesa.Text = dt.Rows(0).Item("cAux2").ToString
        txtRuc.Text = dt.Rows(0).Item("cod_clie").ToString
        TxtDirfiscal.Text = dt.Rows(0).Item("dir_clie").ToString
        txtObservacion.Text = dt.Rows(0).Item("obs").ToString
        cboDocumento.SelectedValue = dt.Rows(0).Item("cod_doc").ToString
        txtSerDocumento.Text = dt.Rows(0).Item("ser_doc").ToString
        txtNroDocumento.Text = dt.Rows(0).Item("nro_doc").ToString
        'mostraritems(dt.Rows(0).Item("cod_vend").ToString, 1)
        cadenaSQL = "select cod_vend as codigo,nom_vend as nombre,isnull(foto) as vnul,foto, b_color,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                           & " from vendedor v inner join usuario u on v.cod_vend=u.cuenta"
        capturaITEM(dt.Rows(0).Item("cod_vend").ToString, cadenaSQL, 1)
        cadenaSQL = "select cod_recurso as codigo,dsc_recurso as nombre,isnull(foto) as vnul,foto, b_color,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                   & " from tipo_recurso where cod_tabla='10'"
        capturaITEM(dt.Rows(0).Item("cAux3").ToString, cadenaSQL, 2)


    End Sub
    Sub cargaDetalle(ByVal nro_ope As Integer, ByVal facturaAnulada As Boolean)
        Dim I As Integer
        Dim dt As New DataTable
        If facturaAnulada Then
            'dt = msalida.recuperaDetalle_anul(nro_ope, pDecimales)
        Else
            dt = msalida.recuperaDetalle(nro_ope, pDecimales)
        End If
        For I = 0 To dt.Rows.Count - 1
            dtDetalle.ImportRow(dt.Rows(I))
        Next

        calculaTotal()
        dataDetalle.Refresh()
    End Sub

    Sub cargaPago(ByVal nro_ope As Integer)
        Dim I As Integer
        Dim dt As New DataTable
        dt = msalida.recuperaPago(nro_ope)

        For I = 0 To dt.Rows.Count - 1
            dtpago.ImportRow(dt.Rows(I))
        Next

        calculaTotal()
        dataPago.Refresh()
    End Sub

    Private Sub cmdpago_Click(sender As System.Object, e As System.EventArgs) Handles cmdpago.Click
        'Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
        'If FormularioActivo("p_caja") = False Then
        '    p_caja.datos(nroOperacion)
        '    p_caja.Show()
        'End If

        'If ltotal > 0 Then
        '    mostraritems(4, "")
        'Else
        '    MessageBox.Show("Falta Registrar Artículos...", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'End If

        panelitem.Controls.Clear()
        mostraritems(40, "")
        PanelSG.Controls.Clear()
        mostraritems(4, "")


    End Sub

    Private Sub chkIGV_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkIGV.CheckedChanged
        calculaTotal()
    End Sub


    Private Sub dataDetalle_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataDetalle.CellEndEdit
        calculadetalle()
        calculaTotal()
    End Sub

    Private Sub calculadetalle()

        Dim lcantidad, lprecio, lneto As Decimal
        'validamos el ingreso de cantidades no nulas
        If IsDBNull(dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value) Or IsDBNull(dataDetalle("precio", dataDetalle.CurrentRow.Index).Value) Or IsDBNull(dataDetalle("neto", dataDetalle.CurrentRow.Index).Value) Then
            dataDetalle("precio", dataDetalle.CurrentRow.Index).Value = 0
            lprecio = 0
            dataDetalle("neto", dataDetalle.CurrentRow.Index).Value = 0
            lneto = 0
        End If
        If dataDetalle.CurrentCell.ColumnIndex = dataDetalle.Columns("cantidad").Index Then
            If IsDBNull(dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value) Or dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value = 0 Then
                lcantidad = 1
                dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value = 1
            Else
                lcantidad = dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value
                lprecio = dataDetalle("precio", dataDetalle.CurrentRow.Index).Value
                lneto = Round(lcantidad * lprecio, pDecimales)
                dataDetalle("neto", dataDetalle.CurrentRow.Index).Value = lneto
            End If
            'txtBuscar.Focus()
            'dataDetalle.ClearSelection()
        End If
        If dataDetalle.CurrentCell.ColumnIndex = dataDetalle.Columns("precio").Index Then
            If IsDBNull(dataDetalle("precio", dataDetalle.CurrentRow.Index).Value) Then
                lprecio = 0
                dataDetalle("precio", dataDetalle.CurrentRow.Index).Value = 0
            Else
                lprecio = dataDetalle("precio", dataDetalle.CurrentRow.Index).Value
                lcantidad = dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value
                lneto = Round(lcantidad * lprecio, pDecimales)
                dataDetalle("neto", dataDetalle.CurrentRow.Index).Value = lneto
            End If
            'txtBuscar.Focus()
            'dataDetalle.ClearSelection()
        End If
        If dataDetalle.CurrentCell.ColumnIndex = dataDetalle.Columns("neto").Index Then
            If IsDBNull(dataDetalle("neto", dataDetalle.CurrentRow.Index).Value) Then
                lneto = 0
                dataDetalle("neto", dataDetalle.CurrentRow.Index).Value = 0
            Else
                lneto = dataDetalle("neto", dataDetalle.CurrentRow.Index).Value
                lcantidad = dataDetalle("cantidad", dataDetalle.CurrentRow.Index).Value
                lprecio = Round(lneto / lcantidad, 10)
                dataDetalle("precio", dataDetalle.CurrentRow.Index).Value = lprecio
            End If
        End If
    End Sub

    Private Sub txtRuc_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub cboCliente_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboCliente.KeyPress, cboCliente.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cboCliente_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboCliente.Validating
        If cboCliente.SelectedValue <> "" Then
            fcod_clie = cboCliente.SelectedValue.ToString
            UbicarCliente(fcod_clie)
        End If

    End Sub

    Private Sub cboDocumento_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboDocumento.KeyPress
        If e.KeyChar = Chr(13) Then
            txtSerDocumento.Focus()
        End If
    End Sub


    Sub GeneraNumDocumento()
        txtSerDocumento.Text = mdocumento.recuperaSerieDocumento(cboDocumento.SelectedValue)
        txtNroDocumento.Text = msalida.maxNroSalida("12", txtSerDocumento.Text)
    End Sub



    Private Sub dataPago_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataPago.CellEndEdit

        If IsDBNull(dataPago("monto", dataPago.CurrentRow.Index).Value) Then
            dataPago("monto", dataPago.CurrentRow.Index).Value = 0.0
        End If
        If IsDBNull(dataPago("propina", dataPago.CurrentRow.Index).Value) Then
            dataPago("propina", dataPago.CurrentRow.Index).Value = 0.0
        End If

        If dataPago.CurrentCell.ColumnIndex = dataPago.Columns("monto").Index Then

            If dataPago("cod_pago", dataPago.CurrentRow.Index).Value <> "06" Then
                '    lcantidad = 0
                If dataPago("monto", dataPago.CurrentRow.Index).Value > ltotal - lvuelto Then
                    MessageBox.Show("el monto no puede ser MAYOR ...PAGO es con TARJETA", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dataPago("monto", dataPago.CurrentRow.Index).Value = ltotal - lvuelto
                    'Else
                    '    lcantidad = dataDetalle("cant_fis", dataDetalle.CurrentRow.Index).Value
                    'End If
                    'Dim mInventario As New Inventario
                    'nroInventario = dataDetalle("saldo", dataDetalle.CurrentRow.Index).Value
                    'mInventario.actualizaFisicoMensual(nroInventario, lcantidad)
                End If

            End If
        End If
        calculaTotal()
    End Sub

    Private Sub cmdNuevo_Click(sender As System.Object, e As System.EventArgs) Handles cmdNuevo.Click
        Dim rpta As Integer
        rpta = MessageBox.Show("¿Esta seguro de iniciar el Documento...?" + Chr(13) + "Este Proceso es Irreversible", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If rpta = 6 Then
            reiniciaDatos()
            reiniciaCliente()
            GeneraNumDocumento()
            modoHabilitado(True)
            txtcod_mesa.Text = ""
        End If

    End Sub

    Private Sub txtTelefono_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If
    End Sub




    Private Sub txtNroDocumento_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroDocumento.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If


    End Sub

    Private Sub txtNroDocumento_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNroDocumento.Validating
        ubicarDocumento()
    End Sub
    Private Sub cmdanular_clikc(sender As System.Object, e As System.EventArgs) Handles cmdAnular.Click
        'Principal.ShowModalPanel(_pventa, DevComponents.DotNetBar.Controls.eSlideSide.Left) 'Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
        If validaAnulacion() Then
            If FormularioActivo("p_notas") = False Then
                p_notas.datosNotas(nroOperacion, 1)
                p_notas.Show()

            End If
        End If
    End Sub
    Sub anulardocumento(ByVal obs As String, ByVal Operacion As Integer, ByVal tipoAnulacion As Integer)
        Dim rpta As Integer
        nroOperacion = Operacion
        If tipoAnulacion = 1 Then
            rpta = MessageBox.Show("¿Esta seguro de Anular el Documento...?" + Chr(13) + "Este Proceso es Irreversible", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            If rpta = 6 Then
                Dim objTransac As MySqlTransaction
                Dim dbConex As MySqlConnection = Conexion.obtenerConexion
                objTransac = dbConex.BeginTransaction
                Try
                    'actualizamos la salida
                    Dim com As New MySqlCommand("Update salida set anul=1,obs='" & obs & "' where operacion=" & nroOperacion)
                    com.Connection = dbConex
                    com.Transaction = objTransac
                    com.ExecuteNonQuery()
                    'actualizamos el detalle
                    Dim com2 As New MySqlCommand("Update salida_det set cant=0 ,precio=0 where operacion=" & nroOperacion)
                    com2.Connection = dbConex
                    com2.Transaction = objTransac
                    com2.ExecuteNonQuery()
                    'actualizamos el Pago
                    Dim com3 As New MySqlCommand("Update salida_detpago set imp_pagado=0,imp_propina=0,imp_vuelto=0 where operacion=" & nroOperacion)
                    com3.Connection = dbConex
                    com3.Transaction = objTransac
                    com3.ExecuteNonQuery()
                    objTransac.Commit()

                    SetDefaultPrinter(mdocumento.NombreImpresora(cboDocumento.SelectedValue))
                    imprimirdoc(pvistaprevia)

                Catch ex As Exception
                    If Not objTransac Is Nothing Then
                        objTransac.Rollback()
                    End If
                    MessageBox.Show(ex.Message.ToString)
                Finally
                    reiniciaControles(True, True)
                    reiniciaDatos()
                    reiniciaCliente()
                    GeneraNumDocumento()
                    txtRuc.Text = ""
                    txtcod_mesa.Text = ""
                    modoHabilitado(True)
                End Try

            End If
        Else

        End If


    End Sub
    Private Sub dataDetalle_DoubleClick(sender As Object, e As System.EventArgs) Handles dataDetalle.DoubleClick
        eliminaritem()
        calculaTotal()
    End Sub

    Private Sub pb_borrar_Click(sender As System.Object, e As System.EventArgs)
        eliminaritem()
        calculaTotal()
    End Sub

    Private Sub txtSerDocumento_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerDocumento.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If

    End Sub

    Private Sub txtDirfiscal_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtContacto_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmdmesas_Click(sender As System.Object, e As System.EventArgs)
        'Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
        If FormularioActivo("mesas") = False Then
            mesas.Show()
        End If
    End Sub

    Private Sub txtcod_mesa_DoubleClick(sender As Object, e As EventArgs)
        ' Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
        If FormularioActivo("mesas") = False Then
            mesas.Show()
        End If
    End Sub

    Private Sub txtcod_mesa_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If

        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If

    End Sub

    Private Sub txtcod_mesa_LostFocus(sender As Object, e As EventArgs)
        If Microsoft.VisualBasic.Len(txtcod_mesa.Text) > 0 Then
            txtcod_mesa.Text = Microsoft.VisualBasic.Right("000" & txtcod_mesa.Text, 3)
        End If
        'general.saleTextoProceso(txtcod_mesa)
    End Sub

    Private Sub txtcod_mesa_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ubicarmesa()
    End Sub
    Sub actualizamesa()
        ubicarmesa()
    End Sub

    Sub actualizapedido(Optional ByVal nro_pedido As Integer = 0)
        nropedido = nro_pedido
        ubicarpedido()
    End Sub

    Private Sub cmdComanda_Click(sender As System.Object, e As System.EventArgs) Handles cmdComanda.Click
        Dim continuar As Boolean = True, grabaGuia As Boolean = True
        Dim ser_doc, nro_doc As String
        'validamos el documento
        ser_doc = ""
        nro_doc = ""
        Try
            If Not (msalida.existeOperacion(nroOperacion, 0)) Then
                grabacomanda(ser_doc, nro_doc)
                imprimircomanda(nroOperacion)
            Else
                grabadetalle()
                imprimircomanda(nroOperacion)
            End If
            mutilidades.ActualizarEstado_Mesas(0, IIf(txtcod_mesa.Text = "", "999", txtcod_mesa.Text), "&H20A5DA")
            'ubicarmesa()
            reiniciaDatos()
            reiniciaCliente()
            GeneraNumDocumento()
            txtcod_mesa.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub imprimircomanda(ByVal operacion As Integer)
        Dim frm As New rptForm
        'If dataDetalle.RowCount() > 0 Then
        If mutilidades.existePedido(operacion) > 0 Then
            Dim com As New MySqlCommand("select impresora from salida_det sd inner join articulo a on sd.cod_art=a.cod_art inner join subgrupo g on a.cod_grupov=g.cod_sgrupo " _
                                        & " where repfiscal and sd.nAux=1 and operacion=" & operacion & " group by impresora order by impresora ", dbConex)
            Dim dr As MySqlDataReader
            dr = com.ExecuteReader
            If dr.HasRows = True Then
                While dr.Read
                    nomImpresora = dr("impresora")
                    'If verificaEstadoPrinter(nomImpresora) Then
                    SetDefaultPrinter(nomImpresora)
                    formato_tck = 2
                    imprimirdoc(pvistaprevia)

                    'frm.cargarComanda(operacion, nomImpresora, pvistaprevia)
                    'Else
                    '    MessageBox.Show("Error de Impresora...Veirificar", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If

                End While
            End If
            dr.Close()

            mutilidades.ActualizarEstado_Pedido(operacion)
        End If
    End Sub

    Private Sub pbcambiomesa_Click(sender As Object, e As EventArgs)
        Dim rpta As Integer
        Dim lmesa As String = Microsoft.VisualBasic.Right("000" & IIf(txtinput.Text = "", "999", txtinput.Text), 3)
        If mutilidades.existeMesa(lmesa, 1) > 0 Then
            rpta = MessageBox.Show("Esta Seguro de realizar el Cambio de MESA...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2)
            If rpta = 6 Then
                mutilidades.ActualizarEstado_Mesas(1, txtcod_mesa.Text, "&H228B22")
                msalida.Actualiza_cambioMesa(nroOperacion, lmesa)
                mutilidades.ActualizarEstado_Mesas(0, IIf(txtinput.Text = "", "999", lmesa), "&H20A5DA")
                ubicarmesa()
            End If
        Else
            MessageBox.Show("No Existe Codigo de Mesa..." + Chr(13) +
                            "ó se encuentra Ocupada", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtinput.Focus()
        End If
    End Sub


    Private Sub cmdmozo_Click(sender As Object, e As EventArgs) Handles cmdmozo.Click
        mostraritems(7, "")
    End Sub

    Private Sub cmdAtencion_Click(sender As Object, e As EventArgs) Handles cmdAtencion.Click
        mostraritems(8, "")
    End Sub



    Private Sub txtRuc1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRuc.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If pn_clientes.Visible = False Then
            pn_clientes.Visible = True
        Else
            pn_clientes.Visible = False
        End If
    End Sub

    Private Sub cmd_clientes_Click(sender As Object, e As EventArgs) Handles cmd_clientes.Click
        If pn_clientes.Visible = False Then
            pn_clientes.Visible = True
        Else
            pn_clientes.Visible = False
        End If
    End Sub



    Private Sub cboCliente_KeyPress1(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub



    Private Sub cboCliente_Validating2(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If cboCliente.SelectedValue <> "" Then
            fcod_clie = cboCliente.SelectedValue.ToString
            UbicarCliente(fcod_clie)
        End If

    End Sub



    Private Sub TextBoxX1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDirfiscal.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub TextBoxX2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContacto.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub



    Private Sub TextBoxX3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If
    End Sub



    Private Sub Txttelefono_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTelefono.Validating
        fnro_telefono = txtTelefono.Text
        If Microsoft.VisualBasic.Len(fnro_telefono) > 0 Then
            UbicarFonoCliente(fnro_telefono)
        End If
    End Sub



    Private Sub txtRuc_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtRuc.Validating
        fcod_clie = txtRuc.Text
        If Microsoft.VisualBasic.Len(fcod_clie) > 0 Then
            UbicarCliente(fcod_clie)
        End If
    End Sub



    Private Sub txtcod_mesa_DoubleClick1(sender As Object, e As EventArgs) Handles txtcod_mesa.DoubleClick
        ' Principal.CloseModalPanel(Me, DevComponents.DotNetBar.Controls.eSlideSide.Left)
        If FormularioActivo("mesas") = False Then
            mesas.Show()
        End If
    End Sub

    Private Sub txtcod_mesa_KeyPress1(sender As Object, e As KeyPressEventArgs) Handles txtcod_mesa.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If

        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) Then
            e.KeyChar = ""
        End If

    End Sub

    Private Sub txtcod_mesa_LostFocus1(sender As Object, e As EventArgs) Handles txtcod_mesa.LostFocus
        If Microsoft.VisualBasic.Len(txtcod_mesa.Text) > 0 Then
            txtcod_mesa.Text = Microsoft.VisualBasic.Right("000" & txtcod_mesa.Text, 3)
        End If
    End Sub



    Private Sub txtcod_mesa_Validating1(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtcod_mesa.Validating
        ubicarmesa()
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If teclado.Size = New Size(595, 0) Then
            teclado.Size = New Size(610, 210)
            teclado.Location = New Point(440, 100)
        Else
            teclado.Size = New Size(595, 0)
        End If
    End Sub


    Private Sub dataPago_DoubleClick(sender As Object, e As EventArgs) Handles dataPago.DoubleClick
        eliminarpago()
        calculaTotal()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs)
        cambiomesa()
    End Sub
    Private Sub cambiomesa()
        Dim rpta As Integer
        Dim lmesa As String = Microsoft.VisualBasic.Right("000" & IIf(txtinput.Text = "", "999", txtinput.Text), 3)
        ' If mutilidades.existeMesa(lmesa, 1) > 0 Then
        If msalida.buscarMesa(lmesa, pFechaSystem) > 0 Then
            rpta = MessageBox.Show("Esta Seguro de realizar el Cambio de MESA...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2)
            If rpta = 6 Then
                'mutilidades.ActualizarEstado_Mesas(txtcod_mesa.Text, "&H228B22")
                'msalida.Actualiza_cambioMesa(nroOperacion, lmesa)
                Dim nroReg As Integer = 0
                Dim nroOperacionX As Integer = msalida.buscarMesa(lmesa, pFechaSystem)
                For Each row As DataGridViewRow In dataDetalle.Rows
                    If row.Selected Then
                        nroReg = nroReg + 1
                        Dim nroOperacion As Integer = row.Cells("operacion").Value
                        Dim nroIngreso As Integer = row.Cells("ingreso").Value
                        msalida.Actualiza_DetcambioMesa(nroOperacionX, nroOperacion, nroIngreso)
                    End If
                Next
                If nroReg = dataDetalle.RowCount Then
                    mutilidades.ActualizarEstado_Mesas(1, txtcod_mesa.Text, "&H228B22")
                End If

                mutilidades.ActualizarEstado_Mesas(0, IIf(txtinput.Text = "", "999", lmesa), "&H20A5DA")
                txtinput.Text = ""
                ubicarmesa()
            End If
        Else
            MessageBox.Show("No Existe Codigo de Mesa..." + Chr(13) +
                            "ó Aperture la Mesa ", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtinput.Focus()
        End If
    End Sub



    Private Sub dataPago_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dataPago.EditingControlShowing
        ' referencia a la celda  
        Dim validar As TextBox = CType(e.Control, TextBox)

        ' agregar el controlador de eventos para el KeyPress  
        AddHandler validar.KeyPress, AddressOf validar_Keypress

    End Sub

    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna  
        Dim columna As Integer = dataPago.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 2 o 3  
        If columna = 3 Or columna = 4 Then

            ' Obtener caracter  
            Dim caracter As Char = e.KeyChar

            ' comprobar si el caracter es un número o el retroceso  
            If (Not Char.IsNumber(caracter) And caracter <> ".") And (caracter = ChrW(Keys.Back)) = False Then
                'Me.Text = e.KeyChar  
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub





    Private Sub Pbmodificador_Click(sender As Object, e As EventArgs) Handles Pbmodificador.Click
        If dataDetalle.RowCount > 0 Then
            Dim mfila As Integer = dataDetalle.CurrentRow.Index
            If dataDetalle("nom_art", mfila).Value <> "" Then
                dataDetalle("nom_art", mfila).Value = dataDetalle("nom_art", mfila).Value + vbCrLf + "        " + txtinput.Text
                txtinput.Text = ""
            End If
        Else
            txtinput.Text = ""
        End If


    End Sub



    Private Sub cmdPax_Click(sender As Object, e As EventArgs) Handles cmdPax.Click
        mostraritems(9, "")
    End Sub




    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        cambiomesa()
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub GroupBox7_Enter(sender As Object, e As EventArgs) Handles GroupBox7.Enter

    End Sub

    Private Sub panelitem_Paint(sender As Object, e As PaintEventArgs) Handles panelitem.Paint

    End Sub

    Private Sub txtinput_TextChanged(sender As Object, e As EventArgs) Handles txtinput.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub PanelG_Paint(sender As Object, e As PaintEventArgs) Handles PanelG.Paint

    End Sub
End Class
