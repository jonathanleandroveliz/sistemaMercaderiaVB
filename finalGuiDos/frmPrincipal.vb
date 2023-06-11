

Imports System.Data.SqlClient
Public Class frmPrincipal

    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarBase()
        cargarDatos()
        Me.txtResultado.TextAlign = HorizontalAlignment.Right
        Me.BackColor = Color.Beige
        Me.lstDatos.BackColor = Color.AntiqueWhite
        Me.txtResultado.BackColor = Color.AntiqueWhite
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub

    Private Sub MovimientosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MovimientosToolStripMenuItem.Click
        frmMovimientos.Show()
    End Sub

    Private Sub ArticulosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArticulosToolStripMenuItem.Click
        frmArticulos.Show()
    End Sub

    Private Sub AgrupacionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgrupacionToolStripMenuItem.Click
        frmAgrupacion.Show()
    End Sub

    Private Sub TipoMovimientosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TipoMovimientosToolStripMenuItem.Click
        frmTiposMovimientos.Show()
    End Sub

    Sub cargarDatos()
        Dim Rs As SqlDataReader
        Dim rs2 As SqlDataReader
        Dim total As Double
        Me.lstDatos.Items.Clear()
        'sql = "select agrupacion.[nom agrupacion] , case when tipomovi.[tip tipomovi] = 'O' then SUM(movimiento.[can movimiento]) - SUM(movimiento.[can movimiento]) else SUM(movimiento.[can movimiento]) END , 'H' from  articulo left join agrupacion on articulo.[id agrupacion] = agrupacion.[id agrupacion] right join movimiento on articulo.[id articulo] = movimiento.[id articulo] right join tipomovi on movimiento.[id tipomovi] = tipomovi.[id tipomovi]  WHERE tipomovi.[nom tipomovi] LIKE 'Veliz%' and tipomovi.[tip tipomovi] = 'I'  GROUP BY agrupacion.[nom agrupacion]"
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
            Me.lstDatos.Items.Add(Space(3) & Rs(0).ToString.PadRight(50, " ") & vbTab & Rs(1).ToString.PadRight(5, " ") & vbTab & vbTab & Format(Rs(2), "##,##0.00").ToString.PadLeft(12, " ") & " ")
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
        Me.txtResultado.Text = Format(total, "##,##0.00")
    End Sub

End Class
