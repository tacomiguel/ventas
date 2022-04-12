<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class p_caja
    Inherits sgv_ventas.base

    'Form invalida a Dispose para limpiar la lista de componentes.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(p_caja))
        Me.panelitem = New System.Windows.Forms.FlowLayoutPanel()
        Me.dataPago = New System.Windows.Forms.DataGridView()
        Me.Column1 = New DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn()
        Me.txtTotal1 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.cmdCancelar = New DevExpress.DXCore.Controls.XtraEditors.SimpleButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmdGrabar = New DevComponents.DotNetBar.ButtonX()
        CType(Me.dataPago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelitem
        '
        Me.panelitem.AutoScroll = True
        Me.panelitem.AutoScrollMargin = New System.Drawing.Size(40, 20)
        Me.panelitem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.panelitem.BackColor = System.Drawing.Color.Transparent
        Me.panelitem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelitem.ForeColor = System.Drawing.Color.Black
        Me.panelitem.Location = New System.Drawing.Point(455, 127)
        Me.panelitem.Name = "panelitem"
        Me.panelitem.Padding = New System.Windows.Forms.Padding(1)
        Me.panelitem.Size = New System.Drawing.Size(430, 224)
        Me.panelitem.TabIndex = 7450
        '
        'dataPago
        '
        Me.dataPago.AllowUserToAddRows = False
        Me.dataPago.AllowUserToDeleteRows = False
        Me.dataPago.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dataPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dataPago.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dataPago.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Maroon
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dataPago.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dataPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataPago.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.dataPago.Location = New System.Drawing.Point(70, 155)
        Me.dataPago.Name = "dataPago"
        Me.dataPago.RowHeadersVisible = False
        Me.dataPago.RowHeadersWidth = 80
        Me.dataPago.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dataPago.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dataPago.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dataPago.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue
        Me.dataPago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataPago.Size = New System.Drawing.Size(394, 224)
        Me.dataPago.TabIndex = 7449
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "nom_pago"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.HeaderText = "FORMA PAGO"
        Me.Column1.Name = "Column1"
        Me.Column1.Text = Nothing
        '
        'txtTotal1
        '
        Me.txtTotal1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.txtTotal1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtTotal1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtTotal1.Location = New System.Drawing.Point(779, 386)
        Me.txtTotal1.Name = "txtTotal1"
        Me.txtTotal1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTotal1.Size = New System.Drawing.Size(129, 91)
        Me.txtTotal1.TabIndex = 174
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.Appearance.Options.UseFont = True
        Me.cmdCancelar.Image = CType(resources.GetObject("cmdCancelar.Image"), System.Drawing.Image)
        Me.cmdCancelar.ImageLocation = DevExpress.DXCore.Controls.XtraEditors.ImageLocation.TopCenter
        Me.cmdCancelar.Location = New System.Drawing.Point(697, 396)
        Me.cmdCancelar.LookAndFeel.SkinName = "iMaginary"
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(64, 40)
        Me.cmdCancelar.TabIndex = 173
        Me.cmdCancelar.Text = "Cancelar"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.cmdGrabar)
        Me.GroupBox4.Controls.Add(Me.cmdCancelar)
        Me.GroupBox4.Controls.Add(Me.panelitem)
        Me.GroupBox4.Controls.Add(Me.txtTotal1)
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.GroupBox4.Location = New System.Drawing.Point(39, 28)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(929, 526)
        Me.GroupBox4.TabIndex = 7454
        Me.GroupBox4.TabStop = False
        '
        'cmdGrabar
        '
        Me.cmdGrabar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.cmdGrabar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.cmdGrabar.Image = CType(resources.GetObject("cmdGrabar.Image"), System.Drawing.Image)
        Me.cmdGrabar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.cmdGrabar.Location = New System.Drawing.Point(624, 396)
        Me.cmdGrabar.Name = "cmdGrabar"
        Me.cmdGrabar.Size = New System.Drawing.Size(67, 40)
        Me.cmdGrabar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmdGrabar.TabIndex = 7451
        Me.cmdGrabar.Tooltip = "Imprimir Ticket"
        '
        'p_caja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(999, 616)
        Me.ControlBox = False
        Me.Controls.Add(Me.dataPago)
        Me.Controls.Add(Me.GroupBox4)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.Name = "p_caja"
        Me.ShowIcon = False
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dataPago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdCancelar As DevExpress.DXCore.Controls.XtraEditors.SimpleButton
    Friend WithEvents txtTotal1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents dataPago As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn
    Friend WithEvents panelitem As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdGrabar As DevComponents.DotNetBar.ButtonX

End Class
