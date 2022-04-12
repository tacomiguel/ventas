Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Math
Imports libreriaClases
Public Class p_receta
    'creamos el BindingSource que relacionara los datos con los controles
    Private bsProduccion As New BindingSource
    Private daProduccion As New MySqlDataAdapter
    Private dsProduccion As DataSet
    Private cbProduccion As MySqlCommandBuilder = New MySqlCommandBuilder(daProduccion)
    Private dtProduccion As DataTable
    Private dtUnidad As DataTable
    Private dtTipo As DataTable
    Private dtSGrupo As DataTable
    Private dtGrupoV As DataTable
    Private dtArea As DataTable
    Private dsInsumo As New DataSet
    Private dsReceta As New DataSet
    Private dsArea As New DataSet
    Private dsAlmacen As New DataSet
    Private dsCombo As New DataSet
    Private dtcombo As New DataTable
    Private estaCargando As Boolean = True
    'capturamos el boton pulsado
    Private lcBoton As String
    'capturamos el codigo del articulo-esto para la edicion
    Private lCodigo As String
    'para validar el separador decimal
    Private sepDecimal As Char
    Private Sub p_receta_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        principal.mp_eventos.Enabled = True
    End Sub
    Private Sub p_receta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = 0
        'capturamos el separador decimal
        sepDecimal = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        'dataset almacen
        Dim daAlmacen As New MySqlDataAdapter
        Dim cadAlma As String

        If pCatalogoXalmacen Then
            If pAlmaUser = "0000" Then
                cadAlma = "select cod_alma,nom_alma from almacen where (esProduccion) and activo=1"
            Else
                cadAlma = "select cod_alma,nom_alma from almacen where cod_alma='" & pAlmaUser & "' and (esProduccion) and activo=1"
            End If
        Else
            cadAlma = "select cod_alma,nom_alma from almacen where (esProduccion) and activo=1"
        End If
        Dim comAlmacen As New MySqlCommand(cadAlma, dbConex)
        daAlmacen.SelectCommand = comAlmacen
        daAlmacen.Fill(dsAlmacen, "almacen")
        With cboAlmacen
            .DataSource = dsAlmacen.Tables("almacen")
            .DisplayMember = dsAlmacen.Tables("almacen").Columns("nom_alma").ToString
            .ValueMember = dsAlmacen.Tables("almacen").Columns("cod_alma").ToString
            If dsAlmacen.Tables("almacen").Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
       
        Dim cAlmacen As String = cboAlmacen.SelectedValue
        cargaProduccion(cAlmacen)
        estructuraProduccion()
        'relacionamos los cuadros de texto con los campos respectivos
        txtCodigo.DataBindings.Add("text", bsProduccion, "cod_art")
        txtProduccion.DataBindings.Add("text", bsProduccion, "nom_art")
        txtdsc_corta.DataBindings.Add("text", bsProduccion, "nom_art2")
        txtPreCosto.DataBindings.Add("text", bsProduccion, "pre_costo")
        txtPreVenta.DataBindings.Add("text", bsProduccion, "pre_venta")
        TxtFactorProd.DataBindings.Add("text", bsProduccion, "factor_prod")
        txtUnidad.DataBindings.Add("text", bsProduccion, "nom_uni")
        TxtArea.DataBindings.Add("text", bsProduccion, "nom_area")
        txtTipo.DataBindings.Add("text", bsProduccion, "nom_tart")
        TxtCodigoVta.DataBindings.Add("text", bsProduccion, "cod_Artext")
        TxtCodigoVta1.DataBindings.Add("text", bsProduccion, "cod_Artext1")
        txtCodigoVta2.DataBindings.Add("text", bsProduccion, "cod_Artext2")
        txtCodigoVta3.DataBindings.Add("text", bsProduccion, "cod_Artext3")
        txtancho.DataBindings.Add("text", bsProduccion, "b_ancho")
        txtalto.DataBindings.Add("text", bsProduccion, "b_alto")
        txtSGrupo.DataBindings.Add("text", bsProduccion, "nom_sgrupo")
        txtGventa.DataBindings.Add("text", bsProduccion, "nom_grupov")
        cboUnidad.DataBindings.Add("selectedValue", bsProduccion, "cod_uni")
        cboUnidad.DataSource = dsProduccion.Tables("unidad")
        cboUnidad.DisplayMember = dsProduccion.Tables("unidad").Columns("nom_uni").ToString
        cboUnidad.ValueMember = dsProduccion.Tables("unidad").Columns("cod_uni").ToString
        cboArea.DataBindings.Add("selectedValue", bsProduccion, "cod_area")
        cboArea.DataSource = dsProduccion.Tables("Area")
        cboArea.DisplayMember = dsProduccion.Tables("Area").Columns("nom_area").ToString
        cboArea.ValueMember = dsProduccion.Tables("Area").Columns("cod_area").ToString

        cboTipo.DataBindings.Add("selectedValue", bsProduccion, "cod_tart")
        cboTipo.DataSource = dsProduccion.Tables("tipo_articulo")
        cboTipo.DisplayMember = dsProduccion.Tables("tipo_articulo").Columns("nom_tart").ToString
        cboTipo.ValueMember = dsProduccion.Tables("tipo_articulo").Columns("cod_tart").ToString

        cboGrupo.DataBindings.Add("selectedValue", bsProduccion, "cod_sgrupo")
        cboGrupo.DataSource = dsProduccion.Tables("subGrupo")
        cboGrupo.DisplayMember = dsProduccion.Tables("subGrupo").Columns("nom_sgrupo").ToString
        cboGrupo.ValueMember = dsProduccion.Tables("subGrupo").Columns("cod_sgrupo").ToString

        cboGventa.DataBindings.Add("selectedValue", bsProduccion, "cod_grupov")
        cboGventa.DataSource = dsProduccion.Tables("GrupoV")
        cboGventa.DisplayMember = dsProduccion.Tables("GrupoV").Columns("nom_sgrupo").ToString
        cboGventa.ValueMember = dsProduccion.Tables("GrupoV").Columns("cod_sgrupo").ToString

        chkPreIncImp.DataBindings.Add("checked", bsProduccion, "pre_inc_imp")
        chkPreAfecImp.DataBindings.Add("checked", bsProduccion, "afecto_igv")
        chkActivo.DataBindings.Add("checked", bsProduccion, "activo")
        chkpcosto.DataBindings.Add("checked", bsProduccion, "es_pcosto")
        '
        Dim mCatalogo As New Catalogo

        dsInsumo = mCatalogo.recuperaProducto(pCatalogoXalmacen, cboAlmacen.SelectedValue, "ads-3f")
        
        dataInsumo.DataSource = dsInsumo.Tables("articulo").DefaultView
        'dataset unidad
        dtUnidad = dsProduccion.Tables("unidad")
        Dim daUnidad As New MySqlDataAdapter
        Dim comUnidad As New MySqlCommand("select * from unidad where activo=1 and (esProduccion) order by nom_uni", dbConex)
        daUnidad.SelectCommand = comUnidad
        daUnidad.Fill(dsProduccion, "unidad")
        'DataSet Area
        dtArea = dsProduccion.Tables("Area")
        Dim daArea As New MySqlDataAdapter
        Dim comArea As New MySqlCommand("select * from area where activo=1  order by nom_area", dbConex)
        daArea.SelectCommand = comArea
        daArea.Fill(dsProduccion, "Area")
        'dataset tipo de articulo
        dtTipo = dsProduccion.Tables("tipo_articulo")
        Dim daTipo As New MySqlDataAdapter
        Dim comTipo As New MySqlCommand("select * from tipo_articulo where activo=1 and ((esProduccion) or (esCombo)) order by nom_tart", dbConex)
        daTipo.SelectCommand = comTipo
        daTipo.Fill(dsProduccion, "tipo_articulo")

        'dataset sub grupo
        Dim cCampo As String = "nom_sgrupo"
        dtSGrupo = dsProduccion.Tables("subGrupo")
        Dim daSGrupo As New MySqlDataAdapter
        Dim comSGrupo As New MySqlCommand("select cod_sgrupo," & general.convierte_enTitulo(cCampo) _
            & " as nom_sgrupo from subGrupo where activo=1 and (esProduccion) order by nom_sgrupo", dbConex)
        daSGrupo.SelectCommand = comSGrupo
        daSGrupo.Fill(dsProduccion, "subGrupo")

        'dataset Grupo Venta
        Dim cCampov As String = "nom_sgrupo"
        dtGrupoV = dsProduccion.Tables("GrupoV")
        Dim daGrupoV As New MySqlDataAdapter
        Dim comGrupoV As New MySqlCommand("select cod_sgrupo," & general.convierte_enTitulo(cCampov) _
            & " as nom_sgrupo from subGrupo where activo=1 and (esVenta) order by nom_sgrupo", dbConex)
        daGrupoV.SelectCommand = comGrupoV
        daGrupoV.Fill(dsProduccion, "GrupoV")

        cargaEstructuraInsumos()

        muestraArea(cboAlmacen.SelectedValue)
        estaCargando = False
    End Sub
    Sub MuestraCombo(ByVal cod_rec As String)

        Dim mReceta As New Receta
        'Datos Combo
        dsCombo = mReceta.recuperaCombo(cod_rec)
        dtcombo = dsCombo.Tables("Combo")
        DataCombo.DataSource = dsCombo.Tables("Combo").DefaultView
        With tvCombo
            .DataSource = dsCombo.Tables("Combo").DefaultView
            .DisplayMembers = "Descripcion,cant,precio"
            .ValueMember = dsCombo.Tables("Combo").Columns("cod_art").ToString
            .GroupingMembers = dsCombo.Tables("Combo").Columns("dsc_combo").ToString
        End With
        'TabControl2.SelectedTabIndex = 2
        cargaestructuratvcombo()
    End Sub
    Sub cargaestructuratvcombo()
        With tvCombo
            .Columns.Item(0).Width.Absolute = 310
            .Columns.Item(1).Width.Absolute = 50
            .Columns.Item(2).Width.Absolute = 50
        End With
    End Sub
    Sub muestraReceta(ByVal cod_rec As String)
        Dim mReceta As New Receta
        dsReceta.Clear()
        dsReceta = mReceta.recuperaReceta(cod_rec)
        dataReceta.DataSource = dsReceta.Tables("receta").DefaultView
        cargaEstructuraReceta()
    End Sub
    Sub cargaProduccion(ByVal cod_alma As String)
        'cargamos el dataset
        dsProduccion = Catalogo.dsCatalogo
        'cargamos el datatable
        dtProduccion = dsProduccion.Tables("articulo")
        'creamos y trabajamos con el comando a ejecutar
        Dim cad, cad1, cad2, cad3, cad4, cad5, cad6, cad7 As String
        Dim cCampo As String = "s1.nom_sgrupo"
        cad1 = "Select cod_art,nom_art,nom_art2,articulo.cod_uni,nom_uni,articulo.cod_sgrupo,articulo.cod_grupov,articulo.cod_area,nom_area," _
                & general.convierte_enTitulo(cCampo) & " as nom_sgrupo,s2.nom_sgrupo as nom_Grupov,factor_prod,tipo_articulo.esProduccion,"
        cad2 = " articulo.cod_tart,nom_tart,es_pcosto,pre_costo,pre_venta,pre_inc_imp,afecto_igv,articulo.activo,cod_artext,cod_artext1,cod_artext2,cod_artext3,articulo.b_ancho,articulo.b_alto"
        cad3 = " from articulo inner join subGrupo s1 on articulo.cod_sgrupo=s1.cod_sgrupo left join subgrupo s2 on articulo.cod_grupov=s2.cod_sgrupo"
        cad4 = " inner join unidad on articulo.cod_uni=unidad.cod_uni"
        cad5 = " inner join tipo_articulo on articulo.cod_tart=tipo_articulo.cod_tart"
        cad6 = " left join area on articulo.cod_area=area.cod_area"
        cad7 = " where (s1.esProduccion or s2.esVenta) and articulo.cod_alma='" & cod_alma & "' order by nom_art"
        cad = cad1 + cad2 + cad3 + cad4 + cad5 + cad6 + cad7
        Dim com As New MySqlCommand(cad, dbConex)
        daProduccion.SelectCommand = com
        daProduccion.Fill(dsProduccion, "articulo")
        'configuramos el bindingSource
        bsProduccion.DataSource = dsProduccion
        bsProduccion.DataMember = "articulo"

    End Sub
    Sub estructuraProduccion()
        With dataProduccion
            .DataSource = bsProduccion
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("cod_art").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("cod_art").HeaderText = "Código"
            .Columns("cod_art").Width = 50
            .Columns("cod_art").ReadOnly = True
            .Columns("nom_art").HeaderText = "Descripción"
            .Columns("nom_art").Width = 250
            .Columns("nom_art").ReadOnly = True
            .Columns("nom_uni").HeaderText = "Unidad"
            .Columns("nom_uni").Width = 60
            .Columns("nom_uni").ReadOnly = True
            .Columns("pre_costo").HeaderText = "Precio Costo"
            .Columns("pre_costo").Width = 65
            .Columns("pre_costo").ReadOnly = True
            .Columns("pre_costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("pre_venta").HeaderText = "Precio Venta"
            .Columns("pre_venta").Width = 65
            .Columns("pre_venta").ReadOnly = True
            .Columns("pre_venta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("activo").Width = 30
            .Columns("activo").HeaderText = "Act."
            .Columns("activo").ReadOnly = True
            .Columns("cod_uni").Visible = False
            .Columns("cod_area").Visible = False
            .Columns("nom_area").Visible = False
            .Columns("cod_sgrupo").Visible = False
            .Columns("cod_grupov").Visible = False
            .Columns("cod_tart").Visible = False
            .Columns("maximo").Visible = False
            .Columns("minimo").Visible = False
            .Columns("pre_inc_imp").Visible = False
            .Columns("afecto_igv").Visible = False
            .Columns("factor_prod").Visible = False
            .Columns("nom_tart").Visible = False
            .Columns("nom_sgrupo").Visible = False
            .Columns("nom_GrupoV").Visible = False
            .Columns("esProduccion").Visible = False
            .Columns("es_pcosto").Visible = False
            .Columns("cod_artext").Visible = False
            .Columns("cod_artext1").Visible = False
            .Columns("cod_artext2").Visible = False
            .Columns("cod_artext3").Visible = False
            .Columns("nom_art2").Visible = False
            .Columns("b_ancho").Visible = False
            .Columns("b_alto").Visible = False

        End With
        'mostramos el numero de registros procesados
        lblRegistros.Text = "Nº de Registros... " & dataProduccion.RowCount
    End Sub
    Private Sub cboAlmacen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAlmacen.SelectedIndexChanged
        If Not estaCargando Then
            dsReceta.Clear()
            dsInsumo.Clear()
            cargaProduccion(cboAlmacen.SelectedValue)
            muestraArea(cboAlmacen.SelectedValue)

        End If
    End Sub

    Private Sub txtBuscarProduccion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscarProduccion.KeyPress
        If e.KeyChar = ChrW(13) Then
            dataProduccion.Focus()
        End If
    End Sub
    Private Sub txtBuscarProduccion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscarProduccion.TextChanged
        If Not estaCargando And Len(txtBuscarProduccion.Text) > 2 Then
            bsProduccion.Filter = "nom_art like '" & txtBuscarProduccion.Text & "%'"
        End If
        
    End Sub
    Private Sub txtCodigo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.Enter
        If txtCodigo.ReadOnly = True Then
            dataProduccion.Focus()
        End If
    End Sub
    Private Sub txtCodigo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.LostFocus
        If Len(txtCodigo.Text) > 0 Then
            txtCodigo.Text = Microsoft.VisualBasic.Right("000000" + Trim(txtCodigo.Text), 6)
        End If
    End Sub
    Private Sub txtProduccion_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProduccion.Enter
        If txtProduccion.ReadOnly = True Then
            dataProduccion.Focus()
        End If
    End Sub
    Private Sub txtPreVenta_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPreVenta.Enter
        If txtPreVenta.ReadOnly = True Then
            dataProduccion.Focus()
        End If
    End Sub
    Private Sub txtPreVenta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPreVenta.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) And Not (e.KeyChar.Equals(sepDecimal)) Then
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtPreCosto_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPreCosto.Enter
        If txtPreCosto.ReadOnly = True Then
            If dataProduccion.Enabled = True Then
                dataProduccion.Focus()
            End If
        End If
    End Sub
    Private Sub txtPreCosto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPreCosto.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) And Not (e.KeyChar.Equals(sepDecimal)) Then
            e.KeyChar = ""
        End If
    End Sub
    Private Sub cmdAñadir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAñadir.Click
        dsProduccion.Tables("articulo").Columns("cod_art").DefaultValue = ""
        dsProduccion.Tables("articulo").Columns("nom_art").DefaultValue = ""
        dsProduccion.Tables("articulo").Columns("pre_costo").DefaultValue = 0.0
        dsProduccion.Tables("articulo").Columns("pre_venta").DefaultValue = 0.0
        dsProduccion.Tables("articulo").Columns("factor_prod").DefaultValue = 1.0
        'dsProduccion.Tables("articulo").Columns("b_ancho").DefaultValue = 125
        'dsProduccion.Tables("articulo").Columns("b_alto").DefaultValue = 50
        dsProduccion.Tables("articulo").Columns("activo").DefaultValue = True
        dsProduccion.Tables("articulo").Columns("pre_inc_imp").DefaultValue = True
        dsProduccion.Tables("articulo").Columns("afecto_igv").DefaultValue = True
        dsProduccion.Tables("articulo").Columns("es_pcosto").DefaultValue = False
        cboUnidad.SelectedIndex = -1
        cboGrupo.SelectedIndex = -1
        cboTipo.SelectedIndex = -1
        bsProduccion.AddNew()
        dataProduccion.DataSource = bsProduccion
        modoGrabar()
        habilitaDetalle()
        deshabilitaCabecera()
        txtBuscarInsumo.Text = ""
        txtBuscarInsumo.Enabled = False
        dsInsumo.Clear()
        lcBoton = "Añadir"
        'mostramos el numero de registros procesados
        lblRegistros.Text = "Nº de Registros... " & dataProduccion.RowCount
        txtCodigo.Focus()
    End Sub
    Private Sub cmdGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.Click
        'Dim lancho As Integer = Val(txtancho.Text)
        'Dim lalto As Integer = Val(txtalto.Text)
        'Try
        If validaIngreso() Then
            Dim cad, color As String, result As Integer, cInicial As String = ""
            txtUnidad.Text = cboUnidad.Text
            txtTipo.Text = cboTipo.Text
            txtSGrupo.Text = cboGrupo.Text
            TxtArea.Text = cboArea.Text
            cInicial = Microsoft.VisualBasic.Left(txtProduccion.Text, 1)

            Dim Imag As Byte() = Imagen_Bytes(Me.pb_foto.Image)
            color = txtColor.Text
            If color.Length = 6 Then
                color = "&H" & color.Substring(4, 2) & color.Substring(2, 2) & color.Substring(0, 2)
            Else
                color = ""
            End If
            If lcBoton = "Añadir" Then
                cad = "Insert Into articulo(cod_art,cod_artext,cod_artext1,cod_artext2,cod_artext3,nom_art,nom_art2,cod_uni,cod_tart,cod_sgrupo,cod_grupov," & _
                    "pre_costo,pre_venta,pre_inc_imp,afecto_igv,factor_prod,cod_alma,cod_area,es_pcosto,activo,b_color,foto)" & _
                    "values('" & _
                txtCodigo.Text & "','" & TxtCodigoVta.Text & "','" & TxtCodigoVta1.Text & "','" & txtCodigoVta2.Text & "','" & txtCodigoVta3.Text & "','" & txtProduccion.Text & "','" & txtdsc_corta.Text & "','" & cboUnidad.SelectedValue & "','" & _
                cboTipo.SelectedValue & "','" & cboGrupo.SelectedValue & "','" & cboGventa.SelectedValue & "'," & txtPreCosto.Text & "," & _
                txtPreVenta.Text & "," & chkPreIncImp.CheckState & "," & chkPreAfecImp.CheckState & "," & TxtFactorProd.Text & ",'" & cboAlmacen.SelectedValue & "','" & cboArea.SelectedValue & "'," & chkpcosto.CheckState & "," & chkActivo.CheckState & ",'" & color & "',?imagen)"
            Else
                cad = "update articulo set cod_art='" & txtCodigo.Text & "'," & "nom_art='" & txtProduccion.Text & "'," & _
                "nom_art2='" & txtdsc_corta.Text & "',cod_uni='" & cboUnidad.SelectedValue & "'," & _
                "pre_costo=" & txtPreCosto.Text & ",pre_venta=" & txtPreVenta.Text & "," & _
                "cod_artext='" & TxtCodigoVta.Text & "',cod_artext1='" & TxtCodigoVta1.Text & "',cod_artext2='" & txtCodigoVta2.Text & "',cod_artext3='" & txtCodigoVta3.Text & "'," & _
                "pre_inc_imp=" & chkPreIncImp.CheckState & ",afecto_igv=" & chkPreAfecImp.CheckState & ",cod_area='" & cboArea.SelectedValue & "'," & _
                "cod_tart='" & cboTipo.SelectedValue & "',cod_sgrupo='" & cboGrupo.SelectedValue & "',cod_grupov='" & cboGventa.SelectedValue & "'," & _
                "factor_prod=" & TxtFactorProd.Text & ",es_pcosto=" & chkpcosto.CheckState & ",activo=" & chkActivo.CheckState & ",foto = ?imagen" & ",b_color='" & color & "'," & _
                "b_ancho=" & Val(txtancho.Text) & ",b_alto=" & Val(txtalto.Text) & _
                " where cod_art='" & lCodigo & "'"
            End If
            Dim com As New MySqlCommand(cad, dbConex)
            com.Parameters.AddWithValue("?imagen", Imag)
            result = com.ExecuteNonQuery()
            If result > 0 Then
                bsProduccion.EndEdit()
            Else
                bsProduccion.CancelEdit()
            End If
            cargaProduccion(cboAlmacen.SelectedValue)
            dataProduccion.Refresh()
            deshabilitaDetalle()
            modoAñadir()
            habilitaCabecera()
            cmdInsertar.Enabled = True
            txtBuscarInsumo.Enabled = True
            txtBuscarInsumo.Text = ""
            dataProduccion.Focus()
            'mostramos el numero de registros procesados
            lblRegistros.Text = "Nº de Registros... " & dataProduccion.RowCount
        End If
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            '    bsProduccion.CancelEdit()
            '    dataProduccion.Refresh()
            '    deshabilitaDetalle()
            '    habilitaCabecera()
            '    modoAñadir()
            '    dataProduccion.Focus()
            'End Try
    End Sub
    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        bsProduccion.CancelEdit()
        dataProduccion.Refresh()
        deshabilitaDetalle()
        habilitaCabecera()
        modoAñadir()
        If dataProduccion.Rows.Count > 0 Then
            dataProduccion.CurrentCell = dataProduccion(1, 0)
        End If
        txtBuscarInsumo.Text = ""
        txtBuscarInsumo.Enabled = True
        cmdInsertar.Enabled = True
        dataProduccion.Focus()
    End Sub
    Private Sub cmdEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditar.Click
        If bsProduccion.Count > 0 Then
            lcBoton = "Editar"
            lCodigo = txtCodigo.Text
            habilitaDetalle()
            deshabilitaCabecera()
            modoGrabar()
            dsInsumo.Clear()
            txtBuscarInsumo.Text = ""
            txtBuscarInsumo.Enabled = False
            cmdInsertar.Enabled = False
            txtCodigo.Focus()
        End If
    End Sub
    Private Sub cmdEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        If bsProduccion.Count > 0 Then
            Dim sql As String, result, resp As Integer, mCatalogo As New Catalogo, existe, existe2, existe3, existe4 As Boolean
            existe = mCatalogo.existeEnIngresos(txtCodigo.Text)
            If existe Then
                MessageBox.Show("La Producción Seleccionada Tiene Ingresos" + Chr(13) + "NO es posible eliminarlo...", "SGA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Else
                existe2 = mCatalogo.existeEnIngresosHistorial(txtCodigo.Text)
                If existe2 Then
                    MessageBox.Show("La Producción Tiene Movimientos en el Historial" + Chr(13) + "NO es posible eliminarlo...", "SGA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Else
                    existe3 = mCatalogo.existeEnSalidas(txtCodigo.Text)
                    If existe3 Then
                        MessageBox.Show("La Producción Seleccionada Tiene Salidas" + Chr(13) + "NO es posible eliminarlo...", "SGA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Else
                        existe4 = mCatalogo.recetaTieneInsumos(txtCodigo.Text)
                        If existe4 Then
                            MessageBox.Show("Primero debe Eliminar los Insumos" + Chr(13) + "NO es posible eliminarlo...", "SGA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        Else
                            resp = MessageBox.Show("¿Esta Seguro de Eliminar la Producción Seleccionada...", "SGA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            If resp = 6 Then
                                Try
                                    sql = "delete from articulo where cod_art='" & txtCodigo.Text & "'"
                                    Dim com As New MySqlCommand(sql, dbConex)
                                    result = com.ExecuteNonQuery()
                                    If result > 0 Then
                                        bsProduccion.RemoveCurrent()
                                        dataProduccion.Focus()
                                    End If
                                    lblRegistros.Text = "Nº de Registros... " & dataProduccion.RowCount
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message)
                                End Try
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    'convertir binario a imágen
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
            Imagen.Save(Bin, Imaging.ImageFormat.Jpeg)
            'retorna binario
            Return Bin.GetBuffer
        Else
            Return Nothing
        End If
    End Function
    Function validaIngreso()
        Dim validado As Boolean = False
        If Len(txtCodigo.Text) > 0 Then
            If Len(txtProduccion.Text) > 0 Then
                If cboUnidad.SelectedIndex >= 0 Then
                    If cboTipo.SelectedIndex >= 0 Then
                        If cboGrupo.SelectedIndex >= 0 Then
                            validado = True
                        Else
                            cboGrupo.Focus()
                        End If
                    Else
                        cboTipo.Focus()
                    End If
                Else
                    cboUnidad.Focus()
                End If
            Else
                txtProduccion.Focus()
            End If
        Else
            txtCodigo.Focus()
        End If
        Return validado
    End Function
    Sub habilitaCabecera()
        cboAlmacen.Enabled = True
        txtBuscarProduccion.Enabled = True
        dataProduccion.Enabled = True
    End Sub
    Sub deshabilitaCabecera()
        cboAlmacen.Enabled = False
        txtBuscarProduccion.Enabled = False
        dataProduccion.Enabled = False
    End Sub
    Sub habilitaDetalle()
        txtCodigo.BackColor = Color.White
        txtCodigo.ReadOnly = False
        txtProduccion.BackColor = Color.White
        txtProduccion.ReadOnly = False
        txtdsc_corta.BackColor = Color.White
        txtdsc_corta.ReadOnly = False
        txtPreVenta.BackColor = Color.White
        txtPreVenta.ReadOnly = False
        TxtFactorProd.BackColor = Color.White
        TxtFactorProd.ReadOnly = False
        TxtCodigoVta.ReadOnly = False
        TxtCodigoVta1.ReadOnly = False
        txtCodigoVta2.ReadOnly = False
        txtCodigoVta3.ReadOnly = False
        cboUnidad.Enabled = True
        cboArea.Enabled = True
        cboGrupo.Enabled = True
        cboGventa.Enabled = True
        cboTipo.Enabled = True
        chkPreIncImp.Enabled = True
        chkPreAfecImp.Enabled = True
        chkpcosto.Enabled = True
        chkActivo.Enabled = True
        cboArea.Visible = True
        cboUnidad.Visible = True
        cboTipo.Visible = True
        cboGrupo.Visible = True
        cboGventa.Visible = True
        txtUnidad.Visible = False
        TxtArea.Visible = False
        txtTipo.Visible = False
        txtSGrupo.Visible = False
        txtGventa.Visible = False
    End Sub
    Sub deshabilitaDetalle()
        txtCodigo.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        txtCodigo.ReadOnly = True
        txtProduccion.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        txtProduccion.ReadOnly = True
        txtdsc_corta.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        txtdsc_corta.ReadOnly = True
        txtPreVenta.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        txtPreVenta.ReadOnly = True
        TxtFactorProd.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        TxtFactorProd.ReadOnly = True
        TxtCodigoVta.ReadOnly = True
        TxtCodigoVta1.ReadOnly = True
        txtCodigoVta2.ReadOnly = True
        txtCodigoVta3.ReadOnly = True
        cboUnidad.Enabled = False
        cboGrupo.Enabled = False
        cboGventa.Enabled = False
        cboTipo.Enabled = False
        cboArea.Enabled = False
        chkPreIncImp.Enabled = False
        chkPreAfecImp.Enabled = False
        chkActivo.Enabled = False
        chkpcosto.Enabled = False
        cboUnidad.Visible = False
        cboArea.Visible = False
        cboTipo.Visible = False
        cboGrupo.Visible = False
        cboGventa.Visible = False
        txtUnidad.Visible = True
        TxtArea.Visible = True
        txtTipo.Visible = True
        txtSGrupo.Visible = True
        txtGventa.Visible = True
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

    Private Sub dataProduccion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dataProduccion.Click
        Dim cod_art As String
        If Not estaCargando Then
            lblReceta.Text = "RECETA:"
            If dataProduccion.RowCount > 0 Then
                lblReceta.Text = "RECETA: " & dataProduccion("nom_art", dataProduccion.CurrentRow.Index).Value
                cod_art = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
                'muestraReceta(cod_art)
                calcularcostoreceta(cod_art)
            End If
        End If
    End Sub

    Private Sub dataProduccion_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dataProduccion.ColumnHeaderMouseClick
        coloreaProductoTerminado()
    End Sub

    Private Sub dataProduccion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dataProduccion.GotFocus
        'coloreaProductoTerminado()
    End Sub
    Private Sub dataProduccion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dataProduccion.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtBuscarInsumo.Focus()
        End If
   
        If (e.Control And e.KeyCode = Keys.E) Then
            If dataProduccion.RowCount > 0 Then
                EnviaraExcel(dataProduccion)
            End If
        End If

    End Sub
    Private Sub dataProduccion_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dataProduccion.SelectionChanged
        Dim cod_art As String
        If Not estaCargando Then
            lblReceta.Text = "RECETA:"
            If dataProduccion.RowCount > 0 Then
                lblReceta.Text = "RECETA: " & dataProduccion("nom_art", dataProduccion.CurrentRow.Index).Value
                cod_art = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
                muestraReceta(cod_art)
                MuestraCombo(cod_art)
                MuestraFoto(cod_art)
                'calcularcostoreceta(cod_art)
            End If
        End If
    End Sub
    Private Sub txtBuscarInsumo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscarInsumo.GotFocus
        If Not estaCargando Then
            If dataProduccion.RowCount > 0 Then
                txtBuscarInsumo.SelectAll()
                dsInsumo.Clear()
                If Len(txtBuscarInsumo.Text) > 0 Then
                    Dim mCatalogo As New Catalogo
                    dsInsumo = mCatalogo.recuperaProducto(pCatalogoXalmacen, cboAlmacen.SelectedValue, txtBuscarInsumo.Text)
                    dataInsumo.DataSource = dsInsumo.Tables("articulo").DefaultView
                    cargaEstructuraInsumos()
                    coloreaProducciones()
                End If
            End If
        End If
    End Sub
    Private Sub txtBuscarInsumo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscarInsumo.TextChanged
        If Not estaCargando Then
            Dim valor As String = txtBuscarInsumo.Text
            dsInsumo.Clear()
            Dim mCatalogo As New Catalogo
            Dim mAlmacen As New Almacen
            Dim cCatalogoDestino As String
            cCatalogoDestino = mAlmacen.devuelveAlmacenCatalogo(cboAlmacen.SelectedValue)

            dsInsumo = mCatalogo.recuperaProducto(IIf(mAlmacen.devuelveEsEvento(cboAlmacen.SelectedValue) = True, False, True), cboAlmacen.SelectedValue, txtBuscarInsumo.Text)
            dataInsumo.DataSource = dsInsumo.Tables("articulo").DefaultView
            cargaEstructuraInsumos()
            coloreaProducciones()

            If dataInsumo.RowCount > 0 Then
                dataInsumo.CurrentCell = dataInsumo("nom_art", dataInsumo.CurrentRow.Index)
            End If
        End If
    End Sub
    Sub cargaEstructuraInsumos()
        With dataInsumo
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("cod_art").HeaderText = "Codigo"
            .Columns("cod_art").Width = 50
            .Columns("cod_art").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("nom_art").HeaderText = "Descripcion"
            .Columns("nom_art").Width = 239
            .Columns("nom_uni").HeaderText = "Unidad"
            .Columns("nom_uni").Width = 60
            .Columns("pre_costo").HeaderText = "Costo"
            .Columns("pre_costo").Width = 60
            .Columns("pre_costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cod_uni").Visible = False
            .Columns("cant_uni").Visible = False
            .Columns("cant_uni").Visible = False
            .Columns("pre_prom").Visible = False
            .Columns("pre_ult").Visible = False
            .Columns("pre_venta").Visible = False
            .Columns("cuenta_v").Visible = False
            .Columns("cuenta_c").Visible = False
            .Columns("maximo").Visible = False
            .Columns("minimo").Visible = False
            .Columns("afecto_igv").Visible = False
            .Columns("cod_sgrupo").Visible = False
            .Columns("esProduccion").Visible = False
        End With
    End Sub
    Sub coloreaProducciones()
        Dim I As Integer = 0
        For I = 0 To dataInsumo.RowCount - 1
            If Not IsDBNull(dataInsumo("cod_art", I).Value) Then
                If dataInsumo("esProduccion", I).Value = True Then
                    dataInsumo("cod_art", I).Style.ForeColor = Color.DarkBlue
                    dataInsumo("nom_art", I).Style.ForeColor = Color.DarkBlue
                    dataInsumo("nom_uni", I).Style.ForeColor = Color.DarkBlue
                    dataInsumo("pre_costo", I).Style.ForeColor = Color.DarkBlue
                End If
            End If
        Next
    End Sub
    Sub coloreaProductoTerminado()
        For Each row As DataGridViewRow In dataProduccion.Rows
            If Not IsDBNull(row.Cells("cod_art").Value) Or Not IsDBNull(row.Cells("esProduccion").Value) Then
                If row.Cells("esProduccion").Value = False Then
                    row.DefaultCellStyle.ForeColor = Color.Purple
                Else
                    row.DefaultCellStyle.ForeColor = Color.Black
                End If
            End If
        Next
    End Sub

    Private Sub dataInsumo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dataInsumo.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtCantidad.Focus()
        End If
    End Sub
    Private Sub dataInsumo_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dataInsumo.SelectionChanged
        If Not estaCargando Then
            txtCantidad.Text = ""
            txtCosto.Text = 0
            lblUnidad.Text = ""
            If dataInsumo.RowCount > 0 Then
                lblUnidad.Text = LCase(dataInsumo("nom_uni", dataInsumo.CurrentRow.Index).Value)
                If dataInsumo("esProduccion", dataInsumo.CurrentRow.Index).Value = True Then
                    lblCantidad.Text = "Cantidad Sub Receta"
                Else
                    lblCantidad.Text = "Cantidad del Insumo"
                End If
            End If
        End If
    End Sub
    Private Sub txtCantidad_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCantidad.Enter
        If dataProduccion.RowCount > 0 Then
            If dataInsumo.RowCount > 0 Then
                Dim lProduccion As String = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
                Dim lInsumo As String = dataInsumo("cod_art", dataInsumo.CurrentRow.Index).Value
                If lProduccion = lInsumo Then
                    txtBuscarInsumo.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not (e.KeyChar = ChrW(Keys.Back)) And Not (e.KeyChar.Equals(sepDecimal)) Then
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtCantidad_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCantidad.LostFocus
        If tabCombo.IsSelected Then
            txtCosto.Text = 0
        Else
            If dataInsumo.RowCount > 0 Then
                If Len(txtCantidad.Text) > 0 Then
                    Dim lcantidad = txtCantidad.Text
                    Dim lcosto = dataInsumo("pre_costo", dataInsumo.CurrentRow.Index).Value
                    Dim lcant_uni = dataInsumo("cant_uni", dataInsumo.CurrentRow.Index).Value
                    txtCosto.Text = Round(lcosto * txtCantidad.Text, pDecimales)
                Else
                    txtCantidad.Text = 0
                End If
            End If
        End If
        
    End Sub
    Private Sub txtCosto_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCosto.Enter
        If txtCosto.ReadOnly = True Then
            cmdInsertar.Focus()
        End If
    End Sub
    Private Sub cmdInsertar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsertar.Click
        If dataProduccion.RowCount > 0 Then
            Dim mReceta As New Receta
            Dim cod_rec = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
            Dim cod_art = dataInsumo("cod_art", dataInsumo.CurrentRow.Index).Value
            Dim costo As Decimal
            If tabreceta.IsSelected Then
                If mReceta.existeInsumoEnReceta(cod_rec, cod_art) Then
                    MessageBox.Show("El Insumo ya Esta Registrado en la Receta...", "CEFE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If cod_rec <> cod_art Then
                        If Val(txtCantidad.Text) > 0 Then
                            mReceta.insertar(cod_rec, cod_art, txtCantidad.Text, txtCosto.Text)
                            muestraReceta(dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value)
                            costo = mReceta.devuelveCostoReceta(cod_rec)
                            mReceta.actualizaCostoReceta(cod_rec, costo)
                            txtPreCosto.Text = costo
                            bsProduccion.EndEdit()
                            dataProduccion.Refresh()
                            txtBuscarInsumo.Focus()
                        Else
                            MessageBox.Show("Ingrese la Cantidad a Registrar...", "CEFE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtCantidad.Focus()
                        End If
                    Else
                        MessageBox.Show("Una Receta NO puede ser Insumo de su Propia Receta...", "CEFE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                If DataCombo.RowCount > 0 Then
                    Try
                        DataCombo.Rows(0).Selected = True
                        'DataCombo.CurrentCell = DataCombo.Rows(1).Cells(0)
                        Dim cod_combo As String = DataCombo("cod_combo", DataCombo.CurrentRow.Index).Value
                        Dim precio As Decimal = txtCosto.Text
                        Dim cant As Decimal = txtCantidad.Text
                        If cod_combo <> cod_art Then
                            If Val(txtCantidad.Text) > 0 Then
                                mReceta.insertarDetCombo(cod_combo, cod_art, cant, precio)
                                MuestraCombo(dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value)
                            Else
                                MessageBox.Show("Ingrese la Cantidad a Registrar...", "CEFE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtCantidad.Focus()
                            End If
                        Else
                            MessageBox.Show("Un Combo NO puede ser Insumo del mismo Combo...", "CEFE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Catch ex As Exception
                        'MessageBox.Show(ex.Message)
                    Finally
                        DataCombo.Rows(0).Selected = True
                        DataCombo.CurrentCell = DataCombo.Rows(0).Cells(0)
                    End Try
                End If
            End If
        End If
    End Sub

    Sub calcularcostoreceta(ByVal cod_art As String)
        Dim table As DataTable, sumObject As Object, mPrecio As New ePrecio, total As Decimal, factor As Decimal
        factor = TxtFactorProd.Text
        table = dsReceta.Tables("receta")
        sumObject = table.Compute("Sum(total)", "")
        total = CType(IIf(IsDBNull(sumObject), 0, sumObject), Double) / IIf(factor = 0, 1, factor)
        total = CStr(Round(CDbl(total), 3, MidpointRounding.ToEven))
        dataProduccion.Item("pre_costo", dataProduccion.CurrentRow.Index).Value = total
        mPrecio.actualizaCostoArticulo(cod_art, total)
    End Sub
    Sub cargaEstructuraReceta()
        With dataReceta
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("cod_art").HeaderText = "Codigo"
            .Columns("cod_art").Width = 50
            .Columns("cod_art").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("nom_art").HeaderText = "Descripcion"
            .Columns("nom_art").Width = 190
            .Columns("nom_uni").HeaderText = "Unidad"
            .Columns("nom_uni").Width = 50
            .Columns("cant").HeaderText = "Cant."
            .Columns("cant").Width = 45
            .Columns("cant").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("cant").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("pre_costo").HeaderText = "Costo"
            .Columns("pre_costo").Width = 60
            .Columns("pre_costo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("total").HeaderText = "Monto"
            .Columns("total").Width = 60
            .Columns("total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("total").DefaultCellStyle.Format = "N" & pDecimales
            .Columns("cod_rec").Visible = False
            .Columns("cant_uni").Visible = False
            .Columns("costo").Visible = False
        End With
    End Sub
    Private Sub dataReceta_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'eliminamos el item seleccionado
        If dataProduccion.RowCount > 0 Then
            Dim mReceta As New Receta, costo As Decimal
            Dim cod_rec = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
            Dim rpta As Integer
            rpta = MessageBox.Show("Esta Seguro de Quitar el Insumo de la Receta...", "SGA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If rpta = 6 Then
                eliminaInsumoDeReceta()
                costo = mReceta.devuelveCostoReceta(cod_rec)
                mReceta.actualizaCostoReceta(cod_rec, costo)
                txtPreCosto.Text = costo
                bsProduccion.EndEdit()
                dataProduccion.Refresh()
            End If
        End If
    End Sub
    Private Sub dataReceta_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'eliminamos el insumo de la receta
        If dataProduccion.RowCount > 0 Then
            Dim mReceta As New Receta, costo As Decimal
            If e.KeyCode = Keys.Delete Then
                Dim cod_rec = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value

                Dim rpta As Integer
                rpta = MessageBox.Show("Esta Seguro de Quitar el Insumo de la Receta...", "SGA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If rpta = 6 Then
                    e.SuppressKeyPress = True
                    eliminaInsumoDeReceta()
                    costo = mReceta.devuelveCostoReceta(cod_rec)
                    mReceta.actualizaCostoReceta(cod_rec, costo)
                    txtPreCosto.Text = costo
                    bsProduccion.EndEdit()
                    dataProduccion.Refresh()
                End If

            End If
        End If
    End Sub
    Sub eliminaInsumoDeReceta()
        If dataReceta.RowCount > 0 Then
            Dim cod_art As String = dataReceta("cod_art", dataReceta.CurrentRow.Index).Value
            Dim cod_rec As String = dataReceta("cod_rec", dataReceta.CurrentRow.Index).Value
            Dim mReceta As New Receta
            mReceta.eliminaInsumo(cod_rec, cod_art)
        End If
        Dim mfila As DataRow = dsReceta.Tables("receta").Rows(dataReceta.CurrentRow.Index)
        dsReceta.Tables("receta").Rows.Remove(mfila)
        txtBuscarInsumo.Focus()
        'End If
    End Sub

    Private Sub cmdImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimir.Click
        'If dataProduccion.RowCount > 0 Then
        '    Dim frm As New rptForm
        '    frm.cargarReceta(dsReceta, dataProduccion("nom_art", dataProduccion.CurrentRow.Index).Value, cboAlmacen.Text, txtUnidad.Text, True)
        '    frm.MdiParent = principal
        '    frm.Show()
        'End If
    End Sub

    Sub muestraArea(ByVal cod_alma As String)
        dsArea.Clear()
        Dim daArea As New MySqlDataAdapter
        Dim comArea As New MySqlCommand("select cod_area,nom_area from area where activo=1" _
                        & " and (destinoTrans) and cod_alma='" & cod_alma & "' order by nom_area", dbConex)
        daArea.SelectCommand = comArea
        daArea.Fill(dsArea, "area")
        With cboArea
            .DataSource = dsArea.Tables("area")
            .DisplayMember = dsArea.Tables("area").Columns("nom_area").ToString
            .ValueMember = dsArea.Tables("area").Columns("cod_area").ToString
           
        End With
    End Sub

    Sub MuestraFoto(ByVal carticulo As String)
        Try
            Dim cad, color As String, dr As MySqlDataReader, resul As Integer
            cad = "select isnull(foto) as vnul,foto,b_color from articulo where cod_art='" & carticulo & "'"
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
                color = dr("b_color")
                If Color.Length > 0 Then
                    txtColor.Text = color.Substring(6, 2) & color.Substring(4, 2) & color.Substring(2, 2)
                Else
                    txtColor.Text = ""
                End If

            End While
            dr.Close()
            dr = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dataReceta_CellDoubleClick1(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataReceta.CellDoubleClick
        'eliminamos el item seleccionado
        If dataProduccion.RowCount > 0 Then
            Dim mReceta As New Receta, costo As Decimal
            Dim cod_rec = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
            Dim rpta As Integer
            rpta = MessageBox.Show("Esta Seguro de Quitar el Insumo de la Receta...", "CEFE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If rpta = 6 Then
                eliminaInsumoDeReceta()
                costo = mReceta.devuelveCostoReceta(cod_rec)
                mReceta.actualizaCostoReceta(cod_rec, costo)
                txtPreCosto.Text = costo
                bsProduccion.EndEdit()
                dataProduccion.Refresh()
            End If
        End If
    End Sub

    Private Sub dataReceta_KeyDown1(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dataReceta.KeyDown
        'eliminamos el insumo de la receta
        If dataProduccion.RowCount > 0 Then
            Dim mReceta As New Receta, costo As Decimal
            If e.KeyCode = Keys.Delete Then
                Dim cod_rec = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value

                Dim rpta As Integer
                rpta = MessageBox.Show("Esta Seguro de Quitar el Insumo de la Receta...", "CEFE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If rpta = 6 Then
                    e.SuppressKeyPress = True
                    eliminaInsumoDeReceta()
                    costo = mReceta.devuelveCostoReceta(cod_rec)
                    mReceta.actualizaCostoReceta(cod_rec, costo)
                    txtPreCosto.Text = costo
                    bsProduccion.EndEdit()
                    dataProduccion.Refresh()
                End If

            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles cmdInsertarcombo.Click
        If dataProduccion.RowCount > 0 Then
            Dim mReceta As New Receta
            Dim cod_art = dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value
            Dim cod_combo As String = mReceta.maxCodCombo()
            Dim dsc_combo As String = txtcombo.Text
            Dim max_combo As Integer = txtmaxCombo.Text
            mReceta.insertarCombo(cod_combo, dsc_combo, max_combo, cod_art)
            MuestraCombo(dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value)
        End If
    End Sub


    Private Sub tvCombo_DoubleClick(sender As Object, e As System.EventArgs) Handles tvCombo.DoubleClick
        'eliminamos el item seleccionado
        If DataCombo.RowCount > 0 Then
            Try
                Dim mReceta As New Receta
                Dim rpta As Integer
                rpta = MessageBox.Show("Esta Seguro de Quitar el Item del Combo...", "CEFE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If rpta = 6 Then
                    DataCombo.Rows(1).Selected = True
                    Dim cod_art As String = DataCombo("cod_art", DataCombo.CurrentRow.Index).Value
                    Dim cod_combo As String = DataCombo("cod_combo", DataCombo.CurrentRow.Index).Value
                    mReceta.eliminaDetCombo(cod_combo, cod_art)
                    MuestraCombo(dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value)
                End If
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            Finally
                DataCombo.Rows(0).Selected = True
                DataCombo.CurrentCell = DataCombo.Rows(0).Cells(0)
            End Try
        End If
    End Sub

    Private Sub cmdEliminaCombo_Click(sender As System.Object, e As System.EventArgs) Handles cmdEliminaCombo.Click
        If DataCombo.RowCount > 0 Then
            Try
                'eliminamos el item seleccionado
                Dim mReceta As New Receta
                Dim rpta As Integer
                rpta = MessageBox.Show("Esta Seguro de Quitar el Combo...", "CEFE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If rpta = 6 Then
                    DataCombo.Rows(0).Selected = True
                    Dim cod_combo As String = DataCombo("cod_combo", DataCombo.CurrentRow.Index).Value
                    mReceta.eliminaCombo(cod_combo)
                    MuestraCombo(dataProduccion("cod_art", dataProduccion.CurrentRow.Index).Value)
                End If

            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            Finally
                DataCombo.Rows(0).Selected = True
                DataCombo.CurrentCell = DataCombo.Rows(0).Cells(0)
            End Try
        End If
    End Sub


    Private Sub cmdExaminar_Click(sender As System.Object, e As System.EventArgs) Handles cmdExaminar.Click
        Dim oFD As New OpenFileDialog, mFile As String
        With oFD
            .Title = "Seleccionar fichero"
            .Multiselect = False
            '.Filter = "Ficheros de texto (*.txt;*.ini)|*.txt;*.ini" & _
            '          "|Todos los ficheros (*.*)|*.*"
            .Filter = "Archivo de Imagen (*.jpg)|*.jpg"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                mFile = .FileName
                Me.pb_foto.Image = Image.FromFile(mFile)
            Else
                mFile = ""
            End If

        End With
    End Sub

    Private Sub SimpleButton1_Click_1(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Dim vImagen As System.Drawing.Image
        vImagen = Clipboard.GetImage
        pb_foto.Image = vImagen
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mcatalogo As New Catalogo
        txtCodigo.Text = mcatalogo.maxCodigo()
    End Sub

    Private Sub lnkColor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkColor.LinkClicked
        System.Diagnostics.Process.Start("http://html-color-codes.info/codigos-de-colores-hexadecimales/")
    End Sub

    Private Sub dataProduccion_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataProduccion.CellContentClick

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        bsProduccion.Filter = "nom_art like '%' "
    End Sub

    Private Sub txtPreCosto_TextChanged(sender As Object, e As EventArgs) Handles txtPreCosto.TextChanged

    End Sub
End Class
