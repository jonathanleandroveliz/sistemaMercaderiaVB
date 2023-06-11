Imports System.Data.SqlClient
Public Class frmTiposMovimientos
    Private Sub frmTiposMovimientos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarBase()
        Me.BotonLimpiar()
        Me.txtID.TextAlign = HorizontalAlignment.Right
        Me.BackColor = Color.Beige
        Me.lstMovimientos.BackColor = Color.AntiqueWhite
        Me.GroupBox1.BackColor = Color.AntiqueWhite
        Me.GroupBox2.BackColor = Color.AntiqueWhite
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub frmTiposMovimientos_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DaoCon.Close()
        MsgBox("Conexión Cerrada", vbInformation)
    End Sub

    Sub BotonLimpiar()

        Me.btnBorrar.Enabled = False ' Boton Eliminar
        Me.btnModificar.Enabled = False ' Boton Modificar
        Me.btnAgregar.Enabled = True ' Boton Agregar
        Me.txtNomMovimiento.Text = ""
        Me.txtID.Text = ""
        Me.txtTipoMovimiento.Text = ""
        Me.txtNomMovimiento.Focus()
        BotonMostrar(0)
    End Sub

    Sub BotonAgregar()
        On Error GoTo Errores
        If Me.txtNomMovimiento.Text = "" And Me.txtTipoMovimiento.Text = "" Then
            MsgBox("El nombre y tipo de movimiento es requerido", vbCritical)
            Me.txtNomMovimiento.Focus()
            Exit Sub
        ElseIf Me.txtNomMovimiento.Text = "" And Me.txtTipoMovimiento.Text <> "" Then
            MsgBox("El nombre de movimiento es requerido", vbCritical)
            Me.txtNomMovimiento.Focus()
            Exit Sub
        ElseIf Me.txtNomMovimiento.Text <> "" And Me.txtTipoMovimiento.Text = "" Then
            MsgBox("El Tipo de movimiento es requerido", vbCritical)
            Me.txtNomMovimiento.Focus()
            Exit Sub
        Else
            sql = "INSERT INTO tipomovi ([nom tipomovi],[tip tipomovi]) VALUES ('" & Me.txtNomMovimiento.Text & "','" & Me.txtTipoMovimiento.Text & "')"
            Instruccion = New SqlCommand(sql, DaoCon)
            Instruccion.ExecuteNonQuery()
            Me.BotonLimpiar()
            Exit Sub
        End If
Errores:
        Select Case Err.Number
            Case Else
                MsgBox(Err.Description & " (" & Format(Err.Number, "00000)"))
                Exit Sub
        End Select
    End Sub

    Sub BotonEliminar()

        Dim movimientoCargado As Integer
        sql = "select COUNT(*) from movimiento where [id tipomovi] = " & Val(Mid(Me.lstMovimientos.SelectedItem, 1, 5)) & ""
        Instruccion = New SqlCommand(sql, DaoCon)
        movimientoCargado = Instruccion.ExecuteScalar()

        If (movimientoCargado > 0) Then
            MsgBox("No se puede eliminar el tipo de movimiento porque tiene articulos vinculados con el tipo de movimiento", vbCritical)
            Me.lstMovimientos.ClearSelected()
            Me.BotonLimpiar()
            Exit Sub
        End If

        Dim OpC As Integer
        On Error GoTo Errores
        OpC = MsgBox("¿Desea eliminar este Registo?", vbYesNo, "Verifique")
        If OpC = vbYes Then
            sql = "DELETE FROM tipomovi WHERE [id tipomovi]=" & Val(Me.txtID.Text) & ""
            Instruccion = New SqlCommand(sql, DaoCon)
            Instruccion.ExecuteNonQuery()
        End If
        Me.BotonLimpiar()
        Exit Sub
