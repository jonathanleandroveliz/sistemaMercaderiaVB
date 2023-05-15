Public Class frmAltasYActualizaciones
    Private Sub ArticulosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArticulosToolStripMenuItem.Click
        frmArticulos.Show()
    End Sub

    Private Sub TiposDeMovimientosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TiposDeMovimientosToolStripMenuItem.Click
        frmTiposMovimientos.Show()
    End Sub

    Private Sub AgrupacionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgrupacionToolStripMenuItem.Click
        frmAgrupacion.Show()
    End Sub
End Class