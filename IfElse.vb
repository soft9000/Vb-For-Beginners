Imports System
Imports Microsoft.VisualBasic

Module Module1

    Sub Main()
        Dim str As String
        str = Console.ReadLine()
        If (str = "1") Then
            Console.WriteLine("We're number 1!")
        Else
            MsgBox("We are not number one?")
        End If
    End Sub

End Module ' IfElse.vb
