' How to split up a long string 
'
Imports System
Module Module1
    Sub Main()
        Dim i As Integer
        For i = 1 To 12 Step 2
            Console.WriteLine("We can" + _
            " split up" + _
            " long strings like" + _
            " this!" _ 
            )
        Next
    End Sub
End Module ' StringSplit.vb
