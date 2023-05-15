Imports System.Data.SqlClient

Public Class frmAgrupacion
    Private Sub frmAgrupacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarBase()
        Me.BotonLimpiar()
        Me.txtID.TextAlign = HorizontalAlignment.Right
    End Sub

    Private Sub frmAgrupacion_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DaoCon.Close()
        MsgBox("Conexión Cerrada", vbInformation)
    End Sub

    Sub BotonLimpiar()

        Me.btnBorrar.Enabled = False ' Boton Eliminar
        Me.btnModificar.Enabled = False ' Boton Modificar
        Me.btnAgregar.Enabled = True ' Boton Agregar
        Me.txtNombreAgrupacion.Text = ""
        Me.txtID.Text = ""
        Me.txtNombreAgrupacion.Focus()
        BotonMostrar(0)
    End Sub

    Sub BotonAgregar()
        On Error GoTo Errores
        If Me.txtNombreAgrupacion.Text = "" Then
            MsgBox("El nombre es requerido", vbCritical)
            Me.txtNombreAgrupacion.Focus()
            Exit Sub
        End If

        sql = "INSERT INTO agrupacion ([nom agrupacion]) VALUES ('" & Me.txtNombreAgrupacion.Text & "')"
        Instruccion = New SqlCommand(sql, DaoCon)
        Instruccion.ExecuteNonQuery()
        Me.BotonLimpiar()
        Exit Sub
Errores:
        Select Case Err.Number
            Case Else
                MsgBox(Err.Description & " (" & Format(Err.Number, "00000)"))
                Exit Sub
        End Select
    End Sub

    Sub BotonEliminar()
        Dim OpC As Integer
        On Error GoTo Errores
        OpC = MsgBox("¿Desea eliminar este Registo?", vbYesNo, "Verifique")
        If OpC = vbYes Then
            sql = "DELETE FROM agrupacion WHERE [id agrupacion]=" & Val(Me.txtID.Text) & ""
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
        If Me.txtNombreAgrupacion.Text = "" Then
            MsgBox("El nombre es requerido", vbCritical)
            Me.txtNombreAgrupacion.Focus()
            Exit Sub
        End If
        sql = "UPDATE agrupacion SET [nom agrupacion]='" & Me.txtNombreAgrupacion.Text & "'WHERE [id agrupacion]=" & Val(Me.txtID.Text) & ""
        Instruccion = New SqlCommand(sql, DaoCon)
        Instruccion.ExecuteNonQuery()
        Me.BotonLimpiar()
        Exit Sub
Errores:
        Select Case Err.Number
            Case Else
                MsgBox(Err.Description & " (" & Format(Err.Number, "00000)"))
                Exit Sub
        End Select
    End Sub

    Sub BotonMostrar(Orden As Integer)
        Dim Rs As SqlDataReader
        Me.lstAgrupacion.Items.Clear()
        Select Case Orden
            Case 0
                sql = "select * from agrupacion ORDER BY [id agrupacion]"

            Case 1
                sql = "select * from agrupacion ORDER BY [nom agrupacion]"
        End Select
        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            Me.lstAgrupacion.Items.Add(Format(Rs(0), "00000") & " " & Rs(1))
        End While
        Rs.Close()
    End Sub

    Private Sub lstAgrupacion_DoubleClick(sender As Object, e As EventArgs) Handles lstAgrupacion.DoubleClick
        Dim Rs As SqlDataReader
        If Me.lstAgrupacion.SelectedItem <> "" Then
            sql = "select * from agrupacion Where [id agrupacion]=" & Val(Mid(Me.lstAgrupacion.SelectedItem, 1, 5)) & ""
            Instruccion = New SqlCommand(sql, DaoCon)
            Rs = Instruccion.ExecuteReader()
            While Rs.Read
                Me.txtID.Text = Rs(0)
                Me.txtNombreAgrupacion.Text = Rs(1)
            End While
            Rs.Close()
            Me.btnBorrar.Enabled = True ' Boton Eliminar
            Me.btnModificar.Enabled = True ' Boton Modificar
            Me.btnAgregar.Enabled = False ' Boton Agregar
        Else
            MsgBox("No se seleccionó ningun item", vbCritical, "Verifique")
            Me.lstAgrupacion.Focus()
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Me.BotonAgregar()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.BotonMostrar(0)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.BotonMostrar(1)
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

    Private Sub txtNombreAgrupacion_TextChanged(sender As Object, e As EventArgs) Handles txtNombreAgrupacion.TextChanged
        Dim texto As String = txtNombreAgrupacion.Text
        soloLetras(texto)
    End Sub

    Private Sub txtNombreAgrupacion_Enter(sender As Object, e As EventArgs) Handles txtNombreAgrupacion.Enter
        txtNombreAgrupacion.TextAlign = HorizontalAlignment.Left
    End Sub

    Private Sub txtID_Enter(sender As Object, e As EventArgs) Handles txtID.Enter
        Me.txtID.ReadOnly = True
        Me.txtID.BackColor = Color.White
        Me.txtID.TabStop = False
        SendKeys.Send("{TAB}")
    End Sub
End Class