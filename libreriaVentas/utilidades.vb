Imports MySql.Data.MySqlClient
Public Class utilidades
    Function devuelvediasFormaPago(ByVal cod_fpago As String) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, ndias As Integer
        com.Connection = clConex
        Dim cad, cad1, cad2 As String
        cad1 = " select dias from forma_pago"
        cad2 = " where cod_fpago ='" & cod_fpago & "'"
        cad = cad1 + cad2
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        ndias = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return ndias
    End Function

    Public Function ActualizarUbicacion_Mesas(ByVal cod_mesa As String, ByVal posx As Integer, ByVal posy As Integer) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Update mesas set pos_x= " & posx & ", pos_y=" & posy & " where cod_mesa='" & cod_mesa & "'"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return True
    End Function
    Public Function ActualizarEstado_Mesas(ByVal estado As Boolean, ByVal cod_mesa As String, ByVal color As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Update mesas set estado=" & estado & ", b_color= '" & color & "' where cod_mesa='" & cod_mesa & "'"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return True
    End Function
    Public Function ActualizarEstado_Pedido(ByVal operacion As Integer) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Update factura_det set nAux=0  where nAux=1 and operacion=" & operacion
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return True
    End Function
    Public Function ActualizarLicencia(ByVal codigo As String, ByVal licencia As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Update empresa set licencia='" & licencia & "'  where cod_emp='" & codigo & "'"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return True
    End Function

    Public Function existeMesa(ByVal cod_mesa As String, ByVal estado As Boolean) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, sql As String
        com.Connection = clConex
        sql = "Select cod_mesa from mesas where cod_mesa='" & cod_mesa & IIf(estado And cod_mesa <> "999", "' and color='&H228B22'", "'")
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function
    Public Function existePedido(ByVal operacion As Integer) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad, cad1, cad2 As String
        com.Connection = clConex
        cad1 = "Select s.operacion from factura s inner join factura_det sd on s.operacion=sd.operacion "
        cad2 = " where sd.nAux=1 and s.operacion= " & operacion
        cad = cad1 + cad2
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function

    Public Function recuperaEmpresa() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        'cad = " select nom_comercial,c.nom_emp,c.cod_emp,dir_emp,left(dir_sucursal,INSTR(dir_sucursal,'-')-1) as dir_sucursal,RIGHT(dir_sucursal,LENGTH(dir_sucursal)-INSTR(dir_sucursal,'-')) as dis_sucursal ,pais_emp,obs,obs1 " _
        '    & " from configuracion c inner join empresa e on c.cod_emp=e.cod_emp where c.activo"
        cad = " select nom_comercial,c.nom_emp,	c.cod_emp,left(dir_emp,INSTR(dir_emp,'/')-1) as dir_emp,RIGHT(dir_emp,LENGTH(dir_emp)-INSTR(dir_emp,'/')) as dis_emp, left(dir_sucursal,INSTR(dir_sucursal,'-')-1) as dir_sucursal,RIGHT(dir_sucursal,LENGTH(dir_sucursal)-INSTR(dir_sucursal,'-')) as dis_sucursal ,pais_emp,obs,obs1" _
            & " from configuracion c inner join empresa e on c.cod_emp=e.cod_emp where c.activo"
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "Empresa")
        Return ds
    End Function
    Public Function recuperacabeceraticket(ByVal operacion As Integer) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad, cad0, cad1, cad2, cad3, cad4 As String
        cad0 = " select concat('--',d.nom_doc,' DE VENTA ELECTRONICA--'),concat(ser_doc,'-',nro_doc) as documento,nro_maquina,cast(fec_doc as date) as fecha,DATE_FORMAT(fecha, '%H:%i:%s') as hora, monto-(monto_igv) as subtotal,monto_igv,monto,v.nom_vend,"
        cad1 = " f.nom_fpago,imp_pagado,imp_vuelto,c.cod_clie,if( anul=1,obs,if(c.cod_clie='',s.contacto,c.raz_soc)) as raz_soc,ope_ped,fono_clie,dir_clie,nom_cont,dsc_recurso"
        cad2 = " from factura s inner join documento_s d on d.cod_docsunat=s.cod_doc inner join vendedor v on s.cod_vend=v.cod_vend"
        cad3 = " inner join cliente c on c.cod_clie=s.cod_clie inner join tipo_recurso tr on tr.cod_recurso=s.cod_atencion"
        cad4 = " left join factura_detpago sp on sp.operacion=s.operacion left join forma_pago f on f.cod_fpago=sp.cod_fpago where s.operacion= " & operacion
        cad = cad0 + cad1 + cad2 + cad3 + cad4
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "cabecera")
        Return ds
    End Function
    Public Function recuperadetalle(ByVal operacion As Integer) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad, cad0, cad1, cad2 As String
        cad0 = " select cant,If(LENGTH(sd.descripcion) > 0,sd.descripcion ,if(nom_Art2='',nom_Art,nom_art2) )  as nom_art ,(cant*precio) as importe,precio " _
             & " from factura s inner join factura_det sd on s.operacion=sd.operacion"
        cad1 = " inner join articulo a on a.cod_art=sd.cod_Art inner join subgrupo g on a.cod_sgrupo=g.cod_sgrupo"
        cad2 = " where cant<>0 and repfiscal and s.operacion= " & operacion
        cad = cad0 + cad1 + cad2
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "Detalle")
        Return ds
    End Function

    Public Function recuperadetalleComanda(ByVal operacion As Integer, ByVal nom_impresora As String) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad, cad0, cad1, cad2, cad3 As String
        cad0 = " select  cant,If(LENGTH(sd.obs) > 0,CONCAT(round(cant,2),' ',sd.obs),nom_Art ) as nom_art,precio,(cant*precio) as importe,g.impresora, " _
             & " ope_ped,date(fec_doc) as fecha, DATE_FORMAT(fecha, '%H:%i:%s') as hora,s.obs,Caux4,dsc_recurso,fono_clie,dir_clie,if(nom_cont='',caux4,nom_cont) as nom_cont," _
             & " IF(ISNULL(dsc_mesa),'',dsc_mesa ) as dsc_mesa "
        cad1 = " from factura s inner join factura_det sd on s.operacion=sd.operacion"
        cad2 = " inner join articulo a on a.cod_art=sd.cod_Art inner join subgrupo g on a.cod_sgrupo=g.cod_sgrupo" _
             & " inner join tipo_recurso tr on tr.cod_recurso= s.Caux3 inner join cliente c on c.cod_clie=s.cod_clie" _
             & " left join mesas on mesas.cod_mesa=s.Caux2 "
        cad3 = " where  s.operacion= " & operacion & " and sd.nAux=1 and g.impresora= '" & nom_impresora & "'"
        cad = cad0 + cad1 + cad2 + cad3
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "Detalle")
        Return ds
    End Function

    Public Function recuperaventas(ByVal fechaI As Date, ByVal xformapago As Boolean) As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        Dim mfecha As String = fechaI.ToString("yyyy-MM-dd")
        clConex = Conexion.obtenerConexion
        Dim cad, cad0, cad1, cad2 As String
        cad0 = " select concat(ser_doc,'-',nro_doc) as doc,nom_fpago," & IIf(xformapago, "sum(monto_igv),sum(monto)", "monto_igv,monto")
        cad1 = " from factura s inner join factura_detpago sp on s.operacion=sp.operacion inner join forma_pago f on f.cod_fpago=sp.cod_fpago"
        cad2 = " where s.fec_doc= '" & mfecha & "' " & IIf(xformapago, " group by 2 order by 2,1", " order by 2,1")
        cad = cad0 + cad1 + cad2
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "Detalle")
        Return ds
    End Function

End Class
