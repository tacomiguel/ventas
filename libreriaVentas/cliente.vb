Imports MySql.Data.MySqlClient
Public Class cliente
    Public Function insertar(ByVal cod_clie As String, _
                            ByVal nom_clie As String, _
                            ByVal raz_soc As String, _
                            ByVal dir_clie As String, _
                            ByVal dir_ent As String, _
                            ByVal nom_cont As String, _
                            ByVal fono_clie As String, _
                            ByVal cod_tipo As String, _
                            ByVal activo As Integer, _
                            ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        Dim com As New MySqlCommand

        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If
        com.Connection = clConex
        Dim sql As String, result As Integer
        sql = "Insert Into cliente(cod_clie,nom_clie,raz_soc,dir_clie,dir_ent,nom_cont,fono_clie,cod_tipo,activo)" & _
            "values('" & _
            cod_clie & "','" & nom_clie & "','" & raz_soc & "','" & dir_clie & "','" & dir_ent & "','" & nom_cont & "','" & fono_clie & "','" & cod_tipo & "'," & activo & ")"
        com.CommandText = sql
        result = com.ExecuteNonQuery
        clConex.Close()
        Return result
    End Function
    Public Function existe(ByVal cod_clie As String, ByVal tipconex As Integer) As Boolean
        Dim clConex As MySqlConnection
        Dim com As New MySqlCommand, result As Integer, cad As String

        If tipconex = 0 Then
            clConex = Conexion.obtenerConexion()
        Else
            clConex = Conexion.obtenerConexionB()
        End If

        com.Connection = clConex
        cad = "Select count(cod_clie) from cliente where cod_clie='" & cod_clie & "'"
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
    Public Function maxCodigoClie() As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Long
        com.Connection = clConex
        com.CommandText = "select max(cod_clie) from cliente  WHERE cod_clie REGEXP ('^[0-9]+$')"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), "00000000", obj), String)
        Dim resp As String = Right("00000000" & Trim(Str(Microsoft.VisualBasic.Val(result) + 1)), 8)
        clConex.Close()
        Return resp
    End Function

End Class
