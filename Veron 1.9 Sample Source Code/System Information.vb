' Developer: S.M.A.Faisal

Imports System.Management
Public Class System_Information
    Dim fx As String
    Private Sub cmbxOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbxOption.SelectedIndexChanged
        fx = "Win32_" & cmbxOption.SelectedItem
        InsertInfo(fx, listview1)
    End Sub
    Private Sub InsertInfo(ByVal Key As String, ByRef lst As ListView)
        lst.Items.Clear()
        Dim searcher As New ManagementObjectSearcher("select * from " & Key)
        Try
            For Each share As ManagementObject In searcher.[Get]()
                Dim grp As ListViewGroup
                Try
                    grp = lst.Groups.Add(share("Name").ToString(), share("Name").ToString())
                Catch
                    grp = lst.Groups.Add(share.ToString(), share.ToString())
                End Try
                If share.Properties.Count <= 0 Then
                    Return
                End If
                For Each PC As PropertyData In share.Properties
                    Dim item As New ListViewItem(grp)
                    If lst.Items.Count Mod 2 <> 0 Then
                        item.BackColor = Color.White
                    Else
                        item.BackColor = Color.WhiteSmoke
                    End If
                    item.Text = PC.Name
                    If PC.Value IsNot Nothing AndAlso PC.Value.ToString() <> "" Then
                        Select Case PC.Value.[GetType]().ToString()
                            Case "System.String[]"
                                Dim str As String() = DirectCast(PC.Value, String())
                                Dim str2 As String = ""
                                For Each st As String In str
                                    str2 += st & " "
                                Next
                                item.SubItems.Add(str2)
                                Exit Select
                            Case "System.UInt16[]"
                                Dim shortData As UShort() = DirectCast(PC.Value, UShort())
                                Dim tstr2 As String = ""
                                For Each st As UShort In shortData
                                    tstr2 += st.ToString() & " "
                                Next
                                item.SubItems.Add(tstr2)
                                Exit Select
                            Case Else
                                item.SubItems.Add(PC.Value.ToString())
                                Exit Select
                        End Select
                    End If
                    lst.Items.Add(item)
                Next
            Next
        Catch
            MsgBox("No data found")
        End Try
    End Sub
End Class