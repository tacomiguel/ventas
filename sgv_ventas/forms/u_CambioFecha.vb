Imports MySql.Data
Imports libreriaVentas


Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Public Class u_CambioFecha
    'para validar el separador decimal
    Private sepDecimal As Char
    Private dscierre As New DataSet
    Private mdocumento As New Documento

    Private Titulo As String = "VENTAS"
    Private prtSettings As PrinterSettings
    Private prtDoc As PrintDocument
    Private prtFont As System.Drawing.Font
    Private lineaActual As Integer

    Private salto_pagina As Boolean = False
    Private registros As Integer = 0

    Private Sub u_tipoCambio_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim mTC As New TipoCambio
        Dim tcambio As Decimal = mTC.recupera(0, pFechaSystem)
        txtTC.Text = tcambio
    End Sub

    Private Sub u_tipoCambio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = 0
        'capturamos el separador decimal
        sepDecimal = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        lblFecha.Text = general.devuelveFechaImpresionEspecificado(pFechaSystem)
        estableceFechaProceso()
    End Sub
    Private Sub txtTC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTC.GotFocus
        general.ingresaTextoProceso(txtTC)
    End Sub
    Private Sub txtTC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTC.LostFocus
        general.saleTextoProceso(txtTC)
    End Sub
    Private Sub txtTC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTC.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) And Not (e.KeyChar.Equals(sepDecimal)) Then
            e.KeyChar = ""
        End If
    End Sub

    Sub estableceFechaProceso()
        If pNivelUser = 0 Then
            mcFIngreso.MaxDate = pFechaSystem
            If pFechaActivaMin > Microsoft.VisualBasic.DateAndTime.DateAdd(DateInterval.Day, -pDiasModificarIngreso, pFechaSystem) Then
                mcFIngreso.MinDate = Microsoft.VisualBasic.DateAndTime.DateAdd(DateInterval.Day, -pDiasModificarIngreso, pFechaSystem)
            Else
                mcFIngreso.MinDate = pFechaActivaMin
            End If
        Else
            mcFIngreso.MinDate = Microsoft.VisualBasic.DateAndTime.DateAdd(DateInterval.Day, -pDiasModificarIngreso, pFechaSystem)
            mcFIngreso.MaxDate = pFechaSystem
        End If

        mcFIngreso.DisplayMonth = pFechaSystem
        mcFIngreso.SelectedDate = pFechaSystem

    End Sub

    Private Sub mcFIngreso_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcFIngreso.ItemClick
        lblFecha.Text = general.devuelveFechaImpresionEspecificado(mcFIngreso.SelectedDate)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim mTC As New TipoCambio
        Dim rpta As Integer
        rpta = MessageBox.Show("Esta Seguro de Cerrar la fecha ?", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If rpta = 6 Then
            'estados fecha 0=abierto,1=cerrado
            If mTC.recupera(1, mcFIngreso.SelectedDate) = 1 Then
                MessageBox.Show("FECHA Seleccionada se encuentra CERRADO...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                'actualizarfecha(mcFIngreso.SelectedDate)
                actualizarfecha(mcFIngreso.SelectedDate.AddDays(1))
            End If

            'actualizarfecha(mcFIngreso.SelectedDate.AddDays(1))
        End If


    End Sub
    Private Sub actualizarfecha(ByVal mfecha As Date)
        Dim mTC As New TipoCambio
        Dim tcambio As Decimal
        If mfecha <= pFechaActivaMax Then
            If Len(txtTC.Text) = 0 Then
                tcambio = 0
            Else
                tcambio = txtTC.Text
            End If
            mTC.actualizar(pFechaSystem, tcambio, pCuentaUser, False, 1)
            If mTC.existe(mfecha) Then
                mTC.actualizar(mfecha, tcambio, pCuentaUser, True, 0)
            Else
                mTC.insertar(mfecha, tcambio, pCuentaUser)
            End If
            pTC = tcambio
            pFechaSystem = fechaSystem(pfechaservidor)

            MessageBox.Show("FECHA DEL SISTEMA Actualizado...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        SetDefaultPrinter("reportes")
        imprimirCierreX()

    End Sub
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        SetDefaultPrinter("reportes")
        imprimirCierreZ()
    End Sub
    Private Sub imprimirdoc(ByVal esPreview As Boolean)
        ' imprimir o mostrar el PrintPreview
        Dim TamañoPersonal As Printing.PaperSize
        Dim Ancho As Short
        Dim Alto As Short
        Dim nomImpresora = mdocumento.NombreImpresora("01")
        SetDefaultPrinter("tmu_220")
        If prtSettings Is Nothing Then
            prtSettings = New PrinterSettings
        End If
        Ancho = Short.Parse("500")
        Alto = Short.Parse("4000")
        TamañoPersonal = New Printing.PaperSize("personal", Ancho, Alto)
        '
        If prtDoc Is Nothing Then
            prtDoc = New PrintDocument
            AddHandler prtDoc.PrintPage, AddressOf prt_PrintPage
        End If
        '
        ' resetear la línea actual
        lineaActual = 0
        '
        ' la configuración a usar en la impresión
        prtDoc.PrinterSettings = prtSettings
        'prtDoc.DefaultPageSettings.PaperSize = TamañoPersonal

        If esPreview Then
            Dim prtPrev As New PrintPreviewDialog
            prtPrev.Document = prtDoc
            prtPrev.Text = "Previsualizar datos de " & Titulo
            prtPrev.ShowDialog()
        Else
            prtDoc.Print()
        End If
    End Sub

    Private Sub prt_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        ' Este evento se produce cada vez que se va a imprimir una página
        Dim lineHeight As Single
        'Dim yPos As Single = e.MarginBounds.Top
        Dim yPos As Single = 2
        Dim leftMargin As Single = e.MarginBounds.Left
        Dim printFont As Font
        prtFont = New System.Drawing.Font("Courier ", 8)
        ' Asignar el tipo de letra
        printFont = prtFont
        lineHeight = printFont.GetHeight(e.Graphics)

        Dim fontTitulo As New Font("Arial", 20, FontStyle.Bold)
        yPos += fontTitulo.GetHeight(e.Graphics)

        ' imprimir la cabecera de la página
        yPos = CabeceraImpresión(e, printFont, yPos)
        'yPos = lineaImpresion(e, printFont, yPos)
        yPos = DetalleImpresión(e, printFont, yPos)
        'yPos = lineaImpresion(e, printFont, yPos)
        'yPos = PiedePagina(e, printFont, yPos)

        'nuevapagina(e)
        e.HasMorePages = False

    End Sub

    Function TextoImpresion(ByVal texto As String, ByVal S As System.Text.StringBuilder, ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal leftmargin As Single, ByVal yPos As Single, ByVal anchocolumna As Integer, ByVal alinear As HorizontalAlignment, ByVal nuevalinea As Boolean) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        'Dim leftmargin As Single = 3
        'Dim sb As System.Text.StringBuilder

        'sb = New System.Text.StringBuilder
        S.AppendFormat("{0} ", ajustar(texto, anchocolumna, alinear))
        If nuevalinea Then
            e.Graphics.DrawString(S.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += lineHeight
        End If

        Return yPos
    End Function
    Public Function CabeceraImpresión(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        Dim mutilidades As New utilidades
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim ds As DataSet = mutilidades.recuperaEmpresa()
        mitabla = ds.Tables(0)
        Dim cabeceras As Integer = 5


        sb = New System.Text.StringBuilder
        TextoImpresion("CIERRE DE TURNO", sb, e, prFont, 3, yPos, 40, HorizontalAlignment.Center, True)
        yPos += (lineHeight)


        For i As Integer = 0 To cabeceras
            sb = New System.Text.StringBuilder
            TextoImpresion(mitabla.Rows(0)(i).ToString(), sb, e, prFont, 3, yPos, 40, HorizontalAlignment.Center, True)
            yPos += (lineHeight)
        Next
        yPos = lineaImpresion(e, prFont, yPos)

        sb = New System.Text.StringBuilder
        TextoImpresion("FECHA     :", sb, e, prFont, 3, yPos, 15, HorizontalAlignment.Left, False)
        TextoImpresion(mcFIngreso.SelectedDate.ToShortDateString, sb, e, prFont, 3, yPos, 15, HorizontalAlignment.Right, True)
        yPos += (lineHeight * 2)

        sb = New System.Text.StringBuilder
        TextoImpresion("VENDEDOR  :", sb, e, prFont, 3, yPos, 15, HorizontalAlignment.Left, False)
        TextoImpresion(pNombreUser, sb, e, prFont, 3, yPos, 15, HorizontalAlignment.Right, True)
        yPos += (lineHeight * 2)


        Return yPos

    End Function

    Public Function DetalleImpresión(ByVal e As PrintPageEventArgs, ByVal prFont As Font, ByVal yPos As Single) As Single
        Dim lineHeight As Single = prFont.GetHeight(e.Graphics)
        Dim leftmargin As Single = 3
        Dim mutilidades As New utilidades
        Dim sb As System.Text.StringBuilder
        Dim mitabla As DataTable
        Dim ds As DataSet = mutilidades.recuperaventas(mcFIngreso.SelectedDate, False)
        Dim importe As Decimal
        mitabla = ds.Tables(0)

        Dim cabeceras As Integer = 3
        Dim formapago As String = ""

        registros = 0

        For i As Integer = registros To mitabla.Rows.Count() - 1


            If mitabla.Rows(i)(1) <> formapago Then
                sb = New System.Text.StringBuilder
                sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(1).ToString(), 15, HorizontalAlignment.Left))
                e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
                yPos += (lineHeight * 2)
            End If


            sb = New System.Text.StringBuilder
            For j As Integer = 0 To cabeceras
                Select Case j
                    Case 0
                        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(j).ToString(), 15, HorizontalAlignment.Left))
                    Case 2
                        importe = mitabla.Rows(i)(j)
                        sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                    Case 3
                        importe = mitabla.Rows(i)(j)
                        sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                End Select

            Next
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += (lineHeight * 2)

            formapago = mitabla.Rows(i)(1)

       


        Next

        yPos = lineaImpresion(e, prFont, yPos)
        sb = New System.Text.StringBuilder
        TextoImpresion("RESUMEN DE PAGOS", sb, e, prFont, 3, yPos, 40, HorizontalAlignment.Center, True)
        yPos += (lineHeight)


        ds = mutilidades.recuperaventas(mcFIngreso.SelectedDate, True)
        mitabla = ds.Tables(0)
        For i As Integer = 0 To mitabla.Rows.Count() - 1
            sb = New System.Text.StringBuilder
            For j As Integer = 0 To cabeceras
                Select Case j
                    Case 1
                        sb.AppendFormat("{0} ", ajustar(mitabla.Rows(i)(j).ToString(), 15, HorizontalAlignment.Left))
                    Case 2
                        importe = mitabla.Rows(i)(j)
                        sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                    Case 3
                        importe = mitabla.Rows(i)(j)
                        sb.AppendFormat("{0} ", ajustar(importe.ToString("##0.00"), 8, HorizontalAlignment.Right))
                End Select
            Next
            e.Graphics.DrawString(sb.ToString, prFont, Brushes.Black, leftmargin, yPos)
            yPos += (lineHeight * 2)
        Next

        Return yPos

    End Function


    Private Sub imprimirCierreX()

        Dim frm As New rptForm, msalida As New Salida
        Dim rpta As Integer

        rpta = MessageBox.Show("Desea Enviar en Vista Previa ?", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        Dim vprevia As Boolean = IIf(rpta = 6, True, False)

        dscierre = msalida.recuperaVentas(mcFIngreso.SelectedDate, mcFIngreso.SelectedDate, False, True, pCuentaUser)
        frm.cargarCierre(dscierre, vprevia, mcFIngreso.SelectedDate, True, pCuentaUser, "rptCierre.rpt")

        If vprevia Then
            frm.Show()
        End If


    End Sub

    Private Sub imprimirCierreZ()

        Dim frm As New rptForm, msalida As New Salida
        Dim rpta As Integer

        rpta = MessageBox.Show("Desea Enviar en Vista Previa ?", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        Dim vprevia As Boolean = IIf(rpta = 6, True, False)

        dscierre = msalida.recuperaVentas(mcFIngreso.SelectedDate, mcFIngreso.SelectedDate, False, False, pCuentaUser)
        frm.cargarCierre(dscierre, vprevia, mcFIngreso.SelectedDate, False, pCuentaUser, "rptCierreZ.rpt")

        If vprevia Then
            frm.Show()
        End If

    End Sub


End Class
