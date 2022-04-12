<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class u_CambioFecha
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(u_CambioFecha))
        Me.txtTC = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblItems = New DevComponents.DotNetBar.LabelX()
        Me.lblFecha = New DevComponents.DotNetBar.LabelX()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.mcFIngreso = New DevComponents.Editors.DateTimeAdv.MonthCalendarAdv()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTC
        '
        '
        '
        '
        Me.txtTC.Border.Class = "TextBoxBorder"
        Me.txtTC.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtTC.Location = New System.Drawing.Point(142, 234)
        Me.txtTC.Name = "txtTC"
        Me.txtTC.Size = New System.Drawing.Size(82, 20)
        Me.txtTC.TabIndex = 0
        Me.txtTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblItems
        '
        Me.lblItems.AutoSize = True
        '
        '
        '
        Me.lblItems.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItems.Location = New System.Drawing.Point(31, 234)
        Me.lblItems.Name = "lblItems"
        Me.lblItems.Size = New System.Drawing.Size(97, 17)
        Me.lblItems.TabIndex = 62
        Me.lblItems.Text = "Tipo de Cambio"
        '
        'lblFecha
        '
        '
        '
        '
        Me.lblFecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblFecha.Font = New System.Drawing.Font("Copperplate Gothic Bold", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.Color.Sienna
        Me.lblFecha.Location = New System.Drawing.Point(55, 9)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(186, 44)
        Me.lblFecha.TabIndex = 63
        Me.lblFecha.Text = "Fecha del Sistema"
        Me.lblFecha.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 72
        Me.PictureBox1.TabStop = False
        '
        'mcFIngreso
        '
        Me.mcFIngreso.AnnuallyMarkedDates = New Date(-1) {}
        Me.mcFIngreso.AutoSize = True
        '
        '
        '
        Me.mcFIngreso.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.mcFIngreso.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcFIngreso.BackgroundStyle.BorderBottomWidth = 1
        Me.mcFIngreso.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.mcFIngreso.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcFIngreso.BackgroundStyle.BorderLeftWidth = 1
        Me.mcFIngreso.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcFIngreso.BackgroundStyle.BorderRightWidth = 1
        Me.mcFIngreso.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.mcFIngreso.BackgroundStyle.BorderTopWidth = 1
        Me.mcFIngreso.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.mcFIngreso.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.mcFIngreso.ContainerControlProcessDialogKey = True
        Me.mcFIngreso.DaySize = New System.Drawing.Size(30, 20)
        Me.mcFIngreso.DisplayMonth = New Date(2010, 10, 1, 0, 0, 0, 0)
        Me.mcFIngreso.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.mcFIngreso.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.mcFIngreso.Location = New System.Drawing.Point(12, 59)
        Me.mcFIngreso.MarkedDates = New Date(-1) {}
        Me.mcFIngreso.MaxDate = New Date(2020, 12, 31, 0, 0, 0, 0)
        Me.mcFIngreso.MinDate = New Date(2010, 1, 1, 0, 0, 0, 0)
        Me.mcFIngreso.MonthlyMarkedDates = New Date(-1) {}
        Me.mcFIngreso.Name = "mcFIngreso"
        '
        '
        '
        Me.mcFIngreso.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.mcFIngreso.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.mcFIngreso.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.mcFIngreso.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.mcFIngreso.Size = New System.Drawing.Size(212, 166)
        Me.mcFIngreso.TabIndex = 73
        Me.mcFIngreso.TabStop = False
        Me.mcFIngreso.TwoLetterDayName = False
        Me.mcFIngreso.WeeklyMarkedDays = New System.DayOfWeek(-1) {}
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(230, 171)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 54)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = "Cambio Fecha"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(230, 59)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(70, 54)
        Me.Button2.TabIndex = 75
        Me.Button2.Text = "Cierre X"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(230, 115)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(70, 54)
        Me.Button3.TabIndex = 76
        Me.Button3.Text = "Cierre Z"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'u_CambioFecha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(318, 274)
        Me.ControlBox = True
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.mcFIngreso)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.txtTC)
        Me.Controls.Add(Me.lblItems)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "u_CambioFecha"
        Me.Text = "Utilidad: CAMBIO DE FECHA"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTC As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblItems As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblFecha As DevComponents.DotNetBar.LabelX
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents mcFIngreso As DevComponents.Editors.DateTimeAdv.MonthCalendarAdv
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button

End Class
