Imports MySql.Data.MySqlClient
Public Class maestros

    Public Function documentos() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        cad = ("select cod_docsunat,nom_doc from documento_s where activo=1 and (esVenta) order by nom_doc")
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "documento_s")
        Return ds
    End Function
    Public Function pagos() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        cad = ("select cod_fpago as codigo,nom_fpago as nombre, isnull(foto) as vnul,foto,b_color, f_color,f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                        & " from forma_pago where activo=1 and b_ancho>0")
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "pago")
        Return ds
    End Function
    Public Function clientes() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        cad = "Select cod_clie,nom_clie,raz_soc,dir_clie,fono_clie,dir_ent,nom_cont,nom_rep,cod_tipo,cod_tipo " _
         & "from cliente where activo=1 order by raz_soc"
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "cliente")
        Return ds
    End Function
    Public Function Grupos() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        cad = ("Select cod_grupo As codigo, nom_grupo As nombre, isnull(foto) As vnul, " _
               & " foto, b_color, f_color, f_tamano, 0.000 As pre_venta, 0 As afecto_igv, b_ancho, b_alto " _
               & " from grupo where activo And esVenta")
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "grupo")
        Return ds
    End Function
    Public Function subGrupos() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        cad = ("Select cod_sgrupo As codigo, nom_sgrupo As nombre, isnull(foto) As vnul,cod_grupo, " _
               & " foto, b_color, f_color, f_tamano, 0.000 As pre_venta, 0 As afecto_igv, b_ancho, b_alto " _
               & " from subgrupo where activo And esVenta")
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "subgrupo")
        Return ds
    End Function
    Public Function Articulos() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String

        cad = ("Select a.cod_art As codigo, a.nom_art As nombre, isnull(a.foto) As vnul, a.foto, If(length(a.b_color) > 0 ,a.b_color, s.b_color) As b_color, " _
                          & " a.cod_grupov, s.f_color, f_tamano, ap.precio As pre_venta, afecto_igv, a.b_ancho, a.b_alto" _
                          & " from articulo a inner join subgrupo s on a.cod_grupov=s.cod_sgrupo " _
                          & " inner join tipo_articulo t on a.cod_tart=t.cod_tart " _
                          & " inner join art_precio ap on a.cod_art=ap.cod_art " _
                          & " inner join ptovta pv on pv.tip_precio=ap.cod_precio " _
                          & " where a.activo  And pv.terminal='C01LUCET-PC'")
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "articulo")
        Return ds
    End Function
    Public Function Atenciones() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String

        cad = "select cod_recurso as codigo,dsc_recurso as nombre,isnull(foto) as vnul,foto,b_color,'&HEFFBF2' as f_color,8 as f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                           & " from tipo_recurso where cod_tabla='1000'"
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "atencion")
        Return ds
    End Function

    Public Function vendedores() As DataSet
        Dim clConex As New MySqlConnection
        Dim da As New MySqlDataAdapter
        Dim ds As New DataSet
        clConex = Conexion.obtenerConexion
        Dim cad As String
        cad = "select cod_vend as codigo,nom_vend as nombre,isnull(foto) as vnul,foto, b_color,'&HEFFBF2' as f_color,8 as f_tamano,0.000 as pre_venta,0 as afecto_igv,b_ancho,b_alto" _
                           & " from vendedor v inner join usuario u on v.cod_vend=u.cuenta where u.activo"
        Dim com As New MySqlCommand(cad)
        com.CommandText = cad
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(ds, "vendedor")
        Return ds
    End Function


End Class
