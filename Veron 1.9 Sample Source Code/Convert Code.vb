' Developer: S.M.A.Faisal

Imports System.IO
Public Class Convert_Code
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RichTextBox1.Text = Converter(RichTextBox1.Text)
    End Sub
    Public Function Converter(ByVal vbSrc As String) As String
        Dim RetS As String = ""
        Try
            Dim TmpReader As TextReader
            Dim TmpVBPars As ICSharpCode.NRefactory.IParser
            Dim TmpCompUnit As ICSharpCode.NRefactory.Ast.CompilationUnit
            Dim TmpCSVisitor As Object
            If RadioButton1.Checked = True Then
                TmpCSVisitor = New ICSharpCode.NRefactory.PrettyPrinter.VBNetOutputVisitor
            Else
                TmpCSVisitor = New ICSharpCode.NRefactory.PrettyPrinter.CSharpOutputVisitor
            End If
            TmpReader = New StringReader(vbSrc)
            If RadioButton1.Checked = True Then
                TmpVBPars = ICSharpCode.NRefactory.ParserFactory.CreateParser(ICSharpCode.NRefactory.SupportedLanguage.CSharp, TmpReader)
            Else
                TmpVBPars = ICSharpCode.NRefactory.ParserFactory.CreateParser(ICSharpCode.NRefactory.SupportedLanguage.VBNet, TmpReader)
            End If
            TmpVBPars.Parse()
            TmpReader.Close()
            If TmpVBPars.Errors.Count > 0 Then
                Return Nothing
            Else
                TmpCompUnit = TmpVBPars.CompilationUnit
                TmpCSVisitor.VisitCompilationUnit(TmpCompUnit, Nothing)
                RetS = TmpCSVisitor.Text
            End If
        Catch
        End Try
        Return RetS
    End Function
End Class