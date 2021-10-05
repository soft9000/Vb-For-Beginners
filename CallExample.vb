Imports System

Module Module1
    Public Sub Test1()
        Console.WriteLine("One")
    End Sub
    Function Test2() As Boolean
        Console.WriteLine("Two")
        Test2 = True
    End Function
    Sub Main()
        Call Test1() ' Example of using Call
        Call Test2() ' Return result is ignored
        Test1        ' Parentheses remain optional
        Test2        ' 
    End Sub
End Module ' CallExample.vb
