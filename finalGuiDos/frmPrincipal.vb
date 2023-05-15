Public Class frmPrincipal
    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AltasYActualizacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AltasYActualizacionesToolStripMenuItem.Click
        frmAltasYActualizaciones.Show()
    End Sub

    Private Sub MovimientosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MovimientosToolStripMenuItem.Click
        frmMovimientos.Show()
    End Sub
End Class
