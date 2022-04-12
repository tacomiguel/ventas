Imports libreriaVentas
Imports MySql.Data.MySqlClient
Imports System.Data


Public Class p_mesas
    Private daMesas As New MySqlDataAdapter
    Private cbMesas As MySqlCommandBuilder = New MySqlCommandBuilder(daMesas)
    Private dsMesas As New DataSet
    Private dtMesas As DataTable
    Private bsMesas As New BindingSource
    Private lcodigo As String
    Private estacargando As Boolean = True
    Private _cod_salon As String
    Private Sub p_mesas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'cargamos el dataset
        'dsMesas = SubGrupo.dsSubGrupo
        'dtMesas = dsMesas.Tables("Mesas")
        'chkProduccion.DataBindings.Add("checked", bsSubGrupo, "esProduccion")
        'ChkVentas.DataBindings.Add("checked", bsSubGrupo, "esVenta")
        'chkInventario.DataBindings.Add("checked", bsSubGrupo, "repInventario")
        'txtancho.DataBindings.Add("text", bsSubGrupo, "b_ancho")
        'txtalto.DataBindings.Add("text", bsSubGrupo, "b_alto")
        'mostrarmesas()
        'TxtCodigo.DataBindings.Add("text", bsMesas, "cod_mesa")
        'TxtDescripcion.DataBindings.Add("text", bsMesas, "dsc_mesa")
        'cboSalon.DataBindings.Add("selectedValue", bsMesas, "cod_salon")
        'txtancho.DataBindings.Add("text", bsMesas, "b_ancho")
        'txtalto.DataBindings.Add("text", bsMesas, "b_alto")
        'chkActivo.DataBindings.Add("checked", bsMesas, "activo")
        estacargando = False
    End Sub

    Sub mostrarmesas()
        Dim com As New MySqlCommand("Select cod_mesa,dsc_mesa,cod_salon,b_ancho,b_alto,activo from Mesas where cod_salon='" & _cod_salon & "' order by dsc_mesa", dbConex)
        daMesas.SelectCommand = com
        daMesas.Fill(dsMesas, "mesas")
        bsMesas.DataSource = dsMesas
        bsMesas.DataMember = "mesas"
        With datos
            .DataSource = bsMesas
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("cod_mesa").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns.Item("cod_mesa").HeaderText = "Código"
            .Columns.Item("cod_mesa").Resizable = DataGridViewTriState.False
            .Columns.Item("cod_mesa").Width = "80"
            .Columns.Item("cod_mesa").DisplayIndex = 0
            .Columns.Item("dsc_mesa").HeaderText = "Descripcion"
            .Columns.Item("dsc_mesa").Resizable = DataGridViewTriState.False
            .Columns.Item("dsc_mesa").Width = "360"
            .Columns.Item("dsc_mesa").DisplayIndex = 1
            .Columns.Item("activo").HeaderText = "Activo"
            .Columns.Item("activo").Resizable = DataGridViewTriState.False
            .Columns.Item("activo").Width = "50"
            .Columns.Item("activo").DisplayIndex = 2
            .Columns.Item("cod_salon").Visible = False
            .Columns.Item("b_ancho").Visible = False
            .Columns.Item("b_alto").Visible = False

        End With
        'relacionamos los cuadros de texto con el bindingSource
        TxtCodigo.DataBindings.Add("text", bsMesas, "cod_mesa")
        TxtDescripcion.DataBindings.Add("text", bsMesas, "dsc_mesa")
        txtancho.DataBindings.Add("text", bsMesas, "b_ancho")
        txtalto.DataBindings.Add("text", bsMesas, "b_alto")
        chkActivo.DataBindings.Add("checked", bsMesas, "activo")

    End Sub

    Private Sub cmdAñadir_Click(sender As System.Object, e As System.EventArgs) Handles cmdAñadir.Click
        habilitaDetalle()
        deshabilitaCabecera()

        dsMesas.Tables("mesas").Columns("cod_salon").DefaultValue = _cod_salon
        dsMesas.Tables("mesas").Columns("cod_mesa").DefaultValue = ""
        dsMesas.Tables("mesas").Columns("activo").DefaultValue = True

        bsMesas.AddNew()
        datos.DataSource = bsMesas
        modoGrabar()
        txtCodigo.Refresh()
        TxtCodigo.Focus()
        TxtDescripcion.Refresh()
        chkActivo.Refresh()


        

    End Sub

    Sub habilitaDetalle()
        txtCodigo.BackColor = Color.White
        txtCodigo.ReadOnly = False
        TxtDescripcion.BackColor = Color.White
        TxtDescripcion.ReadOnly = False
        chkActivo.Enabled = True

    End Sub
    Sub deshabilitaDetalle()

        txtCodigo.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        txtCodigo.ReadOnly = True
        TxtDescripcion.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        TxtDescripcion.ReadOnly = True
        chkActivo.Enabled = False

    End Sub
    Sub habilitaCabecera()
        datos.Enabled = True
    End Sub
    Sub deshabilitaCabecera()
        datos.Enabled = False
    End Sub

    Sub modoAñadir()
        cmdAñadir.Enabled = True
        cmdGrabar.Enabled = False
        cmdCancelar.Enabled = False
        cmdEditar.Enabled = True
        cmdEliminar.Enabled = True
    End Sub
    Sub modoGrabar()
        cmdAñadir.Enabled = False
        cmdGrabar.Enabled = True
        cmdCancelar.Enabled = True
        cmdEditar.Enabled = False
        cmdEliminar.Enabled = False
    End Sub

    Private Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
        Try
            'si hay imagen
            If Not Imagen Is Nothing Then
                'caturar array con memorystream hacia Bin
                Dim Bin As New IO.MemoryStream(Imagen)
                'con el método FroStream de Image obtenemos imagen
                Dim Resultado As Image = Image.FromStream(Bin)
                'y la retornamos
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'convertir imagen a binario
    Private Function Imagen_Bytes(ByVal Imagen As Image) As Byte()
        'si hay imagen
        If Not Imagen Is Nothing Then
            'variable de datos binarios en stream(flujo)
            Dim Bin As New IO.MemoryStream
            'convertir a bytes
            Imagen.Save(Bin, Imaging.ImageFormat.Png)
            'retorna binario
            Return Bin.GetBuffer
        Else
            Return Nothing
        End If
    End Function
    Function validaIngreso()
        Dim validado As Boolean = False
        If Len(txtCodigo.Text) > 0 Then
            If Len(TxtDescripcion.Text) > 0 Then
                validado = True
                'If cboSalon.SelectedIndex >= 0 Then
                '    validado = True
                'Else
                '    MessageBox.Show("Seleccione El Salon...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                'End If
            Else
                MessageBox.Show("Ingrese la Descripcion...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtDescripcion.Focus()
            End If
        Else
            MessageBox.Show("Ingrese el Código...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCodigo.Focus()
        End If
        Return validado
    End Function
    Private Sub cmdGrabar_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrabar.Click
        Dim result As Integer, cad As String
        Try
            If validaIngreso() = True Then
                Dim Imag As Byte() = Imagen_Bytes(Me.pb_foto.Image)
                Dim ImagX As Byte() = Imagen_Bytes(Me.pb_fotoX.Image)
                bsMesas.EndEdit()
                daMesas.Update(dsMesas, "mesas")
                datos.Refresh()
                deshabilitaDetalle()
                habilitaCabecera()
                modoAñadir()
                datos.Focus()
                cad = " update mesas set cod_salon='" & _cod_salon & "', foto = ?imagen, fotoX = ?imagenX " & _
                          " where cod_mesa='" & lcodigo & "'"
                Dim com As New MySqlCommand(cad, dbConex)
                com.Parameters.AddWithValue("?imagen", Imag)
                com.Parameters.AddWithValue("?imagenX", ImagX)
                result = com.ExecuteNonQuery()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bsMesas.CancelEdit()
            datos.Refresh()
            deshabilitaDetalle()
            habilitaCabecera()
            modoAñadir()
            datos.Focus()
        End Try
    End Sub

    Private Sub cmdCancelar_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancelar.Click
        bsMesas.CancelEdit()
        datos.Refresh()
        deshabilitaDetalle()
        habilitaCabecera()
        modoAñadir()
        datos.Focus()
        If datos.Rows.Count > 0 Then
            datos.CurrentCell = datos(1, 0)
        End If
    End Sub

    Private Sub cmdEliminar_Click(sender As System.Object, e As System.EventArgs) Handles cmdEliminar.Click
        If bsMesas.Count > 0 Then
            Dim mSalida As New Salida, exist, rpta As Integer
            'exist = m.existeEnCatalogo(Trim(TxtCodigo.Text))
            exist = 0
            If exist = 0 Then
                rpta = MessageBox.Show("¿Esta seguro de Eliminar el ITEM Seleccionado...", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If rpta = 6 Then
                    If bsMesas.Count > 0 Then
                        bsMesas.RemoveCurrent()
                        daMesas.Update(dsMesas, "mesas")
                        datos.Refresh()
                    End If
                End If
            Else
                MessageBox.Show("El Registro Tiene Datos Relacionados..." + Chr(13) + "!NO Es posible eliminarlo¡", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2)
            End If
        End If
    End Sub

    Private Sub cmdEditar_Click(sender As System.Object, e As System.EventArgs) Handles cmdEditar.Click
        If bsMesas.Count > 0 Then
            habilitaDetalle()
            deshabilitaCabecera()
            modoGrabar()
            TxtCodigo.Focus()
            lcodigo = TxtCodigo.Text
        End If
    End Sub

    Private Sub cmdCerrar_Click(sender As System.Object, e As System.EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
 

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Dim vImagen As System.Drawing.Image
        vImagen = Clipboard.GetImage
        If rb_A.Checked Then
            pb_foto.Image = vImagen
        Else
            pb_fotoX.Image = vImagen
        End If
    End Sub

    Private Sub cmdExaminar_Click(sender As System.Object, e As System.EventArgs) Handles cmdExaminar.Click
        Dim oFD As New OpenFileDialog, mFile As String
        With oFD
            .Title = "Seleccionar fichero"
            .Multiselect = False
            '.Filter = "Ficheros de texto (*.txt;*.ini)|*.txt;*.ini" & _
            '          "|Todos los ficheros (*.*)|*.*"
            .Filter = "Archivo de Imagen (*.jpg, *.jpeg, *.png)|*.jpg; *.jpeg; *.png"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                mFile = .FileName
                If rb_A.Checked Then
                    Me.pb_foto.Image = Image.FromFile(mFile)
                Else
                    Me.pb_fotoX.Image = Image.FromFile(mFile)
                End If
            Else
                mFile = ""
            End If

        End With
    End Sub



    Private Sub datos_SelectionChanged(sender As Object, e As System.EventArgs) Handles datos.SelectionChanged
        Dim codigo As String
        If Not estacargando Then
            If datos.RowCount > 0 Then
                If Not IsDBNull(datos("cod_mesa", datos.CurrentRow.Index).Value) Then
                    codigo = datos("cod_mesa", datos.CurrentRow.Index).Value
                    MuestraFoto(codigo)
                End If
            End If
        End If
    End Sub
    Sub MuestraFoto(ByVal codigo As String)
        Try
            Dim cad As String, dr As MySqlDataReader, resul As Integer
            cad = "select isnull(foto) as vnul,foto,isnull(fotoX) as vnulx,fotoX from mesas where cod_mesa='" & codigo & "'"
            Dim com As New MySqlCommand(cad, dbConex)
            dr = com.ExecuteReader
            Dim Imag As Byte() = Nothing
            While dr.Read
                resul = dr("vnul")
                If resul = 0 Then
                    Imag = dr("foto")
                    Me.pb_foto.Image = Bytes_Imagen(Imag)
                Else
                    Me.pb_foto.Image = Bytes_Imagen(Imag)
                End If
                resul = dr("vnulx")
                If resul = 0 Then
                    Imag = dr("fotoX")
                    Me.pb_fotoX.Image = Bytes_Imagen(Imag)
                Else
                    Me.pb_fotoX.Image = Bytes_Imagen(Imag)
                End If

            End While
            dr.Close()
            dr = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Sub datossalon(ByVal cod_salon As String)
        _cod_salon = cod_salon
        mostrarmesas()
    End Sub

    Private Sub TxtCodigo_LostFocus(sender As Object, e As EventArgs) Handles TxtCodigo.LostFocus
        TxtCodigo.Text = Microsoft.VisualBasic.Right("000" & Trim(Str(Microsoft.VisualBasic.Val(TxtCodigo.Text))), 3)
    End Sub

End Class