Errores:
        Select Case Err.Number
            Case Else
                MsgBox(Err.Description & " (" & Format(Err.Number, "00000)"))
                Exit Sub
        End Select
    End Sub

    Sub botonModificar()
        On Error GoTo Errores
        If Me.txtNomMovimiento.Text = "" And Me.txtTipoMovimiento.Text <> "" And Me.txtID.Text <> "" Then
            MsgBox("El nombre de movimiento es requerido", vbCritical)
            Me.txtNomMovimiento.Focus()
            Exit Sub
        ElseIf Me.txtNomMovimiento.Text <> "" And Me.txtTipoMovimiento.Text = "" And Me.txtID.Text <> "" Then
            MsgBox("El Tipo de movimiento es requerido", vbCritical)
            Me.txtTipoMovimiento.Focus()
            Exit Sub
        ElseIf Me.txtNomMovimiento.Text = "" And Me.txtTipoMovimiento.Text = "" And Me.txtID.Text <> "" Then
            MsgBox("El Nombre y el tipo de movimiento es requerido", vbCritical)
            Me.txtNomMovimiento.Focus()
            Exit Sub
        Else
            sql = "UPDATE tipomovi SET [nom tipomovi]='" & Me.txtNomMovimiento.Text & "',[tip tipomovi]='" & Me.txtTipoMovimiento.Text & "' WHERE [id tipomovi]=" & Val(Me.txtID.Text) & ""
            Instruccion = New SqlCommand(sql, DaoCon)
            Instruccion.ExecuteNonQuery()
            Me.BotonLimpiar()
            Exit Sub
        End If

Errores:
        Select Case Err.Number
            Case Else
                MsgBox(Err.Description & " (" & Format(Err.Number, "00000)"))
                Exit Sub
        End Select
    End Sub

    Sub BotonMostrar(Orden As Integer)
        Dim Rs As SqlDataReader
        Me.lstMovimientos.Items.Clear()
        Select Case Orden
            Case 0
                sql = "select * from tipomovi  WHERE [nom tipomovi] LIKE 'Veliz%' ORDER BY [id tipomovi]"

            Case 1
                sql = "select * from tipomovi  WHERE [nom tipomovi] LIKE 'Veliz%' ORDER BY [nom tipomovi]"
        End Select
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.lstMovimientos.Items.Add(Format(Rs(0), "00000") & vbTab & vbTab & Rs(1).ToString.PadRight(30, " ") & Rs(2))
        End While
        Rs.Close()
    End Sub

    Private Sub lstAgrupacion_DoubleClick(sender As Object, e As EventArgs) Handles lstMovimientos.DoubleClick
        Dim Rs As SqlDataReader
        If Me.lstMovimientos.SelectedItem <> "" Then
            sql = "select * from tipomovi Where [id tipomovi]=" & Val(Mid(Me.lstMovimientos.SelectedItem, 1, 5)) & ""
            Instruccion = New SqlCommand(sql, DaoCon)
            Rs = Instruccion.ExecuteReader()
            While Rs.Read
                Me.txtID.Text = Rs(0)
                Me.txtNomMovimiento.Text = Rs(1)
                Me.txtTipoMovimiento.Text = Rs(2)
            End While
            Rs.Close()
            Me.btnBorrar.Enabled = True ' Boton Eliminar
            Me.btnModificar.Enabled = True ' Boton Modificar
            Me.btnAgregar.Enabled = False ' Boton Agregar
        Else
            MsgBox("No se seleccionó ningun item", vbCritical, "Verifique")
            Me.lstMovimientos.Focus()
        End If
    End Sub


    Private Sub txtID_Enter(sender As Object, e As EventArgs) Handles txtID.Enter
        Me.txtID.ReadOnly = True
        Me.txtID.BackColor = Color.White
        Me.txtID.TabStop = False
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtNomMovimiento_TextChanged(sender As Object, e As EventArgs) Handles txtNomMovimiento.TextChanged
        Dim texto As String = Me.txtNomMovimiento.Text
        soloLetras2(texto)
    End Sub

    Private Sub txtTipoMovimiento_TextChanged(sender As Object, e As EventArgs) Handles txtTipoMovimiento.TextChanged
        Dim texto As String = Me.txtTipoMovimiento.Text
        soloLetras3(texto)
    End Sub

    Private Sub txtNomMovimiento_Enter(sender As Object, e As EventArgs) Handles txtNomMovimiento.Enter
        txtNomMovimiento.BackColor = Color.Aquamarine
    End Sub


    Private Sub txtNomMovimiento_Leave(sender As Object, e As EventArgs) Handles txtNomMovimiento.Leave
        txtNomMovimiento.BackColor = Color.White
    End Sub

    Private Sub txtTipoMovimiento_Enter(sender As Object, e As EventArgs) Handles txtTipoMovimiento.Enter
        txtTipoMovimiento.BackColor = Color.Aquamarine
    End Sub


    Private Sub txtTipoMovimiento_Leave(sender As Object, e As EventArgs) Handles txtTipoMovimiento.Leave
        txtTipoMovimiento.BackColor = Color.White
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Me.BotonAgregar()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Me.BotonLimpiar()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Me.botonModificar()
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        Me.BotonEliminar()
    End Sub
End Class