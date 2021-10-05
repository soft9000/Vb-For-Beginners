Imports System
Module Module1
    Sub Main()
        Dim i As Integer ' Automatically initialized to zero
        Do
            Console.Write(i)
            Console.WriteLine(".)")
            i = i + 1
            If i = 100 Then
                Exit Do
            End If
        Loop
    End Sub
End Module ' DoLoop.vb