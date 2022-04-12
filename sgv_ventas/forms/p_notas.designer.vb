<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class p_notas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(p_notas))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblnotas = New DevComponents.DotNetBar.LabelX()
        Me.cmdCancelar = New DevExpress.DXCore.Controls.XtraEditors.SimpleButton()
        Me.cmdAceptar = New DevExpress.DXCore.Controls.XtraEditors.SimpleButton()
        Me.txtnotas = New System.Windows.Forms.RichTextBox()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblnotas)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox3.Location = New System.Drawing.Point(12, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(381, 35)
        Me.GroupBox3.TabIndex = 175
        Me.GroupBox3.TabStop = False
        '
        'lblnotas
        '
        '
        '
        '
        Me.lblnotas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblnotas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnotas.ForeColor = System.Drawing.Color.Maroon
        Me.lblnotas.Location = New System.Drawing.Point(6, 12)
        Me.lblnotas.Name = "lblnotas"
        Me.lblnotas.Size = New System.Drawing.Size(367, 17)
        Me.lblnotas.TabIndex = 0
        Me.lblnotas.Text = "Ingrese Observacion"
        Me.lblnotas.TextAlignment = System.Drawing.StringAlignment.Center
        Me.lblnotas.WordWrap = True
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.Appearance.Options.UseFont = True
        Me.cmdCancelar.Image = CType(resources.GetObject("cmdCancelar.Image"), System.Drawing.Image)
        Me.cmdCancelar.ImageLocation = DevExpress.DXCore.Controls.XtraEditors.ImageLocation.TopCenter
        Me.cmdCancelar.Location = New System.Drawing.Point(327, 45)
        Me.cmdCancelar.LookAndFeel.SkinName = "iMaginary"
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(64, 50)
        Me.cmdCancelar.TabIndex = 173
        Me.cmdCancelar.Text = "Cancelar"
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAceptar.Appearance.Options.UseFont = True
        Me.cmdAceptar.Image = CType(resources.GetObject("cmdAceptar.Image"), System.Drawing.Image)
        Me.cmdAceptar.ImageLocation = DevExpress.DXCore.Controls.XtraEditors.ImageLocation.TopCenter
        Me.cmdAceptar.Location = New System.Drawing.Point(257, 45)
        Me.cmdAceptar.LookAndFeel.SkinName = "iMaginary"
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(64, 50)
        Me.cmdAceptar.TabIndex = 172
        Me.cmdAceptar.Text = "Anular"
        '
        'txtnotas
        '
        Me.txtnotas.Location = New System.Drawing.Point(12, 45)
        Me.txtnotas.Name = "txtnotas"
        Me.txtnotas.Size = New System.Drawing.Size(230, 72)
        Me.txtnotas.TabIndex = 0
        Me.txtnotas.Text = ""
        '
        'p_notas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(420, 149)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.txtnotas)
        Me.DoubleBuffered = True
        Me.Name = "p_notas"
        Me.Text = "Ingreso Nota Anulacion"
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtnotas As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdCancelar As DevExpress.DXCore.Controls.XtraEditors.SimpleButton
    Friend WithEvents cmdAceptar As DevExpress.DXCore.Controls.XtraEditors.SimpleButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblnotas As DevComponents.DotNetBar.LabelX

End Class
