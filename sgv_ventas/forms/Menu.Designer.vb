<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Menu
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Menu))
        Me.MetroTilePanel1 = New DevComponents.DotNetBar.Metro.MetroTilePanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ButtonX8 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX6 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX7 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX5 = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.MetroTilePanel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroTilePanel1
        '
        Me.MetroTilePanel1.BackColor = System.Drawing.Color.DimGray
        '
        '
        '
        Me.MetroTilePanel1.BackgroundStyle.Class = "MetroTilePanel"
        Me.MetroTilePanel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MetroTilePanel1.ContainerControlProcessDialogKey = True
        Me.MetroTilePanel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.MetroTilePanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.MetroTilePanel1.Location = New System.Drawing.Point(0, 3)
        Me.MetroTilePanel1.Name = "MetroTilePanel1"
        Me.MetroTilePanel1.Size = New System.Drawing.Size(817, 534)
        Me.MetroTilePanel1.TabIndex = 0
        Me.MetroTilePanel1.Text = "MetroTilePanel1"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonX8)
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonX6)
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonX7)
        Me.FlowLayoutPanel1.Controls.Add(Me.ButtonX5)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(47, 36)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(189, 479)
        Me.FlowLayoutPanel1.TabIndex = 6
        '
        'ButtonX8
        '
        Me.ButtonX8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX8.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX8.Image = CType(resources.GetObject("ButtonX8.Image"), System.Drawing.Image)
        Me.ButtonX8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX8.Location = New System.Drawing.Point(3, 3)
        Me.ButtonX8.Name = "ButtonX8"
        Me.ButtonX8.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(20)
        Me.ButtonX8.Size = New System.Drawing.Size(120, 87)
        Me.ButtonX8.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro
        Me.ButtonX8.TabIndex = 6
        Me.ButtonX8.Text = "VENTAS"
        Me.ButtonX8.TextColor = System.Drawing.Color.White
        '
        'ButtonX6
        '
        Me.ButtonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX6.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX6.Image = CType(resources.GetObject("ButtonX6.Image"), System.Drawing.Image)
        Me.ButtonX6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX6.Location = New System.Drawing.Point(3, 96)
        Me.ButtonX6.Name = "ButtonX6"
        Me.ButtonX6.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(20)
        Me.ButtonX6.Size = New System.Drawing.Size(120, 87)
        Me.ButtonX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro
        Me.ButtonX6.TabIndex = 8
        Me.ButtonX6.Text = "CONSULTAS"
        Me.ButtonX6.TextColor = System.Drawing.Color.White
        '
        'ButtonX7
        '
        Me.ButtonX7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX7.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX7.Image = CType(resources.GetObject("ButtonX7.Image"), System.Drawing.Image)
        Me.ButtonX7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX7.Location = New System.Drawing.Point(3, 189)
        Me.ButtonX7.Name = "ButtonX7"
        Me.ButtonX7.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(20)
        Me.ButtonX7.Size = New System.Drawing.Size(120, 87)
        Me.ButtonX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro
        Me.ButtonX7.TabIndex = 7
        Me.ButtonX7.Text = "CAMBIO FECHA"
        Me.ButtonX7.TextColor = System.Drawing.Color.White
        '
        'ButtonX5
        '
        Me.ButtonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX5.BackColor = System.Drawing.Color.Transparent
        Me.ButtonX5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX5.Image = CType(resources.GetObject("ButtonX5.Image"), System.Drawing.Image)
        Me.ButtonX5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.ButtonX5.Location = New System.Drawing.Point(3, 282)
        Me.ButtonX5.Name = "ButtonX5"
        Me.ButtonX5.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(20)
        Me.ButtonX5.Size = New System.Drawing.Size(120, 87)
        Me.ButtonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro
        Me.ButtonX5.TabIndex = 9
        Me.ButtonX5.Text = "SALIR"
        Me.ButtonX5.TextColor = System.Drawing.Color.White
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.MetroTilePanel1)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(820, 485)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        Me.PanelEx1.Text = "Menu"
        '
        'Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelEx1)
        Me.Name = "Menu"
        Me.Size = New System.Drawing.Size(820, 485)
        Me.MetroTilePanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MetroTilePanel1 As DevComponents.DotNetBar.Metro.MetroTilePanel
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents ButtonX5 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX6 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX7 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX8 As DevComponents.DotNetBar.ButtonX

End Class
