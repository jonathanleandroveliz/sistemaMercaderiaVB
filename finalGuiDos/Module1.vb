Imports System.Data.SqlClient

Module Module1
    Public DaoCon As SqlConnection
    Public sql As String
    Public Instruccion As SqlCommand
    Sub ConectarBase()
        On Error GoTo Errores
        Dim Servidor As String = "estack.ddns.net"
        Dim Base As String = "UCES02"
        DaoCon = New SqlConnection("server=" & Servidor & ";database=" & Base & ";User ID=sa;Password=Ita1821!")
        DaoCon.Open()
        frmAgrupacion.ToolStripStatusLabel1.Text = "Establecida"
        frmAgrupacion.ToolStripStatusLabel2.Text = Base
        frmArticulos.ToolStripStatusLabel1.Text = "Establecida"
        frmArticulos.ToolStripStatusLabel2.Text = Base
        frmTiposMovimientos.ToolStripStatusLabel1.Text = "Establecida"
        frmTiposMovimientos.ToolStripStatusLabel2.Text = Base
        frmMovimientos.ToolStripStatusLabel1.Text = "Establecida"
        frmMovimientos.ToolStripStatusLabel2.Text = Base
        Exit Sub
Errores:
        Select Case Err.Number
            Case 5
                MsgBox(Err.Description & " (" & Format(Err.Number, "00000)"), vbInformation)
                MsgBox("El programa se cerrara", vbCritical)
                End
            Case Else
                MsgBox(Err.Description & " (" & Format(Err.Number), "00000)")
                Exit Sub
        End Select
    End Sub

    Public Function soloLetras(nombre As String)
        Dim i As Integer
        For i = 0 To Len(nombre) - 1

            If (Char.IsLetter(nombre(i)) Or Char.IsSeparator(nombre(i))) Then
                frmAgrupacion.txtNombreAgrupacion.Text = Mid(nombre, 1, 50)
                frmAgrupacion.txtNombreAgrupacion.SelectionStart = 50

            Else
                frmAgrupacion.txtNombreAgrupacion.Text = ""
                frmAgrupacion.txtNombreAgrupacion.Focus()

            End If
        Next
    End Function

    Public Function soloLetras1(nombre As String)
        Dim i As Integer
        For i = 0 To Len(nombre) - 1

            If (Char.IsLetter(nombre(i)) Or Char.IsSeparator(nombre(i))) Then

                frmArticulos.txtNombre.Text = Mid(nombre, 1, 50)
                frmArticulos.txtNombre.SelectionStart = 50
            Else

                frmArticulos.txtNombre.Text = ""
                frmArticulos.txtNombre.Focus()
            End If
        Next
    End Function

    Public Function soloLetras2(nombre As String)
        Dim i As Integer
        For i = 0 To Len(nombre) - 1

            If (Char.IsLetter(nombre(i)) Or Char.IsSeparator(nombre(i))) Then

                frmTiposMovimientos.txtNomMovimiento.Text = Mid(nombre, 1, 50)
                frmTiposMovimientos.txtNomMovimiento.SelectionStart = 50
            Else

                frmTiposMovimientos.txtNomMovimiento.Text = ""
                frmTiposMovimientos.txtNomMovimiento.Focus()
            End If
        Next
    End Function

    Public Function soloLetras3(nombre As String)
        Dim i As Integer
        For i = 0 To Len(nombre) - 1

            If (Char.IsLetter(nombre(i)) Or Char.IsSeparator(nombre(i))) Then

                frmTiposMovimientos.txtTipoMovimiento.Text = Mid(nombre, 1, 1)
                frmTiposMovimientos.txtTipoMovimiento.SelectionStart = 1
            Else

                frmTiposMovimientos.txtTipoMovimiento.Text = ""
                frmTiposMovimientos.txtTipoMovimiento.Focus()
            End If
        Next
    End Function


End Module
