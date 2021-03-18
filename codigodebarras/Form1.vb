Imports iTextSharp.text.pdf
Imports System.Data.OleDb
Public Class Form1
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        Dim bcode As New Barcode128
        bcode.BarHeight = 50
        bcode.Code = TxtCodigo.Text
        bcode.GenerateChecksum = True
        bcode.CodeType = Barcode128.CODE128
        Try
            Dim bm As New Bitmap(bcode.CreateDrawingImage(Color.Black, Color.White))
            Dim img As Image
            img = New Bitmap(bm.Width, bm.Height)
            Dim g As Graphics = Graphics.FromImage(img)
            g.FillRectangle(New SolidBrush(Color.White), 0, 0, bm.Width, bm.Height)
            g.DrawImage(bm, 0, 0)
            PictureBox1.Image = img

        Catch ex As Exception
            MsgBox("no se pudo generar el codigo de barras")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.ProductoTableAdapter.InsertQuery(TxtCodigo.Text, TxtNombre.Text, TxtMarca.Text, TxtPrecio.Text)
        PrintPreviewDialog1.Show()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        PictureBox1.DrawToBitmap(bm, New Rectangle(0, 0, bm.Width, bm.Height))
        e.Graphics.DrawImage(bm, 0, 0)
    End Sub

    Private Sub ProductoBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.ProductoBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.CDBDataSet)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'CDBDataSet.Producto' Puede moverla o quitarla según sea necesario.
        Me.ProductoTableAdapter.Fill(Me.CDBDataSet.Producto)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBox1.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            Me.ProductoTableAdapter.FillBy(Me.CDBDataSet.Producto, TextBox1.Text)
        End If
    End Sub

    Private Sub MarcaLabel_Click(sender As Object, e As EventArgs)

    End Sub
End Class
