Imports MySql.Data.MySqlClient
Public Class Salida
    Public Shared Function dsSalida() As DataSet
        Dim ds As New DataSet
        ds.Tables.Add(crearTablaDetalle())
        ds.Tables.Add(crearTablaCabecera())
        ds.Tables.Add(crearTablaSalida())
        ds.Tables.Add(crearTablaPago())
        Return ds
    End Function
    Private Shared Function crearTablaPago() As DataTable
        Dim dt As New DataTable("Pago")
        dt.Columns.Add(New DataColumn("operacion", GetType(Integer)))
        dt.Columns.Add(New DataColumn("cod_pago", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_pago", GetType(String)))
        dt.Columns.Add(New DataColumn("monto", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("propina", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("imp_vuelto", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("observacion", GetType(String)))
        Return dt
    End Function
    Private Shared Function crearTablaCabecera() As DataTable
        Dim dt As New DataTable("cabecera")
        dt.Columns.Add(New DataColumn("operacion", GetType(Integer)))
        dt.Columns.Add(New DataColumn("nro_ped", GetType(Integer)))
        dt.Columns.Add(New DataColumn("cod_doc", GetType(String)))
        dt.Columns.Add(New DataColumn("ser_doc", GetType(String)))
        dt.Columns.Add(New DataColumn("nro_doc", GetType(String)))
        dt.Columns.Add(New DataColumn("fec_doc", GetType(Date)))
        dt.Columns.Add(New DataColumn("cod_alma", GetType(String)))
        dt.Columns.Add(New DataColumn("cod_vend", GetType(String)))
        dt.Columns.Add(New DataColumn("cod_clie", GetType(String)))
        dt.Columns.Add(New DataColumn("cod_fpago", GetType(String)))
        dt.Columns.Add(New DataColumn("tm", GetType(String)))
        dt.Columns.Add(New DataColumn("anul", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("pre_inc_igv", GetType(Boolean)))
        Return dt
    End Function
    Private Shared Function crearTablaDetalle() As DataTable
        Dim dt As New DataTable("detalle")
        dt.Columns.Add(New DataColumn("operacion", GetType(Integer)))
        dt.Columns.Add(New DataColumn("salida", GetType(Integer)))
        dt.Columns.Add(New DataColumn("ingreso", GetType(Integer)))
        dt.Columns.Add(New DataColumn("cod_art", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_uni", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_art", GetType(String)))
        dt.Columns.Add(New DataColumn("cantidad", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("precio", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("afecto_igv", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("neto", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("orden", GetType(Integer)))
        dt.Columns.Add(New DataColumn("saldo", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("igv", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("comi_v", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("comi_jv", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("estado", GetType(String)))
        dt.Columns.Add(New DataColumn("proceso", GetType(String)))
        dt.Columns.Add(New DataColumn("obs", GetType(String)))
        Return dt
    End Function
    Private Shared Function crearTablaSalida() As DataTable
        Dim dt As New DataTable("salida")
        dt.Columns.Add(New DataColumn("operacion", GetType(Integer)))
        dt.Columns.Add(New DataColumn("nro_ped", GetType(Integer)))
        dt.Columns.Add(New DataColumn("ingreso", GetType(Integer)))
        dt.Columns.Add(New DataColumn("salida", GetType(Integer)))
        dt.Columns.Add(New DataColumn("cod_doc", GetType(String)))
        dt.Columns.Add(New DataColumn("ser_doc", GetType(String)))
        dt.Columns.Add(New DataColumn("nro_doc", GetType(String)))
        dt.Columns.Add(New DataColumn("fec_doc", GetType(Date)))
        dt.Columns.Add(New DataColumn("cod_vend", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_vend", GetType(String)))
        dt.Columns.Add(New DataColumn("cod_alma", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_alma", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_uni", GetType(String)))
        dt.Columns.Add(New DataColumn("cod_clie", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_clie", GetType(String)))
        dt.Columns.Add(New DataColumn("raz_soc", GetType(String)))
        dt.Columns.Add(New DataColumn("dir_clie", GetType(String)))
        dt.Columns.Add(New DataColumn("dir_ent", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_cont", GetType(String)))
        dt.Columns.Add(New DataColumn("cod_fpago", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_fpago", GetType(String)))
        dt.Columns.Add(New DataColumn("fono_clie", GetType(String)))
        dt.Columns.Add(New DataColumn("tm", GetType(String)))
        dt.Columns.Add(New DataColumn("obs", GetType(String)))
        dt.Columns.Add(New DataColumn("cant", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("cod_art", GetType(String)))
        dt.Columns.Add(New DataColumn("nom_art", GetType(String)))
        dt.Columns.Add(New DataColumn("precio", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("saldo", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("pre_inc_igv", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("comi_v", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("comi_jv", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("anul", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("estado", GetType(String)))
        Return dt
    End Function
    Public Function recuperaCabeceras(ByVal esHistorial As Boolean, ByVal Proceso As String, _
                ByVal esMensual As Boolean, ByVal fechaInicio As Date, ByVal fechaFinal As Date, _
                ByVal nroDecimales As Integer, ByVal xAlmacen As Boolean, ByVal cod_alma As String, _
                ByVal xVenta As Boolean, ByVal xCliente As Boolean, ByVal cod_clie As String, ByVal xDocumento As Boolean, _
                ByVal cod_doc As String, ByVal xVendedor As Boolean, ByVal cod_vend As String) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim mfechaI As String = fechaInicio.ToString("yyyy-MM-dd")
        Dim mfechaF As String = fechaFinal.ToString("yyyy-MM-dd")
        Dim cad, cad1, cad2, cad21, cad3, cad4, cad5, cad6, cad7, cad8, cad71, cad72 As String
        cad1 = "select h.operacion,fec_doc,if(nom_clie='-',obs,nom_clie) as nom_clie,raz_soc as descripcion,concat(nom_doc,': ',ser_doc,'-',nro_doc)as documento," _
               & "concat(abr,': ',ser_doc,'-',nro_doc)as doc,concat(ser_guia,'-',nro_guia) as nro_guia,monto as monto_doc,monto_igv,"
        cad2 = " monto-monto_igv as subTotal,0 as cant,0 as precio,0 as montoSal,nom_alma,fec_pago,ser_doc,h.cod_vend,nom_vend," _
                & "nro_doc,h.cod_doc,h.cod_clie,nom_fpago"
        cad21 = IIf(xVenta, ",codigo,canti,pre_ven", ",'' as codigo,0 as canti,0 as pre_ven")
        cad3 = IIf(esHistorial, " from h_salida as h", " from salida as h")
        cad71 = IIf(xVenta, " inner join micros_imp on h.caux2=micros_imp.operacion", "")
        cad72 = " left join guia_remision g on h.operacion=g.operacion "
        cad4 = " inner join cliente on h.cod_clie=cliente.cod_clie"
        cad5 = " inner join documento_s on h.cod_doc=documento_s.cod_doc"
        cad6 = " inner join forma_pago on h.cod_fpago=forma_pago.cod_fpago"
        cad7 = " inner join almacen on h.cod_alma=almacen.cod_alma inner join vendedor on h.cod_vend=vendedor.cod_vend "
        cad8 = " where documento_s.esVenta=1 " & IIf(xAlmacen, " and h.cod_alma ='" & cod_alma & "'", "") _
                & IIf(esHistorial, " and year(h.fec_doc)='" & Proceso & "'", "") _
                & IIf(esMensual, " ", " and fec_doc>='" & mfechaI & "'" & " and fec_doc<='" & mfechaF & "'") _
                & IIf(xCliente, "  and h.cod_clie='" & cod_clie & "'", "") _
                & IIf(xDocumento, " and h.cod_doc='" & cod_doc & "'", "") _
                & IIf(xVendedor, " and h.cod_vend='" & cod_vend & "'", "") _
                & " order by fec_doc desc"

        cad = cad1 + cad2 + cad21 + cad3 + cad4 + cad5 + cad6 + cad7 + cad71 + cad72 + cad8
        Dim com As New MySqlCommand(cad)
        com.Connection = clConex
        com.CommandTimeout = 300
        da.SelectCommand = com
        da.Fill(ds, "ventas")
        clConex.Close()
        Return ds
    End Function

    Public Function recuperaVentas(ByVal fechaI As Date, ByVal fechaF As Date, ByVal xarticulo As Boolean, ByVal xvendedor As Boolean, ByVal codvendedor As String) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim mfechaI As String = fechaI.ToString("yyyy-MM-dd")
        Dim mfechaF As String = fechaF.ToString("yyyy-MM-dd")

        Dim cad, cad1, cad2, cad3, cad4, cad5, cad6, cad7, cad8 As String
        cad1 = "select fecha,fec_doc,ser_doc,nro_doc,raz_soc,cliente.cod_clie,dir_clie,monto as monto_doc,tm,"
        cad2 = " monto_igv, pre_inc_igv,d.nro_maquina,nom_doc"
        cad3 = IIf(xarticulo, ",precio,sum(cant)*precio as monto,sum(cant) as cant,pre_costo,sum(cant)*pre_costo as monto_costo,if(LENGTH( nom_art2)=0,nom_art,nom_art2) as nom_art,nom_sgrupo", ",concat(abr,': ',ser_doc,'-',nro_doc)as doc,(sdp.imp_pagado-sdp.imp_vuelto) as monto_pago,nom_fpago")
        cad4 = " from factura f left join cliente on f.cod_clie=cliente.cod_clie"
        cad5 = " inner join documento_s d on d.cod_doc=f.cod_doc "
        cad6 = IIf(xarticulo, " inner join factura_det sd on f.operacion=sd.operacion inner join articulo a on sd.cod_art=a.cod_art inner join subgrupo g on g.cod_sgrupo=a.cod_sgrupo", " inner join factura_detpago sdp on f.operacion=sdp.operacion inner join forma_pago on sdp.cod_fpago=forma_pago.cod_fpago ")
        cad7 = " where fec_doc>='" & mfechaI & "'" & " and fec_doc<='" & mfechaF & "'" & IIf(xvendedor, " and cuenta='" & codvendedor & "'", "")
        cad8 = IIf(xarticulo, " group by sd.cod_art,sd.precio order by sd.cod_art,sd.precio ", "")
        cad = cad1 + cad2 + cad3 + cad4 + cad5 + cad6 + cad7 + cad8
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "factura")
        Return ds
    End Function
    Public Function recuperaAtenciones(ByVal fechaI As Date, ByVal fechaF As Date, ByVal xGrupo As Boolean, ByVal xvendedor As Boolean, ByVal codvendedor As String) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim mfechaI As String = fechaI.ToString("yyyy-MM-dd")
        Dim mfechaF As String = fechaF.ToString("yyyy-MM-dd")

        Dim cad, cad1, cad2, cad3, cad4, cad5, cad6, cad7, cad8, cad9, cad10 As String
        cad1 = "select time(fecha) as hor_doc,fec_doc,raz_soc,cliente.cod_clie,dir_clie,fono_clie,salida.monto as monto_doc,salida.tm,"
        cad2 = " salida.monto_igv, salida.pre_inc_igv,d.nro_maquina,nom_doc,dsc_recurso as nom_atencion"
        cad3 = IIf(xGrupo, ",precio,sum(cant*precio) as monto,sum(cant) as cant,if(LENGTH( nom_art2)=0,nom_art,nom_art2) as nom_art,nom_sgrupo", ",concat(abr,': ',ser_doc,'-',nro_doc)as doc,(sdp.imp_pagado-sdp.imp_vuelto) as monto_pago,nom_fpago")
        cad4 = " from salida "
        cad5 = " left join cliente on salida.cod_clie=cliente.cod_clie"
        cad6 = " inner join documento_s d on d.cod_doc=salida.cod_doc inner join tipo_recurso tr on tr.cod_recurso=salida. cAux3 "
        cad7 = IIf(xGrupo, " inner join salida_det sd on salida.operacion=sd.operacion inner join articulo a on sd.cod_art=a.cod_art inner join subgrupo g on g.cod_sgrupo=a.cod_sgrupo", " inner join salida_detpago sdp on salida.operacion=sdp.operacion inner join forma_pago on sdp.cod_fpago=forma_pago.cod_fpago ")
        cad8 = " "
        cad9 = " where fec_doc>='" & mfechaI & "'" & " and fec_doc<='" & mfechaF & "'" & IIf(xvendedor, " and salida.cod_vend='" & codvendedor & "'", "")
        cad10 = IIf(xGrupo, " group by a.cod_sgrupo order by a.cod_sgrupo ", "")
        cad = cad1 + cad2 + cad3 + cad4 + cad5 + cad6 + cad7 + cad8 + cad9 + cad10
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "factura")
        Return ds
    End Function

    Public Function recuperaGrupoVentas(ByVal fechaI As Date, ByVal fechaF As Date, ByVal xGrupo As Boolean, ByVal xvendedor As Boolean, ByVal codvendedor As String) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim mfechaI As String = fechaI.ToString("yyyy-MM-dd")
        Dim mfechaF As String = fechaF.ToString("yyyy-MM-dd")

        Dim cad, cad1, cad2, cad3, cad4, cad5, cad6, cad7, cad8, cad9, cad10 As String
        cad1 = "select fecha,fec_doc,ser_doc,nro_doc,raz_soc,cliente.cod_clie,dir_clie,salida.monto as monto_doc,salida.tm,"
        cad2 = " salida.monto_igv, salida.pre_inc_igv,d.nro_maquina,nom_doc"
        cad3 = IIf(xGrupo, ",precio,sum(cant*precio) as monto,sum(cant) as cant,if(LENGTH( nom_art2)=0,nom_art,nom_art2) as nom_art,nom_sgrupo", ",concat(abr,': ',ser_doc,'-',nro_doc)as doc,(sdp.imp_pagado-sdp.imp_vuelto) as monto_pago,nom_fpago")
        cad4 = " from salida "
        cad5 = " left join cliente on salida.cod_clie=cliente.cod_clie"
        cad6 = " inner join documento_s d on d.cod_doc=salida.cod_doc "
        cad7 = IIf(xGrupo, " inner join salida_det sd on salida.operacion=sd.operacion inner join articulo a on sd.cod_art=a.cod_art inner join subgrupo g on g.cod_sgrupo=a.cod_sgrupo", " inner join salida_detpago sdp on salida.operacion=sdp.operacion inner join forma_pago on sdp.cod_fpago=forma_pago.cod_fpago ")
        cad8 = " "
        cad9 = " where fec_doc>='" & mfechaI & "'" & " and fec_doc<='" & mfechaF & "'" & IIf(xvendedor, " and salida.cod_vend='" & codvendedor & "'", "")
        cad10 = IIf(xGrupo, " group by a.cod_sgrupo order by a.cod_sgrupo ", "")
        cad = cad1 + cad2 + cad3 + cad4 + cad5 + cad6 + cad7 + cad8 + cad9 + cad10
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "factura")
        Return ds
    End Function

    Public Function recuperaCabecera(ByVal operacion As Integer, ByVal esPedido As Boolean) As DataTable
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As DataSet = Salida.dsSalida
        Dim dt As DataTable = ds.Tables("cabecera")
        clConex = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        Dim cad, cad1, cad2, cad3, cad4 As String
        If esPedido Then
            cad1 = "select salida.operacion,ope_ped,cod_doc,ser_doc,nro_doc,fec_doc,ser_ped,nro_ped,salida.cod_vend,nom_vend,cAux2,cAux3,"
            cad2 = " salida.cod_clie,raz_soc,dir_clie,salida.cod_fpago,salida.pre_inc_igv,salida.anul,salida.tm,salida.obs"
            cad3 = " from salida left join pedido on salida.ope_ped=pedido.operacion left join cliente on salida.cod_clie=cliente.cod_clie"
            cad4 = " left join vendedor on salida.cod_vend=vendedor.cod_vend where salida.operacion=" & operacion
        Else
            cad1 = "select salida.operacion,cod_doc,ser_doc,nro_doc,'0' as ser_ped,'00000' as nro_ped,fec_doc,salida.cod_alma,nom_alma,obs,cod_vend,cAux2,cAux3,"
            cad2 = " salida.cod_clie,raz_soc,dir_clie,fono_clie,salida.cod_fpago,salida.pre_inc_igv,salida.tm"
            cad3 = " from salida inner join cliente on salida.cod_clie=cliente.cod_clie inner join almacen on salida.cod_alma=almacen.cod_alma"
            cad4 = " where Salida.operacion = " & operacion
        End If
        cad = cad1 + cad2 + cad3 + cad4
        com.Connection = clConex
        com.CommandText = cad
        da.SelectCommand = com
        da.Fill(ds, "cabecera")
        clConex.Close()
        Return dt
    End Function

    Public Function recuperaDetalle(ByVal operacion As Integer, ByVal nroDecimales As Integer) As DataTable
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As DataSet = Salida.dsSalida
        Dim dt As DataTable = ds.Tables("detalle")
        clConex = Conexion.obtenerConexion
        Dim cad, cad1, cad2, cad3, cad4, cad5 As String
        cad1 = "select salida,operacion,ingreso,salida_det.cod_art,nom_uni,cant as cantidad,If(LENGTH(obs) > 0,obs ,nom_art ) as nom_art,"
        If nroDecimales = 3 Then
            cad2 = " precio,round(cant*precio,3) as neto,igv,afecto_igv,'reg' as estado,comi_v"
        Else
            cad2 = " precio,round(cant*precio,2) as neto,igv,afecto_igv,'reg' as estado,comi_v"
        End If
        cad3 = " from salida_det inner join articulo on salida_det.cod_art=articulo.cod_art"
        cad4 = " inner join unidad on articulo.cod_uni=unidad.cod_uni"
        cad5 = " where operacion=" & operacion & " order by salida"
        cad = cad1 + cad2 + cad3 + cad4 + cad5
        Dim com As New MySqlCommand(cad)
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "detalle")
        clConex.Close()
        Return dt
    End Function

    Public Function recuperaPago(ByVal operacion As Integer) As DataTable
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As DataSet = Salida.dsSalida
        Dim dt As DataTable = ds.Tables("pago")
        clConex = Conexion.obtenerConexion
        Dim cad, cad1, cad2, cad3, cad4 As String
        cad1 = " select s.operacion,imp_pagado as monto,imp_propina as propina,obs_pago as observacion,sp.cod_fpago as cod_pago,fp.nom_fpago as nom_pago "
        cad2 = " from salida s inner join salida_detpago sp on s.operacion=sp.operacion"
        cad3 = " inner join forma_pago fp on fp.cod_fpago=sp.cod_fpago "
        cad4 = " where s.operacion=" & operacion & " order by s.cod_fpago"
        cad = cad1 + cad2 + cad3 + cad4
        Dim com As New MySqlCommand(cad)
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "Pago")
        clConex.Close()
        Return dt
    End Function

    Public Function recuperaSalida(ByVal fechaI As Date, ByVal fechaF As Date) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim mfechaI As String = fechaI.ToString("yyyy-MM-dd")
        Dim mfechaF As String = fechaF.ToString("yyyy-MM-dd")
        Dim cad, cad1, cad2 As String
        cad1 = "select *,c.nom_clie,c.dir_clie from salida s inner join cliente c on s.cod_clie=c.cod_clie "
        cad2 = " where fec_doc>='" & mfechaI & "'" & " and fec_doc<='" & mfechaF & "'"
        cad = cad1 + cad2
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "salida")
        clConex.Close()
        Return ds
    End Function
    Public Function recuperaSalidaDetalle(ByVal operacion As Integer) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad, cad1, cad2 As String
        cad1 = " select *  from salida_Det"
        cad2 = " where operacion=" & operacion
        cad = cad1 + cad2
        Dim com As New MySqlCommand(cad)
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "detalle")
        clConex.Close()
        Return ds
    End Function
    Public Function recuperaSalidaPago(ByVal operacion As Integer) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad, cad1, cad2 As String
        cad1 = " select *  from salida_detpago"
        cad2 = " where operacion=" & operacion
        cad = cad1 + cad2
        Dim com As New MySqlCommand(cad)
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "detallePago")
        clConex.Close()
        Return ds
    End Function
    Public Function insertar_factura(ByVal nro_ope As Integer,
                            ByVal ope_ped As Integer,
                            ByVal cod_doc As String,
                            ByVal ser_doc As String,
                            ByVal nro_doc As String,
                            ByVal fecha As DateTime,
                            ByVal fec_doc As Date,
                            ByVal fec_prod As Date,
                            ByVal cod_vend As String,
                            ByVal cod_clie As String,
                            ByVal cod_fpago As String,
                            ByVal cancelado As Integer,
                            ByVal pre_inc_igv As Integer,
                            ByVal nro_dec As Integer,
                            ByVal cod_alma As String,
                            ByVal cod_area As String,
                            ByVal obs As String,
                            ByVal cod_emp As String,
                            ByVal usuario As String,
                            ByVal nro_ptovta As String,
                            ByVal subtotal As Decimal,
                            ByVal tip_doc_ref As String,
                            ByVal ser_doc_ref As String,
                            ByVal nro_doc_ref As String,
                            ByVal cod_mesa As String,
                            ByVal cod_atencion As String,
                            ByVal contacto As String,
                            ByVal tc As Decimal
                            ) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        Dim mfecha1 As String = fecha.ToString("yyyy-MM-dd hh:mm:ss")
        Dim mfecha As String = fec_doc.ToString("yyyy-MM-dd")
        Dim mfechaProd As String = fec_prod.ToString("yyyy-MM-dd")
        sql = "Insert Into factura(operacion,ope_ped,cod_doc,ser_doc,nro_doc,fecha,fec_doc,fec_prod,imp_subtotal,tc,cod_mesa,cod_atencion,contacto,
                cod_vend,cod_clie,cod_fpago,cancelado,pre_inc_igv,nro_dec,cod_alma,cod_area,obs,cod_emp,cuenta,nro_ptovta,cod_doc_ref,ser_doc_ref,nro_doc_ref)" &
            "values(" &
            nro_ope & "," & ope_ped & ",'" & cod_doc & "','" & ser_doc & "','" & nro_doc & "','" & mfecha1 & "','" & mfecha & "','" & mfechaProd & "'," & subtotal & "," & tc &
            ",'" & cod_mesa & "','" & cod_atencion & "','" & contacto &
            "','" & cod_vend & "','" & cod_clie & "','" & cod_fpago & "'," & cancelado & "," & pre_inc_igv & "," & nro_dec & ",'" & cod_alma & "','" & cod_area & "','" & obs &
            "','" & cod_emp & "','" & usuario & "','" & nro_ptovta & "','" & tip_doc_ref & "','" & ser_doc_ref & "','" & nro_doc_ref & "')"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result

    End Function
    Public Function insertar_factura_det(ByVal nro_ope As Integer,
                                ByVal salida As Integer,
                                ByVal ingreso As Integer,
                                ByVal cod_art As String,
                                ByVal cant As Decimal,
                                ByVal precio As Decimal,
                                ByVal igv As Decimal,
                                ByVal obs As String,
                                ByVal imptotal As Decimal,
                                ByVal impsubtotal As Decimal,
                                ByVal impigv As Decimal
                                ) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Insert Into factura_det(operacion,salida,ingreso,cod_art,imp_subtotal,imp_total,imp_igv,cant,precio,igv,descripcion)" &
            "values(" &
            nro_ope & "," & salida & "," & ingreso & ",'" & cod_art & "'," & impsubtotal & "," & imptotal & "," & impigv & "," & cant & "," & precio & "," & igv & ",'" & obs & "')"
        com.CommandText = sql
        com.CommandTimeout = 800
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function
    Public Function insertar_aux(ByVal nro_ope As Integer,
                        ByVal ope_ped As Integer,
                        ByVal cod_doc As String,
                        ByVal ser_doc As String,
                        ByVal nro_doc As String,
                        ByVal fec_doc As Date,
                        ByVal cod_vend As String,
                        ByVal cod_clie As String,
                        ByVal cod_fpago As String,
                        ByVal cancelado As Integer,
                        ByVal pre_inc_igv As Integer,
                        ByVal nro_dec As Integer,
                        ByVal cod_alma As String,
                        ByVal cod_area As String,
                        ByVal obs As String,
                        ByVal cod_emp As String,
                        ByVal usuario As String,
                        ByVal nAux As Integer,
                        ByVal cAux As String,
                        ByVal cAux2 As String,
                        ByVal cAux3 As String, ByVal cAux4 As String, ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If


        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        Dim mfecha As String = fec_doc.ToString("yy-MM-dd")
        sql = "Insert Into salida(operacion,ope_ped,cod_doc,ser_doc,nro_doc,fec_doc,cod_vend,cod_clie,cod_fpago,cancelado,pre_inc_igv,nro_dec,cod_alma,cod_area,obs,cod_emp,cuenta,nAux,cAux,cAux2,cAux3,cAux4)" &
            "values(" &
            nro_ope & "," & ope_ped & ",'" & cod_doc & "','" & ser_doc & "','" & nro_doc & "','" & mfecha & "','" & cod_vend & "','" & cod_clie & "','" & cod_fpago & "'," & cancelado & "," & pre_inc_igv & "," & nro_dec & ",'" & cod_alma & "','" & cod_area & "','" & obs & "','" & cod_emp & "','" & usuario & "'," & nAux & ",'" & cAux & "','" & cAux2 & "','" & cAux3 & "','" & cAux4 & "')"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return True
    End Function
    Public Function insertar_imp(ByVal nro_ope As Integer, _
                        ByVal ope_ped As Integer, _
                        ByVal fecha As DateTime, _
                        ByVal cod_doc As String, _
                        ByVal ser_doc As String, _
                        ByVal nro_doc As String, _
                        ByVal fec_doc As Date, _
                        ByVal cod_vend As String, _
                        ByVal cod_clie As String, _
                        ByVal cod_fpago As String, _
                        ByVal cancelado As Integer, _
                        ByVal pre_inc_igv As Integer, _
                        ByVal nro_dec As Integer, _
                        ByVal cod_alma As String, _
                        ByVal cod_area As String, _
                        ByVal obs As String, _
                        ByVal cod_emp As String, _
                        ByVal usuario As String, _
                        ByVal nAux As Integer, _
                        ByVal cAux As String, _
                        ByVal cAux2 As String, ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If

        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        Dim mfecha As String = fec_doc.ToString("yy-MM-dd")
        Dim mfecha1 As String = fecha.ToString("yy-MM-dd HH:mm:ss")
        sql = "Insert Into salida(operacion,ope_ped,fecha,cod_doc,ser_doc,nro_doc,fec_doc,cod_vend,cod_clie,cod_fpago,cancelado,pre_inc_igv,nro_dec,cod_alma,cod_area,obs,cod_emp,cuenta,nAux,cAux,cAux2)" & _
            "values(" & _
            nro_ope & "," & ope_ped & ",'" & mfecha1 & "','" & cod_doc & "','" & ser_doc & "','" & nro_doc & "','" & mfecha & "','" & cod_vend & "','" & cod_clie & "','" & cod_fpago & "'," & cancelado & "," & pre_inc_igv & "," & nro_dec & ",'" & cod_alma & "','" & cod_area & "','" & obs & "','" & cod_emp & "','" & usuario & "'," & nAux & ",'" & cAux & "','" & cAux2 & "')"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return True
    End Function
    Public Function insertar_detObs(ByVal nro_ope As Integer, _
                                ByVal salida As Integer, _
                                ByVal ingreso As Integer, _
                                ByVal cod_art As String, _
                                ByVal cant As Decimal, _
                                ByVal precio As Decimal, _
                                ByVal igv As Decimal, _
                                ByVal cv As Decimal, _
                                ByVal cv1 As Decimal, _
                                ByVal obs As String, ByVal nAux As Integer, ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If

        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Insert Into salida_det(operacion,salida,ingreso,cod_art,cant,precio,igv,comi_v,comi_jv,obs,nAux)" & _
            "values(" & _
            nro_ope & "," & salida & "," & ingreso & ",'" & cod_art & "'," & cant & "," & precio & "," & igv & "," & cv & "," & cv1 & ",'" & obs & "'," & nAux & ")"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function

    Public Function insertar_detPago(ByVal nro_ope As Integer, _
                            ByVal cod_fpago As String, _
                            ByVal imp_pagado As Decimal, _
                            ByVal imp_propina As Decimal, _
                            ByVal imp_vuelto As Decimal, _
                            ByVal obs_Pago As String, ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If

        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Insert Into salida_detPago(operacion,cod_fpago,imp_pagado,imp_propina,imp_vuelto,obs_pago)" & _
            "values(" & _
            nro_ope & ",'" & cod_fpago & "'," & imp_pagado & "," & imp_propina & "," & imp_vuelto & ",'" & obs_Pago & "')"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function

    Public Function Actualiza_montopago(ByVal nro_ope As Integer, ByVal monto As Decimal) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "update salida set monto_pago= " & monto & " where operacion=" & nro_ope
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function

    Public Function Actualiza_cambioMesa(ByVal nro_ope As Integer, ByVal mesa As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "update salida set Caux2= '" & mesa & "' where operacion=" & nro_ope
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function

    Public Function Actualiza_DetcambioMesa(ByVal nro_opeX As Integer, ByVal nro_Ope As Integer, ByVal ingreso As Integer) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql, sql1 As String, result As Integer
        sql = "update salida_det set operacion= " & nro_opeX & ", ingreso = ingreso + 100  where operacion=" & nro_Ope & " and ingreso= " & ingreso

        com.CommandText = sql
        result = com.ExecuteNonQuery
        sql1 = "update salida_det set cant = cant  where operacion=" & nro_opeX
        com.CommandText = sql1
        result = com.ExecuteNonQuery

        clConex.Close()
        Return result
    End Function


    Public Function AnularDetalle(ByVal eshis As Boolean, _
                                 ByVal proceso As String, _
                                 ByVal nroOperacion As Integer, _
                         ByVal nroIngreso As Integer, _
                         ByVal factor1 As Decimal, ByVal factor2 As Decimal) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim result As Integer
        Dim cad, cad1, cad2 As String
        cad1 = "update " & IIf(eshis, "h_salida_det", "salida_det") & " set cant= cant*" & factor1 & ",nAux= " & factor2
        cad2 = " where " & IIf(eshis, "proceso='" & proceso & "' and ", "") & "operacion=" & nroOperacion & " and ingreso=" & nroIngreso
        cad = cad1 + cad2
        com.CommandText = cad
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function
    Public Function Actualiza_documento(ByVal nro_ope As Integer, ByVal lcancelado As Integer, ByVal fecha_pago As Date, ByVal cod_doc As String, ByVal Caux As String, ByVal ser_doc As String, ByVal nro_doc As String, _
                                        ByVal cod_user As String, ByVal cod_clie As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        Dim mfecha As String = fecha_pago.ToString("yyyy-MM-dd")
        sql = "update salida set cancelado=" & lcancelado & ",fec_pago='" & mfecha & "',cod_doc= '" & cod_doc & "', ser_doc= '" & ser_doc & "',nro_doc='" & nro_doc & "',cuenta='" & cod_user & "', " _
               & "cAux='" & Caux & "',cod_clie='" & cod_clie & "' where operacion=" & nro_ope
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function
    Public Function eliminaItem(ByVal nro_ope As Integer, ByVal nroIngreso As Integer) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim cad As String, result As Integer
        cad = "delete from salida_det where operacion=" & nro_ope & " and ingreso=" & nroIngreso
        com.CommandText = cad
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function
    Public Function maxOperacion(ByVal tipconex As Integer) As Integer
        Dim clConex As MySqlConnection
        Dim com As New MySqlCommand, result As Integer
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If
        com.Connection = clConex
        com.CommandText = "select max(operacion) from salida"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result + 1
    End Function
    Public Function maxOperacion_fac() As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer
        com.Connection = clConex
        com.CommandText = "select max(operacion) from factura"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result + 1
    End Function
    Public Function maxSalida() As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer
        com.Connection = clConex
        com.CommandText = "select max(salida) from salida_det"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result + 1
    End Function
    Public Function maxfactura_det() As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer
        com.Connection = clConex
        com.CommandText = "select max(salida) from factura_det"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result + 1
    End Function
    Public Function maxIngreso(ByVal nro_ope As Integer) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer
        com.Connection = clConex
        com.CommandText = "select max(ingreso) from salida_det where operacion=" & nro_ope
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result + 1
    End Function

    Public Function maxPedido() As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer
        com.Connection = clConex
        com.CommandText = "select max(ope_ped) from salida"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result + 1
    End Function

    Public Function existeDocumento(ByVal cod_doc As String, ByVal ser_doc As String, ByVal nro_doc As String, ByVal tipconex As Integer) As Integer
        Dim clConex As MySqlConnection
        Dim com As New MySqlCommand, result As Integer, sql As String
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If
        com.Connection = clConex
        sql = "Select operacion from salida where cod_doc='" & cod_doc & "' and ser_doc='" & ser_doc & "'" _
                & " and nro_doc='" & nro_doc & "'"
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function

    Public Function existeOperacion(ByVal operacion As Integer, ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        Dim com As New MySqlCommand, result As Integer, sql As String
        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If
        com.Connection = clConex
        sql = "Select operacion from salida where operacion=" & operacion
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Integer)
        clConex.Close()
        Return result
    End Function
    Public Function buscarMesa(ByVal cod_mesa As String, ByVal fecha As Date) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, sql As String
        Dim mfechaI As String = fecha.ToString("yyyy-MM-dd")
        com.Connection = clConex
        sql = "Select operacion from salida where LENGTH(nro_doc)=0 and fec_doc='" & mfechaI & "' and cAux2='" & cod_mesa & "'"
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function
    Public Function buscarPedido(ByVal nro_pedido As Integer) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, sql As String
        com.Connection = clConex
        sql = "Select operacion from salida where LENGTH(nro_doc)=0 and ope_ped=" & nro_pedido

        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function
    Public Function buscarOperacionPedido(ByVal nro_operacion As Integer) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, sql As String

        com.Connection = clConex
        sql = "Select ope_ped from salida where  operacion=" & nro_operacion
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function


    Public Function documento(ByVal nro_ope As Integer) As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As String, cad As String
        com.Connection = clConex
        cad = "Select nro_doc from salida where operacion='" & nro_ope & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), String)
        clConex.Close()
        Return result
    End Function

    Public Function usuario(ByVal nro_ope As Integer) As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As String, cad As String
        com.Connection = clConex
        cad = "Select cuenta from salida where operacion='" & nro_ope & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), String)
        clConex.Close()
        Return result
    End Function

    Public Function fechadoc(ByVal nro_ope As Integer) As Date
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Date, cad As String
        com.Connection = clConex
        cad = "Select fec_doc from salida where operacion='" & nro_ope & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Date)
        clConex.Close()
        Return result
    End Function

    Public Function estaAnulada(ByVal nro_ope As Integer) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = clConex
        cad = "Select anul from salida where operacion='" & nro_ope & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Integer)
        clConex.Close()
        Return result
    End Function

    Public Function maxNroSalida(ByVal cod_doc As String, ByVal ser_doc As String) As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As String
        com.Connection = clConex
        com.CommandText = "select max(nro_doc) from salida where caux='" & cod_doc & "'" & " and ser_doc='" & ser_doc & "'"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), "000000000", obj), String)
        Dim resp As String = Right("00000000" & Trim(Str(Microsoft.VisualBasic.Val(result) + 1)), 8)
        clConex.Close()
        Return resp
    End Function
End Class
