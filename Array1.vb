Imports System
Module Module1

    Sub Main()
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        Dim i As Integer
        For i = 0 To cmdArgs.GetUpperBound(0)
            Console.WriteLine(cmdArgs(i))
        Next
    End Sub
End Module ' Array1.vb