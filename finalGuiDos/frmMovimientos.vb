Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class frmMovimientos
    Dim flag As Boolean
    Dim miFecha As String
    Private Sub frmMovimientos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarBase()
        Me.Limpiar()
        Me.CargarArticulos()
        Me.CargarMovimiento()
        Me.txtCantidad.TextAlign = HorizontalAlignment.Right
        Me.txtCodigo.TextAlign = HorizontalAlignment.Right
        Me.txtIdMovimiento.TextAlign = HorizontalAlignment.Right
        Me.txtPrecio.TextAlign = HorizontalAlignment.Right
        Me.BackColor = Color.Beige
        Me.ListBox2.BackColor = Color.AntiqueWhite
        Me.txtObservaciones.BackColor = Color.AntiqueWhite
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

        Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        'Me.ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        If (Me.txtCodigo.Focus()) Then
            flag = False
        End If
        If (Me.ComboBox1.Focus()) Then
            flag = True
        End If
    End Sub

    Private Sub frmMovimientos_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DaoCon.Close()
        MsgBox("Conexión Cerrada", vbInformation)
    End Sub

    Sub Limpiar()
        Me.dtFecha.Value = Now.ToLongDateString
        Me.txtCantidad.Text = ""
        Me.txtIdMovimiento.Text = ""
        Me.txtPrecio.Text = ""
        Me.txtCodigo.Text = ""
        Me.txtObservaciones.Text = ""
        Me.ComboBox1.SelectedIndex = -1
        Me.ComboBox2.Text = ""
        Me.txtCodigo.Focus()
        Me.cargarListaAuxiliar()
        cargarDatosPrincipal()
    End Sub
    Sub cargarListaAuxiliar()
        Dim Rs As SqlDataReader

        Me.ListBox2.Items.Clear()
        sql = "select * from movimiento left join articulo on movimiento.[id articulo] = articulo.[id articulo] WHERE [nom articulo] LIKE 'Veliz%'"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.ListBox2.Items.Add(Space(4) & Rs(0).ToString.PadRight(5, " ") & vbTab & vbTab & Rs(1).ToString.PadRight(5, " ") & vbTab & vbTab & Rs(2).ToString.PadRight(10, " ") & vbTab & Rs(3) & Space(14) & Rs(4).ToString.PadRight(5, " ") & vbTab & Format(Rs(5), "##,##0.00").ToString.PadLeft(12) & vbTab & vbTab & Rs(6).ToString.PadRight(250, " "))
        End While
        Rs.Close()

    End Sub

    Sub CargarArticulos()
        Dim Rs As SqlDataReader
        Me.ComboBox1.Items.Clear()
        sql = "select * from articulo WHERE [nom articulo] LIKE 'Veliz%'"
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
        sql = "select * from tipomovi WHERE [nom tipomovi] LIKE 'Veliz%'"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.ComboBox2.Items.Add(Rs(1))
        End While
        Rs.Close()
    End Sub
    Sub Autocompletado(texto As String)
        Dim Rs As SqlDataReader
        If Val(texto) <> 0 Then
            sql = "select [nom articulo] from articulo Where [id articulo]=" & Val(texto) & ""
            Instruccion = New SqlCommand(sql, DaoCon2)
            Rs = Instruccion.ExecuteReader()
            If Rs.Read = Nothing Then
                Me.ComboBox1.SelectedIndex = -1
            Else

                Me.ComboBox1.Text = Rs(0)
            End If
            Rs.Close()
        End If

    End Sub

    Private Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        Autocompletado(Me.txtCodigo.Text)
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim Rs As SqlDataReader
        If Me.ComboBox1.SelectedIndex > -1 Then
            sql = "select [id articulo] , [pco articulo] from articulo Where [nom articulo]='" & Me.ComboBox1.SelectedItem.ToString & "'"
            Instruccion = New SqlCommand(sql, DaoCon1)
            Rs = Instruccion.ExecuteReader()
            While Rs.Read
                Me.txtCodigo.Text = Rs(0)
                Me.txtPrecio.Text = Rs(1)
            End While
            Rs.Close()
        Else
            Me.txtPrecio.Text = ""
            Me.txtCodigo.Text = ""
        End If

    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim Rs As SqlDataReader
        sql = "select [id tipomovi] from tipomovi Where [nom tipomovi]='" & Me.ComboBox2.SelectedItem.ToString & "'"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.txtIdMovimiento.Text = Rs(0)
        End While
        Rs.Close()
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        Dim Rs As SqlDataReader
        sql = "select [id tipomovi] from tipomovi Where [nom tipomovi]='" & Me.ComboBox2.Text & "'"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        If Rs.Read = Nothing Then
            Me.txtIdMovimiento.Text = ""
        Else
            Me.txtIdMovimiento.Text = Rs(0)
        End If
        Rs.Close()
    End Sub

    Private Sub txtIdMovimiento_Enter(sender As Object, e As EventArgs) Handles txtIdMovimiento.Enter
        Me.txtIdMovimiento.ReadOnly = True
        Me.txtIdMovimiento.BackColor = Color.White
        Me.txtIdMovimiento.TabStop = False
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtPrecio_Enter(sender As Object, e As EventArgs) Handles txtPrecio.Enter
        Me.txtPrecio.ReadOnly = True
        Me.txtPrecio.BackColor = Color.White
        Me.txtPrecio.TabStop = False
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        soloNumeros(Me.txtCantidad.Text)
    End Sub

    Private Sub txtObservaciones_TextChanged(sender As Object, e As EventArgs) Handles txtObservaciones.TextChanged
        Me.txtObservaciones.Text = Mid(Me.txtObservaciones.Text, 1, 250)
        Me.txtObservaciones.SelectionStart = 250
    End Sub

    Private Sub txtCodigo_Enter(sender As Object, e As EventArgs) Handles txtCodigo.Enter
        txtCodigo.BackColor = Color.Aquamarine
    End Sub


    Private Sub txtCodigo_Leave(sender As Object, e As EventArgs) Handles txtCodigo.Leave
        txtCodigo.BackColor = Color.White
    End Sub

    Private Sub txtCantidad_Enter(sender As Object, e As EventArgs) Handles txtCantidad.Enter
        txtCantidad.BackColor = Color.Aquamarine
    End Sub


    Private Sub txtCantidad_Leave(sender As Object, e As EventArgs) Handles txtCantidad.Leave
        txtCantidad.BackColor = Color.White
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Limpiar()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If txtCantidad.Text <> "" And ComboBox1.Text <> "" And ComboBox2.Text <> "" And txtCodigo.Text <> "" And txtObservaciones.Text <> "" And txtIdMovimiento.Text <> "" Then
            Dim precioTotal As String
            precioTotal = Val(Me.txtPrecio.Text.Replace(",", ".")) * Val(Me.txtCantidad.Text.Replace(",", "."))
            miFecha = Format(dtFecha.Value.Date.ToString, "short date")

            sql = "INSERT INTO movimiento ([id tipomovi],[id articulo],[fec movimiento],[can movimiento],[pre movimiento],[obs movimiento] ) VALUES (" & Val(Me.txtIdMovimiento.Text) & "," & Val(Me.txtCodigo.Text) & ",
        '" & miFecha & "'," & Val(Me.txtCantidad.Text) & "," & precioTotal.Replace(",", ".") & ",'" & Me.txtObservaciones.Text & "')"
            Instruccion = New SqlCommand(sql, DaoCon)
            Instruccion.ExecuteNonQuery()
            Me.Limpiar()
        Else
            Me.txtCodigo.Focus()
        End If


        'sql = "DELETE FROM movimiento WHERE [id movimiento]=" & Val(Me.txtCantidad.Text) & ""
        'Instruccion = New SqlCommand(sql, DaoCon)
        'Instruccion.ExecuteNonQuery()
    End Sub
End Class

