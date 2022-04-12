Imports System.Data
Imports MySql.Data.MySqlClient
Public Class usuario
    Public Shared Function dsUsuario() As DataSet
        Dim ds As New DataSet
        Dim dtUsuario As New DataTable("usuario")
        dtUsuario.Columns.Add(New DataColumn("user", GetType(String)))
        dtUsuario.Columns.Add(New DataColumn("cuenta", GetType(String)))
        dtUsuario.Columns.Add(New DataColumn("clave", GetType(String)))
        dtUsuario.Columns.Add(New DataColumn("activo", GetType(Boolean)))
        dtUsuario.Constraints.Add("pkCuenta", dtUsuario.Columns("cuenta"), True)
        ds.Tables.Add(dtUsuario)
        Return ds
    End Function
    Public Shared Function ingresoSistema(ByVal usuario As String, ByVal clave As String) As Boolean
        Dim resp As Integer
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select count(*) from usuario u inner join vendedor v on u.cuenta=v.cod_vend  where v.activo " _
                 & " and ( u.cuenta='" + usuario + "'" + " OR u.clave='" + clave + "' )"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                If resp > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                clConex.Close()
                Return False
            End If
        Catch ex As Exception

        Finally

        End Try
        Return True
    End Function
    Public Shared Function recuperaNivelUsuario(ByVal cuenta As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select nivel from usuario where cuenta='" & cuenta & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
            Else
                clConex.Close()
                resp = ""
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Shared Function recuperaAlmacenUsuario(ByVal cuenta As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select cod_alma from usuario where cuenta='" & cuenta & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
            Else
                clConex.Close()
                resp = ""
            End If

        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Shared Function recuperaDatosUsuario(ByVal cuenta As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select user from usuario where cuenta='" & cuenta & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                Return resp
            Else
                clConex.Close()
                Return resp
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Shared Function recuperaPerfilUsuario(ByVal cuenta As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select perfil from vendedor where cod_vend='" & cuenta & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                Return resp
            Else
                clConex.Close()
                Return resp
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Shared Function recuperalicencia(ByVal codigo As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select licencia from empresa where cod_emp='" & codigo & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                Return resp
            Else
                clConex.Close()
                Return resp
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function


    Public Shared Function recuperaCuentaUsuario(ByVal clave As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select cuenta from usuario where clave='" & clave & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                Return resp
            Else
                clConex.Close()
                Return resp
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Shared Function recuperaNombreUsuario(ByVal clave As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select user from usuario where clave='" & clave & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                Return resp
            Else
                clConex.Close()
                Return resp
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Shared Function recuperaUsuario(ByVal clave As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select user from usuario where clave='" & clave & "'"
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
                Return resp
            Else
                clConex.Close()
                Return resp
            End If
        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Function recuperaUsuarios() As DataSet
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim da As New MySqlDataAdapter
        Dim dsUsuario As New DataSet
        Dim cad, cad1, cad2 As String
        cad1 = "select user,cuenta,clave,activo "
        cad2 = " from usuario "
        cad = cad1 + cad2
        Dim comUsuario As New MySqlCommand(cad)
        comUsuario.Connection = clConex
        da.SelectCommand = comUsuario
        da.Fill(dsUsuario, "usuario")
        clConex.Close()
        Return dsUsuario
    End Function
    Public Function permisos(ByVal cuenta As String) As DataSet
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim da As New MySqlDataAdapter
        Dim dsPermisos As New DataSet
        Dim cad, cad1, cad2 As String
        cad1 = "select cod_smenu,activo"
        cad2 = " from usuario_smenu where cuenta='" & cuenta & "'"
        cad = cad1 + cad2
        Dim com As New MySqlCommand(cad)
        com.Connection = clConex
        da.SelectCommand = com
        da.Fill(dsPermisos, "permisos")
        Return dsPermisos
    End Function
    Public Function existeMenu(ByVal cuenta As String, ByVal menu As String) As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = clConex
        cad = "Select count(cuenta) from usuario_smenu where cuenta='" & cuenta & "' and cod_smenu='" & menu & "'"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        If result > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class