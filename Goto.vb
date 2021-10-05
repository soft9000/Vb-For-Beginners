' Evil, or not? Either way, here is how it works.
'
Imports System
Module Module1
    Sub Main()
        Dim iNumber = 1
        Dim sInput As String
        ' Here is an example of a single line If statement
        If iNumber = 1 Then GoTo Line1 Else GoTo Line2
Line1:
        sInput = "iNumber is 1"
        GoTo FinalLine   ' Go to last line label.
Line2:
        ' The following statement never gets executed.
        sInput = "iNumber set to 2"
FinalLine:
        Console.WriteLine(sInput)
    End Sub
End Module ' GoTo.vb
