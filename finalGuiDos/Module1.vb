Imports System.Data.SqlClient

Module Module1
    Public DaoCon As SqlConnection
    Public DaoCon1 As SqlConnection
    Public DaoCon2 As SqlConnection
    Public DaoCon3 As SqlConnection
    Public sql As String
    Public Instruccion As SqlCommand
    Sub ConectarBase()
        On Error GoTo Errores
        Dim Servidor As String = "estack.ddns.net"
        Dim Base As String = "UCES02"
        DaoCon = New SqlConnection("server=" & Servidor & ";database=" & Base & ";User ID=sa;Password=Ita1821!")
        DaoCon.Open()
        DaoCon1 = New SqlConnection("server=" & Servidor & ";database=" & Base & ";User ID=sa;Password=Ita1821!")
        DaoCon1.Open()
        DaoCon2 = New SqlConnection("server=" & Servidor & ";database=" & Base & ";User ID=sa;Password=Ita1821!")
        DaoCon2.Open()
        DaoCon3 = New SqlConnection("server=" & Servidor & ";database=" & Base & ";User ID=sa;Password=Ita1821!")
        DaoCon3.Open()
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

    Public Function soloNumeros(numero As String)
        Dim i As Integer
        For i = 0 To Len(numero) - 1
            If (IsNumeric(numero(i))) Then
                frmMovimientos.txtCantidad.Text = Mid(numero, 1, 3)
                frmMovimientos.txtCantidad.SelectionStart = 3
            Else
                frmMovimientos.txtCantidad.Text = ""
                frmMovimientos.txtCantidad.Focus()
            End If
        Next
    End Function

    Sub cargarDatosPrincipal()
        Dim Rs As SqlDataReader
        Dim total As Double
        frmPrincipal.lstDatos.Items.Clear()
        'sql = "select DISTINCT articulo.[id articulo] , COUNT([id tipomovi]), SUM([pre movimiento]) from  movimiento left join articulo on movimiento.[id articulo] = articulo.[id articulo] WHERE [nom articulo] LIKE 'Veliz%' GROUP BY articulo.[id articulo]"
        'sql = "select agrupacion.[nom agrupacion] , SUM(movimiento.[can movimiento])  , SUM(movimiento.[pre movimiento]) from  articulo left join agrupacion on articulo.[id agrupacion] = agrupacion.[id agrupacion] right join movimiento on articulo.[id articulo] = movimiento.[id articulo]  WHERE agrupacion.[nom agrupacion] LIKE 'Veliz%' GROUP BY agrupacion.[nom agrupacion] "

        sql = "IF OBJECT_ID('tempdb..#output') is not null drop table #output
               IF OBJECT_ID('tempdb..#input') is not null drop table #input

               create table #output(nomagru nvarchar(50) not null, cant int not null , resul float not null)
               insert into #output(nomagru,cant,resul)
select agrupacion.[nom agrupacion] , SUM(movimiento.[can movimiento]) , SUM(movimiento.[pre movimiento]) from  
                   movimiento right join tipomovi on movimiento.[id tipomovi] = tipomovi.[id tipomovi] 
                      left join articulo on movimiento.[id articulo] = articulo.[id articulo]
                            left join agrupacion on articulo.[id agrupacion] = agrupacion.[id agrupacion]  
                                WHERE tipomovi.[tip tipomovi] = 'O' and agrupacion.[nom agrupacion] LIKE 'Veliz%' 
                                   GROUP BY agrupacion.[nom agrupacion] 

               create table #input(nomagru nvarchar(50) not null, cant int not null , resul float not null)
               insert into #input(nomagru,cant,resul)
select agrupacion.[nom agrupacion] , SUM(movimiento.[can movimiento]) , SUM(movimiento.[pre movimiento]) from  
                   movimiento right join tipomovi on movimiento.[id tipomovi] = tipomovi.[id tipomovi] 
                      left join articulo on movimiento.[id articulo] = articulo.[id articulo]
                            left join agrupacion on articulo.[id agrupacion] = agrupacion.[id agrupacion]  
                                WHERE tipomovi.[tip tipomovi] = 'I' and agrupacion.[nom agrupacion] LIKE 'Veliz%' 
                                   GROUP BY agrupacion.[nom agrupacion]

                select ISNULL(o.nomagru,i.nomagru) as nombre , ISNULL(i.cant,0)-ISNULL(o.cant,0) as cant , ISNULL(i.resul,0)-ISNULL(o.resul,0)  as resul from #output o full join #input i on o.nomagru = i.nomagru  
                        
                drop table #output
                drop table #input
                    "


        Instruccion = New SqlCommand(sql, DaoCon)
        Rs = Instruccion.ExecuteReader()
        While Rs.Read
            frmPrincipal.lstDatos.Items.Add(Space(3) & Rs(0).ToString.PadRight(50, " ") & vbTab & Rs(1).ToString.PadRight(5, " ") & vbTab & vbTab & Format(Rs(2), "##,##0.00").ToString.PadLeft(12, " ") & " ")
        End While
        Rs.Close()

        sql = "IF OBJECT_ID('tempdb..#output') is not null drop table #output
               IF OBJECT_ID('tempdb..#input') is not null drop table #input

               create table #output(nomagru nvarchar(50) not null, cant int not null , resul float not null)
               insert into #output(nomagru,cant,resul)
select agrupacion.[nom agrupacion] , SUM(movimiento.[can movimiento]) , SUM(movimiento.[pre movimiento]) from  
                   movimiento right join tipomovi on movimiento.[id tipomovi] = tipomovi.[id tipomovi] 
                      left join articulo on movimiento.[id articulo] = articulo.[id articulo]
                            left join agrupacion on articulo.[id agrupacion] = agrupacion.[id agrupacion]  
                                WHERE tipomovi.[tip tipomovi] = 'O' and agrupacion.[nom agrupacion] LIKE 'Veliz%' 
                                   GROUP BY agrupacion.[nom agrupacion] 

               create table #input(nomagru nvarchar(50) not null, cant int not null , resul float not null)
               insert into #input(nomagru,cant,resul)
select agrupacion.[nom agrupacion] , SUM(movimiento.[can movimiento]) , SUM(movimiento.[pre movimiento]) from  
                   movimiento right join tipomovi on movimiento.[id tipomovi] = tipomovi.[id tipomovi] 
                      left join articulo on movimiento.[id articulo] = articulo.[id articulo]
                            left join agrupacion on articulo.[id agrupacion] = agrupacion.[id agrupacion]  
                                WHERE tipomovi.[tip tipomovi] = 'I' and agrupacion.[nom agrupacion] LIKE 'Veliz%' 
                                   GROUP BY agrupacion.[nom agrupacion]

                select  sum(ISNULL(i.resul,0)-ISNULL(o.resul,0))  as resul from #output o full join #input i on o.nomagru = i.nomagru  
                        
                drop table #output
                drop table #input
                    "
        Instruccion = New SqlCommand(sql, DaoCon)
        total = Instruccion.ExecuteScalar()
        frmPrincipal.txtResultado.Text = Format(total, "##,##0.00")

    End Sub


End Module
