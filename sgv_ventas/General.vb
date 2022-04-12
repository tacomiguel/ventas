Imports System.Data
Imports MySql.Data.MySqlClient
Imports libreriaVentas
Imports libreriaseguridad
Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Configuration


Module general
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Public dbConex As MySqlConnection = Conexion.obtenerConexion()
    Public BaseDatos, Servidor As String
    Public pCuentaUser, pNombreUser, pNivelUser, pAlmaUser, pDatosUser As String
    Public pIGV, pTC As Decimal, pDecimales As Integer, pEmpresa As String, pNempresa As String, pDirEmpresa As String
    Public pDiasModificarIngreso, pDiasModificarSalida, pDiasModificarPedido, pDiasModificarInventario As Integer
    Public pDiasModificarTrans, pDiasModificarBaja As Integer
    Public pPreciosIncIGV, pDespachoXprecioVenta, pDespachoXtipoCliente, pModoPedidos, pvistaprevia As Boolean
    Public pPorcentPreCostoMax As Decimal = 0.1, pPorcentPreCostoMin As Decimal = 0.1, pDespachoStock0 As Decimal
    Public pMoneda, pMonedaAbr, pLogo As String, pCatalogoXalmacen As Boolean, pImpuestoXarticulo As Boolean
    Public pfechaservidor As Boolean = devuelvetipofecha()
    Public pFechaHoraSystem As Date = fechaSystem(pfechaservidor)
    Public pFechaSystem = pFechaHoraSystem.ToString("dd/MM/yyyy H:mm:ss")
    Public pFechaActivaMin As Date = fechaActivaMin()
    Public pFechaActivaMax As Date = fechaActivaMax(ConfigurationManager.AppSettings("operacion").ToString)
    Public pTituloRep1, pTituloRep2, plicencia, pPerfil As String
    Public pruta = System.AppDomain.CurrentDomain.BaseDirectory()

    'Public Function devuelvetipofecha() As Integer
    '    Dim res As Integer
    '    Dim com As New MySqlCommand
    '    com.CommandType = CommandType.Text
    '    com.CommandText = "select fechaservidor from configuracion  "
    '    com.Connection = dbConex
    '    Dim obj As Object = com.ExecuteScalar
    '    res = CType(IIf(IsDBNull(obj), 0, obj), Decimal)
    'End Function

    Public Function devuelvetipofecha() As Boolean
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = clConex
        cad = "select fechaservidor from configuracion"
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

    Function strPrecioCostoPromedio()
        'Dim cPrecio As String = "round(avg(if(pre_inc_igv,if(tm='D',(precio*tc)/(1+igv),precio/(1+igv)),if(tm='D',precio*tc,precio))),3)"
        Dim cPrecio As String = "round(sum(if(pre_inc_igv,if(tm='D',(precio*tc)/(1+igv),precio/(1+igv)),if(tm='D',precio*tc,precio))*cant)/sum(cant),3)"
        Return cPrecio
    End Function
    Friend Sub BuscarArchivoConfiguracion()
        Dim Encontro As Boolean = False
        Dim DireccionRelativa, DireccionCompleta As String
        ' obtiene el path relativo de la aplicación. 
        DireccionRelativa = System.IO.Directory.GetCurrentDirectory()
        DireccionCompleta = DireccionRelativa + "\configuracion.ini"

        Dim objReader As New IO.StreamReader(DireccionCompleta)
        Dim sLine As String = ""
        Dim arrText As New ArrayList()

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        BaseDatos = arrText(1)
        Servidor = arrText(2)

    End Sub
    Function str_PrecioCompraSIs()
        Dim cPrecio As String = " if(articulo.afecto_igv," _
                        & "round(if(pre_inc_igv,if(tm='D',(precio*tc)/(1+igv),precio/(1+igv))," _
                        & "if(tm='D',precio*tc,precio)),3),round(if(tm='D',precio*tc,precio),3))"
        Return cPrecio
    End Function
    Function str_hPrecioCompraSIs()
        Dim cPrecio As String = " if(hd.afecto_igv,round(if(pre_inc_igv," _
                                & "if(tm='D',(precio*tc)/(1+igv),precio/(1+igv))," _
                                & "if(tm='D',precio*tc,precio)),3),round(if(tm='D',precio*tc,precio),3))"
        Return cPrecio
    End Function
    Function str_PrecioCompraCIs()
        Dim cPrecio As String = " if(articulo.afecto_igv," _
                        & "round(if(pre_inc_igv,if(tm='D',precio*tc,precio)," _
                        & "if(tm='D',(precio*tc)*(1+igv),precio*(1+igv))),3),round(if(tm='D',precio*tc,precio),3))"
        Return cPrecio
    End Function
    Function str_hPrecioCompraCIs()
        Dim cPrecio As String = " if(hd.afecto_igv," _
                        & "round(if(pre_inc_igv,if(tm='D',precio*tc,precio)," _
                        & "if(tm='D',(precio*tc)*(1+igv),precio*(1+igv))),3),round(if(tm='D',precio*tc,precio),3))"
        Return cPrecio
    End Function
    Function str_PrecioCompraSI()
        Dim cPrecio As String = " if(articulo.afecto_igv,round(if(pre_inc_igv,precio/(1+igv),precio),3),round(if(tm='D',precio*tc,precio),3))"
        Return cPrecio
    End Function
    Function str_PrecioCompraSIm()
        Dim cPrecio As String = " if(articulo.afecto_igv,round(if(pre_inc_igv,precio/(1+igv),precio),3),round(precio,3))*if(h.tm='D',h.tc,1)"
        Return cPrecio
    End Function
    Function str_hPrecioCompraSI()
        Dim cPrecio As String = " if(hd.afecto_igv,round(if(pre_inc_igv,precio/(1+igv),precio),3),round(precio,3))"
        Return cPrecio
    End Function
    Function str_hPrecioCompraSIm()
        Dim cPrecio As String = " if(hd.afecto_igv,round(if(pre_inc_igv,precio/(1+igv),precio),3),round(precio,3))*if(h.tm='D',h.tc,1)"
        Return cPrecio
    End Function
    Function str_PrecioCompraCI()
        Dim cPrecio As String = " if(articulo.afecto_igv,round(if(pre_inc_igv,precio,precio*(1+igv)),3),round(precio,3))"
        Return cPrecio
    End Function
    Function str_Tcambio()
        Dim cTcambio As String = " if(h.tm='D',h.tc,1)"
        Return cTcambio
    End Function
    Function str_hPrecioCompraCI()
        Dim cPrecio As String = " if(hd.afecto_igv,round(if(pre_inc_igv,precio,precio*(1+igv)),3),round(precio,3))"
        Return cPrecio
    End Function
    Function str_PrecioCostoCI()
        Dim cPrecio As String = " if(articulo.afecto_igv,round(pre_costo*(1+igv),3),pre_costo)"
        Return cPrecio
    End Function
    Function str_textoNoImpresion()
        Dim cTexto As String = "Información para Impresión... NO Disponible"
        Return cTexto
    End Function
    Function convierte_enTitulo(ByVal nombreCampo As String)
        Dim cTitulo = "concat(upper(Left(" & nombreCampo & ", 1)), lower(substring(" & nombreCampo & ", 2)))"
        Return cTitulo
    End Function
    Function convierteTexto_enFecha(ByVal dia As String, ByVal mes As String, ByVal anno As String) As Date
        Dim mfecha As Date = dia & "/" & mes & "/" & anno
        Return mfecha
    End Function
    'Public Function fechaSystem()
    '    Dim res As Date
    '    Dim com As New MySqlCommand
    '    com.CommandType = CommandType.StoredProcedure
    '    com.CommandText = "spFechaSystem"
    '    com.Connection = dbConex
    '    res = com.ExecuteScalar
    '    Return res
    'End Function
    Function FormularioActivo(ByVal NmbFormulario As String) As Boolean

        For Each mform As Form In Application.OpenForms
            If mform.Name = NmbFormulario Then
                mform.BringToFront()
                mform.WindowState = FormWindowState.Normal
                Return True
            End If
        Next

        Return Nothing

    End Function
    Public Function fechaSystem(ByVal tipofecha As Boolean)
        Dim res As Date
        Dim com As New MySqlCommand
        If tipofecha = True Then
            com.CommandType = CommandType.StoredProcedure
            com.CommandText = "spFechaSystem"
        Else
            com.CommandType = CommandType.Text
            com.CommandText = "select fecha from tipo_cambio where activo=1"
        End If
        
        com.Connection = dbConex
        res = com.ExecuteScalar
        Return res
    End Function
    Public Function fechaActivaMax(ByVal operacion As Integer) As Date
        Dim res As Date
        Dim com As New MySqlCommand
        com.CommandType = CommandType.Text
        com.CommandText = "select fechaMax from actual where operacion=" & operacion
        com.Connection = dbConex
        res = com.ExecuteScalar
        Return res
    End Function
    Public Function fechaActivaMin() As Date
        Dim res As Date
        Dim com As New MySqlCommand
        com.CommandType = CommandType.Text
        com.CommandText = "select fechaMin from actual where activo=1"
        com.Connection = dbConex
        res = com.ExecuteScalar
        Return res
    End Function
    Public Function fechaInventario(ByVal periodo) As Date
        Dim res As Date
        Dim com As New MySqlCommand
        com.CommandType = CommandType.Text
        com.CommandText = "select fechaMax from actual where periodo='" & periodo & "'"
        com.Connection = dbConex
        res = com.ExecuteScalar
        Return res
    End Function
    Public Function ActOperacion() As Integer
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim com As New MySqlCommand, result As Integer
        com.Connection = clConex
        com.CommandText = "select (operacion) from actual where activo=1"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        clConex.Close()
        Return result
    End Function

    Public Function periodoActivo() As String
        Dim clConex As MySqlConnection = Conexion.obtenerConexion
        Dim res As String
        Dim com As New MySqlCommand
        com.CommandType = CommandType.Text
        com.CommandText = "select periodo from actual where activo=1"
        com.Connection = clConex
        res = com.ExecuteScalar
        clConex.Close()
        Return res
    End Function
    Public Function periodoActivo_mesAnnoLetras() As String
        Dim mmes, manno, mfecha As String
        mmes = general.devuelveMes(Month(pFechaActivaMax))
        manno = Trim(Str(Year(pFechaActivaMax)))
        mfecha = mmes + "-" + manno
        Return mfecha
    End Function
    Public Function periodo_mesAnnoLetras(ByVal nroPeriodo As String) As String
        Dim mmes, manno, mfecha As String
        mmes = general.devuelveMes(Val(Microsoft.VisualBasic.Right(Trim(nroPeriodo), 2)))
        manno = Microsoft.VisualBasic.Left(Trim(nroPeriodo), 4)
        mfecha = mmes + "-" + manno
        Return mfecha
    End Function
    Public Function periodoCerrado() As String
        Dim com As New MySqlCommand, result As Integer, cad As String
        com.Connection = dbConex
        cad = "Select periodo_cerrado from configuracion"
        com.CommandText = cad
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), "", obj), String)
        Return result
    End Function
    Public Function periodoActivo_existeDatos() As Boolean
        Dim com As New MySqlCommand, result As Integer
        com.Connection = dbConex
        com.CommandText = "select max(operacion) from ingreso"
        Dim obj As Object = com.ExecuteScalar
        result = CType(IIf(IsDBNull(obj), 0, obj), Integer)
        If result > 0 Then
            Return True
        Else
            Dim com2 As New MySqlCommand, result2 As Integer
            com2.Connection = dbConex
            com2.CommandText = "select max(operacion) from salida"
            Dim obj2 As Object = com2.ExecuteScalar
            result2 = CType(IIf(IsDBNull(obj2), 0, obj2), Integer)
            If result2 > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function esEntero(ByVal numero As Decimal)
        If (numero Mod 1) = 0 Then
            numero = numero
        Else
            numero = Int(numero) + 1
        End If
        Return numero
    End Function
    Public Function devuelveMes(ByVal nroMes) As String
        Dim cMes As String = ""
        Select Case nroMes
            Case Is = 1
                cMes = "Enero"
            Case Is = 2
                cMes = "Febrero"
            Case Is = 3
                cMes = "Marzo"
            Case Is = 4
                cMes = "Abril"
            Case Is = 5
                cMes = "Mayo"
            Case Is = 6
                cMes = "Junio"
            Case Is = 7
                cMes = "Julio"
            Case Is = 8
                cMes = "Agosto"
            Case Is = 9
                cMes = "Setiembre"
            Case Is = 10
                cMes = "Octubre"
            Case Is = 11
                cMes = "Noviembre"
            Case Is = 12
                cMes = "Diciembre"
        End Select
        Return cMes
    End Function
    Public Function devuelveNomMes(ByVal nroMes As String) As String
        Dim cMes As String = ""
        Select Case nroMes
            Case Is = "m1"
                cMes = "Enero"
            Case Is = "m2"
                cMes = "Febrero"
            Case Is = "m3"
                cMes = "Marzo"
            Case Is = "m4"
                cMes = "Abril"
            Case Is = "m5"
                cMes = "Mayo"
            Case Is = "m6"
                cMes = "Junio"
            Case Is = "m7"
                cMes = "Julio"
            Case Is = "m8"
                cMes = "Agosto"
            Case Is = "m9"
                cMes = "Setiembre"
            Case Is = "m10"
                cMes = "Octubre"
            Case Is = "m11"
                cMes = "Noviembre"
            Case Is = "m12"
                cMes = "Diciembre"
        End Select
        Return cMes
    End Function
    Public Function devuelveFechaImpresionSistema()
        Dim mdia, mmes, manno, mfecha As String
        mdia = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Day(general.fechaSystem(pfechaservidor)))), 2)
        mmes = Microsoft.VisualBasic.Left(general.devuelveMes(Month(general.fechaSystem(pfechaservidor))), 3)
        manno = Trim(Str(Year(general.fechaSystem(pfechaservidor))))
        mfecha = "Al: " + mdia + mmes + "-" + manno
        Return mfecha
    End Function
    Public Function devuelveFechaImpresionEspecificado(ByVal mfechaImpresion As Date)
        Dim mdia, mmes, manno, mfecha As String
        mdia = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Day(mfechaImpresion))), 2)
        mmes = Microsoft.VisualBasic.Left(general.devuelveMes(Month(mfechaImpresion)), 3)
        manno = Trim(Str(Year(mfechaImpresion)))
        mfecha = mdia + mmes + "-" + manno
        Return mfecha
    End Function
    Public Function convierteFechaEspecificadaMes(ByVal fecha As Date)
        Dim mdia, mmes, manno, mfecha As String
        mdia = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Day(fecha))), 2)
        mmes = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Month(fecha))), 2)
        manno = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Year(fecha))), 4)
        mfecha = manno & mmes
        Return mfecha
    End Function

    Public Function convierteFechaEspecificadadia(ByVal fecha As Date)
        Dim mdia, mmes, manno, mfecha As String
        mdia = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Day(fecha))), 2)
        mmes = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Month(fecha))), 2)
        manno = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Year(fecha))), 4)
        mfecha = manno & mmes & mdia
        Return mfecha
    End Function
    Public Function devuelveFechaEspecificada(ByVal fecha As Date)
        Dim mdia, mmes, manno, mfecha As String
        mdia = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Day(fecha))), 2)
        mmes = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Month(fecha))), 2)
        manno = Microsoft.VisualBasic.Right("0" + Trim(Str(Microsoft.VisualBasic.DateAndTime.Year(fecha))), 2)
        mfecha = mdia & mmes & manno
        Return mfecha
    End Function
    Public Function devuelvePeriodoImpresion(ByVal mfechaImpresion As Date)
        Dim mmes, manno, mfecha As String
        mmes = general.devuelveMes(Month(mfechaImpresion))
        manno = Trim(Str(Year(mfechaImpresion)))
        mfecha = "Periodo: " + mmes + "-" + manno
        Return mfecha
    End Function

    Public Function devuelvefechaImpresion(ByVal fecha As Date)
        Dim dia, mes, anno, mfecha As String
        Dim NomMes As String
        dia = Microsoft.VisualBasic.DateAndTime.Day(fecha)
        mes = Month(fecha)
        NomMes = UCase(MonthName(mes, True))
        anno = Year(fecha)
        mfecha = "" & dia & " " & NomMes & " del " & CType(anno, String)
        Return mfecha
    End Function
    Function calculaSubTotaldelTotal_afectoIMP(ByVal nTotal As Decimal) As Decimal
        'verificar si se sigue utilizando
        Dim subTotal As Decimal
        subTotal = nTotal / (1 + pIGV)
        Return subTotal
    End Function
    Function calculaIMPdelTotalAfecto_precioInc(ByVal nTotal As Decimal) As Decimal
        Dim nIMP As Decimal
        nIMP = nTotal - (nTotal / (1 + pIGV))
        Return nIMP
    End Function
    Function calculaIMPdelTotalAfecto_precioNOInc(ByVal nTotal As Decimal) As Decimal
        Dim nIMP As Decimal
        nIMP = nTotal * pIGV
        Return nIMP
    End Function
    Sub ingresaTexto(ByVal mTexto As TextBox)
        If mTexto.ReadOnly = False Then
            'mTexto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(136, Byte), Integer))
            'mTexto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
    Sub saleTexto(ByVal mTexto As TextBox)
        If mTexto.ReadOnly = False Then
            'mTexto.BackColor = System.Drawing.SystemColors.Window
            'mTexto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
    Sub ingresaTextoProceso(ByVal mtexto As TextBox)
        If mtexto.ReadOnly = False Then
            mtexto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(136, Byte), Integer))
            mtexto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
    Sub saleTextoProceso(ByVal mtexto As TextBox)
        If mtexto.ReadOnly = False Then
            mtexto.BackColor = System.Drawing.SystemColors.Window
            mtexto.ForeColor = System.Drawing.Color.Black
            mtexto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
    Sub ingresaComboProceso(ByVal mCombo As ComboBox)
        mCombo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        mCombo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Sub saleComboProceso(ByVal mCombo As ComboBox)
        mCombo.BackColor = System.Drawing.SystemColors.Window
        mCombo.ForeColor = System.Drawing.Color.Black
        'mCombo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Public Function mayusculas(ByVal mTexto As TextBox, ByVal texto As String)
        mayusculas = UCase(texto)
        mTexto.SelectionStart = Len(texto)
    End Function
    Sub llenarCeros(ByVal nroCeros As Integer)
        Select Case nroCeros
            Case 1

            Case 2

            Case Else

        End Select
    End Sub
    Function validaempresa(ByVal licencia As String)
        Dim mseguridad As New seguridad
        Dim ingresa As Boolean
        Dim mifecha As Date
        Dim mifechac As String
        Try
            If licencia = "" Then
                formulariolicencia(pEmpresa)
                End
            Else
                Dim cadenaD As String = mseguridad.Cifrado(2, 1, licencia, Mid(pEmpresa, 3, 8), "09855425")
                mifecha = convierteTexto_enFecha(Mid(cadenaD, 7, 2), Mid(cadenaD, 5, 2), Mid(cadenaD, 1, 4))
                mifechac = mifecha.ToString("dd/MM/yyyy")
                mifechac = "31/12/2019"
                Dim diffechas As Integer = DateDiff(DateInterval.Day, DateTime.Parse(pFechaSystem), DateTime.Parse(mifechac))
                If diffechas >= 0 And diffechas <= 10 Then
                    MessageBox.Show("faltan " & diffechas & " dias para caducar su LICENCIA, " & Chr(13) _
                                    & " Por favor, Comunicarse con su Proveedor")
                End If
                If diffechas < 0 Then
                    formulariolicencia(pEmpresa)
                    End
                End If
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
            formulariolicencia(pEmpresa)
            End
        Finally

            ' Dim fecha1 As Integer = mifechac - pFechaSystem

            Dim cadenaC As String = mseguridad.Cifrado(1, 1, convierteFechaEspecificadadia(pFechaSystem), Mid(pEmpresa, 3, 8), "09855425")

            'Dim cadenaD As String = mseguridad.Cifrado(2, 1, licencia, Mid(pEmpresa, 3, 8), "09855425")


        End Try

        Return ingresa
    End Function
    Function validaperfil(ByVal perfil As String)
        Dim ingresa As Boolean = True
        If perfil = "2" Then
            MessageBox.Show("No tiene Autorizacion")
            ingresa = False
        End If
        Return ingresa
    End Function

    Sub formulariolicencia(ByVal ruc As String)
        Dim mutilidades As New utilidades
        Dim frmNuevo As New Form()
        Dim txtBox As New TextBox()
        Dim txtBoxLic As New TextBox()

        With txtBox
            .Text = ruc
            .Location = New System.Drawing.Point(80, 25)
            .Size() = New System.Drawing.Size(170, 40)
            .Font = New Font(New FontFamily("Arial"), 20)
            .Visible = True
        End With

        With txtBoxLic
            .Text = ""
            .Location = New System.Drawing.Point(80, 80)
            .Size() = New System.Drawing.Size(170, 160)
            .Font = New Font(New FontFamily("Arial"), 20)
            .Multiline = True
            .Visible = True
        End With

        With frmNuevo.Controls
            .Add(txtBox)
            .Add(txtBoxLic)
        End With

        With frmNuevo
            .Text = "Licencia"
            .ShowDialog()
            .StartPosition = FormStartPosition.CenterScreen
        End With
        mutilidades.ActualizarLicencia(txtBox.Text, txtBoxLic.Text)
        If txtBoxLic.Text = "" Then
            End
        End If
    End Sub
    Dim appExcel As Microsoft.Office.Interop.Excel.Application
    Dim wbExcel As Microsoft.Office.Interop.Excel.Workbook
    Sub EnviaraExcel(ByVal dg As DataGridView)
        'System.Threading.Thread.CurrentThread.CurrentCulture = _
        'System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        dg.SelectAll()
        dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Clipboard.SetDataObject(dg.GetClipboardContent())
        appExcel = New Microsoft.Office.Interop.Excel.Application
        appExcel.SheetsInNewWorkbook = 1
        wbExcel = appExcel.Workbooks.Add
        appExcel.Visible = True

        wbExcel.Worksheets(1).range("A1").Select()
        wbExcel.Worksheets(1).Paste()
    End Sub
    Function tituloVentanaPrincipal() As String
        Dim mTitulo As String = "(QUIPUPOS - " & pNempresa & ")   Tipo Cambio: " & pTC
        Return mTitulo
    End Function
    Sub asignaPermisos(ByVal cod_smenu As String, ByVal activo As Boolean)
        'If cod_smenu = "0001" Then
        '    If activo Then
        '        principal.ma_catalogo.Enabled = True
        '        principal.catalogo.Enabled = True
        '    Else
        '        principal.ma_catalogo.Enabled = False
        '        principal.catalogo.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0002" Then
        '    If activo Then
        '        principal.ma_familia.Enabled = True
        '    Else
        '        principal.ma_familia.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0003" Then
        '    If activo Then
        '        principal.ma_sgrupo.Enabled = True
        '    Else
        '        principal.ma_sgrupo.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0004" Then
        '    If activo Then
        '        principal.ma_unidades.Enabled = True
        '    Else
        '        principal.ma_unidades.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0005" Then
        '    If activo Then
        '        principal.ma_tArticulo.Enabled = True
        '    Else
        '        principal.ma_tArticulo.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0006" Then
        '    If activo Then
        '        principal.ma_planCuentas.Enabled = True
        '    Else
        '        principal.ma_planCuentas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0007" Then
        '    If activo Then
        '        principal.ma_precios.Enabled = True
        '    Else
        '        principal.ma_precios.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0008" Then
        '    If activo Then
        '        principal.ma_rendimiento.Enabled = True
        '    Else
        '        principal.ma_rendimiento.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0015" Then
        '    If activo Then
        '        principal.ma_almacen.Enabled = True
        '    Else
        '        principal.ma_almacen.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0016" Then
        '    If activo Then
        '        principal.ma_maximos.Enabled = True
        '    Else
        '        principal.ma_maximos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0017" Then
        '    If activo Then
        '        principal.ma_catalogo2.Enabled = True
        '    Else
        '        principal.ma_catalogo2.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0020" Then
        '    If activo Then
        '        principal.ma_cliente.Enabled = True
        '    Else
        '        principal.ma_cliente.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0021" Then
        '    If activo Then
        '        principal.ma_tcliente.Enabled = True
        '    Else
        '        principal.ma_tcliente.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0025" Then
        '    If activo Then
        '        principal.ma_proveedor.Enabled = True
        '    Else
        '        principal.ma_proveedor.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0026" Then
        '    If activo Then
        '        principal.ma_tproveedor.Enabled = True
        '    Else
        '        principal.ma_tproveedor.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0030" Then
        '    If activo Then
        '        principal.ma_transporte.Enabled = True
        '    Else
        '        principal.ma_transporte.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0031" Then
        '    If activo Then
        '        principal.ma_motivo.Enabled = True
        '    Else
        '        principal.ma_motivo.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0032" Then
        '    If activo Then
        '        principal.ma_conductor.Enabled = True
        '    Else
        '        principal.ma_conductor.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0035" Then
        '    If activo Then
        '        principal.ma_vendedor.Enabled = True
        '    Else
        '        principal.ma_vendedor.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0100" Then
        '    If activo Then
        '        principal.mp_ingreso.Enabled = True
        '    Else
        '        principal.mp_ingreso.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0101" Then
        '    If activo Then
        '        principal.mp_notaCredito.Enabled = True
        '    Else
        '        principal.mp_notaCredito.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0105" Then
        '    If activo Then
        '        principal.mp_pedidos.Enabled = True
        '    Else
        '        principal.mp_pedidos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0106" Then
        '    If activo Then
        '        principal.mp_ventas.Enabled = True
        '    Else
        '        principal.mp_ventas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0108" Then
        '    If activo Then
        '        principal.mp_salidas.Enabled = True
        '    Else
        '        principal.mp_salidas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0110" Then
        '    If activo Then
        '        principal.mp_transferencia.Enabled = True
        '    Else
        '        principal.mp_transferencia.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0111" Then
        '    If activo Then
        '        principal.mp_notaDebito.Enabled = True
        '    Else
        '        principal.mp_notaDebito.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0115" Then
        '    If activo Then
        '        principal.mp_transformaciones.Enabled = True
        '    Else
        '        principal.mp_transformaciones.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0116" Then
        '    If activo Then
        '        principal.mp_recetas.Enabled = True
        '    Else
        '        principal.mp_recetas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0117" Then
        '    If activo Then
        '        principal.mp_producciones.Enabled = True
        '    Else
        '        principal.mp_producciones.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0118" Then
        '    If activo Then
        '        principal.mp_actualizaRecetas.Enabled = True
        '    Else
        '        principal.mp_actualizaRecetas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0120" Then
        '    If activo Then
        '        principal.mp_cuentaC.Enabled = True
        '    Else
        '        principal.mp_cuentaC.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0121" Then
        '    If activo Then
        '        principal.mp_cuentaP.Enabled = True
        '    Else
        '        principal.mp_cuentaP.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0125" Then
        '    If activo Then
        '        principal.mp_invInicial.Enabled = True
        '    Else
        '        principal.mp_invInicial.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0127" Then
        '    If activo Then
        '        principal.mp_invDiario.Enabled = True
        '    Else
        '        principal.mp_invDiario.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0128" Then
        '    If activo Then
        '        principal.mp_invMensual.Enabled = True
        '    Else
        '        principal.mp_invMensual.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0130" Then
        '    If activo Then
        '        principal.mp_mermas.Enabled = True
        '    Else
        '        principal.mp_mermas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0200" Then
        '    If activo Then
        '        principal.mr_niveles.Enabled = True
        '    Else
        '        principal.mr_niveles.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0201" Then
        '    If activo Then
        '        principal.mr_kardex.Enabled = True
        '    Else
        '        principal.mr_kardex.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0202" Then
        '    If activo Then
        '        principal.mr_saldos.Enabled = True
        '    Else
        '        principal.mr_saldos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0204" Then
        '    If activo Then
        '        principal.mr_transferencia.Enabled = True
        '    Else
        '        principal.mr_transferencia.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0205" Then
        '    If activo Then
        '        principal.mr_recetas.Enabled = True
        '    Else
        '        principal.mr_recetas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0207" Then
        '    If activo Then
        '        principal.mr_recetas.Enabled = True
        '    Else
        '        principal.mr_recetas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0215" Then
        '    If activo Then
        '        principal.mr_ingresos.Enabled = True
        '    Else
        '        principal.mr_ingresos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0220" Then
        '    If activo Then
        '        principal.mr_pedidos.Enabled = True
        '    Else
        '        principal.mr_pedidos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0221" Then
        '    If activo Then
        '        principal.mr_ventas.Enabled = True
        '    Else
        '        principal.mr_ventas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0222" Then
        '    If activo Then
        '        principal.mr_movimientos.Enabled = True
        '    Else
        '        principal.mr_movimientos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0223" Then
        '    If activo Then
        '        principal.mr_ventaRest.Enabled = True
        '    Else
        '        principal.mr_ventaRest.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0225" Then
        '    If activo Then
        '        principal.mr_transformaciones.Enabled = True
        '    Else
        '        principal.mr_transformaciones.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0226" Then
        '    If activo Then
        '        principal.mr_produccion.Enabled = True
        '    Else
        '        principal.mr_produccion.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0230" Then
        '    If activo Then
        '        principal.mr_cuentaC.Enabled = True
        '    Else
        '        principal.mr_cuentaC.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0231" Then
        '    If activo Then
        '        principal.mr_cuentaP.Enabled = True
        '    Else
        '        principal.mr_cuentaP.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0235" Then
        '    If activo Then
        '        principal.mr_comisiones.Enabled = True
        '    Else
        '        principal.mr_comisiones.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0236" Then
        '    If activo Then
        '        principal.mr_inventarios.Enabled = True
        '    Else
        '        principal.mr_inventarios.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0240" Then
        '    If activo Then
        '        principal.mc_mermas.Enabled = True
        '    Else
        '        principal.mc_mermas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0250" Then
        '    If activo Then
        '        principal.me_precios.Enabled = True
        '    Else
        '        principal.me_precios.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0251" Then
        '    If activo Then
        '        principal.me_compras.Enabled = True
        '    Else
        '        principal.me_compras.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0252" Then
        '    If activo Then
        '        principal.me_ventas.Enabled = True
        '    Else
        '        principal.me_ventas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0253" Then
        '    If activo Then
        '        principal.me_gastos.Enabled = True
        '    Else
        '        principal.me_gastos.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0300" Then
        '    If activo Then
        '        principal.mu_configuracion.Enabled = True
        '    Else
        '        principal.mu_configuracion.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0301" Then
        '    If activo Then
        '        principal.mu_tipoCambio.Enabled = True
        '    Else
        '        principal.mu_tipoCambio.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0305" Then
        '    If activo Then
        '        principal.mu_usuarios.Enabled = True
        '    Else
        '        principal.mu_usuarios.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0310" Then
        '    If activo Then
        '        principal.mu_envioCompras.Enabled = True
        '    Else
        '        principal.mu_envioCompras.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0311" Then
        '    If activo Then
        '        principal.mu_envioVentas.Enabled = True
        '    Else
        '        principal.mu_envioVentas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0312" Then
        '    If activo Then
        '        principal.mu_pixelPoint.Enabled = True
        '    Else
        '        principal.mu_pixelPoint.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0313" Then
        '    If activo Then
        '        principal.mu_impventas.Enabled = True
        '    Else
        '        principal.mu_impventas.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0314" Then
        '    If activo Then
        '        principal.mu_codigosExt.Enabled = True
        '    Else
        '        principal.mu_codigosExt.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0315" Then
        '    If activo Then
        '        principal.mu_actLotes.Enabled = True
        '    Else
        '        principal.mu_actLotes.Enabled = False
        '    End If
        'End If
        'If cod_smenu = "0316" Then
        '    If activo Then
        '        principal.mu_procventas.Enabled = True
        '    Else
        '        principal.mu_procventas.Enabled = False
        '    End If
        'End If
    End Sub

    Function lineaImpresion(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        Dim anchocolumnas As Integer = 46
        Dim leftmargin As Single = 3
        Dim sb As System.Text.StringBuilder

        sb = New System.Text.StringBuilder
        sb.AppendFormat("{0} ", New String("-"c, anchocolumnas))
        e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)

        yPos += lineHeight
        Return yPos
    End Function

    Function ajustar(ByVal cadena As String, ByVal ancho As Integer, ByVal alinear As HorizontalAlignment) As String
        ' devuelve una cadena con el ancho indicado
        If cadena = Nothing OrElse cadena.Length = 0 Then
            Return New String(" "c, ancho)
        End If
        '
        If alinear = HorizontalAlignment.Right Then
            If ancho > cadena.Length Then cadena = New String(" "c, ancho - cadena.Length) & cadena
            Return cadena.Substring(cadena.Length - ancho, ancho)
        ElseIf alinear = HorizontalAlignment.Center Then
            If ancho > cadena.Length Then cadena = New String(" "c, ((ancho - cadena.Length) / 2) + 1) & cadena & New String(" "c, ((ancho - cadena.Length) / 2) + 1)
            Return cadena.Substring(0, ancho)
        Else
            If ancho > cadena.Length Then cadena &= New String(" "c, ancho - cadena.Length)
            Return cadena.Substring(0, ancho)
        End If
    End Function
End Module
