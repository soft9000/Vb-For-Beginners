Imports System
Module Module1
    Sub Main()
        Dim i As Integer ' Automatically initialized to zero
        Dim bDone as boolean ' and this automatically to false
        Do
            Console.Write(i)
            Console.WriteLine(".)")
            i = i + 1
            If (i = 10) Then
                bDone = true
            End If
        Loop Until bDone
    End Sub
End Module ' DoUntil.vb