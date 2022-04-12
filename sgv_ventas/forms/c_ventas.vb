Imports MySql.Data.MySqlClient
Imports libreriaVentas
Public Class c_ventas
    Private dsVentas As New DataSet
    Private dsventasdetalle As New DataSet
    Private dsventaspago As New DataSet
    Private dsAlmacen As New DataSet
    Private dsAlmacenR As New DataSet
    Private dsProducto As New DataSet
    Private dsResumen As New DataSet
    Private cPrecioCI As String = general.str_PrecioCompraCI
    Private chPrecioCI As String = general.str_hPrecioCompraCI
    Private cPrecioSI As String = general.str_PrecioCompraSI
    Private chPrecioSI As String = general.str_hPrecioCompraSI
    Private estaCargando As Boolean = True
    Private precio As Decimal = 0D

    Private Sub c_ventas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = 0
   
        Dim mes, anno As Integer
        mes = Month(pFechaActivaMax)
        anno = Year(pFechaActivaMax)
 
        
        estableceFechas()
        muestraVentas()
        estaCargando = False
    End Sub



    Private Sub optRegistro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRegistro.CheckedChanged
        If Not estaCargando And optRegistro.Checked = True Then
            cboGrupo.SelectedIndex = -1
            chkGrupo.Checked = False
            chkGrupo.Enabled = False
            grupo.Text = ""
            cboGrupo.Enabled = False
            chkIMP.Checked = True
            muestraVentas()
        End If
    End Sub
    Private Sub optventasProducto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVentasProducto.CheckedChanged
        If Not estaCargando And optVentasProducto.Checked = True Then

            chkGrupo.Enabled = True
            chkGrupo.Text = "x Producto"
            cboGrupo.SelectedIndex = -1
            cboGrupo.Enabled = False
            muestraVentas()
        End If
    End Sub
    Private Sub optFormasPago_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFomasPago.CheckedChanged
        If Not estaCargando And optFomasPago.Checked = True Then

            chkGrupo.Enabled = True
            chkGrupo.Text = "x Proveedor"
            cboGrupo.SelectedIndex = -1
            cboGrupo.Enabled = False
            muestraVentas()
        End If
    End Sub

    Private Sub cboGrupo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.GotFocus
        If Not estaCargando And cboGrupo.Enabled = True Then
            muestraVentas()
        End If
    End Sub
    Private Sub cboGrupo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        If Not estaCargando And cboGrupo.Enabled = True Then
            muestraVentas()
        End If
    End Sub
    Private Sub chkIMP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIMP.CheckedChanged
        If Not estaCargando Then
            muestraVentas()
        End If
    End Sub
 
    Sub estableceFechas()
        mcDesde.DisplayMonth = pFechaSystem
        mcDesde.SelectedDate = pFechaSystem
        mcDesde.MaxDate = pFechaActivaMax
        mcHasta.DisplayMonth = pFechaSystem
        mcHasta.SelectedDate = pFechaSystem
        mcHasta.MaxDate = pFechaActivaMax
    End Sub


    'Private Sub chkDetalle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not estaCargando Then
    '        If optRegistro.Checked = False Then
    '            If chkDetalle.Checked = True Then
    '                chkTotalizado.Checked = False
    '            Else
    '                chkTotalizado.Checked = True
    '            End If
    '        Else
    '            chkDetalle.Checked = True
    '            chkTotalizado.Checked = False
    '        End If
    '        muestraVentas()
    '        dataDetalle.Focus()
    '    End If
    'End Sub
    'Private Sub chkAcumulado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not estaCargando Then
    '        If optRegistro.Checked = False Then
    '            If chkTotalizado.Checked = True Then
    '                chkDetalle.Checked = False
    '            Else
    '                chkDetalle.Checked = True
    '            End If
    '        Else
    '            chkTotalizado.Checked = False
    '            chkDetalle.Checked = True
    '        End If
    '        muestraVentas()
    '        dataDetalle.Focus()
    '    End If
    'End Sub
    Private Sub chkGrupo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGrupo.CheckedChanged
        If Not estaCargando Then
            If chkGrupo.Checked = True Then
                cboGrupo.Enabled = True
                If optVentasProducto.Checked = True Then
                    cargaProductos()
                    muestraVentas()
                Else
                    If optFomasPago.Checked = True Then
                        'cargaProveedores()
                        muestraVentas()
                    End If
                End If
            Else
                cboGrupo.SelectedIndex = -1
                cboGrupo.Enabled = False
            End If
        End If
    End Sub
    Sub muestraVentas()
        Dim msalida As New Salida
        Dim periodo As String = ""
        Dim esHistorial As Boolean = IIf(periodo = periodoActivo(), False, False)
        Dim esMensual As Boolean = False


        Dim grpReceta As Boolean = IIf(optRegistro.Checked = True, True, False)
        Dim xProducto As Boolean = IIf(optVentasProducto.Checked = True And cboGrupo.SelectedIndex >= 0, True, False)
        Dim xInsumo As Boolean = IIf(optFomasPago.Checked = True And cboGrupo.SelectedIndex >= 0, True, False)
        Dim preciosConIMP As Boolean = IIf(chkIMP.Checked = True, True, False)

        Dim cGrupo As String = cboGrupo.SelectedValue
        Dim xcliente, xDocumento, xvendedor, xalmacen As Boolean

        dataDetalle.DataSource = ""
        If optRegistro.Checked = True Then
            dsVentas = msalida.recuperaCabeceras(esHistorial, periodo, esMensual, mcDesde.SelectedDate, mcHasta.SelectedDate, pDecimales, _
                                    xalmacen, "", False, xcliente, cGrupo, xDocumento, cGrupo, xvendedor, cGrupo)
            dataDetalle.DataSource = dsVentas.Tables("ventas").DefaultView
        End If
        If optVentasProducto.Checked = True Then
            dsVentas = msalida.recuperaVentas(mcDesde.SelectedDate, mcHasta.SelectedDate, True, False, pCuentaUser)
            dataDetalle.DataSource = dsVentas.Tables("factura").DefaultView
        End If
        If optFomasPago.Checked = True Then
            dsVentas = msalida.recuperaVentas(mcDesde.SelectedDate, mcHasta.SelectedDate, False, False, pCuentaUser)
            dataDetalle.DataSource = dsVentas.Tables("factura").DefaultView
        End If
        If optGrupoVenta.Checked = True Then
            dsVentas = msalida.recuperaGrupoVentas(mcDesde.SelectedDate, mcHasta.SelectedDate, True, False, pCuentaUser)
            dataDetalle.DataSource = dsVentas.Tables("factura").DefaultView
        End If
        If optAtencion.Checked = True Then
            dsVentas = msalida.recuperaAtenciones(mcDesde.SelectedDate, mcHasta.SelectedDate, False, False, pCuentaUser)
            dataDetalle.DataSource = dsVentas.Tables("factura").DefaultView
        End If
        estructuraventas()
        muestraTotales()
        lblRegistros.Text = "Nro de Registros..." & dataDetalle.RowCount
    End Sub

    Sub muestraTotales()
        Dim cMontoDS As Decimal = 0.0
        Dim I As Integer = 0
        For I = 0 To dataDetalle.RowCount - 1
            If optRegistro.Checked Then
                cMontoDS = cMontoDS + dataDetalle.Item("monto_doc", I).Value
            End If
            If optVentasProducto.Checked Or optGrupoVenta.Checked Then
                cMontoDS = cMontoDS + dataDetalle.Item("monto", I).Value
            End If
            If optFomasPago.Checked Or optAtencion.Checked Then
                cMontoDS = cMontoDS + dataDetalle.Item("monto_pago", I).Value
            End If
        Next
        lblMonedaDS.Text = "Total en " & pMoneda
        lblMontoDS.Text = "Monto..." & Format(cMontoDS, "####,###.##")
    End Sub

    Sub estructuraventas()
        With dataDetalle
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            If optRegistro.Checked = True Then
                .Columns("fec_doc").HeaderText = "Fecha"
                .Columns("fec_doc").Width = 70
                .Columns("fec_doc").DisplayIndex = 0
                .Columns("fec_doc").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("nom_clie").HeaderText = "Cliente"
                .Columns("nom_clie").Width = 250
                .Columns("nom_clie").DisplayIndex = 1
                .Columns("doc").HeaderText = "Documento"
                .Columns("doc").Width = 120
                .Columns("doc").DisplayIndex = 2
                .Columns("SubTotal").HeaderText = "SubTotal"
                .Columns("SubTotal").Width = 65
                .Columns("SubTotal").DisplayIndex = 3
                .Columns("SubTotal").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("SubTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto_igv").HeaderText = "IGV"
                .Columns("monto_igv").Width = 65
                .Columns("monto_igv").DisplayIndex = 4
                .Columns("monto_igv").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto_igv").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto_doc").HeaderText = "Importe"
                .Columns("monto_doc").Width = 65
                .Columns("monto_doc").DisplayIndex = 5
                .Columns("monto_doc").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto_doc").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("nom_fpago").HeaderText = "Forma Pago"
                .Columns("nom_fpago").Width = 100
                .Columns("nom_fpago").DisplayIndex = 6
                .Columns("cant").Visible = False
                .Columns("operacion").Visible = False
                .Columns("descripcion").Visible = False
                .Columns("documento").Visible = False
                .Columns("nro_guia").Visible = False
                .Columns("precio").Visible = False
                .Columns("montosal").Visible = False
                .Columns("nom_alma").Visible = False
                .Columns("fec_pago").Visible = False
                .Columns("ser_doc").Visible = False
                .Columns("cod_vend").Visible = False
                .Columns("nom_vend").Visible = False
                .Columns("nro_doc").Visible = False
                .Columns("cod_doc").Visible = False
                .Columns("cod_clie").Visible = False
                .Columns("codigo").Visible = False
                .Columns("canti").Visible = False
                .Columns("pre_ven").Visible = False
            End If

            If optVentasProducto.Checked = True Then
                .Columns("ser_doc").Visible = False
                .Columns("nro_doc").Visible = False
                .Columns("cod_clie").Visible = False
                .Columns("fecha").Visible = False
                .Columns("fec_doc").Visible = False
                .Columns("raz_soc").Visible = False
                .Columns("dir_clie").Visible = False
                .Columns("monto_doc").Visible = False
                .Columns("tm").Visible = False
                .Columns("pre_inc_igv").Visible = False
                .Columns("nro_maquina").Visible = False
                .Columns("monto_igv").Visible = False
                .Columns("nom_doc").Visible = False
                .Columns("cant").HeaderText = "Cantidad"
                .Columns("cant").Width = 65
                .Columns("cant").DisplayIndex = 1
                .Columns("cant").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("cant").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("cant").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("nom_art").HeaderText = "Articulo"
                .Columns("nom_art").Width = 250
                .Columns("nom_art").DisplayIndex = 2
                .Columns("precio").HeaderText = "precio"
                .Columns("precio").Width = 65
                .Columns("precio").DisplayIndex = 3
                .Columns("precio").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("precio").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto").HeaderText = "Importe"
                .Columns("monto").Width = 65
                .Columns("monto").DisplayIndex = 4
                .Columns("monto").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("nom_sgrupo").HeaderText = "Grupo"
                .Columns("nom_sgrupo").Width = 250
                .Columns("nom_sgrupo").DisplayIndex = 5
                .Columns("pre_costo").HeaderText = "costo"
                .Columns("pre_costo").Width = 65
                .Columns("pre_costo").DisplayIndex = 6
                .Columns("pre_costo").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("pre_costo").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("pre_costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto_costo").HeaderText = "Importe"
                .Columns("monto_costo").Width = 65
                .Columns("monto_costo").DisplayIndex = 7
                .Columns("monto_costo").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto_costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto_costo").DefaultCellStyle.Format = "N" & pDecimales

            End If

            If optGrupoVenta.Checked = True Then
                .Columns("ser_doc").Visible = False
                .Columns("nro_doc").Visible = False
                .Columns("cod_clie").Visible = False
                .Columns("fecha").Visible = False
                .Columns("fec_doc").Visible = False
                .Columns("raz_soc").Visible = False
                .Columns("dir_clie").Visible = False
                .Columns("monto_doc").Visible = False
                .Columns("tm").Visible = False
                .Columns("pre_inc_igv").Visible = False
                .Columns("nro_maquina").Visible = False
                .Columns("monto_igv").Visible = False
                .Columns("nom_doc").Visible = False
                .Columns("nom_art").Visible = False
                .Columns("precio").Visible = False
                .Columns("cant").HeaderText = "Cantidad"
                .Columns("cant").Width = 65
                .Columns("cant").DisplayIndex = 1
                .Columns("cant").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("cant").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("cant").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("nom_sgrupo").HeaderText = "Grupo"
                .Columns("nom_sgrupo").Width = 250
                .Columns("nom_sgrupo").DisplayIndex = 2
                .Columns("monto").HeaderText = "Importe"
                .Columns("monto").Width = 65
                .Columns("monto").DisplayIndex = 4
                .Columns("monto").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto").DefaultCellStyle.Format = "N" & pDecimales

            End If
            If optFomasPago.Checked = True Then
                .Columns("ser_doc").Visible = False
                .Columns("nro_doc").Visible = False
                .Columns("cod_clie").Visible = False
                .Columns("fecha").Visible = False
                .Columns("raz_soc").Visible = False
                .Columns("dir_clie").Visible = False
                .Columns("monto_doc").Visible = False
                .Columns("tm").Visible = False
                .Columns("pre_inc_igv").Visible = False
                .Columns("nro_maquina").Visible = False
                .Columns("monto_igv").Visible = False
                .Columns("nom_doc").Visible = False
                .Columns("fec_doc").HeaderText = "Fecha"
                .Columns("fec_doc").Width = 70
                .Columns("fec_doc").DisplayIndex = 1
                .Columns("fec_doc").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("doc").HeaderText = "Documento"
                .Columns("doc").Width = 250
                .Columns("doc").DisplayIndex = 2
                .Columns("monto_pago").HeaderText = "Importe"
                .Columns("monto_pago").Width = 65
                .Columns("monto_pago").DisplayIndex = 3
                .Columns("monto_pago").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto_pago").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto_pago").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("nom_fpago").HeaderText = "Forma Pago"
                .Columns("nom_fpago").Width = 100
                .Columns("nom_fpago").DisplayIndex = 4
            End If

            If optAtencion.Checked = True Then
                .Columns("cod_clie").Visible = False
                .Columns("monto_doc").Visible = False
                .Columns("tm").Visible = False
                .Columns("pre_inc_igv").Visible = False
                .Columns("nro_maquina").Visible = False
                .Columns("monto_igv").Visible = False
                .Columns("nom_doc").Visible = False
                .Columns("nom_fpago").Visible = False
                .Columns("fec_doc").HeaderText = "Fecha"
                .Columns("fec_doc").Width = 70
                .Columns("fec_doc").DisplayIndex = 1
                .Columns("fec_doc").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("hor_doc").HeaderText = "Hora"
                .Columns("hor_doc").Width = 70
                .Columns("hor_doc").DisplayIndex = 2
                .Columns("hor_doc").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("raz_soc").HeaderText = "Cliente"
                .Columns("raz_soc").Width = 200
                .Columns("raz_soc").DisplayIndex = 3
                .Columns("fono_clie").HeaderText = "Telefono"
                .Columns("fono_clie").Width = 70
                .Columns("fono_clie").DisplayIndex = 4
                .Columns("dir_clie").HeaderText = "Direccion"
                .Columns("dir_clie").Width = 150
                .Columns("dir_clie").DisplayIndex = 5
                .Columns("doc").HeaderText = "Documento"
                .Columns("doc").Width = 120
                .Columns("doc").DisplayIndex = 6
                .Columns("monto_pago").HeaderText = "Importe"
                .Columns("monto_pago").Width = 65
                .Columns("monto_pago").DisplayIndex = 7
                .Columns("monto_pago").DefaultCellStyle.BackColor = Color.AliceBlue
                .Columns("monto_pago").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("monto_pago").DefaultCellStyle.Format = "N" & pDecimales
                .Columns("nom_atencion").HeaderText = "Atencion"
                .Columns("nom_atencion").Width = 100
                .Columns("nom_atencion").DisplayIndex = 8
            End If

        End With
    End Sub


    Sub cargaProductos()

    End Sub
    Private Sub cmdImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimir.Click
        'Dim periodo As String = periodoSeleccionado()
        'Dim periodoReporte As String = "Periodo: " & general.periodo_mesAnnoLetras(periodo)
        'Dim preciosConImp As String = IIf(chkIMP.Checked = True, "*Precios Incluyen Impuesto", "*Precios NO Incluyen Impuesto")
        'Dim fechaReporte As String = general.devuelveFechaImpresionEspecificado(dtiDesde.Value)
        'Dim insAgrupados As Boolean
        'Dim frm As New rptForm
        'If chkTotalizado.Checked = True Then
        '    insAgrupados = True
        '    frm.cargarMicros_insumos(dsVentas, cboAlmacen.Text & ":  Cantidad de Insumos Utilizados en las Ventas Totalizado", fechaReporte, periodoReporte, insAgrupados)
        'Else
        '    insAgrupados = False
        '    frm.cargarMicros_insumos(dsVentas, cboAlmacen.Text & ":  Cantidad de Insumos Utilizados en las Ventas x Receta ", fechaReporte, periodoReporte, insAgrupados)
        'End If
        'frm.MdiParent = principal
        'frm.Show()
    End Sub

    Private Sub mcDesde_DateChanged(sender As Object, e As System.EventArgs) Handles mcDesde.DateChanged
        If Not estaCargando Then
            muestraVentas()
        End If
    End Sub

    Private Sub mcHasta_DateChanged(sender As Object, e As System.EventArgs) Handles mcHasta.DateChanged
        If Not estaCargando Then
            muestraVentas()
        End If
    End Sub



    Private Sub dataDetalle_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataDetalle.CellContentClick

    End Sub

    Private Sub dataDetalle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dataDetalle.KeyDown
        If (e.Control And e.KeyCode = Keys.E) Then
            If dataDetalle.RowCount > 0 Then
                EnviaraExcel(dataDetalle)
            End If
        End If
    End Sub


    Private Sub cmdCerrar_Click(sender As System.Object, e As System.EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub cmdProcesar_Click(sender As System.Object, e As System.EventArgs) Handles cmdProcesar.Click
        Dim msalida As New Salida, mcliente As New cliente
        Dim nroOperacion As Integer
        dsVentas = msalida.recuperaSalida(mcDesde.SelectedDate, mcHasta.SelectedDate)
        DataSALIDA.DataSource = dsVentas.Tables("salida").DefaultView

        For I = 0 To DataSALIDA.RowCount - 1
            If msalida.existeDocumento(DataSALIDA("cod_doc", I).Value, DataSALIDA("ser_doc", I).Value, DataSALIDA("nro_doc", I).Value, 1) = 0 Then
                nroOperacion = msalida.maxOperacion(1)
                cmdProcesar.Text = "Exportando... " & I & " de " & DataSALIDA.RowCount
                cmdProcesar.Refresh()
                If Not mcliente.existe(DataSALIDA("cod_clie", I).Value, 1) Then
                    mcliente.insertar(DataSALIDA("cod_clie", I).Value, DataSALIDA("nom_clie", I).Value, DataSALIDA("nom_clie", I).Value, DataSALIDA("dir_clie", I).Value, "", "", "", "00", 1, 1)
                End If

                msalida.insertar_imp(nroOperacion, DataSALIDA("ope_ped", I).Value, DataSALIDA("fecha", I).Value, DataSALIDA("cod_doc", I).Value, DataSALIDA("ser_doc", I).Value, DataSALIDA("nro_doc", I).Value, DataSALIDA("fec_doc", I).Value, DataSALIDA("cod_vend", I).Value, DataSALIDA("cod_clie", I).Value, DataSALIDA("cod_fpago", I).Value, 1, 1, DataSALIDA("nro_dec", I).Value, DataSALIDA("cod_alma", I).Value, "0000", DataSALIDA("obs", I).Value, DataSALIDA("cod_emp", I).Value, DataSALIDA("cuenta", I).Value, 0, "12", "", 1)

                dsventasdetalle = msalida.recuperaSalidaDetalle(DataSALIDA("operacion", I).Value)
                DataSalidaDetalle.DataSource = dsventasdetalle.Tables("detalle").DefaultView
                For X = 0 To DataSalidaDetalle.RowCount - 1
                    msalida.insertar_detObs(nroOperacion, DataSalidaDetalle("salida", X).Value, DataSalidaDetalle("ingreso", X).Value, DataSalidaDetalle("cod_art", X).Value, DataSalidaDetalle("cant", X).Value, DataSalidaDetalle("precio", X).Value, DataSalidaDetalle("igv", X).Value, 0, 0, "", 0, 1)
                Next

                dsventaspago = msalida.recuperaSalidaPago(DataSALIDA("operacion", I).Value)
                DataSalidaPago.DataSource = dsventaspago.Tables("detallepago").DefaultView
                For X = 0 To DataSalidaPago.RowCount - 1
                    msalida.insertar_detPago(nroOperacion, DataSalidaPago("cod_fpago", X).Value, DataSalidaPago("imp_pagado", X).Value, DataSalidaPago("imp_propina", X).Value, DataSalidaPago("imp_vuelto", X).Value, DataSalidaPago("obs_pago", X).Value, 1)
                Next
            End If

        Next
        MessageBox.Show("Migracion Finalizada...", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        cmdProcesar.Text = "Exportar Datos"
    End Sub

    Private Sub optGrupoVenta_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optGrupoVenta.CheckedChanged
        If Not estaCargando And optGrupoVenta.Checked = True Then
            chkGrupo.Enabled = True
            chkGrupo.Text = "x Grupo"
            cboGrupo.SelectedIndex = -1
            cboGrupo.Enabled = False
            muestraVentas()
        End If
    End Sub

    Private Sub optAtencion_CheckedChanged(sender As Object, e As EventArgs) Handles optAtencion.CheckedChanged
        If Not estaCargando And optAtencion.Checked = True Then
            chkGrupo.Enabled = True
            chkGrupo.Text = ""
            cboGrupo.SelectedIndex = -1
            cboGrupo.Enabled = False
            muestraVentas()
        End If
    End Sub
End Class
