Imports System.Data.SqlClient
Public Class frmMovimientos
    Private Sub frmMovimientos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarBase()
        Me.BotonLimpiar()
        Me.CargarArticulos()
        Me.CargarMovimiento()
        Me.txtCantidad.TextAlign = HorizontalAlignment.Right
        Me.txtCodigo.TextAlign = HorizontalAlignment.Right
        Me.txtIdMovimiento.TextAlign = HorizontalAlignment.Right
    End Sub

    Sub BotonLimpiar()
        Me.txtCantidad.Text = ""
        Me.txtCodigo.Text = ""
        Me.txtIdMovimiento.Text = ""
        Me.txtObservaciones.Text = ""
        Me.ComboBox1.Text = ""
        Me.DateTimePicker1.Focus()

    End Sub

    Sub CargarArticulos()
        Dim Rs As SqlDataReader
        Me.ComboBox1.Items.Clear()
        sql = "select * from articulo"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.ComboBox1.Items.Add(Rs(1))
        End While
        Rs.Close()
    End Sub

    Sub CargarMovimiento()
        Dim Rs As SqlDataReader
        Me.ComboBox2.Items.Clear()
        sql = "select * from tipomovi"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.ComboBox2.Items.Add(Rs(1))
        End While
        Rs.Close()
    End Sub

End Class