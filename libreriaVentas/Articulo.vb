Imports MySql.Data.MySqlClient
Public Class Articulo
    Dim nombPC As String = My.Computer.Name
    Public Function existeReceta(ByVal cod_art As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = clConex
        cad = "select count(r.cod_rec) from receta r inner join articulo a on a.cod_Art=r.cod_art inner join " _
             & "tipo_articulo t on a.cod_tart=t.cod_tart where (esProductoterminado) and cod_rec='" & cod_art & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Integer)
        clConex.Close()
        If result = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function existeCombo(ByVal cod_art As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = clConex
        cad = "select count(c.cod_combo) from art_combo c inner join articulo a on a.cod_Art=c.cod_art inner join " _
             & "tipo_articulo t on a.cod_tart=t.cod_tart where (c.activo) and (esCombo) and c.cod_art='" & cod_art & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Integer)
        clConex.Close()
        If result = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function existeDescuento(ByVal cod_art As String) As Decimal
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Decimal, cad As String
        com.Connection = clConex
        cad = "select art.precio_des from articulo a inner join art_precio art on a.cod_art=art.cod_art  " _
                & "inner join ptovta pv on pv.tip_precio=art.cod_precio   " _
             & "where (a.activo) and (CURRENT_TIME BETWEEN art.ini_desc and art.fin_desc) and a.cod_art='" & cod_art & "' and pv.terminal='" & nombPC & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Decimal)
        clConex.Close()
        If result = 0 Then
            Return -1
        Else
            Return result
        End If
    End Function


    Public Function existeCombodet(ByVal cod_art As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = clConex
        cad = "select count(c.cod_combo) from art_combodet c inner join articulo a on a.cod_Art=c.cod_art inner join " _
             & "tipo_articulo t on a.cod_tart=t.cod_tart where (esProductoterminado) and c.cod_combo='" & cod_art & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), False, obj), Integer)
        clConex.Close()
        If result = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function buscarcolorA(ByVal codigo As String) As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String = ""
        com.Connection = clConex

        cad = "select s.b_color from subgrupo s where s.cod_sgrupo='" & codigo & "'"

        com.CommandText = cad
        result = com.ExecuteScalar
        clConex.Close()
        Return result
    End Function

    Public Function buscarcolor(ByVal codigo As String, ByVal proceso As String) As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String = ""
        com.Connection = clConex

        cad = "select " & IIf(proceso = "G", "s.b_color", "if(length(a.b_color)>0,a.b_color,s.b_color)") & " from subgrupo s inner join articulo a on a.cod_sgrupo=s.cod_sgrupo " _
            & "where " & IIf(proceso = "G", "s.cod_sgrupo='", "a.cod_art='") & codigo & "'"

        com.CommandText = cad
        result = com.ExecuteScalar
        clConex.Close()
        Return result
    End Function

    Public Function comboMax(ByVal codcombo As String) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, sql As String
        com.Connection = clConex
        sql = "Select max from art_combo where cod_combo='" & codcombo & "'"
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function

    Public Function comboMin(ByVal codcombo As String) As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, sql As String
        com.Connection = clConex
        sql = "Select min from art_combo where cod_combo='" & codcombo & "'"
        com.CommandText = sql
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function

    Public Function actualizaEstadoCombo(ByVal cod_combo As String, ByVal estado As Boolean, ByVal flag As Boolean) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand
        com.Connection = clConex
        Dim result As Integer
        Dim cad, cad1, cad2 As String
        cad1 = "update art_combo set activo=" & estado
        cad2 = " where " & IIf(flag, " cod_combo='" & cod_combo & "'", " cod_art='" & cod_combo & "'")
        cad = cad1 + cad2
        com.CommandText = cad
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function
End Class
