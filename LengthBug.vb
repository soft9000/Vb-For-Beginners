' WARNING: This code has a bug in it!
Imports System
Module Module1

    Sub Main()
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        Dim i As Integer
        For i = 0 To cmdArgs.Length
            Console.WriteLine(cmdArgs(i))
        Next
    End Sub
End Module ' LengthBug.vb