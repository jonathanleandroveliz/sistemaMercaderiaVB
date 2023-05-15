Imports System.Data.SqlClient

Public Class frmArticulos
    Dim flag As Boolean
    Private Sub frmArticulos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarBase()
        Me.BotonLimpiar()
        Me.CargarComboBox()
        Me.txtPrecio.TextAlign = HorizontalAlignment.Right
        Me.txtID.TextAlign = HorizontalAlignment.Right
        Me.txtIDAgrupacion.TextAlign = HorizontalAlignment.Right
    End Sub

    Private Sub frmArticulos_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DaoCon.Close()
        MsgBox("Conexión Cerrada", vbInformation)
    End Sub
    Sub BotonLimpiar()
        Me.btnBorrar.Enabled = False ' Boton Eliminar
        Me.btnModificar.Enabled = False ' Boton Modificar
        Me.btnAgregar.Enabled = True ' Boton Agregar
        Me.txtNombre.Text = ""
        Me.txtID.Text = ""
        Me.txtPrecio.Text = ""
        Me.txtIDAgrupacion.Text = ""
        Me.ComboBox1.Text = ""
        flag = True
        Me.txtNombre.Focus()
        BotonMostrar(0)
    End Sub

    Sub BotonAgregar()

        On Error GoTo Errores
        If Me.txtNombre.Text = "" And Me.txtPrecio.Text <> "" And Me.txtIDAgrupacion.Text <> "" Then
            MsgBox("El nombre es requerido", vbCritical)
            flag = True
            Me.txtNombre.Focus()
            Exit Sub
        ElseIf Me.txtNombre.Text <> "" And Me.txtPrecio.Text <> "" And Me.txtIDAgrupacion.Text = "" Then
            MsgBox("El IDAgrupacion es requerido", vbCritical)
            flag = True
            Me.ComboBox1.Focus()
            Exit Sub
        ElseIf Me.txtNombre.Text <> "" And Me.txtPrecio.Text = "" And Me.txtIDAgrupacion.Text <> "" Then
            MsgBox("El Precio es requerido", vbCritical)
            flag = True
            Me.txtPrecio.Focus()
            Exit Sub
        ElseIf Me.txtID.Text = "" And Me.txtNombre.Text = "" And Me.txtPrecio.Text = "" And Me.txtIDAgrupacion.Text = "" Then
            MsgBox("El Nombre, Precio , Identificador de Agrupacion es requerido", vbCritical)
            Me.ComboBox1.Text = ""
            flag = True
            Me.txtNombre.Focus()
            Exit Sub
        ElseIf Me.txtID.Text = "" And Me.txtNombre.Text = "" And Me.txtPrecio.Text = "" And Me.txtIDAgrupacion.Text <> "" Then
            MsgBox("El Nombre, Precio , es requerido", vbCritical)
            flag = True
            Me.txtNombre.Focus()
            Exit Sub
        ElseIf Me.txtID.Text = "" And Me.txtNombre.Text <> "" And Me.txtPrecio.Text = "" And Me.txtIDAgrupacion.Text = "" Then
            MsgBox("El Precio , Identificador de Agrupacion es requerido es requerido", vbCritical)
            flag = True
            Me.txtPrecio.Focus()
            Exit Sub
        ElseIf Me.txtID.Text = "" And Me.txtNombre.Text = "" And Me.txtPrecio.Text <> "" And Me.txtIDAgrupacion.Text = "" Then
            MsgBox("El Nombre , Identificador de Agrupacion es requerido es requerido", vbCritical)
            flag = True
            Me.txtNombre.Focus()
            Exit Sub
        ElseIf Me.txtID.Text = "" And Me.txtPrecio.Text <> "" And Me.txtNombre.Text <> "" And Me.txtIDAgrupacion.Text <> "" Then
            sql = "INSERT INTO articulo ([nom articulo],[pco articulo],[id agrupacion] ) VALUES ('" & Me.txtNombre.Text & "','" & Me.txtPrecio.Text.Replace(",", ".") & "','" & Me.txtIDAgrupacion.Text & "')"
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
        Me.ComboBox1.Text = ""
        flag = True
        Dim OpC As Integer
        On Error GoTo Errores
        OpC = MsgBox("¿Desea eliminar este Registo?", vbYesNo, "Verifique")
        If OpC = vbYes Then
            sql = "DELETE FROM articulo WHERE [id articulo]=" & Val(Me.txtID.Text) & ""
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
        If Me.txtNombre.Text = "" And Me.txtPrecio.Text <> "" And Me.txtIDAgrupacion.Text <> "" And Me.txtID.Text <> "" Then
            MsgBox("El nombre es requerido", vbCritical)
            flag = True
            Me.txtNombre.Focus()
            Exit Sub
        ElseIf Me.txtNombre.Text <> "" And Me.txtPrecio.Text = "" And Me.txtIDAgrupacion.Text <> "" And Me.txtID.Text <> "" Then
            MsgBox("El Precio es requerido", vbCritical)
            flag = True
            Me.txtPrecio.Focus()
            Exit Sub
        ElseIf Me.txtID.Text <> "" And Me.txtNombre.Text = "" And Me.txtPrecio.Text = "" And Me.txtIDAgrupacion.Text <> "" Then
            MsgBox("El Nombre, Precio , es requerido", vbCritical)
            flag = True
            Me.txtNombre.Focus()
            Exit Sub
        ElseIf Me.txtID.Text <> "" And Me.txtPrecio.Text <> "" And Me.txtNombre.Text <> "" And Me.txtIDAgrupacion.Text <> "" Then
            sql = "UPDATE articulo SET [nom articulo]='" & Me.txtNombre.Text & "',[id agrupacion]='" & Val(Me.txtIDAgrupacion.Text) & "',[pco articulo]=" & Me.txtPrecio.Text.Replace(",", ".") & " WHERE [id articulo]=" & Val(Me.txtID.Text) & ""
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
        Me.lstArticulos.Items.Clear()
        Select Case Orden
            Case 0
                sql = "select * from articulo ORDER BY [id articulo]"

            Case 1
                sql = "select * from articulo ORDER BY [nom articulo]"
        End Select
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.lstArticulos.Items.Add(Format(Rs(0), "00000") & " " & Rs(1) & " " & Rs(2) & " " & Rs(3))
        End While
        Rs.Close()
    End Sub

    Sub CargarComboBox()
        Dim Rs As SqlDataReader
        Me.ComboBox1.Items.Clear()
        sql = "select * from agrupacion"
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.ComboBox1.Items.Add(Rs(1))
        End While
        Rs.Close()
    End Sub

    Private Sub lstArticulos_DoubleClick(sender As Object, e As EventArgs) Handles lstArticulos.DoubleClick
        Dim Rs As SqlDataReader
        Dim rs1 As SqlDataReader
        Dim numeroString As String
        If Me.lstArticulos.SelectedItem <> "" Then
            sql = "select * from articulo Where [id articulo]=" & Val(Mid(Me.lstArticulos.SelectedItem, 1, 5)) & ""
            Instruccion = New SqlCommand(sql, DaoCon)
            Rs = Instruccion.ExecuteReader()
            While Rs.Read
                Me.txtID.Text = Rs(0)
                Me.txtNombre.Text = Rs(1)
                Me.txtPrecio.Text = Rs(3)
                Me.txtIDAgrupacion.Text = Rs(2)
            End While
            Rs.Close()
            flag = False
            sql = "select [nom agrupacion] from agrupacion where [id agrupacion]=" & Val(Me.txtIDAgrupacion.Text) & ""
            Instruccion = New SqlCommand(sql, DaoCon)
            rs1 = Instruccion.ExecuteReader()
            While rs1.Read
                Me.ComboBox1.Text = rs1(0)
            End While
            rs1.Close()
            flag = True
            Me.btnBorrar.Enabled = True ' Boton Eliminar
            Me.btnModificar.Enabled = True ' Boton Modificar
            Me.btnAgregar.Enabled = False ' Boton Agregar
        Else
            MsgBox("No se seleccionó ningun item", vbCritical, "Verifique")
            Me.lstArticulos.Focus()
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Me.BotonAgregar()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Me.botonModificar()
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        Me.BotonEliminar()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Me.BotonLimpiar()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.BotonMostrar(0)
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Me.BotonMostrar(1)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim Rs2 As SqlDataReader
        sql = "select [id agrupacion] from agrupacion Where [nom agrupacion]='" & Me.ComboBox1.SelectedItem.ToString & "'"
        Instruccion = New SqlCommand(sql, DaoCon)
        If (flag) Then
            Rs2 = Instruccion.ExecuteReader()
            While Rs2.Read
                Me.txtIDAgrupacion.Text = Rs2(0)
            End While
            Rs2.Close()
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtIDAgrupacion_Enter(sender As Object, e As EventArgs) Handles txtIDAgrupacion.Enter
        Me.txtIDAgrupacion.ReadOnly = True
        Me.txtIDAgrupacion.BackColor = Color.White
        Me.txtIDAgrupacion.TabStop = False
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtID_Enter(sender As Object, e As EventArgs) Handles txtID.Enter
        Me.txtID.ReadOnly = True
        Me.txtID.BackColor = Color.White
        Me.txtID.TabStop = False
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtNombre_TextChanged(sender As Object, e As EventArgs) Handles txtNombre.TextChanged
        Dim texto As String
        texto = txtNombre.Text
        soloLetras1(texto)
    End Sub
End Class