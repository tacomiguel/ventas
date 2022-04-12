<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class c_ventas
    Inherits sgv_ventas.base

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(c_ventas))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.chkGrupo = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.grupo = New System.Windows.Forms.GroupBox()
        Me.cboGrupo = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optAtencion = New System.Windows.Forms.RadioButton()
        Me.optGrupoVenta = New System.Windows.Forms.RadioButton()
        Me.optFomasPago = New System.Windows.Forms.RadioButton()
        Me.optVentasProducto = New System.Windows.Forms.RadioButton()
        Me.optRegistro = New System.Windows.Forms.RadioButton()
        Me.lblRegistros = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmdCerrar = New DevComponents.DotNetBar.ButtonX()
        Me.cmdImprimir = New DevComponents.DotNetBar.ButtonX()
        Me.TabControl2 = New DevComponents.DotNetBar.TabControl()
        Me.TabControlPanel4 = New DevComponents.DotNetBar.TabControlPanel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkIMP = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblMontoDS = New DevComponents.DotNetBar.LabelX()
        Me.lblMonedaDS = New DevComponents.DotNetBar.LabelX()
        Me.dataDetalle = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.tabSaldos = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel1 = New DevComponents.DotNetBar.TabControlPanel()
        Me.DataSalidaPago = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataSalidaDetalle = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataSALIDA = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.TabItem1 = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel5 = New DevComponents.DotNetBar.TabControlPanel()
        Me.cboAlmacenR = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.lblMensaje = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optDescripcion = New System.Windows.Forms.RadioButton()
        Me.optCodigo = New System.Windows.Forms.RadioButton()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txtBuscar = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.barraProgreso = New System.Windows.Forms.ProgressBar()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.optValorizado = New System.Windows.Forms.RadioButton()
        Me.optCantidades = New System.Windows.Forms.RadioButton()
        Me.dataResumen = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.tabResumen = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.mcDesde = New DevComponents.Editors.DateTimeAdv.MonthCalendarAdv()
        Me.mcHasta = New DevComponents.Editors.DateTimeAdv.MonthCalendarAdv()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdProcesar = New DevComponents.DotNetBar.ButtonX()
        Me.grupo.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.TabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl2.SuspendLayout()
        Me.TabControlPanel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dataDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlPanel1.SuspendLayout()
        CType(Me.DataSalidaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSalidaDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSALIDA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlPanel5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.dataResumen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkGrupo
        '
        Me.chkGrupo.AutoSize = True
        '
        '
        '
        Me.chkGrupo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGrupo.Enabled = False
        Me.chkGrupo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGrupo.Location = New System.Drawing.Point(411, 6)
        Me.chkGrupo.Name = "chkGrupo"
        Me.chkGrupo.Size = New System.Drawing.Size(28, 15)
        Me.chkGrupo.TabIndex = 154
        Me.chkGrupo.Text = "x"
        Me.chkGrupo.Visible = False
        '
        'grupo
        '
        Me.grupo.Controls.Add(Me.cboGrupo)
        Me.grupo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grupo.ForeColor = System.Drawing.Color.Maroon
        Me.grupo.Location = New System.Drawing.Point(407, 18)
        Me.grupo.Name = "grupo"
        Me.grupo.Size = New System.Drawing.Size(288, 42)
        Me.grupo.TabIndex = 150
        Me.grupo.TabStop = False
        Me.grupo.Visible = False
        '
        'cboGrupo
        '
        Me.cboGrupo.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboGrupo.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboGrupo.DisplayMember = "Text"
        Me.cboGrupo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGrupo.Enabled = False
        Me.cboGrupo.FocusHighlightColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.cboGrupo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGrupo.ItemHeight = 15
        Me.cboGrupo.Location = New System.Drawing.Point(8, 14)
        Me.cboGrupo.Name = "cboGrupo"
        Me.cboGrupo.Size = New System.Drawing.Size(274, 21)
        Me.cboGrupo.TabIndex = 12
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optAtencion)
        Me.GroupBox2.Controls.Add(Me.optGrupoVenta)
        Me.GroupBox2.Controls.Add(Me.optFomasPago)
        Me.GroupBox2.Controls.Add(Me.optVentasProducto)
        Me.GroupBox2.Controls.Add(Me.optRegistro)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(470, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(235, 124)
        Me.GroupBox2.TabIndex = 149
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tipo de Reporte"
        '
        'optAtencion
        '
        Me.optAtencion.AutoSize = True
        Me.optAtencion.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAtencion.ForeColor = System.Drawing.Color.Black
        Me.optAtencion.Location = New System.Drawing.Point(8, 92)
        Me.optAtencion.Name = "optAtencion"
        Me.optAtencion.Size = New System.Drawing.Size(163, 20)
        Me.optAtencion.TabIndex = 6
        Me.optAtencion.Text = "Ventas x Atenciones"
        Me.optAtencion.UseVisualStyleBackColor = True
        '
        'optGrupoVenta
        '
        Me.optGrupoVenta.AutoSize = True
        Me.optGrupoVenta.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGrupoVenta.ForeColor = System.Drawing.Color.Black
        Me.optGrupoVenta.Location = New System.Drawing.Point(8, 72)
        Me.optGrupoVenta.Name = "optGrupoVenta"
        Me.optGrupoVenta.Size = New System.Drawing.Size(173, 20)
        Me.optGrupoVenta.TabIndex = 5
        Me.optGrupoVenta.Text = "Ventas x Grupo Venta"
        Me.optGrupoVenta.UseVisualStyleBackColor = True
        '
        'optFomasPago
        '
        Me.optFomasPago.AutoSize = True
        Me.optFomasPago.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFomasPago.ForeColor = System.Drawing.Color.Black
        Me.optFomasPago.Location = New System.Drawing.Point(8, 53)
        Me.optFomasPago.Name = "optFomasPago"
        Me.optFomasPago.Size = New System.Drawing.Size(168, 20)
        Me.optFomasPago.TabIndex = 4
        Me.optFomasPago.Text = "Ventas x Forma Pago"
        Me.optFomasPago.UseVisualStyleBackColor = True
        '
        'optVentasProducto
        '
        Me.optVentasProducto.AutoSize = True
        Me.optVentasProducto.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optVentasProducto.ForeColor = System.Drawing.Color.Black
        Me.optVentasProducto.Location = New System.Drawing.Point(8, 34)
        Me.optVentasProducto.Name = "optVentasProducto"
        Me.optVentasProducto.Size = New System.Drawing.Size(149, 20)
        Me.optVentasProducto.TabIndex = 2
        Me.optVentasProducto.Text = "Ventas x Producto"
        Me.optVentasProducto.UseVisualStyleBackColor = True
        '
        'optRegistro
        '
        Me.optRegistro.AutoSize = True
        Me.optRegistro.Checked = True
        Me.optRegistro.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRegistro.ForeColor = System.Drawing.Color.Black
        Me.optRegistro.Location = New System.Drawing.Point(8, 15)
        Me.optRegistro.Name = "optRegistro"
        Me.optRegistro.Size = New System.Drawing.Size(156, 20)
        Me.optRegistro.TabIndex = 0
        Me.optRegistro.TabStop = True
        Me.optRegistro.Text = "Registro de Ventas"
        Me.optRegistro.UseVisualStyleBackColor = True
        '
        'lblRegistros
        '
        '
        '
        '
        Me.lblRegistros.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblRegistros.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.ForeColor = System.Drawing.Color.Green
        Me.lblRegistros.Location = New System.Drawing.Point(789, 372)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(181, 19)
        Me.lblRegistros.TabIndex = 160
        Me.lblRegistros.Text = "Nº de Registros... "
        Me.lblRegistros.TextAlignment = System.Drawing.StringAlignment.Center
        Me.lblRegistros.WordWrap = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(-16, 159)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1059, 10)
        Me.GroupBox3.TabIndex = 155
        Me.GroupBox3.TabStop = False
        '
        'cmdCerrar
        '
        Me.cmdCerrar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.cmdCerrar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.cmdCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCerrar.Location = New System.Drawing.Point(887, 11)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(5)
        Me.cmdCerrar.Size = New System.Drawing.Size(100, 36)
        Me.cmdCerrar.TabIndex = 152
        Me.cmdCerrar.Text = "Cerrar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.cmdImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.cmdImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImprimir.Location = New System.Drawing.Point(887, 54)
        Me.cmdImprimir.Name = "cmdImprimir"
        Me.cmdImprimir.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(5)
        Me.cmdImprimir.Size = New System.Drawing.Size(101, 36)
        Me.cmdImprimir.TabIndex = 151
        Me.cmdImprimir.Text = "Imprimir"
        '
        'TabControl2
        '
        Me.TabControl2.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControl2.CanReorderTabs = False
        Me.TabControl2.ColorScheme.TabBackground = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControl2.ColorScheme.TabBackground2 = System.Drawing.Color.White
        Me.TabControl2.ColorScheme.TabItemBackground = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.TabControl2.ColorScheme.TabItemBackground2 = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControl2.Controls.Add(Me.TabControlPanel4)
        Me.TabControl2.Controls.Add(Me.TabControlPanel1)
        Me.TabControl2.Controls.Add(Me.TabControlPanel5)
        Me.TabControl2.Location = New System.Drawing.Point(0, 180)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedTabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TabControl2.SelectedTabIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(997, 420)
        Me.TabControl2.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005Document
        Me.TabControl2.TabIndex = 169
        Me.TabControl2.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox
        Me.TabControl2.Tabs.Add(Me.tabSaldos)
        Me.TabControl2.Tabs.Add(Me.tabResumen)
        Me.TabControl2.Tabs.Add(Me.TabItem1)
        Me.TabControl2.Text = "Precio de Costo Vs. Precio de Venta"
        '
        'TabControlPanel4
        '
        Me.TabControlPanel4.Controls.Add(Me.PictureBox1)
        Me.TabControlPanel4.Controls.Add(Me.chkIMP)
        Me.TabControlPanel4.Controls.Add(Me.lblMontoDS)
        Me.TabControlPanel4.Controls.Add(Me.lblMonedaDS)
        Me.TabControlPanel4.Controls.Add(Me.dataDetalle)
        Me.TabControlPanel4.Controls.Add(Me.lblRegistros)
        Me.TabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel4.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel4.Name = "TabControlPanel4"
        Me.TabControlPanel4.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel4.Size = New System.Drawing.Size(997, 394)
        Me.TabControlPanel4.Style.BackColor1.Color = System.Drawing.Color.White
        Me.TabControlPanel4.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel4.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.TabControlPanel4.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
            Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel4.Style.GradientAngle = 90
        Me.TabControlPanel4.TabIndex = 1
        Me.TabControlPanel4.TabItem = Me.tabSaldos
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(251, 372)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 136
        Me.PictureBox1.TabStop = False
        '
        'chkIMP
        '
        Me.chkIMP.AutoSize = True
        '
        '
        '
        Me.chkIMP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkIMP.Checked = True
        Me.chkIMP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIMP.CheckValue = "Y"
        Me.chkIMP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIMP.Location = New System.Drawing.Point(4, 369)
        Me.chkIMP.Name = "chkIMP"
        Me.chkIMP.Size = New System.Drawing.Size(135, 15)
        Me.chkIMP.TabIndex = 135
        Me.chkIMP.Text = "Precios CON Impuesto"
        Me.chkIMP.TextColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer))
        '
        'lblMontoDS
        '
        Me.lblMontoDS.AutoSize = True
        Me.lblMontoDS.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.lblMontoDS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMontoDS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoDS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblMontoDS.Location = New System.Drawing.Point(271, 372)
        Me.lblMontoDS.Name = "lblMontoDS"
        Me.lblMontoDS.Size = New System.Drawing.Size(42, 15)
        Me.lblMontoDS.TabIndex = 133
        Me.lblMontoDS.Text = "Monto..."
        Me.lblMontoDS.WordWrap = True
        '
        'lblMonedaDS
        '
        Me.lblMonedaDS.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.lblMonedaDS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMonedaDS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonedaDS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblMonedaDS.Location = New System.Drawing.Point(145, 372)
        Me.lblMonedaDS.Name = "lblMonedaDS"
        Me.lblMonedaDS.Size = New System.Drawing.Size(96, 15)
        Me.lblMonedaDS.TabIndex = 132
        Me.lblMonedaDS.Text = "Moneda"
        Me.lblMonedaDS.TextAlignment = System.Drawing.StringAlignment.Far
        Me.lblMonedaDS.WordWrap = True
        '
        'dataDetalle
        '
        Me.dataDetalle.AllowUserToAddRows = False
        Me.dataDetalle.AllowUserToDeleteRows = False
        Me.dataDetalle.AllowUserToResizeColumns = False
        Me.dataDetalle.AllowUserToResizeRows = False
        Me.dataDetalle.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dataDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dataDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dataDetalle.DefaultCellStyle = DataGridViewCellStyle1
        Me.dataDetalle.Dock = System.Windows.Forms.DockStyle.Top
        Me.dataDetalle.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dataDetalle.Location = New System.Drawing.Point(1, 1)
        Me.dataDetalle.Name = "dataDetalle"
        Me.dataDetalle.ReadOnly = True
        Me.dataDetalle.RowHeadersVisible = False
        Me.dataDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dataDetalle.SelectAllSignVisible = False
        Me.dataDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dataDetalle.ShowEditingIcon = False
        Me.dataDetalle.Size = New System.Drawing.Size(995, 362)
        Me.dataDetalle.TabIndex = 52
        '
        'tabSaldos
        '
        Me.tabSaldos.AttachedControl = Me.TabControlPanel4
        Me.tabSaldos.Icon = CType(resources.GetObject("tabSaldos.Icon"), System.Drawing.Icon)
        Me.tabSaldos.Name = "tabSaldos"
        Me.tabSaldos.Text = "Ventas"
        '
        'TabControlPanel1
        '
        Me.TabControlPanel1.Controls.Add(Me.DataSalidaPago)
        Me.TabControlPanel1.Controls.Add(Me.DataSalidaDetalle)
        Me.TabControlPanel1.Controls.Add(Me.DataSALIDA)
        Me.TabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel1.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel1.Name = "TabControlPanel1"
        Me.TabControlPanel1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel1.Size = New System.Drawing.Size(997, 394)
        Me.TabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.White
        Me.TabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.TabControlPanel1.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
            Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel1.Style.GradientAngle = 90
        Me.TabControlPanel1.TabIndex = 3
        Me.TabControlPanel1.TabItem = Me.TabItem1
        '
        'DataSalidaPago
        '
        Me.DataSalidaPago.AllowUserToAddRows = False
        Me.DataSalidaPago.AllowUserToDeleteRows = False
        Me.DataSalidaPago.AllowUserToResizeColumns = False
        Me.DataSalidaPago.AllowUserToResizeRows = False
        Me.DataSalidaPago.BackgroundColor = System.Drawing.Color.White
        Me.DataSalidaPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataSalidaPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataSalidaPago.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataSalidaPago.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataSalidaPago.Location = New System.Drawing.Point(3, 231)
        Me.DataSalidaPago.MultiSelect = False
        Me.DataSalidaPago.Name = "DataSalidaPago"
        Me.DataSalidaPago.ReadOnly = True
        Me.DataSalidaPago.RowHeadersVisible = False
        Me.DataSalidaPago.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataSalidaPago.SelectAllSignVisible = False
        Me.DataSalidaPago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataSalidaPago.ShowEditingIcon = False
        Me.DataSalidaPago.Size = New System.Drawing.Size(919, 92)
        Me.DataSalidaPago.TabIndex = 9
        '
        'DataSalidaDetalle
        '
        Me.DataSalidaDetalle.AllowUserToAddRows = False
        Me.DataSalidaDetalle.AllowUserToDeleteRows = False
        Me.DataSalidaDetalle.AllowUserToResizeColumns = False
        Me.DataSalidaDetalle.AllowUserToResizeRows = False
        Me.DataSalidaDetalle.BackgroundColor = System.Drawing.Color.White
        Me.DataSalidaDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataSalidaDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataSalidaDetalle.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataSalidaDetalle.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataSalidaDetalle.Location = New System.Drawing.Point(3, 119)
        Me.DataSalidaDetalle.MultiSelect = False
        Me.DataSalidaDetalle.Name = "DataSalidaDetalle"
        Me.DataSalidaDetalle.ReadOnly = True
        Me.DataSalidaDetalle.RowHeadersVisible = False
        Me.DataSalidaDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataSalidaDetalle.SelectAllSignVisible = False
        Me.DataSalidaDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataSalidaDetalle.ShowEditingIcon = False
        Me.DataSalidaDetalle.Size = New System.Drawing.Size(919, 92)
        Me.DataSalidaDetalle.TabIndex = 8
        '
        'DataSALIDA
        '
        Me.DataSALIDA.AllowUserToAddRows = False
        Me.DataSALIDA.AllowUserToDeleteRows = False
        Me.DataSALIDA.AllowUserToResizeColumns = False
        Me.DataSALIDA.AllowUserToResizeRows = False
        Me.DataSALIDA.BackgroundColor = System.Drawing.Color.White
        Me.DataSALIDA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataSALIDA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataSALIDA.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataSALIDA.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataSALIDA.Location = New System.Drawing.Point(1, 10)
        Me.DataSALIDA.MultiSelect = False
        Me.DataSALIDA.Name = "DataSALIDA"
        Me.DataSALIDA.ReadOnly = True
        Me.DataSALIDA.RowHeadersVisible = False
        Me.DataSALIDA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataSALIDA.SelectAllSignVisible = False
        Me.DataSALIDA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataSALIDA.ShowEditingIcon = False
        Me.DataSALIDA.Size = New System.Drawing.Size(919, 92)
        Me.DataSALIDA.TabIndex = 7
        '
        'TabItem1
        '
        Me.TabItem1.AttachedControl = Me.TabControlPanel1
        Me.TabItem1.Name = "TabItem1"
        Me.TabItem1.Text = "TabItem1"
        Me.TabItem1.Visible = False
        '
        'TabControlPanel5
        '
        Me.TabControlPanel5.Controls.Add(Me.cboAlmacenR)
        Me.TabControlPanel5.Controls.Add(Me.lblMensaje)
        Me.TabControlPanel5.Controls.Add(Me.GroupBox1)
        Me.TabControlPanel5.Controls.Add(Me.PictureBox2)
        Me.TabControlPanel5.Controls.Add(Me.txtBuscar)
        Me.TabControlPanel5.Controls.Add(Me.LabelX3)
        Me.TabControlPanel5.Controls.Add(Me.barraProgreso)
        Me.TabControlPanel5.Controls.Add(Me.GroupBox6)
        Me.TabControlPanel5.Controls.Add(Me.dataResumen)
        Me.TabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel5.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel5.Name = "TabControlPanel5"
        Me.TabControlPanel5.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel5.Size = New System.Drawing.Size(997, 394)
        Me.TabControlPanel5.Style.BackColor1.Color = System.Drawing.Color.White
        Me.TabControlPanel5.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel5.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.TabControlPanel5.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
            Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel5.Style.GradientAngle = 90
        Me.TabControlPanel5.TabIndex = 2
        Me.TabControlPanel5.TabItem = Me.tabResumen
        '
        'cboAlmacenR
        '
        Me.cboAlmacenR.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAlmacenR.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboAlmacenR.DisplayMember = "Text"
        Me.cboAlmacenR.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboAlmacenR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAlmacenR.Enabled = False
        Me.cboAlmacenR.FocusHighlightColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.cboAlmacenR.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAlmacenR.ItemHeight = 15
        Me.cboAlmacenR.Location = New System.Drawing.Point(6, 6)
        Me.cboAlmacenR.Name = "cboAlmacenR"
        Me.cboAlmacenR.Size = New System.Drawing.Size(184, 21)
        Me.cboAlmacenR.TabIndex = 149
        '
        'lblMensaje
        '
        Me.lblMensaje.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.lblMensaje.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMensaje.Font = New System.Drawing.Font("Trebuchet MS", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.Color.Maroon
        Me.lblMensaje.Location = New System.Drawing.Point(814, 5)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(220, 22)
        Me.lblMensaje.TabIndex = 148
        Me.lblMensaje.Text = "Mensaje"
        Me.lblMensaje.TextAlignment = System.Drawing.StringAlignment.Far
        Me.lblMensaje.Visible = False
        Me.lblMensaje.WordWrap = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.optDescripcion)
        Me.GroupBox1.Controls.Add(Me.optCodigo)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox1.Location = New System.Drawing.Point(461, -5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(169, 36)
        Me.GroupBox1.TabIndex = 144
        Me.GroupBox1.TabStop = False
        '
        'optDescripcion
        '
        Me.optDescripcion.AutoSize = True
        Me.optDescripcion.Checked = True
        Me.optDescripcion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDescripcion.ForeColor = System.Drawing.Color.Black
        Me.optDescripcion.Location = New System.Drawing.Point(75, 11)
        Me.optDescripcion.Name = "optDescripcion"
        Me.optDescripcion.Size = New System.Drawing.Size(90, 18)
        Me.optDescripcion.TabIndex = 1
        Me.optDescripcion.TabStop = True
        Me.optDescripcion.Text = "Descripción"
        Me.optDescripcion.UseVisualStyleBackColor = True
        '
        'optCodigo
        '
        Me.optCodigo.AutoSize = True
        Me.optCodigo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCodigo.ForeColor = System.Drawing.Color.Black
        Me.optCodigo.Location = New System.Drawing.Point(9, 11)
        Me.optCodigo.Name = "optCodigo"
        Me.optCodigo.Size = New System.Drawing.Size(64, 18)
        Me.optCodigo.TabIndex = 0
        Me.optCodigo.Text = "Código"
        Me.optCodigo.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Location = New System.Drawing.Point(783, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 146
        Me.PictureBox2.TabStop = False
        '
        'txtBuscar
        '
        '
        '
        '
        Me.txtBuscar.Border.Class = "TextBoxBorder"
        Me.txtBuscar.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtBuscar.FocusHighlightEnabled = True
        Me.txtBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuscar.Location = New System.Drawing.Point(633, 5)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(147, 21)
        Me.txtBuscar.TabIndex = 145
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.Maroon
        Me.LabelX3.Location = New System.Drawing.Point(407, 7)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(51, 16)
        Me.LabelX3.TabIndex = 147
        Me.LabelX3.Text = "Buscar x"
        Me.LabelX3.TextAlignment = System.Drawing.StringAlignment.Center
        Me.LabelX3.WordWrap = True
        '
        'barraProgreso
        '
        Me.barraProgreso.Location = New System.Drawing.Point(830, 6)
        Me.barraProgreso.Name = "barraProgreso"
        Me.barraProgreso.Size = New System.Drawing.Size(209, 21)
        Me.barraProgreso.TabIndex = 143
        Me.barraProgreso.Visible = False
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.optValorizado)
        Me.GroupBox6.Controls.Add(Me.optCantidades)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.Black
        Me.GroupBox6.Location = New System.Drawing.Point(196, -5)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(186, 36)
        Me.GroupBox6.TabIndex = 142
        Me.GroupBox6.TabStop = False
        '
        'optValorizado
        '
        Me.optValorizado.AutoSize = True
        Me.optValorizado.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optValorizado.ForeColor = System.Drawing.Color.Black
        Me.optValorizado.Location = New System.Drawing.Point(100, 12)
        Me.optValorizado.Name = "optValorizado"
        Me.optValorizado.Size = New System.Drawing.Size(82, 18)
        Me.optValorizado.TabIndex = 1
        Me.optValorizado.Text = "Valorizado"
        Me.optValorizado.UseVisualStyleBackColor = True
        '
        'optCantidades
        '
        Me.optCantidades.AutoSize = True
        Me.optCantidades.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCantidades.ForeColor = System.Drawing.Color.Black
        Me.optCantidades.Location = New System.Drawing.Point(10, 12)
        Me.optCantidades.Name = "optCantidades"
        Me.optCantidades.Size = New System.Drawing.Size(87, 18)
        Me.optCantidades.TabIndex = 0
        Me.optCantidades.Text = "Cantidades"
        Me.optCantidades.UseVisualStyleBackColor = True
        '
        'dataResumen
        '
        Me.dataResumen.AllowUserToAddRows = False
        Me.dataResumen.AllowUserToDeleteRows = False
        Me.dataResumen.AllowUserToResizeColumns = False
        Me.dataResumen.AllowUserToResizeRows = False
        Me.dataResumen.BackgroundColor = System.Drawing.Color.White
        Me.dataResumen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dataResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dataResumen.DefaultCellStyle = DataGridViewCellStyle5
        Me.dataResumen.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dataResumen.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dataResumen.Location = New System.Drawing.Point(1, 18)
        Me.dataResumen.MultiSelect = False
        Me.dataResumen.Name = "dataResumen"
        Me.dataResumen.ReadOnly = True
        Me.dataResumen.RowHeadersVisible = False
        Me.dataResumen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dataResumen.SelectAllSignVisible = False
        Me.dataResumen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataResumen.ShowEditingIcon = False
        Me.dataResumen.Size = New System.Drawing.Size(995, 375)
        Me.dataResumen.TabIndex = 6
        '
        'tabResumen
        '
        Me.tabResumen.AttachedControl = Me.TabControlPanel5
        Me.tabResumen.Icon = CType(resources.GetObject("tabResumen.Icon"), System.Drawing.Icon)
        Me.tabResumen.Name = "tabResumen"
        Me.tabResumen.Text = "Resumen Anual de Ventas"
        '
        'mcDesde
        '
        Me.mcDesde.AnnuallyMarkedDates = New Date(-1) {}
        Me.mcDesde.AutoSize = True
        '
        '
        '
        Me.mcDesde.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.mcDesde.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcDesde.BackgroundStyle.BorderBottomWidth = 1
        Me.mcDesde.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.mcDesde.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcDesde.BackgroundStyle.BorderLeftWidth = 1
        Me.mcDesde.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcDesde.BackgroundStyle.BorderRightWidth = 1
        Me.mcDesde.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcDesde.BackgroundStyle.BorderTopWidth = 1
        Me.mcDesde.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.mcDesde.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.mcDesde.ContainerControlProcessDialogKey = True
        Me.mcDesde.DisplayMonth = New Date(2010, 10, 1, 0, 0, 0, 0)
        Me.mcDesde.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.mcDesde.Location = New System.Drawing.Point(12, 26)
        Me.mcDesde.MarkedDates = New Date(-1) {}
        Me.mcDesde.MaxDate = New Date(2020, 12, 31, 0, 0, 0, 0)
        Me.mcDesde.MinDate = New Date(2010, 1, 1, 0, 0, 0, 0)
        Me.mcDesde.MonthlyMarkedDates = New Date(-1) {}
        Me.mcDesde.Name = "mcDesde"
        '
        '
        '
        Me.mcDesde.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.mcDesde.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.mcDesde.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.mcDesde.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.mcDesde.Size = New System.Drawing.Size(170, 131)
        Me.mcDesde.TabIndex = 171
        Me.mcDesde.Text = "MonthCalendarAdv1"
        Me.mcDesde.WeeklyMarkedDays = New System.DayOfWeek(-1) {}
        '
        'mcHasta
        '
        Me.mcHasta.AnnuallyMarkedDates = New Date(-1) {}
        Me.mcHasta.AutoSize = True
        '
        '
        '
        Me.mcHasta.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.mcHasta.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcHasta.BackgroundStyle.BorderBottomWidth = 1
        Me.mcHasta.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.mcHasta.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcHasta.BackgroundStyle.BorderLeftWidth = 1
        Me.mcHasta.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcHasta.BackgroundStyle.BorderRightWidth = 1
        Me.mcHasta.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcHasta.BackgroundStyle.BorderTopWidth = 1
        Me.mcHasta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.mcHasta.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.mcHasta.ContainerControlProcessDialogKey = True
        Me.mcHasta.DisplayMonth = New Date(2010, 10, 1, 0, 0, 0, 0)
        Me.mcHasta.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.mcHasta.Location = New System.Drawing.Point(222, 26)
        Me.mcHasta.MarkedDates = New Date(-1) {}
        Me.mcHasta.MaxDate = New Date(2020, 12, 31, 0, 0, 0, 0)
        Me.mcHasta.MinDate = New Date(2010, 1, 1, 0, 0, 0, 0)
        Me.mcHasta.MonthlyMarkedDates = New Date(-1) {}
        Me.mcHasta.Name = "mcHasta"
        '
        '
        '
        Me.mcHasta.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.mcHasta.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.mcHasta.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.mcHasta.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.mcHasta.Size = New System.Drawing.Size(170, 131)
        Me.mcHasta.TabIndex = 172
        Me.mcHasta.Text = "MonthCalendarAdv1"
        Me.mcHasta.WeeklyMarkedDays = New System.DayOfWeek(-1) {}
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label1.Location = New System.Drawing.Point(57, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 173
        Me.Label1.Text = "Desde"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label2.Location = New System.Drawing.Point(266, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 16)
        Me.Label2.TabIndex = 174
        Me.Label2.Text = "Hasta"
        '
        'cmdProcesar
        '
        Me.cmdProcesar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.cmdProcesar.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProcesar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.cmdProcesar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.cmdProcesar.Location = New System.Drawing.Point(887, 99)
        Me.cmdProcesar.Name = "cmdProcesar"
        Me.cmdProcesar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(5)
        Me.cmdProcesar.Size = New System.Drawing.Size(101, 37)
        Me.cmdProcesar.TabIndex = 177
        Me.cmdProcesar.Text = "Exportar Datos"
        '
        'c_ventas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1045, 610)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdProcesar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mcHasta)
        Me.Controls.Add(Me.mcDesde)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmdCerrar)
        Me.Controls.Add(Me.cmdImprimir)
        Me.Controls.Add(Me.chkGrupo)
        Me.Controls.Add(Me.grupo)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "c_ventas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Consulta y Reporte: VENTAS "
        Me.grupo.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.TabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl2.ResumeLayout(False)
        Me.TabControlPanel4.ResumeLayout(False)
        Me.TabControlPanel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dataDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlPanel1.ResumeLayout(False)
        CType(Me.DataSalidaPago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSalidaDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSALIDA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlPanel5.ResumeLayout(False)
        Me.TabControlPanel5.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.dataResumen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkGrupo As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents grupo As System.Windows.Forms.GroupBox
    Friend WithEvents cboGrupo As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optFomasPago As System.Windows.Forms.RadioButton
    Friend WithEvents optVentasProducto As System.Windows.Forms.RadioButton
    Friend WithEvents optRegistro As System.Windows.Forms.RadioButton
    Friend WithEvents lblRegistros As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCerrar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cmdImprimir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents TabControl2 As DevComponents.DotNetBar.TabControl
    Friend WithEvents TabControlPanel4 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkIMP As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblMontoDS As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMonedaDS As DevComponents.DotNetBar.LabelX
    Friend WithEvents dataDetalle As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents tabSaldos As DevComponents.DotNetBar.TabItem
    Friend WithEvents TabControlPanel5 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents cboAlmacenR As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents lblMensaje As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optDescripcion As System.Windows.Forms.RadioButton
    Friend WithEvents optCodigo As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents txtBuscar As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents barraProgreso As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents optValorizado As System.Windows.Forms.RadioButton
    Friend WithEvents optCantidades As System.Windows.Forms.RadioButton
    Friend WithEvents dataResumen As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents tabResumen As DevComponents.DotNetBar.TabItem
    Friend WithEvents mcDesde As DevComponents.Editors.DateTimeAdv.MonthCalendarAdv
    Friend WithEvents mcHasta As DevComponents.Editors.DateTimeAdv.MonthCalendarAdv
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControlPanel1 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents TabItem1 As DevComponents.DotNetBar.TabItem
    Friend WithEvents DataSALIDA As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataSalidaDetalle As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataSalidaPago As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents cmdProcesar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents optGrupoVenta As System.Windows.Forms.RadioButton
    Friend WithEvents optAtencion As System.Windows.Forms.RadioButton

End Class
