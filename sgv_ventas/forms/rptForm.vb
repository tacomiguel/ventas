Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports libreriaVentas

Public Class rptForm

    Public Sub cargarFactura(ByVal operacion As Integer, ByVal nomformato As String, ByVal vprevia As Boolean)
        Try
            Dim da As New MySqlDataAdapter
            Dim ds As New DataSet
            Dim dt1 As New DataTable("factura")
            Dim dt2 As New DataTable("Empresa")
            Dim dt3 As New DataTable("Pagos")
            ' Dim dt4 As New DataTable("cliente")
            Dim com As New MySqlCommand
            Dim cad, cad1, cad2, cad3, cad4, cad5, cad6, cad7, cad8, cad9, cad10 As String
            cad1 = "select fecha,fec_doc,ser_doc,nro_doc,if(LENGTH( salida.cod_clie)=0,salida.obs,raz_soc) as raz_soc,cliente.cod_clie,dir_clie,salida.monto as monto_doc,salida.tm,"
            cad2 = " salida.monto_igv, salida.pre_inc_igv,if(LENGTH( nom_art2)=0,lower(nom_art),nom_art2) as nom_art,cant as cantidad,"
            cad3 = " precio,cant*precio as monto,igv,nom_fpago,salida_det.obs,d.nro_maquina,monto_pago,v.nom_vend"
            cad4 = " from salida inner join salida_det on salida.operacion=salida_det.operacion"
            cad5 = " left join cliente on salida.cod_clie=cliente.cod_clie inner join vendedor v on v.cod_vend=salida.cod_vend"
            cad6 = " inner join documento_s d on d.cod_doc=salida.cod_doc "
            cad7 = " inner join articulo on salida_det.cod_art=articulo.cod_art"
            cad8 = " inner join forma_pago on salida.cod_fpago=forma_pago.cod_fpago"
            cad9 = " where salida.operacion=" & operacion
            cad10 = " order by salida"
            cad = cad1 + cad2 + cad3 + cad4 + cad5 + cad6 + cad7 + cad8 + cad9 + cad10
            com.CommandText = cad
            com.Connection = dbConex
            da.SelectCommand = com
            da.Fill(ds, "factura")

            Dim cadEmp As String
            cadEmp = "select cod_emp,nom_emp,nom_comercial,dir_emp,dir_sucursal,pais_emp  from configuracion"
            com.CommandText = cadEmp
            com.Connection = dbConex
            da.SelectCommand = com
            da.Fill(ds, "Empresa")

            Dim cadPag As String
            cadPag = "select operacion,nom_fpago,imp_pagado,imp_vuelto  from salida_detpago sd inner join forma_pago f on sd.cod_fpago=f.cod_fpago where sd.operacion=" & operacion
            com.CommandText = cadPag
            com.Connection = dbConex
            da.SelectCommand = com
            da.Fill(ds, "Pagos")

            'Dim cadClie As String
            'cadClie = "select c.cod_clie,nom_clie from salida s inner join cliente c on s.cod_clie=c.cod_clie where s.operacion=" & operacion
            'com.CommandText = cadClie
            'com.Connection = dbConex
            'da.SelectCommand = com
            'da.Fill(ds, "cliente")

            'Dim mirpt As New rptFactura2
            Dim mirpt As New ReportDocument
            mirpt.Load(nomformato)
            mirpt.Subreports.Item("rptEmpresa.rpt").SetDataSource(ds)
            mirpt.Subreports.Item("rptPagos.rpt").SetDataSource(ds)
            'mirpt.Subreports.Item("rptCliente.rpt").SetDataSource(ds)

            mirpt.SetDataSource(ds)
            If vprevia Then
                crv.ReportSource = mirpt
                crv.RefreshReport()
            Else
                mirpt.PrintToPrinter(1, True, 1, 1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
  
    Public Sub cargarComanda(ByVal operacion As Integer, ByVal nom_impresora As String, ByVal vprevia As Boolean)
        'Try
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        Dim pf0 As New ParameterField
        Dim pdv0 As New ParameterDiscreteValue
        Dim pfs As New ParameterFields

        Dim dt As New DataTable("factura")
        Dim com As New MySqlCommand
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim cad, cad1, cad2, cad3, cad4, cad5, cad6, cad7, cad8, cad9, cad10 As String
        cad1 = "select s.ope_ped as operacion,fecha,fec_doc,ser_doc,nro_doc,raz_soc,cliente.cod_clie,dir_clie,s.monto as monto_doc,s.tm,"
        cad2 = " s.monto_igv, s.pre_inc_igv,if(LENGTH( nom_art2)=0,nom_art,nom_art2) as nom_art,cant as cantidad,"
        cad3 = " precio,cant*precio as monto,igv,nom_fpago,s.obs,d.nro_maquina,monto_pago"
        cad4 = " from salida s inner join salida_det sd on s.operacion=sd.operacion"
        cad5 = " left join cliente on s.cod_clie=cliente.cod_clie"
        cad6 = " inner join documento_s d on d.cod_doc=s.cod_doc "
        cad7 = " inner join articulo a on sd.cod_art=a.cod_art inner join subgrupo g on a.cod_grupov=g.cod_sgrupo"
        cad8 = " inner join forma_pago on s.cod_fpago=forma_pago.cod_fpago"
        cad9 = " where s.operacion=" & operacion & " and g.impresora= '" & nom_impresora & "'"
        cad10 = " order by salida"
        cad = cad1 + cad2 + cad3 + cad4 + cad5 + cad6 + cad7 + cad8 + cad9 + cad10
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "factura")

        Dim mirpt As New rptComanda
        pf0.Name = "nom_impresora"
        pdv0.Value = nom_impresora
        pf0.CurrentValues.Add(pdv0)
        pfs.Add(pf0)

        mirpt.SetDataSource(ds)
        If vprevia Then
            crv.ParameterFieldInfo = pfs
            crv.ReportSource = mirpt
            'crv.RefreshReport()
        Else
            mirpt.SetParameterValue(0, nom_impresora)
            mirpt.PrintToPrinter(1, True, 1, 1)
        End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Public Sub cargarCierre(ByVal dsCierre As DataSet, ByVal vprevia As Boolean, ByVal fecha As Date, ByVal xvendedor As Boolean, ByVal codvendedor As String, ByVal nomformato As String)
        Try
            Dim da As New MySqlDataAdapter
            Dim ds As New DataSet
            Dim dt1 As New DataTable("factura")
            Dim dt2 As New DataTable("Empresa")
            Dim dt3 As New DataTable("Pagos")
            Dim dt4 As New DataTable("cliente")
            Dim com As New MySqlCommand
            Dim mfecha As String = fecha.ToString("yyyy-MM-dd")
            Dim pf0 As New ParameterField
            Dim pdv0 As New ParameterDiscreteValue
            Dim pfs As New ParameterFields

            Dim cadEmp As String
            cadEmp = "select cod_emp,nom_emp,nom_comercial,dir_emp,dir_sucursal,pais_emp  from configuracion"
            com.CommandText = cadEmp
            com.Connection = dbConex
            da.SelectCommand = com
            da.Fill(ds, "Empresa")

            Dim cadPag As String
            cadPag = "select s.operacion,nom_fpago,sum(imp_pagado-imp_vuelto) as imp_pagado,imp_vuelto  " _
                 & " from salida s inner join  salida_detpago sdp on s.operacion=sdp.operacion inner join forma_pago f on sdp.cod_fpago=f.cod_fpago " _
                 & " where s.fec_doc='" & mfecha & "'" & IIf(xvendedor, " and s.cuenta='" & codvendedor & "'", "") & " group by sdp.cod_fpago"
            com.CommandText = cadPag
            com.Connection = dbConex
            da.SelectCommand = com
            da.Fill(ds, "Pagos")

            'Dim cadClie As String
            'cadClie = "select c.cod_clie,nom_clie from salida s inner join cliente c on s.cod_clie=c.cod_clie where s.operacion=" & operacion
            'com.CommandText = cadClie
            'com.Connection = dbConex
            'da.SelectCommand = com
            'da.Fill(ds, "cliente")
            'Dim mirptX As New rptCierre
            'Dim mirptZ As New rptCierreZ
            'If tip_reporte = "X" Then
            '    Dim mirpt As New mirptX
            'Else
            '    Dim mirpt As New rptCierreZ
            'End If

            pf0.Name = "nombreUser"
            pdv0.Value = pNombreUser
            pf0.CurrentValues.Add(pdv0)
            pfs.Add(pf0)

            Dim mirpt As New ReportDocument
            mirpt.Load(nomformato)
            mirpt.Subreports.Item("rptEmpresa.rpt").SetDataSource(ds)
            mirpt.Subreports.Item("rptPagos.rpt").SetDataSource(ds)
            'mirpt.Subreports.Item("rptCliente.rpt").SetDataSource(ds)

            mirpt.SetDataSource(dsCierre)
            If vprevia Then
                crv.ParameterFieldInfo = pfs
                crv.ReportSource = mirpt
                'crv.RefreshReport()
            Else
                mirpt.SetParameterValue(0, pNombreUser)
                mirpt.PrintToPrinter(1, True, 1, 1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub crv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crv.Load

    End Sub

    Private Sub rptForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
