Imports MySql.Data.MySqlClient
Imports System.Configuration


Public Class Conexion
    Public Shared Function obtenerConexion() As MySqlConnection
        Try
            ' Dim conex As String = "User Id=custom;password=custom;server=" & Servidor & ";database=" & BaseDatos & ";Convert Zero Datetime=True;persist security info=True;use procedure bodies=False;Connection Timeout=300 ; pooling=true; Max Pool Size=300"
            Dim conex As String = "User Id=custom;password=P4nt3r4--;server=" & ConfigurationManager.AppSettings("Servidor").ToString & ";database=" & ConfigurationManager.AppSettings("BaseDatos").ToString &
                ";Convert Zero Datetime=True;persist security info=True;use procedure bodies=False;Connection Timeout=300 ; pooling=true; Max Pool Size=300"

            'Convert Zero Datetime=True
            '; Allow Zero Datetime=True
            Dim ConexBD As New MySqlConnection(conex)
            If ConexBD.State = ConnectionState.Open Then
                ConexBD.Close()
            End If
            ConexBD.Open()
            Return ConexBD
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerConexionB() As MySqlConnection
        'Const BaseDatos As String = "modelo"
        'Const BaseDatos As String = "demo"
        'Const BaseDatos As String = "fiestra_bk"
        'Const BaseDatos As String = "ficus"
        'Const BaseDatos As String = "spaisac"
        Const BaseDatos As String = "buenrecado"
        'Const BaseDatos As String = "spaisac"
        'Const BaseDatos As String = "rosanautica"
        Const Servidor As String = "12.100.10.12"
        'Const Servidor As String = "localhost"
        'Const Servidor As String = "192.168.1.3"
        Try
            Dim conex As String = "User Id=custom;password=h1808;server=" & Servidor & ";database=" & BaseDatos & ";Convert Zero Datetime=True;persist security info=True;use procedure bodies=False;Connection Timeout=300 ; pooling=true; Max Pool Size=300"
            'Convert Zero Datetime=True
            '; Allow Zero Datetime=True
            Dim ConexBD As New MySqlConnection(conex)
            If ConexBD.State = ConnectionState.Open Then
                ConexBD.Close()
            End If
            ConexBD.Open()
            Return ConexBD
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

End Class