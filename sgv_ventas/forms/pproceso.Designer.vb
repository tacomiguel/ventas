<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class panel_proceso
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(panel_proceso))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdfecha = New DevComponents.DotNetBar.ButtonX()
        Me.cmdCerrar = New DevComponents.DotNetBar.ButtonX()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdfecha)
        Me.GroupBox1.Controls.Add(Me.cmdCerrar)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.MenuText
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(273, 387)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        '
        'cmdfecha
        '
        Me.cmdfecha.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.cmdfecha.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.cmdfecha.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.cmdfecha.Image = CType(resources.GetObject("cmdfecha.Image"), System.Drawing.Image)
        Me.cmdfecha.Location = New System.Drawing.Point(22, 19)
        Me.cmdfecha.Name = "cmdfecha"
        Me.cmdfecha.Size = New System.Drawing.Size(234, 69)
        Me.cmdfecha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmdfecha.TabIndex = 73
        Me.cmdfecha.Text = "CAMBIO DE FECHA"
        Me.cmdfecha.TextColor = System.Drawing.Color.Black
        '
        'cmdCerrar
        '
        Me.cmdCerrar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.cmdCerrar.BackColor = System.Drawing.Color.Transparent
        Me.cmdCerrar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.cmdCerrar.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"), System.Drawing.Image)
        Me.cmdCerrar.Location = New System.Drawing.Point(22, 298)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(234, 69)
        Me.cmdCerrar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmdCerrar.TabIndex = 72
        Me.cmdCerrar.Text = "SALIR DE PROCESOS"
        Me.cmdCerrar.TextColor = System.Drawing.Color.Black
        Me.cmdCerrar.Tooltip = "Salir"
        '
        'panel_proceso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "panel_proceso"
        Me.Size = New System.Drawing.Size(793, 422)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdfecha As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cmdCerrar As DevComponents.DotNetBar.ButtonX

End Class
