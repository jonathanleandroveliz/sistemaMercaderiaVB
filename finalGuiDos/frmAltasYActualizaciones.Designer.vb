<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAltasYActualizaciones
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArticulosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TiposDeMovimientosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgrupacionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArticulosToolStripMenuItem, Me.TiposDeMovimientosToolStripMenuItem, Me.AgrupacionToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArticulosToolStripMenuItem
        '
        Me.ArticulosToolStripMenuItem.Name = "ArticulosToolStripMenuItem"
        Me.ArticulosToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.ArticulosToolStripMenuItem.Text = "Articulos"
        '
        'TiposDeMovimientosToolStripMenuItem
        '
        Me.TiposDeMovimientosToolStripMenuItem.Name = "TiposDeMovimientosToolStripMenuItem"
        Me.TiposDeMovimientosToolStripMenuItem.Size = New System.Drawing.Size(136, 20)
        Me.TiposDeMovimientosToolStripMenuItem.Text = "Tipos de Movimientos"
        '
        'AgrupacionToolStripMenuItem
        '
        Me.AgrupacionToolStripMenuItem.Name = "AgrupacionToolStripMenuItem"
        Me.AgrupacionToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.AgrupacionToolStripMenuItem.Text = "Agrupacion"
        '
        'frmAltasYActualizaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmAltasYActualizaciones"
        Me.Text = "Altas y Actualizaciones"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArticulosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TiposDeMovimientosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AgrupacionToolStripMenuItem As ToolStripMenuItem
End Class
