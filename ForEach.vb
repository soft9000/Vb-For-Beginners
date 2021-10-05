' ForEach.vb - Demonstrate a better array to display array content
'
Imports System
Module Module1

    Sub Main()
        Dim Ary As String() = Environment.GetCommandLineArgs()
        Dim str As String
        For Each str In Ary
            Console.WriteLine("We see: " + str)
        Next
    End Sub

End Module
