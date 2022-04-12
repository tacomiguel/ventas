Imports MySql.Data.MySqlClient
Public Class Documento
    Public Function recuperarCantidadImpresion(ByVal cod_doc As String) As Integer
        Dim resp As Integer = 0
        Dim cmd As New MySqlCommand
        Dim nombPC As String = My.Computer.Name
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select cantImp from ptovta_tipodocu_series where CDTIPODOCU='" & cod_doc & "'  and terminal='" & nombPC & "' "
            If Not clConex Is Nothing Then
                resp = cmd.ExecuteScalar()
                clConex.Close()
            Else
                clConex.Close()
                resp = 0
            End If

        Catch ex As Exception
        Finally
        End Try
        Return resp
    End Function
    Public Function recuperaSerieDocumento(ByVal cod_doc As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Dim nombPC As String = My.Computer.Name
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select serie from ptovta_tipodocu_series where CDTIPODOCU='" & cod_doc & "'  and terminal='" & nombPC & "' "
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
    Public Function recuperaSerieDocumentoParaNotaCred(ByVal cod_doc As String, ByVal terminal As String, ByVal cod_doc_ref As String) As String
        Dim resp As String = ""
        Dim cmd As New MySqlCommand
        Dim nombPC As String = My.Computer.Name
        Try
            Dim clConex As MySqlConnection = Conexion.obtenerConexion()
            cmd.Connection = clConex
            cmd.CommandText = "select serie from ptovta_tipodocu_series where CDTIPODOCU='" & cod_doc & "'  and nroptovta='" & terminal & "'  and id='" & cod_doc_ref & "' "
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

    Public Function NombreFormato(ByVal tipodoc As String) As String
        Dim cmd As New MySqlCommand, result As String, cad As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion()
        cmd.Connection = clConex
        cad = "Select formato from documento_s where cod_doc='" & tipodoc & "'"
        cmd.CommandText = cad
        Dim obj As Object = cmd.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), "", obj), String)
        Return result
    End Function

    Public Function NombreImpresora(ByVal tipodoc As String) As String
        Dim cmd As New MySqlCommand, result As String, cad As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion()
        cmd.Connection = clConex
        cad = "Select Impresora from documento_s where cod_doc='" & tipodoc & "'"
        cmd.CommandText = cad
        Dim obj As Object = cmd.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), "", obj), String)
        Return result
    End Function

End Class
